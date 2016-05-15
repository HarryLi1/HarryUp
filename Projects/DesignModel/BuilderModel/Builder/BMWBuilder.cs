using BuilderModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderModel.Builder
{
    public class BMWBuilder:CarBuilder
    {
        private BMWModel bmw = new BMWModel();

        public override void SetSequence(IEnumerable<string> sequence)
        {
            this.bmw.SetSequence(sequence);
        }

        public override Model.CarModel GetCarModel()
        {
            return this.bmw;
        }
    }
}
