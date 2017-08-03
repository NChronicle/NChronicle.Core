Param([string] $target, [string] $msbuild)

$PROGRAM_FILES_32 = ${env:ProgramFiles(x86)}
$DEFAULT_LOCATION_FOR_BUILDTOOLS = "$PROGRAM_FILES_32\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\MSBuild.exe"
$DEFAULT_LOCATION_FOR_VS_COMMUNITY = "$PROGRAM_FILES_32\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe"
$DEFAULT_LOCATION_FOR_VS_PROFESSIONAL = "$PROGRAM_FILES_32\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MSBuild.exe"
$DEFAULT_LOCATION_FOR_VS_ENTERPRISE = "$PROGRAM_FILES_32\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe"

if (![String]::IsNullOrEmpty($msbuild)) {
    if (-not (Test-Path $msbuild)) {
        Throw [System.IO.FileNotFoundException] "The path $msbuild was invalid or the file did not exist."
    }
} else {
    $msbuild = $DEFAULT_LOCATION_FOR_BUILDTOOLS
    if (-not (Test-Path $msbuild)) {
        $msbuild = $DEFAULT_LOCATION_FOR_VS_COMMUNITY
    }
    if (-not (Test-Path $msbuild)) {
        $msbuild = $DEFAULT_LOCATION_FOR_VS_PROFESSIONAL
    }
    if (-not (Test-Path $msbuild)) {
        $msbuild = $DEFAULT_LOCATION_FOR_VS_ENTERPRISE
    }
    if (-not (Test-Path $msbuild)) {
        Throw [System.IO.FileNotFoundException] "Could not find a C# 7 MSBuild tool."
    }
}

&$msbuild $target