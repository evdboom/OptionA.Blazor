# Verify devteam invocation formatting in run logs
# Exits 0 if OK, 1 if bad invocations found
param()

$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Definition
$stateRuns = Join-Path $scriptRoot "..\state\runs.json"
$exportedRuns = Join-Path $scriptRoot "..\exported\state\runs.json"
$paths = @($stateRuns)
if (Test-Path $exportedRuns) { $paths += $exportedRuns }

$bad = @()
foreach ($p in $paths) {
    if (-not (Test-Path $p)) { continue }
    $text = Get-Content -LiteralPath $p -Raw -ErrorAction SilentlyContinue
    if (-not $text) { continue }
    # Detect missing space between command and issue id: e.g. "devteam edit-issue16"
    $matches = [regex]::Matches($text, 'devteam\s+edit-issue\d')
    foreach ($m in $matches) { $bad += @{Path=$p; Match=$m.Value; Index=$m.Index} }
}

if ($bad.Count -gt 0) {
    Write-Error "Found malformed devteam invocations (missing space between 'edit-issue' and issue id):"
    foreach ($b in $bad) {
        Write-Host "  File: $($b.Path) -> $($b.Match)" -ForegroundColor Red
    }
    exit 1
} else {
    Write-Host "No malformed devteam invocations detected." -ForegroundColor Green
    exit 0
}