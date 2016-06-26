using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lift.Core.Entity
{
    public class Passenger
    {
        public int ID { private set; get; }
        public byte CurrentFloor { private set; get; }
        public LiftDirectionEnum Direction { private set; get; }
        public byte TargetFloor { private set; get; }
        public byte Weight { private set; get; }
        public int Test { set; get; }

        public Passenger() { }
        public Passenger(int id, byte currentFloor, byte targetFloor, byte weight = 0)
        {
            this.ID = id;
            this.CurrentFloor = currentFloor;
            this.TargetFloor = targetFloor;
            this.Weight = weight;

            if (this.TargetFloor > this.CurrentFloor)
            {
                this.Direction = LiftDirectionEnum.Up;
            }
            else if(this.TargetFloor<this.CurrentFloor)
            {
                this.Direction = LiftDirectionEnum.Down;
            }
            else
            {
                this.Direction = LiftDirectionEnum.None;
            }
        }
    }
}
