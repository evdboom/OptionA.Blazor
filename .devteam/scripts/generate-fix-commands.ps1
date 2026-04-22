<#
Generate corrected devteam edit-issue commands from run logs.
Usage:
  .\generate-fix-commands.ps1 [-OutputPath corrected-commands.ps1] [-Execute]

This scans .devteam/state/runs.json and .devteam/exported/state/runs.json for malformed invocations like
  devteam edit-issue16 --status done --note "..." --workspace .devteam
and emits corrected commands with a space between `edit-issue` and the id.

If -Execute is specified, the script will attempt to run the corrected commands (requires `devteam` CLI available and appropriate workspace access).
#>
param(
    [string]$OutputPath = ".devteam/scripts/corrected-edit-commands.ps1",
    [switch]$Execute
)

$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Definition
$stateRuns = Join-Path $scriptRoot "..\state\runs.json"
$exportedRuns = Join-Path $scriptRoot "..\exported\state\runs.json"
$paths = @($stateRuns)
if (Test-Path $exportedRuns) { $paths += $exportedRuns }

$commands = @()
foreach ($p in $paths) {
    if (-not (Test-Path $p)) { continue }
    $text = Get-Content -LiteralPath $p -Raw -ErrorAction SilentlyContinue
    if (-not $text) { continue }
    # Match patterns like: devteam edit-issue16 --status done --note "..." --workspace C:\repo\...
    $rx = [regex]'devteam\s+edit-issue(\d+)([^`\r\n]*)'
    $m = $rx.Matches($text)
    foreach ($match in $m) {
        $id = $match.Groups[1].Value
        $rest = $match.Groups[2].Value.Trim()

        # Ensure --workspace value is quoted if it contains spaces. Use string parsing so paths with spaces (unquoted) are handled.
        $wsToken = '--workspace'
        $idx = $rest.IndexOf($wsToken, [System.StringComparison]::InvariantCultureIgnoreCase)
        if ($idx -ge 0) {
            $after = $rest.Substring($idx + $wsToken.Length).TrimStart()
            if ($after.StartsWith('"')) {
                # already quoted: extract content between quotes
                $end = $after.IndexOf('"', 1)
                if ($end -gt 0) { $wsVal = $after.Substring(1, $end - 1) } else { $wsVal = $after.Trim('"') }
                $prefix = $rest.Substring(0, $idx)
                $suffix = $after.Substring($end + 1)
            } else {
                # take until next --option or end of string
                $m2 = [regex]::Match($after, '(.+?)(?=\s--\w|$)')
                $wsVal = $m2.Groups[1].Value.Trim()
                $prefix = $rest.Substring(0, $idx)
                $suffix = $after.Substring($m2.Length)
            }

            if ($wsVal -and ($wsVal -match '\s')) {
                $escaped = $wsVal -replace '"','\"'
                $rest = "$prefix--workspace `"$escaped`"$suffix"
            }
        }

        $correct = "devteam edit-issue $id $rest"
        $commands += $correct
    }
}

if (-not $commands) {
    Write-Host "No malformed edit-issue invocations found to fix." -ForegroundColor Green
    exit 0
}

# Write commands to output file
$outFull = Join-Path (Get-Location) $OutputPath
$header = "# Corrected devteam edit-issue commands generated on $(Get-Date -Format o)`r`n" + "# Review before running. Running these will modify workspace state.`r`n"
$body = $commands -join "`r`n"
$scriptContent = $header + "`r`n" + $body + "`r`n"
Set-Content -Path $outFull -Value $scriptContent -Force -Encoding UTF8
Write-Host "Wrote corrected commands to $outFull" -ForegroundColor Green

if ($Execute) {
    Write-Host "Executing corrected commands (this will call 'devteam' CLI)." -ForegroundColor Yellow
    foreach ($cmd in $commands) {
        Write-Host "Running: $cmd"
        iex $cmd
        if ($LASTEXITCODE -ne 0) {
            Write-Error "Command failed: $cmd (exit $LASTEXITCODE)"
        } else {
            Write-Host "Command succeeded: $cmd" -ForegroundColor Green
        }
    }
}

exit 0