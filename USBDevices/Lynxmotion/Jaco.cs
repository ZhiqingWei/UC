using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCProtocol;

namespace Lynxmotion
{
    [Export(typeof(UCProtocol.IDevice))]
    public class Jaco : SSC32, IDevice
    {
        public Jaco() { }

        public Jaco(string portName)
            : base(portName, 9600, 6)
        {
            Initialise();
        }

        public object ConnectDevice(string serialPort)
        {
            System.Diagnostics.Debug.WriteLine("connect Jaco!");
            return new Jaco(serialPort);
        }

        public string GetSerialPort()
        {
            SSC32ENumerationResult[] SSC32s = Jaco.EnumerateConnectedSSC32(9600);
            if (SSC32s.Length > 0)
            {
                return SSC32s[0].PortName;
            }
            return "";
        }

        public void Initialise()
        {
            
        }

        public string Forward()
        {
            return "Jaco moving forward!!";
        }
    }
}
