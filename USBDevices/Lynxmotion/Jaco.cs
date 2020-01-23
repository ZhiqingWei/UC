//  BuddyHub Universal Controller
//
//  Created by Zhiqing Wei, 2019
//  https://github.com/ZhiqingWei/UC

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
        private bool _isDrinking = false;

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

        public string Forward(string Mode)
        {
            switch (Mode)
            {
                case "arm":
                    return "Jaco moving forward!! (Arm mode)";
                case "wrist":
                    return "Jaco moving forward!! (Wrist mode)";
                default:
                    return "Jaco moving forward!!";
            }
 
        }

        public string Backward(string Mode)
        {
            switch (Mode)
            {
                case "arm":
                    return "Jaco moving backward!! (Arm mode)";
                case "wrist":
                    return "Jaco moving backward!! (Wrist mode)";
                default:
                    return "Jaco moving backward!!";
            }
        }

        public string TiltUp(string Mode)
        {
            switch (Mode)
            {
                case "arm":
                    return "Jaco moving UP!! (Arm mode)";
                case "wrist":
                    return "Jaco moving UP!! (Wrist mode)";
                case "finger":
                    return "Jaco opening three fingers! (Finger mode)";
                default:
                    return "Jaco moving UP!!";
            }
        }

        public string TiltDown(string Mode)
        {
            switch (Mode)
            {
                case "arm":
                    return "Jaco moving DOWN!! (Arm mode)";
                case "wrist":
                    return "Jaco moving DOWN!! (Wrist mode)";
                case "finger":
                    return "Jaco closing three fingers! (Finger mode)";
                default:
                    return "Jaco moving DOWN!!";
            }
        }

        public string TurnLeft(string Mode)
        {
            switch (Mode)
            {
                case "arm":
                    return "Jaco turning left!! (Arm mode)";
                case "wrist":
                    return "Jaco turning left!! (Wrist mode)";
                case "finger":
                    return "Jaco opening two fingers! (Finger mode)";
                default:
                    return "Jaco turning left!!";
            }
        }
                    
        public string TurnRight(string Mode)
        {
            switch (Mode)
            {
                case "arm":
                    return "Jaco turning right!! (Arm mode)";
                case "wrist":
                    return "Jaco turning right!! (Wrist mode)";
                case "finger":
                    return "Jaco closing two fingers! (Finger mode)";
                default:
                    return "Jaco turning right!!";
            }
        }

        public string DrinkingMode(string Mode)
        {
            if (_isDrinking)
            {       
                _isDrinking = false;
                return "Exit drinking mode";
            }
            else
            {
                _isDrinking = true;
                return "In drinking mode";
            }
        }

        public string Reset(string Mode)
        {
            return "Jaco returning to HOME position.";
        }
    }
}
    