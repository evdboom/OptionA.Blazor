$targets = @(10,11,13,14,16,19,21,24,26,28,31,32,35,38,39,40,41,42,43)
devteam export --output .devteam/export.json
Expand-Archive -LiteralPath .devteam/export.json -DestinationPath .devteam/exported -Force
$json = Get-Content .devteam/exported/state/issues.json -Raw | ConvertFrom-Json
$failed = @()
foreach ($id in $targets) {
    $issue = $json | Where-Object { $_.Id -eq $id }
    if ($null -eq $issue) {
        Write-Output "Missing issue $id in exported state"
        $failed += $id
    } elseif ($issue.Status -ne "Done") {
        Write-Output "Issue $id status: $($issue.Status)"
        $failed += $id
    } else {
        Write-Output "Issue $id OK (Done)"
    }
}
if ($failed.Count -gt 0) {
    Write-Output "Batch-close verification FAILED for IDs: $($failed -join ', ')"
    exit 1
} else {
    Write-Output "All target issues are Done"
    exit 0
}
