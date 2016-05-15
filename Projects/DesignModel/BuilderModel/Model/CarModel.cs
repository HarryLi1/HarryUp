using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderModel.Model
{
    public abstract class CarModel
    {
        //各个基本方法的执行顺序
        private List<string> Sequence = new List<string>();
        //模型是启动开始跑了
        protected abstract void Start();
        //能发动，还要能停下来，那才是真本事
        protected abstract void Stop();
        //喇叭会出声音，是滴滴叫，还是哔哔叫 
        protected abstract void Alarm();
        //引擎会轰隆隆地响，不响那是假的
        protected abstract void EngineBoom();
        //那模型应该会跑吧，别管是人推的，还是电力驱动，总之要会跑 
        public void Run()
        {
            for (int i = 0; i < this.Sequence.Count; i++)
            {
                string actionName = Sequence[i];
                if(actionName.Equals("start", StringComparison.OrdinalIgnoreCase))
                {
                    this.Start();
                }
                else if (actionName.Equals("stop", StringComparison.OrdinalIgnoreCase))
                {
                    this.Stop();
                }
                else if (actionName.Equals("alarm", StringComparison.OrdinalIgnoreCase))
                {
                    this.Alarm();
                }
                else if (actionName.Equals("engine boom", StringComparison.OrdinalIgnoreCase))
                {
                    this.EngineBoom();
                }
            }
            Console.WriteLine("------------------------------------");
        }
        //把传递过来的值传递到类内
        public void SetSequence(IEnumerable<string> sequence)
        {
            this.Sequence.Clear();
            this.Sequence.AddRange(sequence);
        }
    }
}
