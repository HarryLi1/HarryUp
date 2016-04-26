using R = DesignModel.FactoryModel.ReflectFactory;
using T = DesignModel.FactoryModel.TranditionalFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignModel.FactoryModel
{
    class Test
    {
        static void Main(string[] args)
        {
            Console.WriteLine("反射工厂");
            //根据需要获得不同的产品零件
            R.IProduct window1 = R.FactoryManager.GetProduct(R.RoomParts.Window);
            R.IProduct roof1 = R.FactoryManager.GetProduct(R.RoomParts.Roof);
            R.IProduct pillar1 = R.FactoryManager.GetProduct(R.RoomParts.Pillar);

            Console.WriteLine("传统工厂");
            //根据需要获得不同的产品零件
            T.IProduct window2 = T.FactoryManager.GetProduct(T.RoomParts.Window);
            T.IProduct roof2 = T.FactoryManager.GetProduct(T.RoomParts.Roof);
            T.IProduct pillar2 = T.FactoryManager.GetProduct(T.RoomParts.Pillar);
            Console.Read();
        }
    }
}
