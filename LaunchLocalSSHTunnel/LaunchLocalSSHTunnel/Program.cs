using System;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace LaunchLocalSSHTunnel
{
    class Program
    {
        static void Main()
        {
            // Enter your environments information
            Console.Write("\nEnter username to connect to remote machine: ");
            string user_name = Console.ReadLine();

            Console.Write("\nEnter that users password: ");
            string user_password = Console.ReadLine();

            Console.Write("\nEnter the remote servers hostname or ipv4 address: ");
            string remote_host = Console.ReadLine();

            Console.Write("\nEnter the port of the remote service you want to access: ");
            uint remote_port = Convert.ToUInt32(Console.ReadLine());

            Console.Write("\nEnter the local port you wish to connect to for access to the remote service: ");
            uint local_port = Convert.ToUInt32(Console.ReadLine());

            // Begin SSH connection to localhost
            Console.WriteLine("Launching Local SSH Tunnel Session");
            using var client = new SshClient(remote_host, user_name, user_password);
            {
                client.Connect();

                var pwd_local_port = new ForwardedPortLocal("127.0.0.1", local_port, "localhost", remote_port);
                client.AddForwardedPort(pwd_local_port);

                pwd_local_port.Exception += delegate (object sender, ExceptionEventArgs e)
                {
                    Console.WriteLine(e.Exception.ToString());
                };

                pwd_local_port.Start();

                Console.WriteLine("SSH Local Tunnel has been opened.");
                Console.WriteLine("If you wish to close your connection type c or any word that begins with c. Otherwise do nothing and let this run");

                string answer = Console.ReadLine();
                if (answer.StartsWith("c"))
                {
                    pwd_local_port.Stop();
                    client.Disconnect();
                }
            }
        }
    }
}