using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DesignModel.FactoryModel.ReflectFactory
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
    /// 这个特性用来附加在产品类型之上
    /// 来标注该类型代码哪个产品，方便反射使用
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    class ProductAttribute : Attribute
    {
        /// <summary>
        /// 标注零件的成员
        /// </summary>
        private RoomParts _myRoomPart;
        public ProductAttribute(RoomParts part)
        {
            _myRoomPart = part;
        }

        public RoomParts RoomPart
        {
            get
            {
                return _myRoomPart;
            }
        }
    }

    /// <summary>
    /// 这个特性用来附加在产品接口之上
    /// 来标注一共实现了多少产品零件，方便反射使用
    /// </summary>
    class ProductListAttribute : Attribute
    {
        //产品类型集合
        private Type[] _myList;
        public ProductListAttribute(Type[] Products)
        {
            _myList = Products;
        }

        public Type[] ProductList
        {
            get
            {
                return _myList;
            }
        }
    }

    #region 产品类族
    /// <summary>
    /// 产品零件接口
    /// 需要添加所有实现该接口的列表
    /// </summary>
    [ProductList(new Type[] { typeof(Roof), typeof(Window), typeof(Pillar) })]
    interface IProduct
    {
        string GetName();
    }

    /// <summary>
    /// 屋顶类型
    /// </summary>
    [Product(RoomParts.Roof)]
    class Roof : IProduct
    {
        public string GetName()
        {
            return "屋顶";
        }
    }


    /// <summary>
    /// 窗户类型
    /// </summary>
    [Product(RoomParts.Window)]
    class Window : IProduct
    {
        public string GetName()
        {
            return "窗户";
        }
    }


    /// <summary>
    /// 柱子类型
    /// </summary>
    [Product(RoomParts.Pillar)]
    class Pillar : IProduct
    {
        public string GetName()
        {
            return "柱子";
        }
    }
    #endregion

    #region 工厂类
    /// <summary>
    /// 工厂类型，这里不再需要一个类族，而只需要一个工厂类型
    /// </summary>
    class Factory
    {
        public IProduct Produce(RoomParts part)
        {
            //通过反射，从IProduct接口中获取属性
            //从而获得所有的产品零件列表
            ProductListAttribute attr = (ProductListAttribute)Attribute.GetCustomAttribute(typeof(IProduct), typeof(ProductListAttribute));
            //遍历所有的实现产品零件类型
            foreach (var type in attr.ProductList)
            {
                //利用反射查找其属性
                ProductAttribute pa = (ProductAttribute)Attribute.GetCustomAttribute(type, typeof(ProductAttribute));

                //确定是否是需要的产品
                if (pa.RoomPart == part)
                {
                    //再一次利用反射，创建产品零件类型
                    Object product = Assembly.GetExecutingAssembly().CreateInstance(type.FullName);
                    return product as IProduct;
                }
            }

            return null;
        }
    }
    #endregion

    class FactoryManager
    {
        public static IProduct GetProduct(RoomParts part)
        {
            Factory factory = new Factory();
            IProduct product = factory.Produce(part);
            Console.WriteLine("生产了一个产品：{0}", product.GetName());
            return product;
        }
    }

}