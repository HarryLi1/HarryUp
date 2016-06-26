using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lift.Core.Entity
{
    public class StoriedBuilding
    {
        public string Name { private set; get; }
        public byte MaxFloor { private set; get; }
        public byte MinFloor { private set; get; }
        public byte LiftNum { private set; get; }

        public StoriedBuilding(string name, byte maxFloor, byte minFloor, byte liftNum)
        {
            this.Name = name;
            this.MaxFloor = maxFloor;
            this.MinFloor = minFloor;
            this.LiftNum = liftNum;
        }
    }
}
