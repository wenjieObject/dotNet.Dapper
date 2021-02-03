using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.IContract contract = new ServiceReference1.ContractClient();
            ServiceReference1.GetPrintInLabelRequest json = new ServiceReference1.GetPrintInLabelRequest();
             
            var s= contract.GetPrintInLabelAsync(json);
            Console.WriteLine("Hello World!");
        }
    }
}
