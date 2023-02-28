Copy-Item -Path "Packets.cs" -Destination "Servers\LoginServer\General" -Force
Copy-Item -Path "Packets.cs" -Destination "Servers\GameServer\General" -Force
Copy-Item -Path "Packets.cs" -Destination "CerberusClient\Assets\Scripts\Network" -Force

Copy-Item -Path "Libraries\NovaCoreNetworking\NovaCoreNetworking\bin\Debug\netstandard2.1\NovaCoreNetworking.dll" -Destination "CerberusClient\Assets\Plugins" -Force

Read-Host -Prompt "Completed transfer of shared libraries, Press key to exit...."