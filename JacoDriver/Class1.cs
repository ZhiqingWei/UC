using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace JacoDriver
{

    public class Wrapper
    {

        #region ***** E R R O R   C O D E S ****** CommunicationLayer
        ///<summary>
        ///No error, everything is fine.
        ///</summary>
        public const int NO_ERROR_KINOVA = 1;

        ///<summary>
        ///We know that an error has occured but we don't know where it comes from.
        ///</summary>
        public const int UNKNOWN_ERROR = 666;

        /// <summary>
        /// Unable to load the USB library.
        /// </summary>
        public const int ERROR_LOAD_USB_LIBRARY = 1001;

        ///<summary>
        ///Unable to access the Open method from the USB library.
        ///</summary>
        public const int ERROR_OPEN_METHOD = 1002;

        /// <summary>
        /// Unable to access the Write method from the USB library.
        /// </summary>
        public const int ERROR_WRITE_METHOD = 1003;

        /// <summary>
        /// Unable to access the Read method from the USB library.
        /// </summary>
        public const int ERROR_READ_METHOD = 1004;

        /// <summary>
        /// Unable to access the Read Int method from the USB library.
        /// </summary>
        public const int ERROR_READ_INT_METHOD = 1005;

        //Unable to access the Free Library method from the USB library.
        public const int ERROR_FREE_LIBRARY = 1006;

        //There is a problem with the USB connection between the device and the computer.
        public const int ERROR_JACO_CONNECTION = 1007;

        //Unable to claim the USB interface.
        public const int ERROR_CLAIM_INTERFACE = 1008;

        //Unknown type of device.
        public const int ERROR_UNKNOWN_DEVICE = 1009;

        //The functionality you are trying to use has not been initialized.
        public const int ERROR_NOT_INITIALIZED = 1010;

        //The USB library cannot find the device.
        public const int ERROR_LIBUSB_NO_DEVICE = 1011;

        //The USB Library is bussy and could not perform the action.
        public const int ERROR_LIBUSB_BUSY = 1012;

        //The functionality you are trying to perform is not supported by the version installed.
        public const int ERROR_LIBUSB_NOT_SUPPORTED = 1013;

        //Unknown error while sending a packet.
        public const int ERROR_SENDPACKET_UNKNOWN = 1014;

        //Cannot find the requested device.
        public const int ERROR_NO_DEVICE_FOUND = 1015;

        //The operation was not entirely completed :)
        public const int ERROR_OPERATION_INCOMPLETED = 1016;

        //Handle used is not valid.
        public const int ERROR_RS485_INVALID_HANDLE = 1017;

        //An overlapped I/O operation is in progress but has not completed.
        public const int ERROR_RS485_IO_PENDING = 1018;

        //Not enough memory to complete the opreation.
        public const int ERROR_RS485_NOT_ENOUGH_MEMORY = 1019;

        //The operation has timed out.
        public const int ERROR_RS485_TIMEOUT = 1020;

        //You are trying to call a USB function but the OpenRS485_Activate has been called. Functions are no longer available
        public const int ERROR_FUNCTION_NOT_ACCESSIBLE = 1021;

        //No response timeout reached 
        public const int ERROR_COMM_TIMEOUT = 1022;

        //If the robot answered a NACK to our command
        public const int ERROR_NACK_RECEIVED = 9999;
        #endregion

        #region ***** E R R O R   C O D E S ****** CommandLayer
        public const int ERROR_INIT_API = 2001;      // Error while initializing the API
        public const int ERROR_LOAD_COMM_DLL = 2002; // Error while loading the communication layer

        //Those 3 codes are mostly for internal use
        public const int JACO_NACK_FIRST = 2003;
        public const int JACO_COMM_FAILED = 2004;
        public const int JACO_NACK_NORMAL = 2005;

        //Unable to initialize the communication layer.
        public const int ERROR_INIT_COMM_METHOD = 2006;

        //Unable to load the Close() function from the communication layer.
        public const int ERROR_CLOSE_METHOD = 2007;

        //Unable to load the GetDeviceCount() function from the communication layer.
        public const int ERROR_GET_DEVICE_COUNT_METHOD = 2008;

        //Unable to load the SendPacket() function from the communication layer.
        public const int ERROR_SEND_PACKET_METHOD = 2009;

        //Unable to load the SetActiveDevice() function from the communication layer.
        public const int ERROR_SET_ACTIVE_DEVICE_METHOD = 2010;

        //Unable to load the GetDeviceList() function from the communication layer.
        public const int ERROR_GET_DEVICES_LIST_METHOD = 2011;

        //Unable to initialized the system semaphore.
        public const int ERROR_SEMAPHORE_FAILED = 2012;

        //Unable to load the ScanForNewDevice() function from the communication layer.
        public const int ERROR_SCAN_FOR_NEW_DEVICE = 2013;

        //Unable to load the GetActiveDevice function from the communication layer.
        public const int ERROR_GET_ACTIVE_DEVICE_METHOD = 2014;

        //Unable to load the OpenRS485_Activate() function from the communication layer.
        public const int ERROR_OPEN_RS485_ACTIVATE = 2015;

        //A function's parameter is not valid.
        public const int ERROR_INVALID_PARAM = 2100;

        //The API is not initialized.
        public const int ERROR_API_NOT_INITIALIZED = 2101;

        //Unable to load the InitDataStructure() function from the communication layer.
        public const int ERROR_INIT_DATA_STRUCTURES_METHOD = 2102;
        #endregion

        #region ***** C O N S T A N T S *****
        public const int SERIAL_LENGTH = 20;
        public const int MAX_KINOVA_DEVICE = 20;

        /// <summary>
        /// Size of the <see cref="ControlsModeMap"/> array in the structure <see cref="JoystickCommand"/>.
        /// </summary>
        public const int JOYSTICK_BUTTON_COUNT = 16;

        /// <summary>
        /// This is the size of the array ControlSticks contained in the structure <see cref="ControlsModeMap"/>.
        /// </summary>
        public const int STICK_EVENT_COUNT = 6;

        /// <summary>
        /// This is the size of the array ControlButtons contained in the structure <see cref="ControlsModeMap"/>.
        /// </summary>
        public const int BUTTON_EVENT_COUNT = 26;

        /// <summary>
        /// This is the size of the arrays ModeControlsA and ModeControlsB contained in the structure <see cref="ControlMapping"/>.
        /// </summary>
        public const int MODE_MAP_COUNT = 6;

        /// <summary>
        /// This is the size of the array Mapping contained in the structure <see cref="ControlMappingCharts"/>.
        /// </summary>
        public const int CONTROL_MAPPING_COUNT = 6;
        #endregion

        #region ***** E N U M A R A T I O N *****
        /// <summary>
        /// That represents the type of a position.
        /// </summary>
        /// <remarks>
        /// <para>If used during a trajectory, the type of position will change the behaviour of the robot.</para>
        /// <para>For example if the position type is CARTESIAN_POSITION, 
        /// then the robot's end effector will move to that position using the inverse kinematics.</para>
        /// <para>But if the type of position is CARTESIAN_VELOCITY then the robot will use the values as velocity command.</para>
        /// </remarks>
        public enum POSITION_TYPE
        {
            NOMOVEMENT_POSITION,
            CARTESIAN_POSITION,
            ANGULAR_POSITION,
            RETRACTED,
            PREDEFINED1,
            PREDEFINED2,
            PREDEFINED3,
            CARTESIAN_VELOCITY,
            ANGULAR_VELOCITY,
            PREDEFINED4,
            PREDEFINED5,
            ANY_TRAJECTORY,
            TIME_DELAY
        }

        /// <summary>
        /// That indicates how the end effector will be used.
        /// </summary>
        public enum HAND_MODE
        {
            HAND_NOMOVEMENT,
            POSITION_MODE,
            VELOCITY_MODE
        }

        /// <summary>
        /// Indicates the type of controller.
        /// </summary>
        public enum ControlMappingMode
        {
            /// <summary>
            /// Represents a 1-axis controller.
            /// </summary>
            OneAxis,

            /// <summary>
            /// Represents a 2-axis controller.
            /// </summary>
            TwoAxis,

            /// <summary>
            /// Represents a 3-axis controller.
            /// </summary>
            ThreeAxis,

            /// <summary>
            /// Represents a 6-axis controller.
            /// </summary>
            SixAxis
        };

        /// <summary>
        /// <para>This is the list of available feature that can be mapped with a controller through the mappign system.</para>
        /// <para>Every list of mode that a mapping contains is mapped with one of these features.</para>
        /// <para>The default value is CF_NoFunctionality.</para>
        /// </summary>
        public enum ControlFunctionalityTypeEnum
        {
            /// <summary>
            /// Default value, represents nothing.
            /// </summary>
            CF_NoFunctionality = 0,

            /// <summary>
            /// Virtually turn on and off the joystick.
            /// </summary>
            CF_Disable_EnableJoystick = 1,

            /// <summary>
            /// <para>Home the robot if the is initialized and anywhere in the workspace except between the READY and RETRACTED position.</para>
            /// <para>Go to <c>RETRACTED</c> position if the robot is in <c>READY</c> position 
            /// and go to <c>READY</c> position if the robot is in <c>RETRACTED</c> position.</para>
            /// </summary>
            CF_Retract_ReadyToUse = 2,

            /// <summary>
            /// Not used for now.
            /// </summary>
            CF_Change_TwoAxis_ThreeAxis = 3,

            /// <summary>
            /// Put the robotical arm in the drinking mode.
            /// </summary>
            CF_Change_DrinkingMode = 4,

            /// <summary>
            /// Iterate mode in the list A.
            /// </summary>
            CF_Cycle_ModeA_list = 5,

            /// <summary>
            /// Iterate mode in the list B.
            /// </summary>
            CF_Cycle_ModeB_list = 6,

            /// <summary>
            /// Divide the velocity by 2.
            /// </summary>
            CF_DecreaseSpeed = 7,

            /// <summary>
            /// Double the speed.
            /// </summary>
            CF_IncreaseSpeed = 8,

            /// <summary>
            /// Move the robotical arm's end position to the <c>GOTO</c> position 1.
            /// </summary>
            CF_Goto_Position1 = 9,

            /// <summary>
            /// Move the robotical arm's end position to the <c>GOTO</c> position 2.
            /// </summary>
            CF_Goto_Position2 = 10,

            /// <summary>
            /// Move the robotical arm's end position to the <c>GOTO</c> position 3.
            /// </summary>
            CF_Goto_Position3 = 11,

            /// <summary>
            /// Move the robotical arm's end position to the <c>GOTO</c> position 4.
            /// </summary>
            CF_Goto_Position4 = 12,

            /// <summary>
            /// Move the robotical arm's end position to the <c>GOTO</c> position 5.
            /// </summary>
            CF_Goto_Position5 = 13,

            /// <summary>
            /// Store the current cartesian position into the <c>GOTO</c> position 1.
            /// </summary>
            CF_RecordPosition1 = 14,

            /// <summary>
            /// Store the current cartesian position into the <c>GOTO</c> position 2.
            /// </summary>
            CF_RecordPosition2 = 15,

            /// <summary>
            /// Store the current cartesian position into the <c>GOTO</c> position 3.
            /// </summary>
            CF_RecordPosition3 = 16,

            /// <summary>
            /// Store the current cartesian position into the <c>GOTO</c> position 4.
            /// </summary>
            CF_RecordPosition4 = 17,

            /// <summary>
            /// Store the current cartesian position into the <c>GOTO</c> position 5.
            /// </summary>
            CF_RecordPosition5 = 18,

            /// <summary>
            /// Move the robotical arm's end effector along the X axis toward the positive values.
            /// <para>If the robotical arm is in angular control, this will move the actuator 1 counterclockwise.</para>
            /// </summary>
            CF_X_Positive = 19,

            /// <summary>
            /// Move the robotical arm's end effector along the X axis toward the negative values.
            /// <para>If the robotical arm is in angular control, this will move the actuator 1 clockwise.</para>
            /// </summary>
            CF_X_Negative = 20,

            /// <summary>
            /// Move the robotical arm's end effector along the Y axis toward the positive values.
            /// <para>If the robotical arm is in angular control, this will move the actuator 2 counterclockwise.</para>
            /// </summary>
            CF_Y_Positive = 21,

            /// <summary>
            /// Move the robotical arm's end effector along the Y axis toward the negative values.
            /// <para>If the robotical arm is in angular control, this will move the actuator 2 clockwise.</para>
            /// </summary>
            CF_Y_Negative = 22,

            /// <summary>
            /// Move the robotical arm's end effector along the Z axis toward the positive values.
            /// <para>If the robotical arm is in angular control, this will move the actuator 3 counterclockwise.</para>
            /// </summary>
            CF_Z_Positive = 23,

            /// <summary>
            /// Move the robotical arm's end effector along the Z axis toward the negative values.
            /// <para>If the robotical arm is in angular control, this will move the actuator 3 clockwise.</para>
            /// </summary>
            CF_Z_Negative = 24,

            /// <summary>
            /// Rotate the robotical arm's end effector around the X axis counterclockwise.
            /// <para>If the robotical arm is in angular control, this will move the actuator 4 counterclockwise.</para>
            /// </summary>
            CF_R_Positive = 25,

            /// <summary>
            /// Rotate the robotical arm's end effector around the X axis clockwise.
            /// <para>If the robotical arm is in angular control, this will move the actuator 4 clockwise.</para>
            /// </summary>
            CF_R_Negative = 26,

            /// <summary>
            /// Rotate the robotical arm's end effector around the Y axis counterclockwise.
            /// <para>If the robotical arm is in angular control, this will move the actuator 5 counterclockwise.</para>
            /// </summary>
            CF_U_Positive = 27,

            /// <summary>
            /// Rotate the robotical arm's end effector around the Y axis clockwise.
            /// <para>If the robotical arm is in angular control, this will move the actuator 5 clockwise.</para>
            /// </summary>
            CF_U_Negative = 28,

            /// <summary>
            /// Rotate the robotical arm's end effector around the Z axis counterclockwise.
            /// <para>If the robotical arm is in angular control, this will move the actuator 6 counterclockwise.</para>
            /// </summary>
            CF_V_Positive = 29,

            /// <summary>
            /// Rotate the robotical arm's end effector around the Z axis clockwise.
            /// <para>If the robotical arm is in angular control, this will move the actuator 6 clockwise.</para>
            /// </summary>
            CF_V_Negative = 30,

            /// <summary>
            /// Not used for now.
            /// </summary>
            CF_OpenHandOneFingers = 31,

            /// <summary>
            /// Not used for now.
            /// </summary>
            CF_CloseHandOneFingers = 32,

            /// <summary>
            /// Open fingers 1 and 2 of the hand.
            /// </summary>
            CF_OpenHandTwoFingers = 33,

            /// <summary>
            /// Close fingers 1 and 2 of the hand.
            /// </summary>
            CF_CloseHandTwoFingers = 34,

            /// <summary>
            /// Open fingers 1, 2 and 3 of the hand.
            /// </summary>
            CF_OpenHandThreeFingers = 35,

            /// <summary>
            /// Close fingers 1, 2 and 3 of the hand.
            /// </summary>
            CF_CloseHandThreeFingers = 36,

            /// <summary>
            /// Put the robotical arm in angular control mode.
            /// </summary>
            CF_ForceAngularVelocity = 37,

            /// <summary>
            /// Turn ON/OFF the force control if the feature is available.
            /// </summary>
            CF_ForceControlStatus = 38,

            CF_Trajectory = 39,

            /// <summary>
            /// Orient the end effector toward the positive X Axis.
            /// </summary>
            CF_AutomaticOrientationXPlus = 40,

            /// <summary>
            /// Orient the end effector toward the negative X Axis.
            /// </summary>
            CF_AutomaticOrientationXMinus = 41,

            /// <summary>
            /// Orient the end effector toward the positive Y Axis.
            /// </summary>
            CF_AutomaticOrientationYPlus = 42,

            /// <summary>
            /// Orient the end effector toward the negative Y Axis.
            /// </summary>
            CF_AutomaticOrientationYMinus = 43,

            /// <summary>
            /// Orient the end effector toward the positive Z Axis.
            /// </summary>
            CF_AutomaticOrientationZPlus = 44,

            /// <summary>
            /// Orient the end effector toward the negative Z Axis.
            /// </summary>
            CF_AutomaticOrientationZMinus = 45,

            /// <summary>
            /// Move the robot along the advance <c>GOTO</c> position 1.
            /// </summary>
            CF_AdvanceGOTO_1 = 46,

            /// <summary>
            /// Clear the advance <c>GOTO</c>'s trajectory 1.
            /// </summary>
            CF_AdvanceGOTO_Clear_1 = 47,

            /// <summary>
            /// Add a point to the advance <c>GOTO</c>'s trajectory 1.
            /// </summary>
            CF_AdvanceGOTO_Add_1 = 48,

            CF_AdvanceGOTO_2 = 49,
            CF_AdvanceGOTO_3 = 50,
            CF_AdvanceGOTO_4 = 51,
            CF_AdvanceGOTO_5 = 52,
            CF_AdvanceGOTO_Clear_2 = 53,
            CF_AdvanceGOTO_Clear_3 = 54,
            CF_AdvanceGOTO_Clear_4 = 55,
            CF_AdvanceGOTO_Clear_5 = 56,
            CF_AdvanceGOTO_add_2 = 57,
            CF_AdvanceGOTO_add_3 = 58,
            CF_AdvanceGOTO_add_4 = 59,
            CF_AdvanceGOTO_add_5 = 60,

            CF_IncreaseSpasmLevel = 61,
            CF_DecreaseSpasmLevel = 62,

            CF_CycleDown_ModeA_list = 63,
            CF_CycleDown_ModeB_list = 64,

            CF_Theta7_Positive = 65,
            CF_Theta7_Negative = 66
        }
        #endregion

        /// <summary>
        /// That is a device you can communicate with via this library.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct KinovaDevice
        {
            /// <summary>
            /// The serial number of the device. char SerialNumber[SERIAL_LENGTH];
            /// </summary>
            /// <remarks>
            /// If you are communicating with more than 1 device, this will be used to identify the devices.
            /// </remarks>
            public string SerialNumber;

            /// <summary>
            /// The model of the device. char Model[SERIAL_LENGTH];
            /// </summary>
            public string Model;

            //Those variables represents the code version - Major.Minor.Release
            public int VersionMajor;
            public int VersionMinor;
            public int VersionRelease;

            /// <summary>
            /// The type of the device.
            /// </summary>
            public int DeviceType;

            /// <summary>
            /// This is a device ID used by the API. User should not use it.
            /// </summary>
            public int DeviceID;
        }

        ///<summary>
        ///This data structure holds values in an cartesian control context.
        ///</summary>
        ///<remarks>
        ///\struct CartesianInfo KinovaTypes.h "Definition"
        ///</remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct CartesianInfo
        {
            //
            //Unit depends on the context it's been used.
            //As an example if the current control mode is cartesian position the unit will be meters 
            //but if the control mode is cartesian velocity then the unit will be meters per second.
            /// <summary>
            /// This is the value related to the <c>translation</c> along the X axis.
            /// </summary>
            public float X;
            /// <summary>
            /// This is the value related to the <c>translation</c> along the Y axis.
            /// </summary>
            public float Y;
            /// <summary>
            /// This is the value related to the <c>translation</c> along the Z axis.
            /// </summary>
            public float Z;

            //
            //Unit depends on the context it's been used.
            //As an example if the current control mode is cartesian position the unit will be RAD 
            //but if the control mode is cartesian velocity then the unit will be RAD per second.
            /// <summary>
            /// This is the value related to the <c>orientation</c> around the X axis.
            /// </summary>
            public float ThetaX;
            /// <summary>
            /// This is the value related to the <c>orientation</c> around the Y axis.
            /// </summary>
            public float ThetaY;
            /// <summary>
            /// This is the value related to the <c>orientation</c> around the Z axis.
            /// </summary>
            public float ThetaZ;

            /// <summary>
            /// This method will initialises all the values to 0
            /// </summary>
            public void InitStruct()
            {
                X = 0.0f;
                Y = 0.0f;
                Z = 0.0f;
                ThetaX = 0.0f;
                ThetaY = 0.0f;
                ThetaZ = 0.0f;
            }
        }

        /// <summary>
        /// This data structure holds values in an angular(joint by joint) control context. 
        /// As an example struct could contains position, temperature, torque, ...
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct AngularInfo
        {
            public float Actuator1;
            public float Actuator2;
            public float Actuator3;
            public float Actuator4;
            public float Actuator5;
            public float Actuator6;
            public float Actuator7;

            /// <summary>
            /// This method will initialises all the values to 0
            /// </summary>
            public void InitStruct()
            {
                Actuator1 = 0.0f;
                Actuator2 = 0.0f;
                Actuator3 = 0.0f;
                Actuator4 = 0.0f;
                Actuator5 = 0.0f;
                Actuator6 = 0.0f;
                Actuator7 = 0.0f;
            }
        }

        ///<summary>
        ///This data structure holds the values of the robot's fingers.
        ///</summary>
        ///<remarks>
        ///\struct FingersPosition KinovaTypes.h "Definition"
        ///</remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct FingersPosition
        {
            /// <summary>
            /// This is the value of the finger #1. The units will depends on the context it's been used.
            /// </summary>
            public float Finger1;

            /// <summary>
            /// This is the value of the finger #2. The units will depends on the context it's been used.
            /// </summary>
            public float Finger2;

            /// <summary>
            /// This is the value of the finger #3. The units will depends on the context it's been used.
            /// </summary>
            public float Finger3;

            /// <summary>
            /// This method will initialises all the values to 0
            /// </summary>
            public void InitStruct()
            {
                Finger1 = 0.0f;
                Finger2 = 0.0f;
                Finger3 = 0.0f;
            }
        }

        /// <summary>
        /// <para>This data structure represents an abstract position built by a user.</para>
        /// <para>Depending on the control type the Cartesian information, the angular information or both will be used.</para>
        /// </summary>
        /// <remarks>
        /// \struct UserPosition KinovaTypes.h "Definition"
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct UserPosition
        {
            /// <summary>
            /// The type of this position.
            /// </summary>
            public POSITION_TYPE Type;

            /// <summary>
            /// This is used only if the type of position is TIME_DELAY. It represents the delay in second.
            /// </summary>
            public float Delay;

            /// <summary>
            /// Cartesian information about this position.
            /// </summary>
            public CartesianInfo CartesianPosition;

            /// <summary>
            /// Angular information about this position.
            /// </summary>
            public AngularInfo Actuators;

            /// <summary>
            /// Mode of the gripper.
            /// </summary>
            public HAND_MODE HandMode;

            /// <summary>
            /// Fingers information about this position.
            /// </summary>
            public FingersPosition Fingers;

            /// <summary>
            /// This method will initialises all the values to 0, 
            /// default position type is <see cref="POSITION_TYPE.CARTESIAN_POSITION"/>
            /// and default hand mode is <see cref="HAND_MODE.POSITION_MODE"/>
            /// </summary>
            public void InitStruct()
            {
                Type = POSITION_TYPE.CARTESIAN_POSITION;
                Delay = 0.0f;
                CartesianPosition.InitStruct();
                Actuators.InitStruct();
                HandMode = HAND_MODE.POSITION_MODE;
                Fingers.InitStruct();
            }
        }

        ///<summary>
        ///<para>This data structure represents all limitation that can be applied to a control context.</para>
        ///<para>Depending on the context, units and behaviour can change. See each parameter for more informations.</para>
        ///</summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Limitation
        {
            /// <summary>
            /// In a cartesian context, this represents the translation velocity 
            /// but in an angular context, this represents the velocity of the actuators 1, 2 and 3.
            /// </summary>
            public float speedParameter1;

            /// <summary>
            /// In a cartesian context, this represents the orientation velocity 
            /// but in an angular context, this represents the velocity of the actuators 4, 5 and 6.
            /// </summary>
            public float speedParameter2;
            public float speedParameter3;           /*!< Not used for now. */
            public float forceParameter1;           /*!< Not used for now. */
            public float forceParameter2;           /*!< Not used for now. */
            public float forceParameter3;           /*!< Not used for now. */
            public float accelerationParameter1;    /*!< Not used for now. */
            public float accelerationParameter2;    /*!< Not used for now. */
            public float accelerationParameter3;    /*!< Not used for now. */

            /// <summary>
            /// This method will initialises all the values to 0
            /// </summary>
            public void InitStruct()
            {
                speedParameter1 = 0.0f;
                speedParameter2 = 0.0f;
                speedParameter3 = 0.0f;
                forceParameter1 = 0.0f;
                forceParameter2 = 0.0f;
                forceParameter3 = 0.0f;
                accelerationParameter1 = 0.0f;
                accelerationParameter2 = 0.0f;
                accelerationParameter3 = 0.0f;
            }
        }

        /// <summary>
        /// This data structure represents a point of a trajectory. It contains the position a limitation that you can applied.
        /// </summary>
        /// <remarks>
        /// \struct TrajectoryPoint KinovaTypes.h "Definition"
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct TrajectoryPoint
        {
            /// <summary>
            /// Position information that described this trajectory point.
            /// </summary>
            public UserPosition Position;

            /// <summary>
            /// A flag that indicates if the limitation are active or not (1 is active 0 is not).
            /// </summary>
            public int LimitationsActive;

            /// <summary>
            /// A flag that indicates if the tracjetory's synchronization is active. (1 is active 0 is not). ONLY AVAILABLE IN ANGULAR CONTROL.
            /// </summary>
            public int SynchroType;

            /// <summary>
            /// Limitation applied to this point if the limitation flag is active.
            /// </summary>
            public Limitation Limitations;

            /// <summary>
            /// This method will initialises all the values to 0
            /// </summary>
            public void InitStruct()
            {
                Position.InitStruct();
                LimitationsActive = 0;
                SynchroType = 0;
                Limitations.InitStruct();
            }
        }

        ///<summary>
        ///This data structure holds the values of a cartesian position.
        ///</summary>
        /// <remarks>
        ///<para>Coordinates holds the cartesian parts of the position and Fingers holds contains the value of the fingers.</para>
        ///<para>
        ///As an example, if an instance of the CartesianPosition 
        ///is used in a cartesian velocity control context, the values in the struct will be velocity.
        ///</para>
        ///<para>\struct CartesianPosition KinovaTypes.h "Definition"</para>
        ///</remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct CartesianPosition
        {
            /// <summary>
            /// This contains values regarding the cartesian information.(end effector).
            /// </summary>
            public CartesianInfo Coordinates;

            ///<summary>
            ///This contains value regarding the fingers.
            ///</summary>
            public FingersPosition Fingers;

            ///<summary>
            ///This method will initialises all the values to 0
            ///</summary>
            public void InitStruct()
            {
                Coordinates.InitStruct();
                Fingers.InitStruct();
            }
        }

        /// <summary>
        /// This is a virtual representation of a 6-axis joystick.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct JoystickCommand
        {
            /// <summary>
            /// This array contains the state of all the buttons. (1 = PRESSED, 0 = RELEASED)
            /// </summary>
            public short[] ButtonValue;

            ///<summary>
            ///<para>That holds the behaviour of the stick when it is inclined from left to right. (value between -1 and 1 inclusively)</para>
            ///<para>
            ///2 functionalities can be mapped with this value, there is an event when the value is negative and there is one when it is positive.
            ///</para>
            ///</summary>
            public float InclineLeftRight;

            ///<summary>
            ///<para>That holds the behaviour of the stick when it is inclined forward and backward. (value between -1 and 1 inclusively)</para>
            ///<para>
            ///2 functionalities can be mapped with this value, there is an event when the value is negative and there is one when it is positive.
            ///</para>
            ///</summary>
            public float InclineForwardBackward;

            ///<summary>
            ///<para>
            ///That holds the behaviour of the stick when it is rotated clockwork and counter clockwork. (value between -1 and 1 inclusively)
            ///</para>
            ///<para>
            ///2 functionalities can be mapped with this value, there is an event when the value is negative and there is one when it is positive.
            ///</para>
            ///</summary>
            public float Rotate;

            ///<summary>
            ///<para>
            ///That holds the behaviour of the stick when it is moved from left to right. (value between -1 and 1 inclusively)
            ///</para>
            ///<para>
            ///2 functionalities can be mapped with this value, there is an event when the value is negative and there is one when it is positive.
            ///</para>
            ///</summary>
            public float MoveLeftRight;

            ///<summary>
            ///<para>
            ///That holds the behaviour of the stick when it is moved forward and backward. (value between -1 and 1 inclusively)
            ///</para>
            ///<para>
            ///2 functionalities can be mapped with this value, there is an event when the value is negative and there is one when it is positive.
            ///</para>
            ///</summary>
            public float MoveForwardBackward;

            ///<summary>
            ///<para>
            ///That holds the behaviour of the stick when it is pushed and pulled. (value between -1 and 1 inclusively)
            ///</para>
            ///<para>
            ///2 functionalities can be mapped with this value, there is an event when the value is negative and there is one when it is positive.
            ///</para>
            ///</summary>
            public float PushPull;

            public void InitStruct()
            {
                ButtonValue = new short[JOYSTICK_BUTTON_COUNT];
                for (int i = 0; i < JOYSTICK_BUTTON_COUNT; i++)
                {
                    ButtonValue[i] = 0;
                }

                InclineLeftRight = 0.0f;
                InclineForwardBackward = 0.0f;
                Rotate = 0.0f;
                MoveLeftRight = 0.0f;
                MoveForwardBackward = 0.0f;
                PushPull = 0.0f;
            }
        }

        /// <summary>
        /// This is an event from a controller's stick. Each variable of the struct can be mapped with a <see cref="ControlFunctionalityTypeEnum"/>.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct StickEvents
        {
            ///<summary>
            ///<para>This represents the negative value of the event.</para>
            ///<example>As an example, if you incline the stick to the left it will trigger that event.</example>
            ///</summary> 
            public byte Minus;

            ///<summary>
            ///<para>This represents the positive value of the event.</para>
            ///<example>As an example, if you incline the stick to the right it will trigger that event.</example>
            ///</summary> 
            public byte Plus;
        }

        ///<summary>
        ///This is an event from a controller's button. Each variable of the struct can be mapped with a <see cref="ControlFunctionalityTypeEnum"/>.
        ///</summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct ButtonEvents
        {
            ///<summary>
            ///Represents a single CLICK event.(PRESS and RELEASE)
            ///</summary>
            public byte OneClick;

            ///<summary>
            ///Not use for now.
            ///</summary>
            public byte TwoClick;

            ///<summary>
            ///Represents a PRESS and HOLD for 1 second event .
            ///</summary>
            public byte HoldOneSec;

            ///<summary>
            ///Represents a PRESS and HOLD for 2 second event.
            ///</summary>
            public byte HoldTwoSec;

            ///<summary>
            ///Represents a PRESS and HOLD for 3 second event.
            ///</summary>
            public byte HoldThreeSec;

            ///<summary>
            ///Represents a PRESS and HOLD for 4 second event.
            ///</summary>
            public byte HoldFourSec;

            ///<summary>
            ///Represents a PRESS and HOLD event.
            ///</summary>
            public byte HoldDown;
        }

        /// <summary>
        /// Represents one mode map of a control mapping. Each control mapping has 2 list of mode map.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct ControlsModeMap
        {
            ///<summary>
            ///A flag that indicates if we can perform movement in more than one direction at a time.
            ///</summary>
            public int DiagonalsLocked;

            ///<summary>
            ///Not use for now.
            ///</summary>
            public int Expansion;

            /// <summary>
            /// All events from the stick of the controller. Fixed size to <see cref="STICK_EVENT_COUNT"/>
            /// </summary>
            public StickEvents[] ControlSticks;

            /// <summary>
            /// All events from the buttons of the controller. Fixed size to <see cref="BUTTON_EVENT_COUNT"/>
            /// </summary>
            public ButtonEvents[] ControlButtons;
        }

        /*
         * As an example, the kinova 3-axis joystick has its own control mapping. This API also has its own control mapping. Note that
         * since list A and list B cannot be used at the same time in the same control mapping, it implies that either one of the variable
         * can have a >= 0 value.
         */
        /// <summary>
        /// This represents a group of functionalities mapped to some events triggered by a specific controller.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct ControlMapping
        {
            /// <summary>
            /// List <see cref="ModeControlsA"/>'s element count. If this value exceeds <see cref="MODE_MAP_COUNT"/>, we got a problem.
            /// </summary>
            public int NumOfModesA;

            /// <summary>
            /// List <see cref="ModeControlsB"/>'s element count. If this value exceeds <see cref="MODE_MAP_COUNT"/>, we got a problem.
            /// </summary>
            public int NumOfModesB;

            /// <summary>
            /// <para>This is the actual index of the active mode map in the <see cref="ModeControlsA"/>.</para>
            /// <para>If the list A is currently unused, this value will be -1.</para>
            /// </summary>
            public int ActualModeA;

            /// <summary>
            /// <para>This is the actual index of the active mode map in the <see cref="ModeControlsB"/>.</para>
            /// <para>If the list B is currently unused, this value will be -1.</para>
            /// </summary>
            public int ActualModeB;

            /// <summary>
            /// That indicates what kind of controller is in use.
            /// </summary>
            public ControlMappingMode Mode;

            /// <summary>
            /// This is the mode map list A. By default, on the 3-axis kinova joystick, it corresponds to the modes accessible with 
            /// the left button on the top of the stick.
            /// </summary>
            public ControlsModeMap[] ModeControlsA;

            /// <summary>
            /// This is the mode map list B. By default, on the 3-axis kinova joystick, it corresponds to the modes accessible with 
            /// the right button on the top of the stick.
            /// </summary>
            public ControlsModeMap[] ModeControlsB;
        }
        
        ///<summary>
        ///This structure holds all the control mapping of the system. It is the entry point if you want to use the mapping system.
        ///</summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct ControlMappingCharts
        {
            ///<summary>
            ///This tells you how many control mapping we got in the charts. it cannot exceeds <see cref="CONTROL_MAPPING_COUNT"/>.
            ///</summary>
            public int NumOfConfiguredMapping;

            /// <summary>
            /// This is the active control mapping.
            /// </summary>
            public int ActualControlMapping;

            /// <summary>
            /// This is the list of all control mapping stored in the charts.
            /// </summary>
            public ControlMapping[] Mapping;
        }
    }
}
