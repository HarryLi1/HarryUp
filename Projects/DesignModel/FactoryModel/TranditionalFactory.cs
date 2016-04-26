using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignModel.FactoryModel.TranditionalFactory
{

    /// <summary>
    /// 屋子的生产零件
    /// </summary>
    enum RoomParts
    {
        Roof,//屋顶
        Window,//窗户
        Pillar//柱子
    }

    /// <summary>
    /// 工厂接口
    /// </summary>
    interface IFactory
    {
        IProduct Produce();
    }

    /// <summary>
    /// 产品接口
    /// </summary>
    interface IProduct
    {
        string GetName();
    }

    #region 产品类族
    class Roof : IProduct
    {
        /// <summary>
        /// 实现产品接口，返回产品名字
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return "屋顶";
        }
    }

    class Window : IProduct
    {
        /// <summary>
        /// 实现产品接口，返回产品名字
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return "窗户";
        }
    }

    class Pillar : IProduct
    {
        /// <summary>
        /// 实现产品接口，返回产品名字
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return "柱子";
        }
    }
    #endregion

    #region 工厂类族
    class RoofFactory : IFactory
    {
        /// <summary>
        /// 实现工厂接口，返回产品对象
        /// </summary>
        /// <returns></returns>
        public IProduct Produce()
        {
            return new Roof();
        }
    }

    class WindowFactory : IFactory
    {
        /// <summary>
        /// 实现工厂接口，返回产品对象
        /// </summary>
        /// <returns></returns>
        public IProduct Produce()
        {
            return new Window();
        }
    }

    class PillarFactory : IFactory
    {
        /// <summary>
        /// 实现工厂接口，返回产品对象
        /// </summary>
        /// <returns></returns>
        public IProduct Produce()
        {
            return new Pillar();
        }
    }
    #endregion

    /// <summary>
    /// 工厂管理者
    /// </summary>
    class FactoryManager
    {
        public static IProduct GetProduct(RoomParts part)
        {
            IFactory factory = null;

            //这里就是传统工厂模式的弊端，工厂管理者和工厂类族类耦合
            //根据不同的产品类型，找到对应的工厂
            switch (part)
            {
                case RoomParts.Roof:
                    factory = new RoofFactory();
                    break;
                case RoomParts.Window:
                    factory = new WindowFactory();
                    break;
                case RoomParts.Pillar:
                    factory = new PillarFactory();
                    break;
                default:
                    break;
            }

            //利用工厂生产出产品
            IProduct product = factory.Produce();
            Console.WriteLine("生产了一个产品：{0}", product.GetName());
            return product;
        }
    }
}