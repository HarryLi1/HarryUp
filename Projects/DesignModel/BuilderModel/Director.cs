using BuilderModel.Builder;
using BuilderModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderModel
{
    public class Director
    {
        public static BenzModel GetABenzModel()
        {
            string[] sequence = new string[] { "start", "stop" };

            BenzBuilder benzBuilder = new BenzBuilder();
            benzBuilder.SetSequence(sequence);
            return (BenzModel)benzBuilder.GetCarModel();
        }

        public static BenzModel GetBBenzModel()
        {
            string[] sequence = new string[] { "engine boom", "start", "stop" };

            BenzBuilder benzBuilder = new BenzBuilder();
            benzBuilder.SetSequence(sequence);
            return (BenzModel)benzBuilder.GetCarModel();
        }

        public static BMWModel GetCBMWModel()
        {
            string[] sequence = new string[] { "alerm", "start", "stop" };

            BMWBuilder bmwBuilder = new BMWBuilder();
            bmwBuilder.SetSequence(sequence);
            return (BMWModel)bmwBuilder.GetCarModel();
        }

        public static BMWModel GetDBMWModel()
        {
            string[] sequence = new string[] { "start"};

            BMWBuilder bmwBuilder = new BMWBuilder();
            bmwBuilder.SetSequence(sequence);
            return (BMWModel)bmwBuilder.GetCarModel();
        }
    }
}
