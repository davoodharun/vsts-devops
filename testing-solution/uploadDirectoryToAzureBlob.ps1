#>  

Param(
  [string]$SubscriptionName,
  [string]$StorageName,
  [string]$ContainerName,
  [string]$DestFolderName,
  [string]$Source,
  [string]$StorageKey,
  [string]$DefaultWorkingDirectory,
  [string]$BuildDefinitionName,
  [string]$releaseName,
  [string]$releaseId,
  [string]$ResultsFolder
)

cd $ResultsFolder
Invoke-Expression "$($DefaultWorkingDirectory)\$($BuildDefinitionName)\drop\packages\ReportUnit.1.2.1\tools\ReportUnit.exe .\" 
Remove-Item -Path "index.html"
$fileName = "$($releaseName)_$($releaseId).html"
Get-ChildItem *.html | ForEach { Rename-Item $_ -NewName $fileName}
Get-ChildItem $DefaultWorkingDirectory -filter "*.csv" -recurse | Copy-Item -Destination $ResultsFolder

# Open Storage  
$StorageContext = New-AzureStorageContext -StorageAccountKey $StorageKey -StorageAccountName $StorageName # Get the Context 

 
    # Directory with files to upload must already exist.  
cd $Source
Write-Host "Uploading Files to Container... " $StorageContext.BlobEndPoint $ContainerName  -ForegroundColor Green  
# $_.mode -match "-a---" scans the data directory and only fetches the files. It filters out all directories 
$files = Get-ChildItem -force # Filter for better fp 
# you could also use the -filter "*.ps1" as a parameter to Get-ChildItem to only grab certain files. 
#    Example: Get-ChildItem C:\Scripts, -filter "*.ps1" 
# you can also look at multiple folders at the same time  
#    Example: Get-ChildItem C:\Scripts, C:\Test, "C:\Documents and Settings\ITProGuru\Desktop" -filter "*.ps1" 
# iterate through all the files and start uploading data 
foreach ($file in $files){ 
   #fqName represents fully qualified name 
   Write-Host $file
   $fqName = $file.FullName
   $filename = $file.Name
   $blob = "$DestFolderName\$filename" 
   # Examples of working with other properties... 
            # $file.DirectoryName ; $file.Mode ; $file.Attributes ; $file.Directory 
            # $file.Exists ; $file.Extension ;  $file.Name ; $file.VersionInfo 
            # Note: simi-colon to run multiple commands on the same line :) 
   if ($file.Extension -ne ".publishsettings") {     # Exclude PublishSettings Files 
      Write-Host "Uploading " $dir $file.Name  -ForegroundColor Green  
      Set-AzureStorageBlobContent -Blob $blob -Container $ContainerName -File $fqName -Context $StorageContext -Force 
   } 
} 
