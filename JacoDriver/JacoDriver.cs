using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using static JacoDriver.Wrapper;

namespace JacoDriver
{
    public partial class Driver
    {
        #region *** C O D I N G ***

        /// <summary>
        /// Set the new controlMapping
        /// </summary>
        public static int SetControlMapping()
        {
            ControlMappingCharts controlMappingCharts = new ControlMappingCharts();
            GetControlMapping(ref controlMappingCharts);

            int actualMapping = controlMappingCharts.ActualControlMapping;
            int actualModeA = controlMappingCharts.Mapping[controlMappingCharts.ActualControlMapping].ActualModeA;
            int actualModeB = controlMappingCharts.Mapping[controlMappingCharts.ActualControlMapping].ActualModeB;

            if (actualModeB >= 0)
            {
                controlMappingCharts.Mapping[actualMapping].ActualModeB = -1;
                controlMappingCharts.Mapping[actualMapping].ActualModeA = actualModeB;

                controlMappingCharts.Mapping[actualMapping]
                                    .ModeControlsA[actualModeB]
                                    .ControlButtons[0]
                                    .OneClick = (byte)ControlFunctionalityTypeEnum.CF_Change_DrinkingMode;
                controlMappingCharts.Mapping[actualMapping]
                                    .ModeControlsA[actualModeB]
                                    .ControlButtons[1]
                                    .OneClick = (byte)ControlFunctionalityTypeEnum.CF_Retract_ReadyToUse;
                controlMappingCharts.Mapping[actualMapping]
                                    .ModeControlsA[actualModeB]
                                    .ControlButtons[2]
                                    .OneClick = (byte)ControlFunctionalityTypeEnum.CF_OpenHandTwoFingers;
                controlMappingCharts.Mapping[actualMapping]
                                    .ModeControlsA[actualModeB]
                                    .ControlButtons[3]
                                    .OneClick = (byte)ControlFunctionalityTypeEnum.CF_OpenHandThreeFingers;
                controlMappingCharts.Mapping[actualMapping]
                                    .ModeControlsA[actualModeB]
                                    .ControlButtons[4]
                                    .OneClick = (byte)ControlFunctionalityTypeEnum.CF_CloseHandTwoFingers;
                controlMappingCharts.Mapping[actualMapping]
                                    .ModeControlsA[actualModeB]
                                    .ControlButtons[5]
                                    .OneClick = (byte)ControlFunctionalityTypeEnum.CF_CloseHandThreeFingers;
            }
            else if (actualModeA >= 0)
            {
                controlMappingCharts.Mapping[actualMapping]
                                    .ModeControlsA[actualModeA]
                                    .ControlButtons[0]
                                    .OneClick = (byte)ControlFunctionalityTypeEnum.CF_Change_DrinkingMode;
                controlMappingCharts.Mapping[actualMapping]
                                    .ModeControlsA[actualModeA]
                                    .ControlButtons[1]
                                    .OneClick = (byte)ControlFunctionalityTypeEnum.CF_Retract_ReadyToUse;
                controlMappingCharts.Mapping[actualMapping]
                                    .ModeControlsA[actualModeA]
                                    .ControlButtons[2]
                                    .OneClick = (byte)ControlFunctionalityTypeEnum.CF_OpenHandTwoFingers;
                controlMappingCharts.Mapping[actualMapping]
                                    .ModeControlsA[actualModeA]
                                    .ControlButtons[3]
                                    .OneClick = (byte)ControlFunctionalityTypeEnum.CF_OpenHandThreeFingers;
                controlMappingCharts.Mapping[actualMapping]
                                    .ModeControlsA[actualModeA]
                                    .ControlButtons[4]
                                    .OneClick = (byte)ControlFunctionalityTypeEnum.CF_CloseHandTwoFingers;
                controlMappingCharts.Mapping[actualMapping]
                                    .ModeControlsA[actualModeA]
                                    .ControlButtons[5]
                                    .OneClick = (byte)ControlFunctionalityTypeEnum.CF_CloseHandThreeFingers;
            }
            else
            {
                Console.WriteLine("No mode list found. Error");
                return 0;
            }

            return SetControlMapping(controlMappingCharts);
        }

