Write-Host "modify app base"
$settingsFilePath = "Release/UIBlazor/wwwroot/appsettings.json"
$settingsFilePath = "BBH/wwwroot/appsettings.json"
$settingsContent = Get-Content -Path $settingsFilePath -Raw
$updatedSettingsContent = $settingsContent -replace '"AppBasePath": "/"', '"AppBasePath": "/BlazorBrowserHistory/"'
Set-Content -Path $settingsFilePath -Value $updatedSettingsContent
Write-Host "Change back AppBasePath in $settingsFilePath "