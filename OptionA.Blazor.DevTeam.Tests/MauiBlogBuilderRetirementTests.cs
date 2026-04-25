using System;
using System.IO;
using Xunit;

public class MauiBlogBuilderRetirementTests
{
    private static string FindRepoRoot()
    {
        var dir = new DirectoryInfo(Environment.CurrentDirectory);
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir.FullName, "OptionA.Blazor.sln")))
            {
                return dir.FullName;
            }

            dir = dir.Parent;
        }

        throw new InvalidOperationException("Repository root (OptionA.Blazor.sln) not found from " + Environment.CurrentDirectory);
    }

    [Fact]
    public void MauiTestApp_Remains_Detached_From_BlogBuilder()
    {
        var repoRoot = FindRepoRoot();
        var mauiRoot = Path.Combine(repoRoot, "OptionA.Blazor.Maui.Test");

        var projectFile = File.ReadAllText(Path.Combine(mauiRoot, "OptionA.Blazor.Maui.Test.csproj"));
        Assert.DoesNotContain("OptionA.Blazor.Blog.Builder", projectFile, StringComparison.Ordinal);

        var importsFile = File.ReadAllText(Path.Combine(mauiRoot, "Components", "_Imports.razor"));
        Assert.DoesNotContain("OptionA.Blazor.Blog.Builder", importsFile, StringComparison.Ordinal);

        var mainLayoutFile = File.ReadAllText(Path.Combine(mauiRoot, "Components", "Layout", "MainLayout.razor"));
        Assert.DoesNotContain("BlogBuilder", mainLayoutFile, StringComparison.Ordinal);
        Assert.DoesNotContain("/components/blogbuilder", mainLayoutFile, StringComparison.OrdinalIgnoreCase);

        Assert.False(File.Exists(Path.Combine(mauiRoot, "Components", "Pages", "BlogBuilder.razor")));
        Assert.False(File.Exists(Path.Combine(mauiRoot, "Components", "Pages", "BlogBuilder.razor.cs")));
    }
}