        public static IntPtr ControlMappingCharts_helper()
        {
            StickEvents[] stickEvents = new StickEvents[STICK_EVENT_COUNT];
            ButtonEvents[] buttonEvents = new ButtonEvents[BUTTON_EVENT_COUNT];
            int stick_size = Marshal.SizeOf(typeof(StickEvents));
            int button_size = Marshal.SizeOf(typeof(ButtonEvents));
            IntPtr stickPtr = Marshal.AllocHGlobal(stick_size * STICK_EVENT_COUNT);
            IntPtr buttonPtr = Marshal.AllocHGlobal(button_size * BUTTON_EVENT_COUNT);

            IntPtr mapPtr = Marshal.AllocHGlobal(2 * sizeof(int));
            IntPtr.Add(mapPtr, Marshal.SizeOf(stickPtr));
            IntPtr.Add(mapPtr, Marshal.SizeOf(buttonPtr));

            return mapPtr;
        }

        #region ARM_MODE

        private static int Arm_Forward(TrajectoryPoint pointToSend, string speed)
        {
            CartesianPosition currentCommand = new CartesianPosition { };
            if (GetCartesianCommand(ref currentCommand) == NO_ERROR_KINOVA)
            {
                pointToSend.Position.CartesianPosition.X = currentCommand.Coordinates.X;
                pointToSend.Position.CartesianPosition.Y = currentCommand.Coordinates.Y;
                pointToSend.Position.CartesianPosition.ThetaX = currentCommand.Coordinates.ThetaX;
                pointToSend.Position.CartesianPosition.ThetaY = currentCommand.Coordinates.ThetaY;
                pointToSend.Position.CartesianPosition.ThetaZ = currentCommand.Coordinates.ThetaZ;

                switch (speed)
                {
                    case "low":
                        pointToSend.Position.CartesianPosition.Z = currentCommand.Coordinates.Z + 0.1f; //Z change positively
                        break;
                    case "medium":
                        pointToSend.Position.CartesianPosition.Z = currentCommand.Coordinates.Z + 0.3f; //Z change positively
                        break;
                    case "high":
                        pointToSend.Position.CartesianPosition.Z = currentCommand.Coordinates.Z + 0.9f; //Z change positively
                        break;
                }

                return SendBasicTrajectory(pointToSend);
            }
            else
            {
                return 0;
            }
        }

        private static int Arm_Backward(TrajectoryPoint pointToSend, string speed)
        {
            CartesianPosition currentCommand = new CartesianPosition { };
            if (GetCartesianCommand(ref currentCommand) == NO_ERROR_KINOVA)
            {
                pointToSend.Position.CartesianPosition.X = currentCommand.Coordinates.X;
                pointToSend.Position.CartesianPosition.Y = currentCommand.Coordinates.Y;
                pointToSend.Position.CartesianPosition.ThetaX = currentCommand.Coordinates.ThetaX;
                pointToSend.Position.CartesianPosition.ThetaY = currentCommand.Coordinates.ThetaY;
                pointToSend.Position.CartesianPosition.ThetaZ = currentCommand.Coordinates.ThetaZ;

                switch (speed)
                {
                    case "low":
                        pointToSend.Position.CartesianPosition.Z = currentCommand.Coordinates.Z - 0.1f; //Z change negatively
                        break;
                    case "medium":
                        pointToSend.Position.CartesianPosition.Z = currentCommand.Coordinates.Z - 0.3f; //Z change negatively
                        break;
                    case "high":
                        pointToSend.Position.CartesianPosition.Z = currentCommand.Coordinates.Z - 0.9f; //Z change negatively
                        break;
                }

                return SendBasicTrajectory(pointToSend);
            }
            else
            {
                return 0;
            }
        }

        private static int Arm_Left(TrajectoryPoint pointToSend, string speed)
        {
            CartesianPosition currentCommand = new CartesianPosition { };
            if (GetCartesianCommand(ref currentCommand) == NO_ERROR_KINOVA)
            {
                pointToSend.Position.CartesianPosition.Y = currentCommand.Coordinates.Y;
                pointToSend.Position.CartesianPosition.Z = currentCommand.Coordinates.Z;
                pointToSend.Position.CartesianPosition.ThetaX = currentCommand.Coordinates.ThetaX;
                pointToSend.Position.CartesianPosition.ThetaY = currentCommand.Coordinates.ThetaY;
                pointToSend.Position.CartesianPosition.ThetaZ = currentCommand.Coordinates.ThetaZ;

                switch (speed)
                {
                    case "low":
                        pointToSend.Position.CartesianPosition.X = currentCommand.Coordinates.X - 0.1f; //X change negatively
                        break;
                    case "medium":
                        pointToSend.Position.CartesianPosition.X = currentCommand.Coordinates.X - 0.3f; //X change negatively
                        break;
                    case "high":
                        pointToSend.Position.CartesianPosition.X = currentCommand.Coordinates.X - 0.9f; //X change negatively
                        break;
                }

                return SendBasicTrajectory(pointToSend);
            }
            else
            {
                return 0;
            }
        }

