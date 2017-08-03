Param([string] $sourcePath, [string] $buildPath)

$files = get-childitem "$sourcePath*.md";
$regex = [System.Text.RegularExpressions.Regex]::new("\<a href\=\`".*?\`"\>.*?\<\/a\>");
$targetRegex = [System.Text.RegularExpressions.Regex]::new("(?<=(href\=\`")).*?(?=\`")");
$titleRegex = [System.Text.RegularExpressions.Regex]::new("(?<=(\`"\>)).*?(?=(\<\/a\>))");

foreach ($file in $files) {
    set-content $file $regex.Replace(([IO.File]::ReadAllText($file)), {
        param($match);
        
        $target = $targetRegex.Match($match).Value;
        if ([String]::IsNullOrEmpty($target) -or $target.ToString().StartsWith('#') -or !(Test-Path "$sourcePath$target.md")) {
            return $match;
        }

        Write-Host("Found link $match");
        $title = $titleRegex.Match($match).Value;
        if ([String]::IsNullOrEmpty($title)) {
            $title = $target
        }

        Write-Host("Rewritten to [$title]($target)")
        return "[$title]($target)"
    });
}

Write-Host("Removing markdown files in $buildPath")
Remove-Item $buildPath\*.md
Write-Host("Removing media files in $buildPath")
Remove-Item -Force -Recurse $buildPath\media\

Write-Host("Copying markdown files from $sourcePath to $buildPath")
Copy-Item $sourcePath\*.md $buildPath\
Write-Host("Copying media files from $sourcePath to $buildPath")
Copy-Item -Force -Recurse $sourcePath\media $buildPath\