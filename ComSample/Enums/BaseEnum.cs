using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComSample.Enums
{
    /// <summary>
    /// 端口号
    /// </summary>
    public enum Port
    {
        COM1,
        COM2,
        COM3,
        COM4,
        COM5,
        COM6,
        COM7,
        COM8,
        COM9,
        COM10,
        COM11,
        COM12,
        COM13,
        COM14,
        COM15,
        COM16,
        COM17,
        COM18,
        COM19,
        COM20,
        COM21,
        COM22,
        COM23,
        COM24,
        COM25,
        COM26,
        COM27,
        COM28,
        COM29,
        COM30
    }

    /// <summary>
    /// 奇偶校验
    /// </summary>
    public enum CheckMode
    {
        None = 0,
        Odd = 1,
        Even = 2,
        Mark = 3,
        Space = 4
    }

    /// <summary>
    /// 停止位
    /// </summary>
    public enum StopBit
    {
        One=1,
        Two=2,
        OnePointFive=3,
    }
}
