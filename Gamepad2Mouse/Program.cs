using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Gamepad2Mouse gamepad2Mouse = new Gamepad2Mouse();
            gamepad2Mouse.Start();
            Console.ReadKey();
        }
    }
}
