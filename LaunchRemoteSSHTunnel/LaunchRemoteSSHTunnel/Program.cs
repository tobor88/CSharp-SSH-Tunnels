using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Resources;

[assembly: NeutralResourcesLanguageAttribute("en-US")]
namespace LaunchRemoteSSHTunnel
{
    class Program
    {
        static void Main()
        {
            Console.Write("\nEnter username to connect to remote device: ");
            string remote_user = Console.ReadLine();

            Console.Write("\nEnter password for that remote user: ");
            string remote_password = Console.ReadLine();

            Console.Write("\nEnter the service port number you want access too: ");
            uint remote_port = Convert.ToUInt32(Console.ReadLine());

            Console.Write("\nEnter the port number you are going to use to access the remote service: ");
            uint access_port = Convert.ToUInt32(Console.ReadLine());

            // Begin SSH connection to remote host
            Console.WriteLine("Launching Remote SSH Tunnel Session");
            using var client = new SshClient("127.0.0.1", remote_user, remote_password);
            {
                client.Connect();


                var pwd_remote_port = new ForwardedPortRemote("localhost", access_port, "127.0.0.1", remote_port);
                client.AddForwardedPort(pwd_remote_port);

                pwd_remote_port.Exception += delegate (object sender, ExceptionEventArgs e)
                {
                    Console.WriteLine(e.Exception.ToString());
                };

                pwd_remote_port.Start();

                Console.WriteLine("SSH Remote Tunnel has been opened.");
                Console.WriteLine("If you wish to close your connection type c or any word that begins with c. Otherwise do nothing and let this run");

                string answer = Console.ReadLine();
                if (answer.StartsWith("c"))
                {
                    pwd_remote_port.Stop();
                    client.Disconnect();
                }
            }
        }
    }
}