﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using UCUI.Models;
using UCUI.UserControls;
using System.Windows.Interop;
using System.Media;
using System.Windows.Controls.Primitives;
using System.Net.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Media;
using System.Net.Sockets;
using UCProtocol;

namespace UCUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button[] ButtonArray;
        //private Server server;
        public HttpClient client;
        //private WebServer internalServer;

        //public const string SERVER_ADDRESS = "http://192.168.0.105:8080/";
        private const string INTERNAL_ADDRESS = "http://localhost:8192/";
        // USB Message Constants
        private const int WM_DEVICECHANGE = 0x219;
        private const int WM_DEVICEARRIVAL = 0x8000;
        private const int WM_DEVICEREMOVECOMPLETE = 0X8004;
        public string localIP;
        public bool buttonPressed = false;
        public MainWindow()
        {

            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = String.Format("http://{0}:8080/", endPoint.Address.ToString());
            }
            client = new HttpClient()
            {
                BaseAddress = new Uri(localIP)
            };

            try
            {
                NotifyServer(localIP, "Test", "POST");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Environment.Exit(1);
            }

            //internalServer = new WebServer(ProcessUINotif, INTERNAL_ADDRESS);
            //Task.Run(() =>
            //{
            //    internalServer.Run();
            //});
            DataContext = new UCSettings();
            InitializeComponent();
            ButtonArray = new Button[9];
            SettingsView.ExecuteMethod += new EventHandler(UserControlHandler); //Handling when a button from SettingsView is pressed
            //var serverResponse = NotifyServerAsyncResult(SERVER_ADDRESS + (int)Notif.GetServerStatus, null, "GET").Result;
            //if (serverResponse != null)
            //{
            //    if (serverResponse.IsSuccessStatusCode)
            //    {
            //        serverResponse = NotifyServerAsyncResult(SERVER_ADDRESS + (int)Notif.GetControlOption, null, "GET").Result;
            //    }
            //}
                HelpView.ExecuteMethod += new EventHandler(UserControlHandler);
            try
            {
                ControlOptions.ItemsSource = ControlSource.Options;
            }
            catch (TypeInitializationException)
            {
                TitleBlock.Text = "Make sure the Control Options folder is set up correctly!";
                OptionsHeader.Text = "Couldn't list options";
            }
            if (File.Exists("UCConfig.txt"))
            {
                try
                {
                    string lines = System.IO.File.ReadAllText("UCConfig.txt");

                    string[] words = lines.Split(' ');

                    for (int i = 0; i < 9; i++)
                    {
                        UCSettings.SetKey(words[i], i);
                    }
                    ((UCSettings)DataContext).IsCenter = words[9] == "True";
                    ((UCSettings)DataContext).IsHover = words[10] == "True";
                    ((UCSettings)DataContext).IsShake = words[11] == "True";
                    ((UCSettings)DataContext).IsSound = words[12] == "True";
                }
                catch(Exception)
                {
                    TitleBlock.Text = "Could not load settings from UCConfig.txt";
                }

            }
            

        }

            
        

        /*---------------------------
         
               UIElement events

         ---------------------------*/
        #region Navigating usercontrols
        private void PageOpen(object sender, RoutedEventArgs e)
        {
            Button myButton = (Button)sender;
            switch (myButton.Content)
            {
                case "Settings":
                    SettingsView.Visibility = System.Windows.Visibility.Visible;
                    break;
                case "Help":
                    HelpView.Visibility = System.Windows.Visibility.Visible;
                    break;
            }
            Overlay.Visibility = Visibility.Visible;
            MainView.Effect = new BlurEffect();
            CheckCenterMouse();
            OptionsExpander.Focusable = false;
            ControlOptions.IsTabStop = false;
            ((UCSettings)DataContext).IsOpen = true;
            
        }

        public void Outside_Click(object sender, RoutedEventArgs e)
        {

            HelpView.Visibility = System.Windows.Visibility.Collapsed;
            SettingsView.Visibility = System.Windows.Visibility.Collapsed;
            Overlay.Visibility = Visibility.Collapsed;
            MainView.Effect = null;
            ControlOptions.Focusable = true;
            OptionsExpander.IsTabStop = true;
            ((UCSettings)DataContext).IsOpen = false;
            SettingsView.SaveButton.Content = "Save Settings";
        }
        #endregion

        //Every time the selection changes in the CotrolOptions listbox the 3x3 
        //array of buttons in ButtonGrid is repolpulated. 
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonGrid.Children.Clear();

            if (ControlOptions.SelectedItem != null)
            {
                ButtonArray = new Button[9];
                TextBox myTextbox = null;
                ControlOption myOption = (ControlOption)ControlOptions.SelectedItem;
                int visibleButtonCounter = 0;          //Used to iterate through label array from ControlOption

                if (ControlOptions.SelectedIndex == 5)
                {
                    Button[] sideButtonArray = new Button[3];
                    string[] sideButtonLabel = { "Arm", "Wrist", "Finger" };

                    for (int i = 0; i < 3; i++)
                    {
                        sideButtonArray[i] = new Button();
                        sideButtonArray[i].Style = (Style)Application.Current.Resources["Pusher"];
                        sideButtonArray[i].Name = "Button" + (i + 10).ToString();
                        sideButtonArray[i].Margin = new Thickness(5, 0, 0, 0);

                        StackPanel ButtonContent = new StackPanel();
                        ButtonContent.HorizontalAlignment = HorizontalAlignment.Right;
                        ButtonContent.Orientation = Orientation.Vertical;

                        TextBlock ContentText = new TextBlock();
                        ContentText.Text = sideButtonLabel[i];
                        ButtonContent.Children.Add(ContentText);

                        sideButtonArray[i].Content = ButtonContent;
                        ButtonGrid.Children.Add(sideButtonArray[i]);
                        Grid.SetColumn(sideButtonArray[i], i + 2);
                        Grid.SetRow(sideButtonArray[i], 4);
                    }

                    sideButtonArray[0].PreviewMouseDown += delegate 
                    {
                        CheckSound();
                        SelectionChangedJaco("Arm"); 
                        ControlOptions.SelectedIndex = 5; 
                    };    
                    sideButtonArray[1].PreviewMouseDown += delegate 
                    {
                        CheckSound();
                        SelectionChangedJaco("Wrist"); 
                        ControlOptions.SelectedIndex = 5; 
                    };
                    sideButtonArray[2].PreviewMouseDown += delegate 
                    {
                        CheckSound();
                        SelectionChangedJaco("Finger"); 
                        ControlOptions.SelectedIndex = 5; 
                    };

                }
               
                    for (int i = 0; i < 9; i++)
                    {
                        ButtonArray[i] = new Button();

                        if (myOption.buttonVisible[i])
                        {
                            ButtonArray[i].Style = (Style)Application.Current.Resources["Pusher"];
                            ButtonArray[i].Name = "Button" + i.ToString();

                            ButtonArray[i].Margin = new Thickness(10, 10, 10, 10);

                            StackPanel ButtonContent = new StackPanel();
                            ButtonContent.HorizontalAlignment = HorizontalAlignment.Center;
                            ButtonContent.Orientation = Orientation.Vertical;
                            if (myOption.buttonUris[visibleButtonCounter] != null)  
                            {
                                Image ContentImage = new Image();
                                ContentImage.Source = new BitmapImage(myOption.buttonUris[visibleButtonCounter]);
                                ContentImage.HorizontalAlignment = HorizontalAlignment.Center;
                                ContentImage.MaxWidth = 50;
                                ButtonContent.Children.Add(ContentImage);
                            }
                            TextBlock ContentText = new TextBlock();
                            ContentText.Text = myOption.buttonLabels[visibleButtonCounter];
                            ContentText.HorizontalAlignment = HorizontalAlignment.Center;
                            ButtonContent.Children.Add(ContentText);

                            ButtonArray[i].Content = ButtonContent;
                            Grid.SetColumn(ButtonArray[i], i % 3 + 1);
                            Grid.SetRow(ButtonArray[i], i / 3 + 1);
                            ButtonGrid.Children.Add(ButtonArray[i]);



                            ButtonArray[i].PreviewMouseDown += delegate (object a, MouseButtonEventArgs b)
                            {
                                buttonPressed = true;
                                CheckSound();
                                ((UCSettings)DataContext).ButtonKey = ((Button)a).Name;

                            };

                            ButtonArray[i].PreviewMouseUp += delegate (object a, MouseButtonEventArgs b)
                            {
                                buttonPressed = false;
                                ((UCSettings)DataContext).ButtonKey = "ButtonNull";
                            };


                            ButtonArray[i].MouseEnter += delegate (object a, MouseEventArgs b)
                            {
                                if (((UCSettings)DataContext).IsHover)
                                {
                                    CheckSound();
                                    buttonPressed = true;
                                    CheckCenterMouse();
                                    ((UCSettings)DataContext).ButtonKey = ((Button)a).Name;
                                }
                            };

                            ButtonArray[i].MouseLeave += delegate (object a, MouseEventArgs b)
                            {
                                if (((UCSettings)DataContext).IsHover)
                                {
                                    buttonPressed = false;
                                    ((UCSettings)DataContext).ButtonKey = "ButtonNull";
                                }
                            };

                            visibleButtonCounter++;
                        }
                    }

                    if (myOption.textBoxVisible)
                    {
                        myTextbox = new TextBox();
                        myTextbox.TextWrapping = TextWrapping.Wrap;
                        myTextbox.Name = "TextInput";
                        myTextbox.FontSize = 36;
                        Grid.SetColumn(myTextbox, 1);
                        Grid.SetRow(myTextbox, 1);
                        Grid.SetColumnSpan(myTextbox, 3);
                        ButtonGrid.Children.Add(myTextbox);
                        foreach (Control curControl in ButtonGrid.Children)
                        {
                            if (curControl.GetType() == HelpButton.GetType()) //Note the bug: If no image added to Button label
                                if (((TextBlock)((StackPanel)((Button)curControl).Content).Children[1]).Text.Equals("Clear"))
                                {
                                    ((Button)curControl).Click += delegate (object a, RoutedEventArgs i)
                                    {
                                        myTextbox.Text = null;
                                    };
                                    ((Button)curControl).MouseEnter += delegate (object a, MouseEventArgs b)
                                    {
                                        if (((UCSettings)DataContext).IsHover)
                                        {
                                            CheckSound();
                                            ((Button)a).RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                                        }

                                    };
                                }
                        }
                        foreach (Control curControl in ButtonGrid.Children)
                        {
                            if (curControl.GetType() == HelpButton.GetType()) //Note the bug: If no image added to Button label
                                if (((TextBlock)((StackPanel)((Button)curControl).Content).Children[1]).Text.Equals("Send"))
                                {
                                    ((Button)curControl).Click += delegate (object a, RoutedEventArgs i)
                                    {
                                        NotifyServer(localIP + "Alexa" + "/" + 4,
                                            myTextbox.Text,
                                            "POST",
                                            "text/plain");
                                    };
                                    ((Button)curControl).MouseEnter += delegate (object a, MouseEventArgs b)
                                    {
                                        if (((UCSettings)DataContext).IsHover)
                                        {
                                            CheckSound();
                                            ((Button)a).RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                                        }

                                    };

                                }
                        }


                    }
                    HeaderPic.Source = new BitmapImage(myOption.actualUri);
                    ((UCSettings)DataContext).Message = myOption.name;

                
            }
            CheckCenterMouse();


        }

        private void SelectionChangedJaco(string Mode)
        {
            System.Diagnostics.Debug.WriteLine("Jaco arm " + Mode);
            ControlSource.Options = ControlSource.setOptions(Mode);
            ControlOptions.ItemsSource = null;  
            ControlOptions.ItemsSource = ControlSource.Options; 
            //foreach (ControlOption option in ControlOptions.ItemsSource)
            //{
            //    System.Diagnostics.Debug.WriteLine(option.buttonVisible[0]);
            //    System.Diagnostics.Debug.WriteLine(option.name);
            //    System.Diagnostics.Debug.WriteLine(option.imageName);
            //    System.Diagnostics.Debug.WriteLine(option.buttonImages);
            //}
            ControlSource.updateUris();         

        }

        #region Detecting keystrokes for keybinds and animating it
        //In Pusher style the multibinding animationcondition fires by comparing 
        //the name of indiviudual buttons to a stored "Button that's supposed to 
        //be pressed" string. This function changes that string, and fires the 
        //click event. Alternatively a new dependencyproperty could have been 
        //delared but this was simpler. 
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (!((UCSettings)DataContext).IsOpen)
            {
                if (e.Key == Key.Return)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if ((ButtonArray?[i]?.Content) != null && ButtonArray[i].IsFocused)
                        {
                            buttonPressed = true;
                            ((UCSettings)DataContext).ButtonKey = "Button" + i.ToString();
                            ButtonArray[i].RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                            CheckSound();
                            return;
                        }
                    }
                }

                for (int i = 0; i < 9; i++)
                {
                    if (UCSettings.GetKey(i).Equals(e.Key.ToString()) && (ButtonArray[i].Content) != null) //if content is null the button is not visible, so checksound shouldn't be played
                    {
                        buttonPressed = true;
                        ((UCSettings)DataContext).ButtonKey = "Button" + i.ToString();
                        //ButtonArray[i].RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                        CheckSound();


                        e.Handled = true;
                        return;
                    }
                }
                if (UCSettings.GetKey(9).Equals(e.Key.ToString()))  //key 9 is the key bound to the sidebar
                {
                    OptionsExpander.IsExpanded = !OptionsExpander.IsExpanded;
                    e.Handled = true;
                }
            }
            else if (e.Key == Key.Escape) Outside_Click(null, null);
        }


        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            buttonPressed = false;
            ((UCSettings)DataContext).ButtonKey = "ButtonNull";
        }
        #endregion

        #region Checks for settings
        private void CheckCenterMouse()
        {
            if (((UCSettings)DataContext).IsCenter)
                UCMethods.SetPosition(this);

        }
        
        private void CheckSound()
        {
            if (((UCSettings)DataContext).IsSound)
                UCMethods.PlayMySound();
        }
        #endregion

        #region Switch Control functions
       private void OptionsExpander_LostFocus(object sender, RoutedEventArgs e)
        {
            Expander myExpander = (Expander)sender;
            myExpander.SetResourceReference(Expander.BackgroundProperty, "ThemeBrush");
            OptionsHeader.SetResourceReference(TextBlock.BackgroundProperty, "ThemeBrush");
        }

        private void OptionsExpander_GotFocus(object sender, RoutedEventArgs e)
        {
            Expander myExpander = (Expander)sender;

            if (!myExpander.IsMouseOver)
            {
                myExpander.Background = (Brush)Application.Current.Resources["GoldBrush"];
                OptionsHeader.Background = (Brush)Application.Current.Resources["GoldBrush"];
            }
        }
        
        private void ControlOptions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (ControlOptions.SelectedIndex >= ControlSource.Options.Count - 1) ControlOptions.SelectedIndex = 0;
                else ControlOptions.SelectedIndex++;
            }
        }

        #endregion

        protected void UserControlHandler(object sender, EventArgs e)
        {
            Outside_Click(null, null); //Mainwindow has access to this method
        }

       public void NotifyServer(string url, string content, string method, string contentType = "application/json")
        {
            Console.WriteLine(url);
            System.Diagnostics.Debug.WriteLine("Notify server try");
            Task.Run(() =>
            {
                HttpRequestMessage message = new HttpRequestMessage()
                {
                    Method = new HttpMethod(method),
                    RequestUri = new Uri(url)
                };
                if (method != "GET")
                {
                    message.Content = new StringContent(content);
                    message.Content.Headers.Clear();
                    message.Content.Headers.Add("Content-Type", contentType);
                }
                else
                {
                    message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                }
                try
                {
                    var result = client.SendAsync(message).Result;
                }
                catch (AggregateException ae)
                {
                    string errorStr = "";
                    foreach (Exception inner in ae.InnerExceptions)
                    {
                        errorStr += inner.Message;
                        errorStr += "\n";
                    }
                    MessageBox.Show("Local Server Not Found", "Error");
                    Environment.Exit(1);
                }
            });
        }

        private async Task<HttpResponseMessage> NotifyServerAsyncResult(string url, string content, string method)
        {
            HttpRequestMessage message = new HttpRequestMessage()
            {
                Method = new HttpMethod(method),
                RequestUri = new Uri(url)
            };
            if (method != "GET")
            {
                message.Content = new StringContent(content);
                message.Content.Headers.Clear();
                message.Content.Headers.Add("Content-Type", "application/json");
            }
            else
            {
                message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
            var result = client.SendAsync(message).Result;

            return result;
        }

        private string ProcessUINotif(HttpListenerRequest request)
        {
            string rawUrl = request.RawUrl.Replace("%20", " ");
            string[] parsedRequest = rawUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (request.HttpMethod == "POST")
            {
                MessageBox.Show(parsedRequest[0]);
            }
            return "";
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(((UCSettings)DataContext).IsBuddy)
            {
                UCMethods.SendKeyPress(InputInfo.KeyCode.ENTER);
            }
        }
    }

}
