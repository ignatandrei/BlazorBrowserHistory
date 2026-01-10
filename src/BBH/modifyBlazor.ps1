# Define the path to the HTML file
$htmlFilePath = "Release/UIBlazor/wwwroot/index.html"

# Read the content of the HTML file
$htmlContent = Get-Content -Path $htmlFilePath -Raw

$updatedHtmlContent = $htmlContent
# Replace the <base href="/" /> with <base href="/Blazor" />
$updatedHtmlContent = $updatedHtmlContent -replace '<base href="/" />', '<base href="/BlazorBrowserHistory/" />'


# Write-Output "The <base href> tag has been updated successfully."

$currentDateTime = (Get-Date).ToString("yyyyMMddHHmmss")


# Replace all href="..." with href="...?v=<todayDatewithSeconds>"
#$updatedHtmlContent = $updatedHtmlContent  -replace 'href="([^"]*)"', "href=`"$1 $currentDateTime`""

$updatedHtmlContent = $updatedHtmlContent -replace 'href="([^"]*).css"', ('href="./$1.css?v=' + $currentDateTime + '"')
$updatedHtmlContent = $updatedHtmlContent -replace 'src="([^"]*).js"', ('src="./$1.js?v=' + $currentDateTime + '"')

# Write the updated content back to the HTML file

Set-Content -Path $htmlFilePath -Value $updatedHtmlContent


Write-Output "All href attributes have been updated successfully."
return
# http://localhost:55397/_framework/dotnet.kvh1oe3gbp.js
# http://localhost:57805/_framework/dotnet.kvh1oe3gbp.js
$blazorBoot = "Release/UIBlazor/wwwroot/_framework/blazor.boot.json"
$blazorBootjson =( Get-Content $blazorBoot  -raw | ConvertFrom-Json -Depth 20)
$blazorBootjson.appsettings[0]= $blazorBootjson.appsettings[0] + "?v=" + $currentDateTime
$blazorBootjson | ConvertTo-Json -depth 20| set-content $blazorBoot
