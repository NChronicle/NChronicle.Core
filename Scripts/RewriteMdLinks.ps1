$path =  ".\" 
$pattern = "*.md";
if ($args.Length -eq 2) {
    $path = $args[0]; 
    $pattern = $args[1];
}
$files = get-childitem "$path$pattern";
$regex = [System.Text.RegularExpressions.Regex]::new("(?<=(href\=\`")).*?(?=\`")");
foreach ($file in $files) {
    set-content $file $regex.Replace(([IO.File]::ReadAllText($file)), {
        param($match);
        ("$match.md", $match)[$match.ToString().StartsWith('#') -or !(Test-Path "$path$match.md")]
    });
}