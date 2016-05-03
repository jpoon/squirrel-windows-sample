[CmdletBinding()]
param(
	[Parameter(Mandatory)][string] $containerName,
	[Parameter(Mandatory)][string] $destinationFolder,
	[Parameter(Mandatory)][string] $connectionString
)

$storage_account = New-AzureStorageContext -ConnectionString $connectionString
$blobs = Get-AzureStorageBlob -Container $containerName -Context $storage_account
Write-Output  "Found $($blobs.count) blob(s)"

$destinationPath = $(Join-Path (Get-Item $PSScriptRoot ).parent.parent.FullName $destinationFolder)

$directory = New-Item -ItemType Directory -Force -Path $destinationPath
Write-Output  "Downloading to $directory"

foreach ($blob in $blobs)
{
    $blobContent = Get-AzureStorageBlobContent `
        -Container $containerName `
        -Blob $blob.Name `
        -Destination $destinationPath `
        -Context $storage_account `
        -Force
        
    Write-Output "$($blobContent.Name)"
}   