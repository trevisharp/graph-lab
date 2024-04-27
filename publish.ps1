$key = gc .\.env

cd src
$csproj = gc .\GraphLab.csproj
$versionText = $csproj | % {
    if ($_.Contains("PackageVersion"))
    {
        $_
    }
}

$version = ""
$flag = 0
for ($i = 0; $i -lt $versionText.Length; $i++)
{
    $char = $versionText[$i]

    if ($flag -eq 1)
    {
        if ($char -eq "<")
        {
            break
        }

        $version += $char
    }

    if ($char -eq ">")
    {
        $flag = 1
    }
}

dotnet pack -c Release
$file = ".\bin\Release\GraphLab." + $version + ".nupkg"
cp $file GraphLab.nupkg

dotnet nuget push GraphLab.nupkg --api-key $key --source https://api.nuget.org/v3/index.json
rm .\GraphLab.nupkg
cd..