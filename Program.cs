using System;
using System.Net;
using System.Net.NetworkInformation;

namespace GetURI_DNS_IPAdress
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Enter a valid web adress: ");
            string url = Console.ReadLine();

            try
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    Console.WriteLine("Bad adress!");
                    return;
                }
                var uri = new Uri(url);
                Console.WriteLine("#########");
                Console.WriteLine($"Scheme: {uri.Scheme}");
                Console.WriteLine($"Port: {uri.Port}");
                Console.WriteLine($"Host: {uri.Host}");
                Console.WriteLine($"Path: {uri.AbsolutePath}");
                Console.WriteLine($"Query: {uri.Query}");
                Console.WriteLine();
                IPHostEntry entry = Dns.GetHostEntry(uri.Host);
                Console.WriteLine($"{entry.HostName} has the following IPAdress:");
                foreach (IPAddress address in entry.AddressList)
                {
                    Console.WriteLine($"{address}");
                }

                var ping = new Ping();
                PingReply reply = ping.Send(uri.Host);
                Console.WriteLine($"{uri.Host} was pinged, and replied: {reply.Status}.");

                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine($"Reply from {reply.Address} took {reply.RoundtripTime:N0}ms");
                }
                else if (reply.Status == IPStatus.TimedOut)
                {
                    Console.WriteLine($"Reply from {reply.Address} timed out after {reply.RoundtripTime:N0}ms");
                }
                else if (reply.Status == IPStatus.Unknown)
                {
                    Console.WriteLine($"Reply from {reply.Address} UNKNOWN!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            
        }
    }
}
