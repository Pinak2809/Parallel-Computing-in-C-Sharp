using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

class intensiveFunctionsWithPLINQ
{
    static void Main()
    {
        Console.WriteLine("PLINQ's With DegreeOfParallelism Demo in C#:");
        Console.WriteLine("---------------------------------------------");

        string[] websites = {
        "www.Google.com",
        "www.wikipedia.org",
        "www.yahoo.com",
        "www.youtube.com"
        };

        var results = websites. AsParallel() // for parallel execution of LINQ queries
                                .WithDegreeOfParallelism(4) //Gives hint to run with a maximum of four parallel tasks.
                                .Select(site =>
                                {
                                    Ping objPing = new Ping();
                                    PingReply reply = objPing.Send(site);
                                    return new
                                    {
                                        site,
                                        Result = reply. Status,
                                        Time = reply. RoundtripTime
                                    };
                                });
        foreach (var result in results)
        {
        Console.WriteLine($"Website: {result.site}, Result: {result. Result}, Time: {result. Time}ms");
        }
        Console.ReadLine();
    }
}