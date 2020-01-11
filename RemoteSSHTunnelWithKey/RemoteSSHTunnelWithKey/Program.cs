using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Collections.Generic;

namespace RemoteSSHTunnelWithKey
{
    class Program
    {
        static void Main()
        {
            // Enter your environments information
            Console.Write("\nEnter the servers username: ");
            string user_name = Console.ReadLine();

            Console.Write("\nEnter the full file path for the users private ssh key: ");
            string key_path = Console.ReadLine();

            Console.Write("\nEnter the keys password: ");
            string key_password = Console.ReadLine();

            Console.Write("\nEnter the local port you wish to connect to for access to the remote service: ");
            uint local_port = Convert.ToUInt32(Console.ReadLine());

            Console.Write("\nEnter the port of the remote service you want to access: ");
            uint remote_port = Convert.ToUInt32(Console.ReadLine());

            // Connecting through SSH using password protected private key
            var port = new ForwardedPortRemote("127.0.0.1", local_port, "localhost", remote_port);
            var pk = new PrivateKeyFile(key_path, key_password);
            var keyFiles = new[] { pk };

            var methods = new List<AuthenticationMethod>();
            methods.Add(new PrivateKeyAuthenticationMethod(user_name, keyFiles));

            // 22 is the port that SSH is being accessed over. Change this if the default value differs
            var remotes_con = new ConnectionInfo("localhost", 22, user_name, methods.ToArray());

            using (var remotes_client = new SshClient(remotes_con))
            {
                remotes_client.Connect();

                var remotes_port = new ForwardedPortRemote("127.0.0.1", local_port, "localhost", remote_port);
                remotes_client.AddForwardedPort(remotes_port);

                remotes_port.Exception += delegate (object sender, ExceptionEventArgs e)
                {
                    Console.WriteLine(e.Exception.ToString());
                };
                remotes_port.Stop();
                remotes_client.Disconnect();
            };
        }
    }
}
