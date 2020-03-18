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

        public string Forward(string Mode, string Speed)
        {
            switch (Mode)
            {
                case "arm":
                    return "Jaco moving forward  at "+ Speed+  " speed !!(Arm mode)";
                case "wrist":
                    return "Jaco moving forward at " + Speed + " speed !!(Wrist mode)";
                default:
                    return "Jaco moving forward at " + Speed + " speed !!";
            }
 
        }

        public string Backward(string Mode, string Speed)
        {
            switch (Mode)
            {
                case "arm":
                    return "Jaco moving backward at " + Speed + " speed !! (Arm mode)";
                case "wrist":
                    return "Jaco moving backward at " + Speed + " speed !! (Wrist mode)";
                default:
                    return "Jaco moving backward at " + Speed + " speed!!";
            }
        }

        public string TiltUp(string Mode, string Speed)
        {
            switch (Mode)
            {
                case "arm":
                    return "Jaco moving UP at " + Speed + " speed!!";
                case "wrist":
                    return "Jaco moving UP!! at  " + Speed + " speed!!  (Wrist mode)";
                case "finger":
                    return "Jaco opening three fingers! at " + Speed + " speed!! (Finger mode)";
                default:
                    return "Jaco moving UP at " + Speed + "speed!! ";
            }
        }

        public string TiltDown(string Mode, string Speed)
        {
            switch (Mode)
            {
                case "arm":
                    return "Jaco moving DOWN at" + Speed + " speed!!";
                case "wrist":
                    return "Jaco moving DOWN at " + Speed + " speed!! (Wrist mode)";
                case "finger":
                    return "Jaco closing three fingers! at " + Speed + " speed!!  (Finger mode)";
                default:
                    return "Jaco moving DOWN!! at  " + Speed + " speed!!";
            }
        }

        public string TurnLeft(string Mode, string Speed)
        {
            switch (Mode)
            {
                case "arm":
                    return "Jaco turning left at " + Speed + " speed!!";
                case "wrist":
                    return "Jaco turning left at " + Speed + " speed!! (Wrist mode)";
                case "finger":
                    return "Jaco opening two fingers at  " + Speed + " speed!! (Finger mode)";
                default:
                    return "Jaco turning left!!";
            }
        }
                    
        public string TurnRight(string Mode, string Speed)
        {
            switch (Mode)
            {
                case "arm":
                    return "Jaco turning right at " + Speed + " speed!!";
                case "wrist":
                    return "Jaco turning right at " + Speed + " speed!! (Wrist mode)";
                case "finger":
                    return "Jaco closing two fingers! at " + Speed + " speed!!(Finger mode)";
                default:
                    return "Jaco turning right at " + Speed + " speed ";
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
    
