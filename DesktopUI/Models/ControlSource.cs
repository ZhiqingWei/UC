using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UCUI.Models
{
    class ControlSource
    {
        private static List<ControlOption> _options;
        private const int CURRENT_APPS = 5;


        static ControlSource()
        {
                _options = new List<ControlOption>();
            string[] filenames = Directory.GetFiles("ControlOptions", "*.txt")
                                .Where(file => Regex.IsMatch(Path.GetFileName(file), "^[0-9]+"))
                                .ToArray();         

                        
            for (int i = 0; i < filenames.Length; i++)
            {       
                System.Console.WriteLine(filenames.Length);
                string[] lines = System.IO.File.ReadAllLines(filenames[i]);
                string[] boolWords = lines[0].Split(' ');       
                bool[] _buttonVisible = new bool[9];
                string[] _buttonLabels = lines[5].Split(' ');
                for (int j = 0; j < 9; j++)         
                {
                    _buttonVisible[j] = boolWords[j] == "true";
                }
                string[] _buttonImages = lines[6].Split(' ');


                    _options.Add(new ControlOption
                {
                    buttonVisible = _buttonVisible,
                    textBoxVisible = lines[1] == "true",
                    name = lines[2],
                    description = lines[3],
                    imageName = lines[4],
                    buttonLabels = _buttonLabels,
                    buttonImages = _buttonImages

                });

            }

            updateUris();
            #region CommentOutOriginalForLoop
            //foreach (ControlOption curOption in _options)
            //{
            //    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + curOption.imageName))
            //    {
            //        curOption.actualUri = new Uri(AppDomain.CurrentDomain.BaseDirectory + curOption.imageName, UriKind.RelativeOrAbsolute);
            //    }       
            //    curOption.buttonUris = new Uri[curOption.buttonLabels.Length];
            //    int i = 0;
            //    foreach (string curImage in curOption.buttonImages)     
            //    {
            //        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + curOption.buttonImages[i]))
            //        {
            //            curOption.buttonUris[i] = new Uri(AppDomain.CurrentDomain.BaseDirectory + curOption.buttonImages[i++], UriKind.RelativeOrAbsolute);
            //        }
            //        //String path = AppDomain.CurrentDomain.BaseDirectory + curOption.buttonImages[i];
            //    }
            //}     
            #endregion


        }

        #region NewForJaco
        public static List<ControlOption> setOptions(string Mode)
        {
            string newJacoMode = "";
            List<ControlOption> newOptions = new List<ControlOption>();

            for(int i = 0; i < CURRENT_APPS; i++)
            {
                //System.Diagnostics.Debug.WriteLine($"{i} = {Options[i]}");
                newOptions.Add(Options[i]);
            }
                    
            switch (Mode)
            {
                case "Arm":
                    newJacoMode = Directory.GetFiles("controloptions", "arm*")[0];
                    //newJacoMode = Directory.GetFiles("controloptions\\jacomodes", "arm*")[0];       
                    break;
                case "Wrist":
                    newJacoMode = Directory.GetFiles("controloptions", "wrist*")[0];
                    //newJacoMode = Directory.GetFiles("controloptions\\jacomodes", "wrist*")[0];
                    break;
                case "Finger":
                    newJacoMode = Directory.GetFiles("controloptions", "finger*")[0];
                    //newJacoMode = Directory.GetFiles("controloptions\\jacomodes", "finger*")[0];
                    break;
            }

            string[] lines = System.IO.File.ReadAllLines(newJacoMode);
            string[] boolWords = lines[0].Split(' ');
            bool[] _buttonVisible = new bool[9];
            string[] _buttonLabels = lines[5].Split(' ');
            for (int j = 0; j < 9; j++)
            {
                _buttonVisible[j] = boolWords[j] == "true";
            }
            string[] _buttonImages = lines[6].Split(' ');

            newOptions.Add(new ControlOption
            {
                buttonVisible = _buttonVisible,
                textBoxVisible = lines[1] == "true",
                name = lines[2],
                description = lines[3],
                imageName = lines[4],
                buttonLabels = _buttonLabels,
                buttonImages = _buttonImages

            });

            return newOptions;
        }

        public static void updateUris()
        {
            foreach (ControlOption curOption in _options)
            {
                //System.Diagnostics.Debug.WriteLine(AppDomain.CurrentDomain.BaseDirectory + curOption.imageName);
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + curOption.imageName))
                {
                    curOption.actualUri = new Uri(AppDomain.CurrentDomain.BaseDirectory + curOption.imageName, UriKind.RelativeOrAbsolute);
                }
                curOption.buttonUris = new Uri[curOption.buttonLabels.Length];
                int i = 0;
                //System.Diagnostics.Debug.WriteLine("button image:" + AppDomain.CurrentDomain.BaseDirectory + curOption.buttonImages[0]);
                foreach (string curImage in curOption.buttonImages)
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + curOption.buttonImages[i]))
                    {
                        curOption.buttonUris[i] = new Uri(AppDomain.CurrentDomain.BaseDirectory + curOption.buttonImages[i++], UriKind.RelativeOrAbsolute);
                    }
                }
            }
        }
        #endregion

        public static List<ControlOption> Options
        {
            get
            {
                return _options;
            }
            set
            {
                _options = value;
            }
        }

    }


}
