using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lift.Core;
using Lift.Core.Entity;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            StoriedBuilding building = new StoriedBuilding("B1", 10, 1, 1);
            LiftController lc = new LiftController(building);
            lc.Start();

            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                byte currentFloor = (byte)rnd.Next(building.MinFloor, building.MaxFloor);
                byte targetFloor = (byte)currentFloor;
                while (targetFloor == currentFloor)
                {
                    targetFloor = (byte)rnd.Next(building.MinFloor, building.MaxFloor);
                }
                byte weight = (byte)rnd.Next(0, 0);

                Passenger p = new Passenger(i, currentFloor, targetFloor, weight);

                lc.AddPassenger(p);
                Console.WriteLine("Add passenger [{0}]", i);
                Thread.Sleep(20000);
            }

            Console.Read();
        }
    }
}
