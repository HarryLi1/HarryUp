using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lift.Core.Entity
{
    /// <summary>
    /// 电梯类
    /// </summary>
    public class LiftEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 电梯名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电梯描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 当前楼层
        /// </summary>
        public byte CurrentFloor { get; set; }
        /// <summary>
        /// 是否限制楼层
        /// 如果有限制楼层，需设置AllowedFloors
        /// </summary>
        public bool IsLimitted { get; set; }
        /// <summary>
        /// 允许停靠的楼层
        /// </summary>
        public byte[] AllowedFloors { get; set; }
        /// <summary>
        /// 允许最大载重量
        /// </summary>
        public int MaxLoad { get; set; }
        /// <summary>
        /// 当前载重量
        /// </summary>
        public int CurrentLoad { get; set; }
        /// <summary>
        /// 建议最大人数
        /// </summary>
        public int MaxPeopleNum { get; set; }
        /// <summary>
        /// 当前人数
        /// </summary>
        public int CurrentPeopleNum { get; set; }
        /// <summary>
        /// 当前运行方向
        /// </summary>
        public LiftDirectionEnum Direction { get; set; }
        /// <summary>
        /// 电梯状态
        /// </summary>
        public LiftStatusEnum Status { get; set; }
        /// <summary>
        /// 申请进入电梯的乘客
        /// </summary>
        public List<Passenger> RequestInPassengers { get; set; }
        /// <summary>
        /// 申请出去电梯的乘客
        /// </summary>
        public List<Passenger> RequestOutPassengers { get; set; }
        /// <summary>
        /// 电梯停靠楼层列表
        /// </summary>
        public List<byte> InTargets { get; set; }
        /// <summary>
        /// 电梯停靠楼层列表
        /// </summary>
        public List<byte> OutTargets { get; set; }
        /// <summary>
        /// 电梯所属楼房
        /// </summary>
        public StoriedBuilding Building { private set; get; }

        public LiftEntity(int id, StoriedBuilding building)
        {
            ID = id;
            Name = id.ToString();
            Description = "";

            MaxLoad = int.MaxValue;
            MaxPeopleNum = int.MaxValue;

            Building = building;

            Status = LiftStatusEnum.Terminated;
            CurrentFloor = 0;

            IsLimitted = false;
        }

        /// <summary>
        /// 启动电梯
        /// 启动电梯后，电梯方可为乘客服务
        /// </summary>
        public void Start()
        {
            InTargets = new List<byte>();
            OutTargets = new List<byte>();
            RequestInPassengers = new List<Passenger>();
            RequestOutPassengers = new List<Passenger>();
            Status = LiftStatusEnum.Paused;

            if (CurrentFloor < Building.MinFloor)
            {
                CurrentFloor = Building.MinFloor;
                //Direction = LiftDirectionEnum.Up;
            }
            else if (CurrentFloor > Building.MaxFloor)
            {
                CurrentFloor = Building.MaxFloor;
                //Direction = LiftDirectionEnum.Down;
            }

            Console.WriteLine("[{0}] Started", Name);
        }

        /// <summary>
        /// 终止电梯
        /// 终止电梯后，电梯将停止运行，且不会为乘客提供任何服务，包括开门、关门等
        /// </summary>
        private void Terminate()
        {
            InTargets = new List<byte>();
            OutTargets = new List<byte>();
            RequestInPassengers = new List<Passenger>();
            Status = LiftStatusEnum.Terminated;

            Console.WriteLine("[{0}] Terminated", Name);
        }

        /// <summary>
        /// 电梯开门
        /// </summary>
        public void OpenDoor() 
        {
            Console.WriteLine("[{0}] Open Door at Floor#{1} with directory {2}", Name, CurrentFloor, Direction.ToString());
        }

        /// <summary>
        /// 电梯关门
        /// </summary>
        private void CloseDoor() 
        {
            Console.WriteLine("[{0}] Open Door at Floor#{1} with directory {2}", Name, CurrentFloor, Direction.ToString());
        }
    }
}
