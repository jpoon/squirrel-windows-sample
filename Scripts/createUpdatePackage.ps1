[CmdletBinding()]
param(
	[Parameter(Mandatory)][string] $nugetSearchPath
)

# Find squirrel.exe
$squirrel = gci -Recurse -Filter *.exe | ?{ !$_.PSIsContainer -and [System.IO.Path]::GetFileNameWithoutExtension($_.Name) -eq "Squirrel"} | % { $_.FullName }
if(!$squirrel)
{
    Write-Error "Unable to find Squirrel.exe"
    exit 1
}

Write-Output "Found Squirrel Instalation. Using $squirrel"

# Find nupkg
$nupkgs = gci $nugetSearchPath -Recurse -Filter *.nupkg | ?{ !$_.PSIsContainer } | % { $_.FullName }
if(!$nupkgs)
{
    Write-Warning "Unable to find nupkg(s)"
    exit 1
}

Write-Output  "Found $($nupkgs.count) nupkg(s)"
foreach ($nupkg in $nupkgs) {
    Write-Output  $nupkg

    # Crete squirrel package from nupkg
    &$squirrel --releasify $nupkg | Write-Output
}