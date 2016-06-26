using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lift.Core.Entity
{
    public enum LiftStatusEnum
    {
        /// <summary>
        /// 已终止
        /// </summary>
        Terminated,
        /// <summary>
        /// 运行中（上或下）
        /// </summary>
        Running,
        /// <summary>
        /// 暂停（无请求）
        /// </summary>
        Paused,
        /// <summary>
        /// 上下人
        /// </summary>
        OUT_IN

    }
}
