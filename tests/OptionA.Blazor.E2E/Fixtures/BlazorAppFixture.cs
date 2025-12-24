using System.Diagnostics;

namespace OptionA.Blazor.E2E.Fixtures;

/// <summary>
/// Base class for Blazor app fixtures that manages the hosting and cleanup of test applications.
/// </summary>
public abstract class BlazorAppFixture : IAsyncLifetime
{
    private Process? _hostProcess;
    protected string? BaseUrl { get; private set; }
    
    protected abstract string ProjectPath { get; }
    protected abstract string[] LaunchArguments { get; }

    public async Task InitializeAsync()
    {
        // Build and publish the app
        var buildProcess = Process.Start(new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"publish {ProjectPath} -c Release -o {GetPublishPath()}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        });

        if (buildProcess == null)
        {
            throw new InvalidOperationException("Failed to start build process");
        }

        await buildProcess.WaitForExitAsync();
        
        if (buildProcess.ExitCode != 0)
        {
            var error = await buildProcess.StandardError.ReadToEndAsync();
            throw new InvalidOperationException($"Build failed: {error}");
        }

        // Start the app
        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = string.Join(" ", LaunchArguments),
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WorkingDirectory = GetPublishPath()
        };

        _hostProcess = Process.Start(startInfo);
        
        if (_hostProcess == null)
        {
            throw new InvalidOperationException("Failed to start host process");
        }

        // Wait for the app to be ready
        BaseUrl = await WaitForAppToStartAsync(_hostProcess);
    }

    private async Task<string> WaitForAppToStartAsync(Process process)
    {
        var timeout = TimeSpan.FromSeconds(30);
        var startTime = DateTime.UtcNow;
        string? baseUrl = null;

        while (DateTime.UtcNow - startTime < timeout)
        {
            if (process.HasExited)
            {
                var error = await process.StandardError.ReadToEndAsync();
                throw new InvalidOperationException($"Process exited prematurely: {error}");
            }

            // Read output to find the URL
            var line = await process.StandardOutput.ReadLineAsync();
            if (line != null && line.Contains("Now listening on:"))
            {
                // Extract URL from line like "Now listening on: http://localhost:5000"
                var urlStart = line.IndexOf("http");
                if (urlStart >= 0)
                {
                    baseUrl = line.Substring(urlStart).Trim();
                    break;
                }
            }

            await Task.Delay(100);
        }

        if (baseUrl == null)
        {
            throw new TimeoutException("Failed to detect application URL within timeout period");
        }

        // Give the app a moment to fully initialize
        await Task.Delay(2000);

        return baseUrl;
    }

    protected string GetPublishPath()
    {
        var projectName = Path.GetFileNameWithoutExtension(ProjectPath);
        return Path.Combine(Path.GetTempPath(), "OptionA.Blazor.E2E", projectName);
    }

    public async Task DisposeAsync()
    {
        if (_hostProcess != null && !_hostProcess.HasExited)
        {
            _hostProcess.Kill(true);
            await _hostProcess.WaitForExitAsync();
            _hostProcess.Dispose();
        }

        // Clean up publish directory
        var publishPath = GetPublishPath();
        if (Directory.Exists(publishPath))
        {
            try
            {
                Directory.Delete(publishPath, true);
            }
            catch
            {
                // Ignore cleanup errors
            }
        }
    }

    public string GetBaseUrl()
    {
        if (BaseUrl == null)
        {
            throw new InvalidOperationException("Fixture not initialized. Call InitializeAsync first.");
        }
        return BaseUrl;
    }
}
