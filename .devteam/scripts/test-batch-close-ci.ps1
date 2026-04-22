Param(
    [Parameter(Mandatory=$false)]
    [int[]]$Targets = @(10,11,13,14,16,19,21,24,26,28,31,32,35,38,39,40,41,42,43),
    [Parameter(Mandatory=$false)]
    [string]$OutputJsonPath = ".devteam/test-batch-close-result.json"
)

# Verify devteam invocation formatting (fail fast if malformed)
$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Definition
$verifyScript = Join-Path $scriptRoot 'verify-devteam-invocations.ps1'
if (Test-Path $verifyScript) {
    & $verifyScript
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Invocation verification failed. Aborting batch-check."
        exit 2
    }
} else {
    Write-Host "Verification script not found; skipping invocation-format check." -ForegroundColor Yellow
}

# Export authoritative workspace state
devteam export --output .devteam/export.json
Expand-Archive -LiteralPath .devteam/export.json -DestinationPath .devteam/exported -Force

# Read exported issues
$json = Get-Content .devteam/exported/state/issues.json -Raw | ConvertFrom-Json

$failed = @()
$checked = @()

foreach ($id in $Targets) {
    $issue = $json | Where-Object { $_.Id -eq $id }
    if ($null -eq $issue) {
        $status = "Missing"
        $failed += $id
    } elseif ($issue.Status -ne "Done") {
        $status = $issue.Status
        $failed += $id
    } else {
        $status = "Done"
    }
    $checked += @{ Id = $id; Status = $status }
    Write-Output "Issue $id status: $status"
}

$result = [PSCustomObject]@{
    Success = ($failed.Count -eq 0)
    FailedIds = $failed
    Checked = $checked
    Message = if ($failed.Count -eq 0) { "All target issues are Done" } else { "Batch-close verification FAILED for IDs: $($failed -join ', ')" }
}

# Emit machine-readable JSON for pipelines
$resultJson = $result | ConvertTo-Json -Depth 5
Set-Content -Path $OutputJsonPath -Value $resultJson -Force
Write-Output "Wrote result JSON to $OutputJsonPath"

# Also write a short message to stdout and set exit code for CI
if (-not $result.Success) {
    Write-Output $result.Message
    exit 1
} else {
    Write-Output $result.Message
    exit 0
}
