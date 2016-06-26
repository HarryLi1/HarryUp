using Lift.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lift.Core
{
    public class LiftController
    {
        private List<Passenger> _passengers;
        private StoriedBuilding _building;
        private int _liftNum;
        private List<LiftEntity> _lifts;
        private Passenger testP = new Passenger();

        public LiftController(StoriedBuilding building)
        {
            _building = building;
            _liftNum = _building.LiftNum;
            _lifts = new List<LiftEntity>();
            _passengers = new List<Passenger>();
        }
        public void AddPassenger(Passenger passenger)
        {
            _passengers.Add(passenger);
        }

        public void Start()
        {
            if (_liftNum == 0)
            {
                Console.WriteLine("楼房{0}无电梯!", _building.Name);
                return;
            }

            for (int i = 0; i < _liftNum; i++)
            {
                LiftEntity lift = new LiftEntity(i, _building);
                lift.Start();
                _lifts.Add(lift);

                ThreadPool.QueueUserWorkItem(LiftWork, lift);
            }

            ThreadPool.QueueUserWorkItem(ManagerWork, _passengers);
        }

        
        private void LiftWork(object obj)
        {
            while(true)
            {
                LiftEntity lift = (LiftEntity)obj;
                Console.WriteLine("Lift[{0}] is {1} at {2}", lift.ID, lift.Direction.ToString(), lift.CurrentFloor);
                Console.WriteLine("Lift[{0}] InTargets:{1}", lift.ID, lift.InTargets.Count);
                Console.WriteLine("Lift[{0}] OutTargets:{1}", lift.ID, lift.OutTargets.Count);

                if (lift.InTargets.Contains(lift.CurrentFloor) || lift.OutTargets.Contains(lift.CurrentFloor))
                {
                    lift.Status = LiftStatusEnum.OUT_IN;
                    Console.WriteLine("Lift[{0}]: Out Or In", lift.ID);
                }
                else if (lift.InTargets.Any() || lift.OutTargets.Any())
                {
                    lift.Status = LiftStatusEnum.Running;
                    //根据InTarget和OutTarget，修正最佳方向
                    if (lift.OutTargets.Count == 0)
                    {
                        if (lift.InTargets.All(it => it < lift.CurrentFloor))
                        {
                            lift.Direction = LiftDirectionEnum.Down;
                        }
                        else if (lift.InTargets.All(it => it > lift.CurrentFloor))
                        {
                            lift.Direction = LiftDirectionEnum.Up;
                        }
                    }
                    Console.WriteLine("Lift[{0}]: Running", lift.ID);
                }
                else
                {
                    lift.Status = LiftStatusEnum.Paused;
                    Console.WriteLine("Lift[{0}]: Paused", lift.ID);
                }

                if (lift.Status == LiftStatusEnum.OUT_IN)
                {
                    if (lift.InTargets.Contains(lift.CurrentFloor))
                    {
                        var passengers = lift.RequestInPassengers.Where(p => p.CurrentFloor == lift.CurrentFloor).ToList();
                        
                        for (int i = 0; i < passengers.Count; i++)
                        {
                            var passenger = passengers[i];
                            Console.WriteLine("Lift[{0}]: Passenger[{1}] IN", lift.ID, passenger.ID);

                            lift.CurrentPeopleNum++;
                            lift.CurrentLoad += passenger.Weight;
                            if (!lift.OutTargets.Contains(passenger.TargetFloor))
                            {
                                lift.OutTargets.Add(passenger.TargetFloor);
                            }
                            lift.RequestOutPassengers.Add(passenger);

                            lift.RequestInPassengers.Remove(passenger);
                            lift.InTargets.Remove(lift.CurrentFloor);
                            
                            passengers.Remove(passenger);
                            
                            i--;

                            //没有新需求，电梯直接去接乘客
                            if (lift.InTargets.Count == 0)
                            {
                                if (lift.CurrentFloor == passenger.CurrentFloor)
                                {
                                    lift.Direction = passenger.Direction;
                                }
                                
                            }
                        }

                    }

                    if (lift.OutTargets.Contains(lift.CurrentFloor))
                    {
                        var passengers = lift.RequestOutPassengers.Where(p => p.TargetFloor == lift.CurrentFloor).ToList();
                        for (int i = 0; i < passengers.Count; i++)
                        {
                            var passenger = passengers[i];
                            Console.WriteLine("Lift[{0}]: Passenger[{1}] OUT", lift.ID, passenger.ID);

                            lift.CurrentPeopleNum--;
                            lift.CurrentLoad -= passenger.Weight;

                            lift.RequestInPassengers.Remove(passenger);
                            lift.OutTargets.Remove(lift.CurrentFloor);
                            passengers.Remove(passenger);

                            i--;
                        }
                    }

                    if (!lift.InTargets.Any() && !lift.OutTargets.Any())
                    {
                        lift.Direction = LiftDirectionEnum.None;
                        lift.Status = LiftStatusEnum.Paused;
                    }

                    //模拟乘客进出电梯的时间
                    Thread.Sleep(Constants.OutInInMS);
                    if (lift.Status == LiftStatusEnum.OUT_IN)
                    {
                        Console.WriteLine("Lift[{0}] is leaving {1}", lift.ID, lift.CurrentFloor);

                        lift.Status = LiftStatusEnum.Running;
                    }
                }

                if(lift.Status == LiftStatusEnum.Running)
                {
                    if (lift.Direction == LiftDirectionEnum.None)
                        lift.Direction = LiftDirectionEnum.Up;

                    //模拟上行或下行一层的时间
                    Thread.Sleep(Constants.OneFloorTimeInMS);

                    if (lift.Direction == LiftDirectionEnum.Up)
                    {
                        lift.CurrentFloor++;

                        if (lift.CurrentFloor == lift.Building.MaxFloor)
                        {
                            lift.Direction = LiftDirectionEnum.Down;
                        }
                    }
                    else if (lift.Direction == LiftDirectionEnum.Down)
                    {
                        lift.CurrentFloor--;

                        if (lift.CurrentFloor == lift.Building.MinFloor)
                        {
                            lift.Direction = LiftDirectionEnum.Up;
                        }
                    }
                }
                else
                {
                    //暂停中
                    Thread.Sleep(Constants.PausedLoopInMS);
                }
            }
        }

        private void ManagerWork(object obj)
        {
            while (true)
            {
                List<Passenger> passengers = (List<Passenger>)obj;
                Console.WriteLine("ManagerWork 有[0]个乘客在等待", passengers.Count);
                passengers.ForEach(p =>
                {
                    Console.WriteLine("ManagerWork 乘客[{0}] {1}/{2}", p.ID, p.CurrentFloor, p.TargetFloor);
                });

                for (int i = 0; i < passengers.Count; i++)
                {
                    int index = -1;
                    byte distince = byte.MaxValue;
                    Passenger p = passengers[i];

                    for (byte j = 0; j < _lifts.Count; j++)
                    {
                        var lift = _lifts[j];
                        byte t_distince = (byte)Math.Abs(p.CurrentFloor - lift.CurrentFloor);

                        if (lift.Direction == p.Direction)//人梯同向
                        {
                            //人梯同层，且电梯允许进人
                            if (t_distince == 0 && lift.Status == LiftStatusEnum.OUT_IN)
                            {
                                index = j;
                                distince = t_distince;
                                break;
                            }

                            //人在电梯的前行方向上
                            if (p.Direction == LiftDirectionEnum.Up && p.CurrentFloor > lift.CurrentFloor
                                || p.Direction == LiftDirectionEnum.Down && p.CurrentFloor < lift.CurrentFloor)
                            {
                                if (t_distince < distince)
                                {
                                    index = j;
                                    distince = t_distince;
                                }
                            }
                        }

                        else if(lift.Direction == LiftDirectionEnum.None)//没有请求，电梯暂停中
                        {
                            if (t_distince < distince)
                            {
                                index = j;
                                distince = t_distince;
                            }
                        }

                        else if(p.CurrentFloor == _building.MaxFloor && lift.Direction== LiftDirectionEnum.Down 
                            || p.CurrentFloor==_building.MinFloor && lift.Direction == LiftDirectionEnum.Up)//人位于顶层且电梯上行或人位于底层且电梯下行
                        {
                            if (t_distince < distince)
                            {
                                index = j;
                                distince = t_distince;
                            }
                        }
                    }

                    if (index > -1)
                    {
                        Console.WriteLine("ManagerWork 为乘客[{0}]分配电梯[{1}]", p.ID, _lifts[index].ID);
                        //为乘客分配最快电梯
                        _lifts[index].RequestInPassengers.Add(p);
                        if (!_lifts[index].InTargets.Contains(p.CurrentFloor))
                            _lifts[index].InTargets.Add(p.CurrentFloor);

                        passengers.Remove(p);
                        i--;
                    }
                }

                Thread.Sleep(Constants.ManagerLoopInMS);
            }
            
        }

        public void TestStart()
        {
            ThreadPool.QueueUserWorkItem(TestA, testP);
            ThreadPool.QueueUserWorkItem(TestB, testP);
        }


        private void TestA(object obj)
        {
            while (true)
            {
                Passenger p = (Passenger)obj;
                p.Test = new Random().Next(0, 100);
                Console.WriteLine("TestA set {0}", p.Test);
                Thread.Sleep(2000);
            }
        }

        private void TestB(object obj)
        {
            while (true)
            {
                Passenger p = (Passenger)obj;
                Console.WriteLine("TestB get {0}", p.Test);
                Thread.Sleep(3000);
            }
        }
    }
}
