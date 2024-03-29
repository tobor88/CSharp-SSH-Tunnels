# CSharp-SSH-Tunnels

C# Applications that can be run to easily open local or remote SSH Tunnels. Great application if you need to allow an every day user or contractor the ability to create a tunnel in order to access a resource. I don't think this happens often but I made it to better learn C# tool making.

Here are a some SSH tunnels that are interactive with simple prompts for creating local and remote SSH tunnels. This could also be beneficial to someone who needs to look up how to make an SSH tunnel every time they need one. The OpenSSH Translations are below.

- __LaunchLocalSSHTunnel__ (Executed on a local machine trying to access a remote resource)
```
ssh -L <Local-port>:<remote_host>:<R_service_port> <user>@<remote_host>.com -p 22
```


- __LaunchRemoteSSHTunnel__ (Requires access on the remote server and requires the sshd_config value 'GatewayPorts yes')
```
ssh -R <Listen-port>:localhost:<R_service_port> <user>@<server_hostname>.com -p 22
```


- __RemoteSSHTunnelWithKey__ (Requires access on the remote server and requires the sshd_config value 'GatewayPorts yes')
```
ssh -R <Listen-port>:localhost:<R_service_port> <user>@<server_hostname>.com -i $env:USERPROFILE/.ssh/id_rsa -p 22
```

To show what the execution of LaunchRemoteSSHTunnel looks like I have included the below image.
![alt text](https://raw.githubusercontent.com/tobor88/CSharp-SSH-Tunnels/master/RemoteSSHTunnel.png "Image of Applications Prompts")

The below image is to show the listening ports that were opened. All this was done on a single machine.
![alt text](https://raw.githubusercontent.com/tobor88/CSharp-SSH-Tunnels/master/Listening.png "Image of Applications Prompts")
