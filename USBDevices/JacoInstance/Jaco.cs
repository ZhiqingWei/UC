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
        private Driver driver;

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
            //Initialse the driver to control Jaco
            driver = new Driver();
            driver.InitialseControl();
        }

        /// <summary>
        /// Move Jaco arm forward in chosen speed
        /// </summary>
        /// <param name="mode">One of {"arm", "wrist", "finger"}</param>
        /// <param name="speed">One of {"low", "medium", "high"}</param>
        /// <returns></returns>
        public string Forward(string Mode, string Speed)    
        {
            
            int result = driver.Forward(Mode, Speed);

            switch (Mode)
            {
                case "arm":
                    return "Jaco moving FORWARD at " + Speed + " speed !! (Arm mode)" + Environment.NewLine + "Status code: " + result;
                case "wrist":
                    return "Jaco moving FORWARD at " + Speed + " speed !! (Wrist mode)" + Environment.NewLine + "Status code: " + result;
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

            int result = driver.Backward(Mode, Speed);

            switch (Mode)
            {
                case "arm":
                    
                    return "Jaco moving BACKWARD at " + Speed + " speed !! (Arm mode)" + Environment.NewLine + "Status code: " + result;
                case "wrist":
                    return "Jaco moving BACKWARD at " + Speed + " speed !! (Wrist mode)" + Environment.NewLine + "Status code: " + result;
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
            int result = driver.TiltUp(Mode, Speed);

            switch (Mode)
            {
                case "arm":
                    return "Jaco moving UP at " + Speed + " speed!! (Arm mode)" + Environment.NewLine + "Status code: " + result;
                case "wrist":
                    return "Jaco moving UP at " + Speed + " speed!! (Wrist mode)" + Environment.NewLine + "Status code: " + result;
                case "finger":
                    return "Jaco opening three fingers !! (Finger mode)" + Environment.NewLine + "Status code: " + result;
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
            int result = driver.TiltDown(Mode, Speed);

            switch (Mode)
            {
                case "arm":
                    return "Jaco moving DOWN at " + Speed + " speed !! (Arm mode)" + Environment.NewLine + "Status code: " + result;
                case "wrist":
                    return "Jaco moving DOWN at " + Speed + " speed !! (Wrist mode)" + Environment.NewLine + "Status code: " + result;
                case "finger":
                    return "Jaco closing three fingers !! (Finger mode)" + Environment.NewLine + "Status code: " + result;
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
            int result = driver.TurnLeft(Mode, Speed);

            switch (Mode)
            {
                case "arm":
                    return "Jaco turning LEFT at " + Speed + " speed !! (Arm mode)" + Environment.NewLine + "Status code: " + result;
                case "wrist":
                    return "Jaco turning LEFT at " + Speed + " speed !! (Wrist mode)" + Environment.NewLine + "Status code: " + result;
                case "finger":
                    return "Jaco opening two fingers !! (Finger mode)" + Environment.NewLine + "Status code: " + result;
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
            int result = driver.TurnRight(Mode, Speed);

            switch (Mode)
            {
                case "arm":
                    return "Jaco turning RIGHT at " + Speed + " speed !! (Arm mode)" + Environment.NewLine + "Status code: " + result;
                case "wrist":
                    return "Jaco turning RIGHT at " + Speed + " speed !! (Wrist mode)" + Environment.NewLine + "Status code: " + result;
                case "finger":
                    return "Jaco closing two fingers !! (Finger mode)" + Environment.NewLine + "Status code: " + result;
                default:
                    return "Jaco turning RIGHT at " + Speed + " speed !!";
            }
        }

        /// <summary>
        /// Enter or leave the Drinking Mode
        /// </summary>
        public string DrinkingMode(string Mode, string Pre)
        {
            int result = driver.ToggleDrinkingMode();

            if (_isDrinking)
            {
                _isDrinking = false;
                return "Exit drinking mode" + Environment.NewLine + "Status code: " + result;
            }
            else
            {
                _isDrinking = true;
                return "In drinking mode" + Environment.NewLine + "Status code: " + result;
            }
        }

        /// <summary>
        /// Enter HOME position
        /// </summary>
        public string Reset(string Mode, string Pre)
        {
            int result = driver.ResetHOME();
            return "Jaco returning to HOME position." + Environment.NewLine + "Status code: " + result;
        }
    }
}
    