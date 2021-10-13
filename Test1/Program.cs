using System;

namespace Test1
{
    class Program
    {
        static void Main(string[] args)
        {
            int x  = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());
            int z = y;
            for (int a = 0; a<z; a++)
            {
                for (int b = 0; b<y; b++)
                {
                    Console.Write(x);
                }
            }
            Console.WriteLine();
            y--;
        }
    }
}
