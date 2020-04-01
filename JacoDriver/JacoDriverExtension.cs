using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using static CSharpStruct.Wrapper;

namespace JacoDriver
{
    public partial class Driver
    {
        
        /// <summary>
        /// This function initializes the API. It is the first function you call if you want the rest of the library.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>
        /// <term>NO_ERROR_KINOVA</term>
        /// <description>if operation is a success.</description>
        /// </item>
        /// <item>
        /// <term>ERROR_LOAD_COMM_DLL</term>
        /// <description>if unable to load the communication layer.</description>
        /// </item>
        /// <item>
        /// <term>ERROR_INIT_COMM_METHOD</term>
        /// <description>if unable to load the InitComm function from communication layer.</description>
        /// </item>
        /// <item>
        /// <term>ERROR_CLOSE_METHOD</term>
        /// <description>if unable to load the Close function from communication layer.</description>
        /// </item>
        /// <item>
        /// <term>ERROR_GET_DEVICE_COUNT_METHOD</term>
        /// <description>if unable to load the GetDeviceCount function from communication layer.</description>
        /// </item>
        /// <item>
        /// <term>ERROR_SEND_PACKET_METHOD</term>
        /// <description>if unable to load the SendPacket function from communication layer.</description>
        /// </item>
        /// <item>
        /// <term>ERROR_SET_ACTIVE_DEVICE_METHOD</term>
        /// <description>if unable to load the SetActiveDevice function from communication layer.</description>
        /// </item>
        /// <item>
        /// <term>ERROR_GET_ACTIVE_DEVICE_METHOD</term>
        /// <description>if unable to load the GetActiveDevice function from the communication layer.</description>
        /// </item>
        /// <item>
        /// <term>ERROR_GET_DEVICES_LIST_METHOD</term>
        /// <description>if unable to load the GetDevices function from communication layer.</description>
        /// </item>
        /// <item>
        /// <term>ERROR_OPEN_RS485_ACTIVATE</term>
        /// <description>if unable to load the OpenRS485_Activate function from the communication layer.</description>
        /// </item>
        /// <item>
        /// <term>ERROR_SCAN_FOR_NEW_DEVICE</term>
        /// <description>if unable to load the ScanForNewDevice function from the communication layer.</description>
        /// </item>
        /// </list>
        /// </returns>
        [DllImport("CommandLayerWindows.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        private static extern int InitAPI();

        /// <summary>
        /// This function must called when your application stops using the API. It closes the USB link and the library properly.
        /// </summary>
        /// <returns>NO_ERROR_KINOVA - Always returns success.</returns>
        [DllImport("CommandLayerWindows.dll", CharSet = CharSet.Unicode, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern int CloseAPI();

        /// <summary>
        /// This function returns a list of devices accessible by this API.
        /// </summary>
        /// <param name="devices">A list of devices.</param>
        /// <param name="result">
        /// <para>Result of the operation.</para>
        /// <list type="bullet">
        /// <item>
        /// <term>NO_ERROR_KINOVA</term>
        /// <description>If operation is a success.</description>
        /// </item>
        /// <item>
        /// <term>ERROR_NO_DEVICE_FOUND</term>
        /// <description>If no kinova device is found on the bus.</description>
        /// </item>
        /// <item>
        /// <term>ERROR_API_NOT_INITIALIZED</term>
        /// <description>If the function <see cref="InitAPI()"/> has not been called previously.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <returns>Devices count found on the USB bus.</returns>
        [DllImport("CommunicationLayerWindows.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int GetDevices(IntPtr list, ref int result);

        /// <summary>
        /// <para>This function sets the current active device.</para>
        /// <para>The active device is the device that will receive the command send by this API.</para>
        /// <para>If no active device is set, the first one discovered is the default active device.</para>
        /// </summary>
        /// <param name="device"></param>
        /// <returns>
        /// <list type="bullet">
        /// <item>
        /// <term>NO_ERROR_KINOVA</term>
        /// <description>if operation is a success.</description>
        /// </item>
        /// <item>
        /// <term>ERROR_API_NOT_INITIALIZED</term>
        /// <description>if the API has not been initialized.</description>
        /// </item>
        /// </list>
        /// </returns>
        [DllImport("CommandLayerWindows.dll", CharSet = CharSet.Unicode, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SetActiveDevice(KinovaDevice device);

        /// <summary>
        /// This moves the robot to the HOME position, also known as the <c>READY</c> position.
        /// </summary>
        /// <returns></returns>
        [DllImport("CommandLayerWindows.dll", CharSet = CharSet.Unicode, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern int MoveHome();

        /// <summary>
        /// <para>This function initializes the fingers of the robotical arm.</para>
        /// <para>After the initialization, the robotical arm will be in angular control mode.</para>
        /// <para>If you want to use the cartesian control mode, use the function <see cref="SetCartesianControl()"/>.</para>
        /// </summary>
        /// <returns></returns>
        [DllImport("CommandLayerWindows.dll", CharSet = CharSet.Unicode, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern int InitFingers();

        /// <summary>
        /// This function sets the robotical arm in cartesian control mode (if possible).
        /// </summary>
        /// <returns></returns>
        [DllImport("CommandLayerWindows.dll", CharSet = CharSet.Unicode, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SetCartesianControl();

        /// <summary>
        /// This function sends a trajectory point (WITHOUT limitation) that will be added in the robotical arm's <c>FIFO</c>.
        /// </summary>
        /// <param name="trajectory">The trajectory point that you need to send to the robotical arm.</param>
        /// <returns></returns>
        [DllImport("CommandLayerWindows.dll", CharSet = CharSet.Unicode, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SendBasicTrajectory(TrajectoryPoint trajectory);

        /// <summary>
        /// This function gets the cartesian command of the end effector. The orientation is defined by Euler angles(Convention XYZ).
        /// </summary>
        /// <param name="Response">
        /// A <see cref="CartesianPosition"/> struct containing the values. 
        /// Units are meters for the translation part and radians for the orientation part.
        /// </param>
        /// <returns></returns>
        [DllImport("CommandLayerWindows.dll", CharSet = CharSet.Unicode, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern int GetCartesianCommand(ref CartesianPosition Response);

        /// <summary>
        /// <para>Once recieved by the robot, this function tells the robotical arm that the API will control it from this point forward.</para> 
        /// <para>It must be called before sending trajectories or any commands via the joystick.</para>
        /// </summary>
        /// <returns></returns>
        [DllImport("CommandLayerWindows.dll", CharSet = CharSet.Unicode, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern int StartControlAPI();

        /// <summary>
        /// <para>This function sends a virtual joystick command to the robotical arm.</para>
        /// <para>The behaviour of the robotical arm is determined by the control mapping associated with the API (index 2).</para>
        /// <example>
        /// In the example, we assume that the robotical arm uses the default mapping of the API and that it is in mode B0 (Joystick translation). 
        /// If that is the case, the robotical arm should move along the Z axis.
        /// </example>
        /// </summary>
        /// <param name="joystickCommand">The command that you need to send to the robotical arm.</param>
        /// <returns></returns>
        [DllImport("CommandLayerWindows.dll", CharSet = CharSet.Unicode, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SendJoystickCommand(JoystickCommand joystickCommand);

        /// <summary>
        /// This function sets new control mapping charts to the robotical arm.
        /// </summary>
        /// <param name="Command">The new control mapping charts.</param>
        /// <returns></returns>
        [DllImport("CommandLayerWindows.dll", CharSet = CharSet.Unicode, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SetControlMapping(ControlMappingCharts Command);

        /// <summary>
        /// This function gets the control mapping charts.
        /// </summary>
        /// <param name="Response">A struct containing the control mapping charts.</param>
        /// <returns></returns>
        [DllImport("CommandLayerWindows.dll", CharSet = CharSet.Unicode, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern int GetControlMapping(ref ControlMappingCharts Response);
    }

}
