$action = $args[0]
if ( $action -eq "clean")
{
	Remove-Item Snake\Snake\bin -Force -Recurse
	Remove-Item Snake\Snake\obj -Force -Recurse
}
else {
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe Snake\Snake\Snake.csproj
}
