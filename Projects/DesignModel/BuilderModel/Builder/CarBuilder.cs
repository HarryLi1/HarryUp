using BuilderModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderModel.Builder
{
    public abstract class CarBuilder
    {
        //建造一个模型，你要给我一个顺序要求，就是组装顺序
        public abstract void SetSequence(IEnumerable<string> sequence);
        //设置完毕顺序后，就可以直接拿到这个车辆模型
        public abstract CarModel GetCarModel();
    }
}
