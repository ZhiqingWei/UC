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
using JacoDriver;

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
            ////Initialse the driver to control Jaco
            //Driver.InitialseControl();
        }

        /// <summary>
        /// Move Jaco arm forward in chosen speed
        /// </summary>
        /// <param name="mode">One of {"arm", "wrist", "finger"}</param>
        /// <param name="speed">One of {"low", "medium", "high"}</param>
        /// <returns></returns>
        public string Forward(string Mode, string Speed)    
        {
            
            //int result = Driver.Forward(Mode, Speed);

            switch (Mode)
            {
                case "arm":
                    return "Jaco moving FORWARD at " + Speed + " speed !! (Arm mode)";
                case "wrist":
                    return "Jaco moving FORWARD at " + Speed + " speed !! (Wrist mode)";
                default:
                    return "Jaco moving FORWARD at " + Speed + " speed !!";
            }

        }

        /// <summary>
        /// Move Jaco arm backward in chosen speed
        /// </summary>
        /// <param name="mode">One of {"arm", "wrist", "finger"}</param>
        /// <param name="speed">One of {"low", "medium", "high"}</param>
        /// <returns></returns>
        public string Backward(string Mode, string Speed)
        {
            
            //int result = Driver.Backward(Mode, Speed);

            switch (Mode)
            {
                case "arm":
                    
                    return "Jaco moving BACKWARD at " + Speed + " speed !! (Arm mode)";
                case "wrist":
                    return "Jaco moving BACKWARD at " + Speed + " speed !! (Wrist mode)";
                default:
                    return "Jaco moving BACKWARD at " + Speed + " speed !!";
            }
        }

        /// <summary>
        /// Move Jaco arm up in chosen speed
        /// </summary>
        /// <param name="mode">One of {"arm", "wrist", "finger"}</param>
        /// <param name="speed">One of {"low", "medium", "high"}</param>
        /// <returns></returns>
        public string TiltUp(string Mode, string Speed)
        {
            //int result = Driver.TiltUp(Mode, Speed);

            switch (Mode)
            {
                case "arm":
                    return "Jaco moving UP at " + Speed + " speed!! (Arm mode)";
                case "wrist":
                    return "Jaco moving UP at " + Speed + " speed!! (Wrist mode)";
                case "finger":
                    return "Jaco opening three fingers !! (Finger mode)";
                default:
                    return "Jaco moving UP at " + Speed + " speed !! ";
            }
        }

        /// <summary>
        /// Move Jaco arm down in chosen speed
        /// </summary>
        /// <param name="mode">One of {"arm", "wrist", "finger"}</param>
        /// <param name="speed">One of {"low", "medium", "high"}</param>
        /// <returns></returns>
        public string TiltDown(string Mode, string Speed)
        {
            //int result = Driver.TiltDown(Mode, Speed);
            
            switch (Mode)
            {
                case "arm":
                    return "Jaco moving DOWN at " + Speed + " speed !! (Arm mode)";
                case "wrist":
                    return "Jaco moving DOWN at " + Speed + " speed !! (Wrist mode)";
                case "finger":
                    return "Jaco closing three fingers !! (Finger mode)";
                default:
                    return "Jaco moving DOWN at  " + Speed + " speed!! ";
            }
        }

        /// <summary>
        /// Rotate Jaco arm left in chosen speed
        /// </summary>
        /// <param name="mode">One of {"arm", "wrist", "finger"}</param>
        /// <param name="speed">One of {"low", "medium", "high"}</param>
        /// <returns></returns>
        public string TurnLeft(string Mode, string Speed)
        {
            //int result = Driver.TurnLeft(Mode, Speed);
            
            switch (Mode)
            {
                case "arm":
                    return "Jaco turning LEFT at " + Speed + " speed !! (Arm mode)";
                case "wrist":
                    return "Jaco turning LEFT at " + Speed + " speed !! (Wrist mode)";
                case "finger":
                    return "Jaco opening two fingers !! (Finger mode)";
                default:
                    return "Jaco turning LEFT at " + Speed + " speed !!";
            }
        }

        /// <summary>
        /// Rotate Jaco arm right in chosen speed
        /// </summary>
        /// <param name="mode">One of {"arm", "wrist", "finger"}</param>
        /// <param name="speed">One of {"low", "medium", "high"}</param>
        /// <returns></returns>
        public string TurnRight(string Mode, string Speed)
        {
            //int result = Driver.TurnRight(Mode, Speed);

            switch (Mode)
            {
                case "arm":
                    return "Jaco turning RIGHT at " + Speed + " speed !! (Arm mode)";
                case "wrist":
                    return "Jaco turning RIGHT at " + Speed + " speed !! (Wrist mode)";
                case "finger":
                    return "Jaco closing two fingers !! (Finger mode)";
                default:
                    return "Jaco turning RIGHT at " + Speed + " speed !!";
            }
        }

        /// <summary>
        /// Enter or leave the Drinking Mode
        /// </summary>
        public string DrinkingMode(string Mode, string Pre)
        {
            //int result = Driver.ToggleDrinkingMode();

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

        /// <summary>
        /// Enter HOME position
        /// </summary>
        public string Reset(string Mode, string Pre)
        {
            //int result = Driver.ResetHOME();
            return "Jaco returning to HOME position.";
        }
    }
}
    