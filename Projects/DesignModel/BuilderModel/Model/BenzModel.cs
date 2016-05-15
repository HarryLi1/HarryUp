using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderModel.Model
{
    public class BenzModel: CarModel
    {
        protected override void Start()
        {
            Console.WriteLine("奔驰汽车跑起来是这样子的……");
        }

        protected override void Stop()
        {
            Console.WriteLine("奔驰汽车应该这样停车……");
        }

        protected override void Alarm()
        {
            Console.WriteLine("奔驰汽车的喇叭声是这样子的……");
        }

        protected override void EngineBoom()
        {
            Console.WriteLine("奔驰车的引擎是这个声音的……");
        }
    }
}
