

    using System;
    using System.Net.NetworkInformation;
    using System.Threading;

    public class PingUtility
    {
        public void PingHost(string ipAddress, int pingTimeout, int packetCount)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                throw new ArgumentException("IP address cannot be null or empty", nameof(ipAddress));
            }

            if (pingTimeout <= 0)
            {
                throw new ArgumentException("Ping timeout must be greater than 0", nameof(pingTimeout));
            }

            if (packetCount <= 0)
            {
                throw new ArgumentException("Packet count must be greater than 0", nameof(packetCount));
            }

            using (Ping ping = new Ping())
            {
                Console.WriteLine($"Pinging {ipAddress} with {packetCount} packets:");

                int successfulPings = 0;
                long totalTime = 0;

                for (int i = 0; i < packetCount; i++)
                {
                    try
                    {
                        PingReply reply = ping.Send(ipAddress, pingTimeout);

                        if (reply.Status == IPStatus.Success)
                        {
                            Console.WriteLine($"Reply from {reply.Address}: bytes={reply.Buffer.Length} time={reply.RoundtripTime}ms TTL={reply.Options?.Ttl ?? 0}");
                            successfulPings++;
                            totalTime += reply.RoundtripTime;
                        }
                        else
                        {
                            Console.WriteLine($"Error: {reply.Status}");
                        }
                    }
                    catch (PingException pe)
                    {
                        Console.WriteLine($"Ping exception: {pe.InnerException?.Message ?? pe.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }

                    // Add small delay between pings if not the last packet
                    if (i < packetCount - 1)
                    {
                        Thread.Sleep(1000);
                    }
                }

                // Display statistics
                Console.WriteLine();
                Console.WriteLine($"Ping statistics for {ipAddress}:");
                Console.WriteLine($"    Packets: Sent = {packetCount}, Received = {successfulPings}, Lost = {packetCount - successfulPings} ({(packetCount - successfulPings) * 100 / packetCount}% loss)");

                if (successfulPings > 0)
                {
                    Console.WriteLine($"Approximate round trip times in milli-seconds:");
                    Console.WriteLine($"    Average = {totalTime / successfulPings}ms");
                }
            }
        }
    }