        private static int Arm_Right(TrajectoryPoint pointToSend, string speed)
        {
            CartesianPosition currentCommand = new CartesianPosition { };
            if (GetCartesianCommand(ref currentCommand) == NO_ERROR_KINOVA)
            {
                pointToSend.Position.CartesianPosition.Y = currentCommand.Coordinates.Y;
                pointToSend.Position.CartesianPosition.Z = currentCommand.Coordinates.Z;
                pointToSend.Position.CartesianPosition.ThetaX = currentCommand.Coordinates.ThetaX;
                pointToSend.Position.CartesianPosition.ThetaY = currentCommand.Coordinates.ThetaY;
                pointToSend.Position.CartesianPosition.ThetaZ = currentCommand.Coordinates.ThetaZ;

                switch (speed)
                {
                    case "low":
                        pointToSend.Position.CartesianPosition.X = currentCommand.Coordinates.X + 0.1f; //X change positively
                        break;
                    case "medium":
                        pointToSend.Position.CartesianPosition.X = currentCommand.Coordinates.X + 0.3f; //X change positively
                        break;
                    case "high":
                        pointToSend.Position.CartesianPosition.X = currentCommand.Coordinates.X + 0.9f; //X change positively
                        break;
                }

                return SendBasicTrajectory(pointToSend);
            }
            else
            {
                return 0;
            }
        }

        private static int Arm_Up(TrajectoryPoint pointToSend, string speed)
        {
            CartesianPosition currentCommand = new CartesianPosition { };
            if (GetCartesianCommand(ref currentCommand) == NO_ERROR_KINOVA)
            {
                pointToSend.Position.CartesianPosition.X = currentCommand.Coordinates.X;
                pointToSend.Position.CartesianPosition.Z = currentCommand.Coordinates.Z;
                pointToSend.Position.CartesianPosition.ThetaX = currentCommand.Coordinates.ThetaX;
                pointToSend.Position.CartesianPosition.ThetaY = currentCommand.Coordinates.ThetaY;
                pointToSend.Position.CartesianPosition.ThetaZ = currentCommand.Coordinates.ThetaZ;

                switch (speed)
                {
                    case "low":
                        pointToSend.Position.CartesianPosition.Y = currentCommand.Coordinates.Y + 0.1f; //Y change positively
                        break;
                    case "medium":
                        pointToSend.Position.CartesianPosition.Y = currentCommand.Coordinates.Y + 0.3f; //Y change positively
                        break;
                    case "high":
                        pointToSend.Position.CartesianPosition.Y = currentCommand.Coordinates.Y + 0.9f; //Y change positively
                        break;
                }

                return SendBasicTrajectory(pointToSend);
            }
            else
            {
                return 0;
            }
        }

