using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuilderModel
{
    static class Program
    {
        static void Main()
        {
            Director.GetABenzModel().Run();
            Director.GetBBenzModel().Run();

            Director.GetCBMWModel().Run();
            Director.GetDBMWModel().Run();

            Console.Read();

        }
    }
}
