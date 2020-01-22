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
    public class Jaco : IDevice
    {
        public Jaco() { }

        public Jaco(string portName)
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
            return "true";
        }

        public void Initialise()
        {
        }

        public string Forward()
        {
            return "Jaco moving forward!!";
        }

        public string Backward()
        {
            return "Jaco moving backward!!";
        }

        public string TiltUp()
        {
            return "Jaco tiltling up!!";
        }
    }
}
    