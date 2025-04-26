namespace ConsoleAppTestPing
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var pingUtility = new PingUtility();

            Console.WriteLine("=== Basic Ping Test ===");
            TestPing(pingUtility, "google.com", 1000, 4);

            Console.WriteLine("\n=== Localhost Test ===");
            TestPing(pingUtility, "localhost", 500, 3);

            Console.WriteLine("\n=== Invalid Address Test ===");
            TestPing(pingUtility, "invalid.address", 1000, 2);

            Console.WriteLine("\n=== Short Timeout Test ===");
            TestPing(pingUtility, "github.com", 10, 2);  // Very short timeout to test timeout handling

            Console.WriteLine("\n=== Multiple Packets Test ===");
            TestPing(pingUtility, "microsoft.com", 1000, 10);

            // Uncomment to test parameter validation
            // Console.WriteLine("\n=== Parameter Validation Test ===");
            // try
            // {
            //     TestPing(pingUtility, "", 1000, 2);  // Empty address
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine($"Expected error: {ex.Message}");
            // }
        }

        static void TestPing(PingUtility pingUtility, string address, int timeout, int count)
        {
            try
            {
                Console.WriteLine($"Testing {address} with {count} packets (timeout: {timeout}ms)");
                pingUtility.PingHost(address, timeout, count);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

