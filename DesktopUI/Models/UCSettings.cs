//  BuddyHub Universal Controller
//
//  Modified by Zhiqing Wei, 2019
//  https://github.com/ZhiqingWei/UC

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UCUI.Models
{
    class UCSettings : INotifyPropertyChanged
    {

        //KeyBinds stored in string instead of Key so it can be more easily read from text file (No backwards conversion needed). Alternatively it could be read binary mode.
        static private string[] keyBinds = new string[10];
        private string selectedDevice = "";
        string speed;
#pragma warning disable CS0414 // The field 'UCSettings.check_click' is assigned but its value is never used
        Boolean check_click = false;
#pragma warning restore CS0414 // The field 'UCSettings.check_click' is assigned but its value is never used
        
        // AutoResetEvent
        static public void SetKey(string keyIn, int i)
        {
            if (keyIn != keyBinds[i])
            {
                keyBinds[i] = keyIn;
            }
        }
        static public string GetKey(int i)
        {
            if (keyBinds[i] != null)
                return keyBinds[i];
            else return "null";
        }

        private bool isShake; //Increased visual feedback animation for shaker buttons
        public bool IsShake
        {
            get { return isShake; }
            set
            {
                if (value != isShake)
                {
                    isShake = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isCenter; //Mouse centering
        public bool IsCenter
        {
            get { return isCenter; }
            set
            {
                if (value != isCenter)
                {
                    isCenter = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isHover; //CLicking on hover
        public bool IsHover
        {
            get { return isHover; }
            set
            {
                if (value != isHover)
                {
                    isHover = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isSound; //Audio Feedback
        public bool IsSound
        {
            get { return isSound; }
            set
            {
                if (value != isSound)
                {
                    isSound = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isOpen; //If any view (i.e. SettingsView or HelpView) is visible, this is true
        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                if (value != isOpen)
                {
                    isOpen = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isFull; //Fullscreen
        public bool IsFull
        {
            get { return isFull; }
            set
            {
                if (value != isFull)
                {
                    isFull = value;
                    OnPropertyChanged();
                    if (isFull)
                    {
                        App.Current.MainWindow.WindowState = WindowState.Normal;
                        App.Current.MainWindow.WindowStyle = WindowStyle.ToolWindow;
                        App.Current.MainWindow.ResizeMode = ResizeMode.NoResize;
                        App.Current.MainWindow.WindowState = WindowState.Maximized;

                    }
                    else
                    {
                        App.Current.MainWindow.WindowState = WindowState.Normal;
                        App.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
                        App.Current.MainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                    }
                }
            }
        }

        private bool isBuddy;
        public bool IsBuddy
        {
            get { return isBuddy; }
            set
            {
                if (value != isBuddy)
                {
                    isBuddy = value;
                    OnPropertyChanged();
                }
            }
        }

        private string message; //Displayed on TitleBlock in MainWindow
        public string Message
        {
            get { return message; }
            set
            {
                if (value != message)
                {
                    message = value;
                    OnPropertyChanged();
                }
            }
        }

        private string buttonKey = "ButtonNull"; //ToString() of the current key down in the main window. Compared with e.Key is compared with keybinds[i] to determine its value. Fires the pressdown animation.
        public string ButtonKey
        {
            get { return buttonKey; }
            set
            {
                MainWindow currentWindow = (MainWindow)App.Current.MainWindow;
                ControlOption selected = (ControlOption)currentWindow.ControlOptions.SelectedItem;
                if (selected != null)
                {
                    selectedDevice = selected.name;
                    int buttonIndex = 0;
                    if (Int32.TryParse(buttonKey.Substring(6), out buttonIndex))
                    {
                        if (value != buttonKey)
                        {
                            switch (selectedDevice)
                            {
                                case "Robotic arm":
                                    //selectedDevice = "AL5D";
                                    //Task.Run(() => 
                                    //{
                                    //    while (currentWindow.buttonPressed)
                                    //    {
                                    //        currentWindow.NotifyServer(currentWindow.localIP + selectedDevice + "/" + buttonIndex,
                                    //          "",
                                    //        "POST");
                                    //        Thread.Sleep(20);     
                                    //    }
                                    //});
                                    selectedDevice = "AL5D";
                                    break;
                                case "Light switch":
                                    selectedDevice = "smart lamp";
                                    break;
                                case "Text-to-Speech":
                                    selectedDevice = "Alexa";
                                    break;
                            }
                            if (selectedDevice.Contains("Jaco"))
                            {
                                selectedDevice = "Jaco";
                            }
                            if (value == "ButtonNull")
                            {
                                if (selectedDevice != "Alexa" && selectedDevice != "AL5D" && selectedDevice != "Jaco")
                                {
                                    currentWindow.NotifyServer(currentWindow.localIP + selectedDevice + "/" + buttonIndex,
                                   "", "POST");
                                }
                            }
                            buttonKey = value;
                            OnPropertyChanged();
                        }
                    }
                    else
                    {
                        Int32.TryParse(value.Substring(6), out buttonIndex);
                        if (value != buttonKey)
                        {
                            switch (selectedDevice)
                            {
                                case "Robotic arm":
                                    selectedDevice = "AL5D";
                                    Task.Run(() =>
                                    {
                                        while (currentWindow.buttonPressed)
                                        {
                                            currentWindow.NotifyServer(currentWindow.localIP + "AL5D" + "/" + buttonIndex,
                                              "",
                                            "POST");
                                            Thread.Sleep(100);
                                        }   
                                        Thread.Sleep(50);
                                    });
                                    break;
                            }

                            
                            if (selectedDevice.Contains("Jaco"))
                            {
                                string Mode = selectedDevice.Split(' ')[1];
                                selectedDevice = "Jaco";

                                if (buttonIndex.ToString().StartsWith("9"))     
                                {
                                    Int32.TryParse(value.Substring(7), out buttonIndex);
                                    buttonIndex += 10;
                                }       

                                Task.Run(() =>
                                {
                                    while (currentWindow.buttonPressed)
                                    {
                                        
                                        if (buttonIndex==10)
                                        {
                                            this.speed = "low";    
                                        }
                                        else if (buttonIndex == 11)
                                        {
                                            this.speed = "medium";
                                        }
                                        else if (buttonIndex == 12)
                                        {
                                            this.speed = "high";
                                        }
                                        else if (this.speed == null)
                                        {
                                            this.speed = "medium";
                                        }

                                        currentWindow.NotifyServer(currentWindow.localIP + "Jaco" + "/" + buttonIndex + "/" + Mode + "/"+ this.speed,
                                          "",
                                        "POST");
                                        Thread.Sleep(100);
                                    }
                                    Thread.Sleep(50);
                                });
                            }
                            buttonKey = value;
                            OnPropertyChanged();
                        }
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region AutoTabbing
        public static int AutoTabTime;
        public static bool IsAuto;

        static public void AutoTab()
        {

            while (IsAuto)
            {
                Thread.Sleep(AutoTabTime);
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    UCMethods.NextTab();
                });

            }
            if (Thread.CurrentThread.IsBackground) Thread.CurrentThread.Abort();

        }
        #endregion

        
    }
}
