using System;

namespace Navicon.JsonSerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new DateTime(DateTime.Now.Subtract(Convert.ToDateTime("1998/11/22")).Ticks).Year - 1);
        }
    }
}
