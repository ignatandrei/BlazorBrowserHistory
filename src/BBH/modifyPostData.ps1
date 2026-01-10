Write-Host "modify index.html"
$indexFilePath = "Release/UIBlazor/wwwroot/index.html"
$Content = Get-Content -Path $indexFilePath -Raw
$updatedContent = $Content -replace '"./_framework/', '"/BlazorBrowserHistory/_framework/'
Set-Content -Path $indexFilePath -Value $updatedContent
#TODO: delete br and gz files
Remove-Item -Path "Release/UIBlazor/wwwroot/index.html.br"
Remove-Item -Path "Release/UIBlazor/wwwroot/index.html.gz"


Write-Host "modify app base"
$settingsFilePath = "BBH/wwwroot/appsettings.json"
$settingsContent = Get-Content -Path $settingsFilePath -Raw
$updatedSettingsContent = $settingsContent -replace '"AppBasePath": "/BlazorBrowserHistory/"','"AppBasePath": "/"'
Set-Content -Path $settingsFilePath -Value $updatedSettingsContent
Write-Host "Change back AppBasePath in $settingsFilePath "
Write-Host "Change back AppBasePath in $indexFilePath "