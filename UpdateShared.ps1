Copy-Item -Path "Libraries\Packets.cs" -Destination "Servers\LoginServer\General" -Force
Copy-Item -Path "Libraries\Packets.cs" -Destination "CerberusClient\Assets\Scripts\Network" -Force
Copy-Item -Path "Libraries\Packets.cs" -Destination "Servers\CereberusGameServer\Assets\Scripts" -Force
Copy-Item -Path "Libraries\AnimationStates.cs" -Destination "CerberusClient\Assets\Scripts\Network" -Force
Copy-Item -Path "Libraries\AnimationStates.cs" -Destination "Servers\CereberusGameServer\Assets\Scripts" -Force


Copy-Item -Path "..\NovaCoreNetworking\NovaCoreNetworking\bin\Debug\netstandard2.1\NovaCoreNetworking.dll" -Destination "CerberusClient\Assets\Plugins" -Force
Copy-Item -Path "..\NovaCoreNetworking\NovaCoreNetworking\bin\Debug\netstandard2.1\NovaCoreNetworking.dll" -Destination "Servers\CereberusGameServer\Assets\Plugins" -Force
Copy-Item -Path "..\NovaCoreNetworking\NovaCoreNetworking\bin\Debug\netstandard2.1\NovaCoreNetworking.dll" -Destination "Libraries" -Force

Read-Host -Prompt "Completed transfer of shared libraries, Press key to exit...."