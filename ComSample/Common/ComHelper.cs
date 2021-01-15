using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComSample.Common
{
    public class ComHelper
    {
        public static StopBits GetStopBits(string emp)                                                                                                                                                                                                                                                                                                                    
        {
            StopBits bits = StopBits.None;
            switch (emp)
            {
                case "One": bits = StopBits.One; break;
                case "Two": bits = StopBits.Two; break;
                case "OnePointFive": bits = StopBits.OnePointFive; break;
            }
            return bits;
        }

        public static Parity GetParity(string emp)
        {
            Parity parity = Parity.None;
            switch (emp)
            {
                case "None": parity = Parity.None; break;
                case "Even": parity = Parity.Even; break;
                case "Mark": parity = Parity.Mark; break;
                case "Odd": parity = Parity.Odd; break;
                case "Space": parity = Parity.Space; break;
            }
            return parity;
        }

    }
}