        private static int Arm_Down(TrajectoryPoint pointToSend, string speed)
        {
            CartesianPosition currentCommand = new CartesianPosition { };
            if (GetCartesianCommand(ref currentCommand) == NO_ERROR_KINOVA)
            {
                pointToSend.Position.CartesianPosition.X = currentCommand.Coordinates.X;
                pointToSend.Position.CartesianPosition.Z = currentCommand.Coordinates.Z;
                pointToSend.Position.CartesianPosition.ThetaX = currentCommand.Coordinates.ThetaX;
                pointToSend.Position.CartesianPosition.ThetaY = currentCommand.Coordinates.ThetaY;
                pointToSend.Position.CartesianPosition.ThetaZ = currentCommand.Coordinates.ThetaZ;

                switch (speed)
                {
                    case "low":
                        pointToSend.Position.CartesianPosition.Y = currentCommand.Coordinates.Y - 0.1f; //Y change negatively
                        break;
                    case "medium":
                        pointToSend.Position.CartesianPosition.Y = currentCommand.Coordinates.Y - 0.3f; //Y change negatively
                        break;
                    case "high":
                        pointToSend.Position.CartesianPosition.Y = currentCommand.Coordinates.Y - 0.9f; //Y change negatively
                        break;
                }

                return SendBasicTrajectory(pointToSend);
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region WRIST_MODE

        private static int Wrist_Z(TrajectoryPoint pointToSend, string speed, bool isPositive)
        {
            //We specify that this point will be used an angular(joint by joint) velocity vector.
            pointToSend.Position.Type = POSITION_TYPE.CARTESIAN_VELOCITY;
            int sign = -1;
            if (isPositive) { sign = 1; }

            switch (speed)
            {
                case "low":
                    pointToSend.Position.CartesianPosition.ThetaZ = sign * 0.15f; //Rotate along Z axis at 20 cm per second
                    return SendBasicTrajectory(pointToSend);
                case "medium":
                    pointToSend.Position.CartesianPosition.ThetaZ = sign * 0.30f; //Rotate along Z axis at 40 cm per second
                    return SendBasicTrajectory(pointToSend);
                case "high":
                    pointToSend.Position.CartesianPosition.ThetaZ = sign * 0.45f; //Rotate along Z axis at 60 cm per second
                    return SendBasicTrajectory(pointToSend);
                default:
                    return 0;
            }
        }

        private static int Wrist_Y(TrajectoryPoint pointToSend, string speed, bool isPositive)
        {
            //We specify that this point will be used an angular(joint by joint) velocity vector.
            pointToSend.Position.Type = POSITION_TYPE.CARTESIAN_VELOCITY;
            int sign = -1;
            if (isPositive) { sign = 1; }

            switch (speed)
            {
                case "low":
                    pointToSend.Position.CartesianPosition.ThetaY = sign * 0.15f; //Rotate along Y axis at 20 cm per second
                    return SendBasicTrajectory(pointToSend);
                case "medium":
                    pointToSend.Position.CartesianPosition.ThetaY = sign * 0.30f; //Rotate along Y axis at 40 cm per second
                    return SendBasicTrajectory(pointToSend);
                case "high":
                    pointToSend.Position.CartesianPosition.ThetaY = sign * 0.45f; //Rotate along Y axis at 60 cm per second
                    return SendBasicTrajectory(pointToSend);
                default:
                    return 0;
            }
        }

        private static int Wrist_X(TrajectoryPoint pointToSend, string speed, bool isPositive)
        {
            //We specify that this point will be used an angular(joint by joint) velocity vector.
            pointToSend.Position.Type = POSITION_TYPE.CARTESIAN_VELOCITY;
            int sign = -1;
            if (isPositive) { sign = 1; }

            switch (speed)
            {
                case "low":
                    pointToSend.Position.CartesianPosition.ThetaX = sign * 0.15f; //Rotate along X axis at 20 cm per second
                    return SendBasicTrajectory(pointToSend);
                case "medium":
                    pointToSend.Position.CartesianPosition.ThetaX = sign * 0.30f; //Rotate along X axis at 40 cm per second
                    return SendBasicTrajectory(pointToSend);
                case "high":
                    pointToSend.Position.CartesianPosition.ThetaX = sign * 0.45f; //Rotate along X axis at 60 cm per second
                    return SendBasicTrajectory(pointToSend);
                default:
                    return 0;
            }
        }

        #endregion

        #region FINGER_MODE
        private static int Two_Fingers(bool ifOpen)
        {
            JoystickCommand fingerCommand = new JoystickCommand { };
            fingerCommand.InitStruct();

            if (ifOpen)
            {
                fingerCommand.ButtonValue[2] = 1; //Open two fingers
            }
            else
            {
                fingerCommand.ButtonValue[4] = 1; //Close two fingers
            }

            return SendJoystickCommand(fingerCommand);
        }

        private static int Three_Fingers(bool ifOpen)
        {
            JoystickCommand fingerCommand = new JoystickCommand { };
            fingerCommand.InitStruct();

            if (ifOpen)
            {
                fingerCommand.ButtonValue[3] = 1; //Open three fingers
            }
            else
            {
                fingerCommand.ButtonValue[5] = 1; //Close three fingers
            }

            return SendJoystickCommand(fingerCommand);
        }
        #endregion

        #region *** Main_Functions ***
        static KinovaDevice[] devices = new KinovaDevice[MAX_KINOVA_DEVICE];

        /// <summary>
        /// Initialse API to control,
        /// get the Jaco robotic arm connected to computer and active it, 
        /// set the unique control mapping for the functions and
        /// start the control using API
        /// </summary>
        /// <returns></returns>
        public static int InitialseControl()
        {
            //Initialise the API to control Jaco robotic arm
            int result = InitAPI();

            KinovaDevice[] list = new KinovaDevice[MAX_KINOVA_DEVICE];
            int size = Marshal.SizeOf(typeof(KinovaDevice));
            IntPtr mem = Marshal.AllocHGlobal(size * MAX_KINOVA_DEVICE);

            if (result == NO_ERROR_KINOVA)
            {
                //Get connected Jaco robotic arm
                int devicecount = GetDevices(mem, ref result);
                Console.WriteLine("result of GetDevices = {0}, {1}", result, devicecount);
                try
                {
                    KinovaDevice jacodevice = Marshal.PtrToStructure<KinovaDevice>(mem);

                    if (result == NO_ERROR_KINOVA)
                    {
                        SetActiveDevice(jacodevice); //Active Jaco arm for control
                        MoveHome();
                        InitFingers();
                        SetControlMapping();
                        result = StartControlAPI();
                    }

                    return result;
                }
#pragma warning disable CS0168 // The variable 'e' is declared but never used
                catch (Exception e) { }
#pragma warning restore CS0168 // The variable 'e' is declared but never used
                finally
                {
                    Marshal.FreeHGlobal(mem);
                }
            }

            return result;
        }

        /// <summary>
        /// Close API
        /// </summary>
        /// <returns></returns>
        public static int CloseControl()
        {
            return CloseAPI();
        }

        /// <summary>
        /// Try to active and deactive drinking mode, MUST be called after <see cref="InitialseControl"/>
        /// </summary>
        public static int ToggleDrinkingMode()
        {
            JoystickCommand drinkingCommand = new JoystickCommand { };
            drinkingCommand.InitStruct();

            drinkingCommand.ButtonValue[0] = 1; //Active drinking mode button

            return SendJoystickCommand(drinkingCommand);
        }

        /// <summary>
        /// Try to reset to HOME position, MUST be called after <see cref="InitialseControl"/>
        /// </summary>
        public static int ResetHOME()
        {
            JoystickCommand resetCommand = new JoystickCommand { };
            resetCommand.InitStruct();

            resetCommand.ButtonValue[1] = 1; //Active HOME button

            return SendJoystickCommand(resetCommand);
        }

        /// <summary>
        /// Move Jaco arm forward in chosen speed
        /// </summary>
        /// <param name="mode">One of {"arm", "wrist", "finger"}</param>
        /// <param name="speed">One of {"low", "medium", "high"}</param>
        /// <returns>Status code</returns>
        public static int Forward(string mode, string speed)
        {
            TrajectoryPoint pointToSend = new TrajectoryPoint();
            pointToSend.InitStruct();
            
            switch (mode)
            {
                case "arm":
                    return Arm_Forward(pointToSend, speed);
                case "wrist":
                    return Wrist_Z(pointToSend, speed, true);
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Move Jaco arm backward in chosen speed
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static int Backward(string mode, string speed)
        {
            TrajectoryPoint pointToSend = new TrajectoryPoint();
            pointToSend.InitStruct();

            switch (mode)
            {
                case "arm":
                    return Arm_Backward(pointToSend, speed);
                case "wrist":
                    return Wrist_Z(pointToSend, speed, false);
                default:
                    return 0;
            }
        }

        public static int TiltUp(string mode, string speed)
        {
            TrajectoryPoint pointToSend = new TrajectoryPoint();
            pointToSend.InitStruct();

            switch (mode)
            {
                case "arm":
                    return Arm_Up(pointToSend, speed);
                case "wrist":
                    return Wrist_Y(pointToSend, speed, true);
                case "finger":
                    return Three_Fingers(true);
                default:
                    return 0;
            }
        }

        public static int TiltDown(string mode, string speed)
        {
            TrajectoryPoint pointToSend = new TrajectoryPoint();
            pointToSend.InitStruct();

            switch (mode)
            {
                case "arm":
                    return Arm_Down(pointToSend, speed);
                case "wrist":
                    return Wrist_Y(pointToSend, speed, false);
                case "finger":
                    return Three_Fingers(false);
                default:
                    return 0;
            }
        }

        public static int TurnLeft(string mode, string speed)
        {
            TrajectoryPoint pointToSend = new TrajectoryPoint();
            pointToSend.InitStruct();

            switch (mode)
            {
                case "arm":
                    return Arm_Left(pointToSend, speed);
                case "wrist":
                    return Wrist_X(pointToSend, speed, false);
                case "finger":
                    return Two_Fingers(true);
                default:
                    return 0;
            }
        }

        public static int TurnRight(string mode, string speed)
        {
            TrajectoryPoint pointToSend = new TrajectoryPoint();
            pointToSend.InitStruct();

            switch (mode)
            {
                case "arm":
                    return Arm_Right(pointToSend, speed);
                case "wrist":
                    return Wrist_X(pointToSend, speed, true);
                case "finger":
                    return Two_Fingers(false);
                default:
                    return 0;
            }
        }
        #endregion

        #endregion
    }
}