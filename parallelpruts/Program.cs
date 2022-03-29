// See https://aka.ms/new-console-template for more information

using System;
using System.Threading;

namespace parallelpruts
{
    class Program
    {
        static void setvalue()
        {
            LPT_X LPT;
           // int read;
            int Adress = 0x3FE8;

            LPT = new LPT_X(Adress);
            for (int x = 0; x < 10; x++)
            {
                LPT.WriteData(255);
                //LPT.WriteControl(0);
                Thread.Sleep(1000);
                LPT.WriteData(0);
                Thread.Sleep(1000);
            }
            //read = LPT.ReadStatus();               

        }
        static void Main(string[] args)
        {
            Console.WriteLine("test");
            setvalue();
            Console.ReadLine();
        }
    }
}
