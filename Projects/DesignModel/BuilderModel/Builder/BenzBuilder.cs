using BuilderModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderModel.Builder
{
    public class BenzBuilder:CarBuilder
    {
        private BenzModel benz = new BenzModel();

        public override void SetSequence(IEnumerable<string> sequence)
        {
            this.benz.SetSequence(sequence);
        }

        public override Model.CarModel GetCarModel()
        {
            return this.benz;
        }
    }
}
