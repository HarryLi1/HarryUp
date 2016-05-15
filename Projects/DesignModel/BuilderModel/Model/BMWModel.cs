using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderModel.Model
{
    public class BMWModel:CarModel
    {
        protected override void Start()
        {
            Console.WriteLine("宝马汽车跑起来是这样子的……");
        }

        protected override void Stop()
        {
            Console.WriteLine("宝马汽车应该这样停车……");
        }

        protected override void Alarm()
        {
            Console.WriteLine("宝马汽车的喇叭声是这样子的……");
        }

        protected override void EngineBoom()
        {
            Console.WriteLine("宝马车的引擎是这个声音的……");
        }
    }
}
