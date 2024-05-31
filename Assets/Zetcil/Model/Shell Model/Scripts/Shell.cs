using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

namespace Zetcil
{
    public class Shell: ShellSingleton<Shell>
    {

        [Header("Shell Settings")]
        public GameObject TerminalCanvas;
        public GameObject TerminalSystem;
        public GameObject TerminalContent;
        public Text TerminalCaption;
        public Text TerminalText;
        public InputField TerminalInput;
        public int SortOrder;

        [Header("Graph Settings")]
        public GameObject TargetGraph;

        public static string TerminalDebugText;

        string LastCommand;
        bool AxisDebug;
        bool JoystcikDebug;
        bool StatsDebug;

        float updateInterval = 1.0f;
        float lastInterval; // Last interval end time
        float frames = 0; // Frames over current interval
        float framesavtick = 0;
        float framesav = 0.0f;
        static float MaxLine = 10000;

        public static void SaveLog(string aValue)
        {
            StreamWriter writer = new StreamWriter(LogDirectory() + LogFile(), true);
            writer.WriteLine(aValue);
            writer.Close();
        }

        public static string LogFile()
        {
            string filename = System.DateTime.UtcNow.ToString("yyyy_MMMM_dd") + ".log";
            return filename;
        }

        public void DebugLogReset()
        {
            AxisDebug = false;
            JoystcikDebug = false;
            StatsDebug = false;
        }

        public static void DebugLogClear()
        {
            TerminalDebugText = "";
        }

        public void MessageLog(string Value)
        {
            if (TerminalDebugText.Length > MaxLine)
            {
                TerminalDebugText = "";
            }
            string datetime = System.DateTime.UtcNow.ToString("dd/MMMM/yyyy HH:mm:ss WIB");
            TerminalDebugText = TerminalDebugText + "\n" + Value + " :: [" + datetime + "]";
            RectTransform tempContent = TerminalContent.GetComponent<RectTransform>();
            tempContent.sizeDelta += new Vector2(0, 20);
            SaveLog(Value + " :: [" + datetime + "]");
        }

        public void MessageLogNoDate(string Value)
        {
            if (TerminalDebugText.Length > MaxLine)
            {
                TerminalDebugText = "";
            }
            TerminalDebugText = TerminalDebugText + "\n" + Value;
            RectTransform tempContent = TerminalContent.GetComponent<RectTransform>();
            tempContent.sizeDelta += new Vector2(0, 20);
            SaveLog(Value);
        }

        public static void DebugLog(string Value)
        {
            if (TerminalDebugText.Length > MaxLine)
            {
                TerminalDebugText = "";
            }
            string datetime = System.DateTime.UtcNow.ToString("dd/MMMM/yyyy HH:mm:ss WIB");
            TerminalDebugText = TerminalDebugText + "\n" + Value + " :: [" + datetime + "]";
        }

        public static void DebugLogNoDate(string Value)
        {
            if (TerminalDebugText.Length > MaxLine)
            {
                TerminalDebugText = "";
            }
            TerminalDebugText = TerminalDebugText + "\n" + Value;
        }

        public static void DebugLogLine(string Value, string Title = "")
        {
            if (TerminalDebugText.Length > MaxLine)
            {
                TerminalDebugText = "";
            }
            TerminalDebugText = "\n" + Title + "\n" + Value;
        }

        // Start is called before the first frame update
        void Awake()
        {
            TerminalCaption.text = " Zetcil Shell Engine (Ver.3.22.05)";

            TargetGraph.transform.parent = null;
            TargetGraph.SetActive(false);

            if (TerminalDebugText == null)
            {
                TerminalDebugText = "";
            }
            TerminalCanvas.GetComponent<Canvas>().sortingOrder = SortOrder;
            TerminalCanvas.GetComponent<Canvas>().enabled = false;

            lastInterval = Time.realtimeSinceStartup;
            frames = 0;
            framesav = 0;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.F9))
            {
                TargetGraph.SetActive(!TargetGraph.activeSelf);
            }

            if (TerminalCanvas.GetComponent<Canvas>().enabled)
            {
                TerminalText.text = TerminalDebugText;
            }
        }

        void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                TerminalCanvas.GetComponent<Canvas>().enabled = false;
                DebugLogReset();
            }
            if (Input.GetKey("`"))
            {
                TerminalCanvas.GetComponent<Canvas>().enabled = true;
                if (TerminalCanvas.GetComponent<Canvas>().enabled)
                {
                    TerminalInput.Select();
                    if (TerminalInput.isFocused)
                    {
                        TerminalInput.ActivateInputField();
                    }
                }
            }
            if (TerminalCanvas.GetComponent<Canvas>().enabled)
            {
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                {
                    if (TerminalCanvas.GetComponent<Canvas>().enabled)
                    {
                        TerminalInput.text = LastCommand;
                        TerminalInput.Select();
                        if (TerminalInput.isFocused)
                        {
                            TerminalInput.ActivateInputField();
                        }
                    }
                }
                if (AxisDebug)
                {
                    if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                    {
                        DebugLogLine("Horizontal Value: " + Input.GetAxis("Horizontal").ToString() + " :: " + "Vertical Value: " + Input.GetAxis("Vertical").ToString(),
                            "Joystick Axis/Button Debugger: ON");
                    }
                }
                if (StatsDebug)
                {
                    ++frames;
                    float timeNow = Time.realtimeSinceStartup;
                    if (timeNow > lastInterval + updateInterval)
                    {
                        float fps = frames / (timeNow - lastInterval);
                        float ms = 1000.0f / Mathf.Max(fps, 0.00001f);

                        ++framesavtick;
                        framesav += fps;
                        float fpsav = framesav / framesavtick;

                        string tx = string.Format("Time : {0} ms \nCurrent FPS: {1} \nAvgFPS: {2}\nGPU memory : {3} \nSys Memory : {4}\n", ms, fps, fpsav, SystemInfo.graphicsMemorySize, SystemInfo.systemMemorySize);

                        DebugLogLine(tx, "Unity Performance Statistic Monitor: ON");

                        //.AppendFormat("TotalAllocatedMemory : {0}mb\nTotalReservedMemory : {1}mb\nTotalUnusedReservedMemory : {2}mb",
                        //Profiler.GetTotalAllocatedMemory() / 1048576,
                        //Profiler.GetTotalReservedMemory() / 1048576,
                        //Profiler.GetTotalUnusedReservedMemory() / 1048576
                        //);

                        //#if UNITY_EDITOR
                        //tx = string.Format("\nDrawCalls : {0}\nUsed Texture Memory : {1}\nrenderedTextureCount : {2}", UnityStats.drawCalls, UnityStats.usedTextureMemorySize / 1048576, UnityStats.usedTextureCount);
                        //#endif
                        frames = 0;
                        lastInterval = timeNow;
                    }
                }
                if (JoystcikDebug)
                {
                    if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                    {
                        DebugLogLine("Button Pressed Index = 0", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button1))
                    {
                        DebugLogLine("Button Pressed Index = 1", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button2))
                    {
                        DebugLogLine("Button Pressed Index = 2", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button3))
                    {
                        DebugLogLine("Button Pressed Index = 3", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button4))
                    {
                        DebugLogLine("Button Pressed Index = 4", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button5))
                    {
                        DebugLogLine("Button Pressed Index = 5", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button6))
                    {
                        DebugLogLine("Button Pressed Index = 6", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button7))
                    {
                        DebugLogLine("Button Pressed Index = 7", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button8))
                    {
                        DebugLogLine("Button Pressed Index = 8", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button9))
                    {
                        DebugLogLine("Button Pressed Index = 9", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button10))
                    {
                        DebugLogLine("Button Pressed Index = 10", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button11))
                    {
                        DebugLogLine("Button Pressed Index = 11", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button12))
                    {
                        DebugLogLine("Button Pressed Index = 12", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button13))
                    {
                        DebugLogLine("Button Pressed Index = 13", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button14))
                    {
                        DebugLogLine("Button Pressed Index = 14", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button15))
                    {
                        DebugLogLine("Button Pressed Index = 15", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button16))
                    {
                        DebugLogLine("Button Pressed Index = 16", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button17))
                    {
                        DebugLogLine("Button Pressed Index = 17", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button18))
                    {
                        DebugLogLine("Button Pressed Index = 18", "Joystick Axis/Button Debugger: ON");
                    }
                    if (Input.GetKeyDown(KeyCode.Joystick1Button19))
                    {
                        DebugLogLine("Button Pressed Index = 19", "Joystick Axis/Button Debugger: ON");
                    }
                }
                if (TerminalCanvas.GetComponent<Canvas>().enabled && Input.GetKeyUp(KeyCode.Return))
                {
                    
                    ExecuteTerminalCommand();
                    LastCommand = TerminalInput.text;
                    TerminalInput.text = "";
                    TerminalInput.Select();
                    if (TerminalInput.isFocused)
                    {
                        TerminalInput.ActivateInputField();
                    }
                }
            }
        }

        public static string LogDirectory()
        {
            string folder = "/Log/";
            if (!Directory.Exists(Application.persistentDataPath + folder))
            {
                Directory.CreateDirectory(Application.persistentDataPath + folder);
            }
            return Application.persistentDataPath + folder;
        }

        public string TempDirectory()
        {
            string folder = "/Temp/";
            if (!Directory.Exists(Application.persistentDataPath + folder))
            {
                Directory.CreateDirectory(Application.persistentDataPath + folder);
            }
            return Application.persistentDataPath + folder;
        }

        public string DataDirectory()
        {
            string folder = "/Data/";
            if (!Directory.Exists(Application.persistentDataPath + folder))
            {
                Directory.CreateDirectory(Application.persistentDataPath + folder);
            }
            return Application.persistentDataPath + folder;
        }

        public string ConfigDirectory()
        {
            string folder = "/Config/";
            if (!Directory.Exists(Application.persistentDataPath + folder))
            {
                Directory.CreateDirectory(Application.persistentDataPath + folder);
            }
            return Application.persistentDataPath + folder;
        }

        public string LicenseDirectory()
        {
            string folder = "/License/";
            if (!Directory.Exists(Application.persistentDataPath + folder))
            {
                Directory.CreateDirectory(Application.persistentDataPath + folder);
            }
            return Application.persistentDataPath + folder;
        }

        public void ExecuteTerminalCommand()
        {
            string command = TerminalInput.text;
            string[] array_command = command.Split(' ');

            if (TerminalInput.text == "?")
            {
                DebugLogReset();
                DebugLogClear();
                DebugLogNoDate("All Active Command & Parameters");
                DebugLogNoDate("");
                DebugLogNoDate("? - show all command");
                DebugLogNoDate("axis [on/off] - show debug input horizontal/vertical");
                DebugLogNoDate("count - get total gameobject on project");
                DebugLogNoDate("config - read config application");
                DebugLogNoDate("clear - clear screen");
                DebugLogNoDate("graph [on/off] - show game statistic");
                DebugLogNoDate("joystick - on/off show debug on joystick input");
                DebugLogNoDate("list - show all gameobject in hierarchy");
                DebugLogNoDate("license - show license file");
                DebugLogNoDate("log [path] - read log data from streamming assets");
                DebugLogNoDate("machine - show machine id");
                DebugLogNoDate("reset - reset all active command");
                DebugLogNoDate("save - save log file for current date");
                DebugLogNoDate("stats - show application statistics");
                DebugLogNoDate("scene - show current scene");
                DebugLogNoDate("xml [path] - read xml data from streamming assets");
                DebugLogNoDate("quit - quit application");
            }
            else if (TerminalInput.text == "save")
            {
                SaveLog(Shell.TerminalDebugText);
                MessageLog("Log file saved successfully. ");
            }
            else if (TerminalInput.text == "clear")
            {
                DebugLogClear();
            }
            else if (TerminalInput.text == "reset")
            {
                DebugLogReset();
                DebugLogClear();
            }
            else if (TerminalInput.text == "stats")
            {
                DebugLogClear();
                StatsDebug = true; 
            }
            else if (TerminalInput.text == "about")
            {
                DebugLogClear();
                DebugLogNoDate("Zetcil Framework (Rickman Roedavan © 2019-2020)");
                DebugLogNoDate("\nThe Zetcil framework including scripts / libraries / machines / shell SHOULD NOT be reproduced, change, " +
                               "or transmitted in any form or in any way using electronic or mechanical methods, " +
                               "without the copyright permission of the copyright owner, including specifically, cooperation issues within a certain time " +
                               "or other use that may not be and is permitted by copyright law." +
                               "\nFor permission requests, use can contact email rroedavan@gmail.com. \nCopyright protected by law. ");
            }
            else if (TerminalInput.text == "scene")
            {
                DebugLog("Current scene: " + SceneManager.GetActiveScene().name);
            }
            else if (TerminalInput.text == "list")
            {
                DebugLogClear();
                DebugLog("Get Hierarchy List");
                foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
                {
                    DebugLogNoDate("GameObject: " + obj.name);
                }
            }
            else if (TerminalInput.text == "count")
            {
                DebugLogClear();
                DebugLog("Get Total GameObject");
                DebugLogNoDate("Total GameObjects: " + Object.FindObjectsOfType(typeof(GameObject)).Length);
            }
            else if (TerminalInput.text == "machine")
            {
                DebugLogClear();
                DebugLog("Get Machine Information");
                DebugLogNoDate("MachineID: " + SystemInfo.deviceUniqueIdentifier);
                DebugLogNoDate("Device Name: " + SystemInfo.deviceName);
                DebugLogNoDate("Graphics Info: " + SystemInfo.graphicsDeviceName + ":" + SystemInfo.graphicsDeviceVersion);
                DebugLogNoDate("Operating System: " + SystemInfo.operatingSystem);
            }
            else if (TerminalInput.text == "config")
            {
                DebugLogClear();
                string xmlfile = ConfigDirectory() + "config" + ".xml";
                DebugLog("Try opening: " + xmlfile);
                if (File.Exists(xmlfile))
                {
                    string xmlfile_result = System.IO.File.ReadAllText(xmlfile);
                    DebugLog("File exists! Read file: \n\n " + xmlfile_result + "\n Done reading: ");
                }
            }
            else if (TerminalInput.text == "license")
            {
                DebugLogClear();
                string xmlfile = LicenseDirectory() + "license" + ".xml";
                DebugLog("Try opening: " + xmlfile);
                if (File.Exists(xmlfile))
                {
                    string xmlfile_result = System.IO.File.ReadAllText(xmlfile);
                    DebugLog("License file exists! Read file: \n\n " + xmlfile_result + "\n Done reading: ");
                }
            }
            else if (TerminalInput.text == "quit")
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
            else if (array_command[0] == "joystick")
            {
                if (array_command.Length == 2)
                {
                    if (array_command[1] == "on")
                    {
                        DebugLogClear();
                        JoystcikDebug = true;
                        AxisDebug = true;
                        DebugLogNoDate("Joystick Axis/Button Debugger: ON");
                    }
                    if (array_command[1] == "off")
                    {
                        JoystcikDebug = false;
                        AxisDebug = false;
                        DebugLogNoDate("Joystick Axis/Button Debugger: OFF");
                    }
                }
                else
                {
                    DebugLog("Parameter Error. Joystick command need 2 parameter, ex: joystick [ON/OFF]. ");
                }
            }
            else if (array_command[0] == "read" || array_command[0] == "xml")
            {
                if (array_command.Length == 2)
                {
                    DebugLogClear();
                    string xmlfile = DataDirectory() + array_command[1] + ".xml";
                    DebugLog("Try opening: " + xmlfile);
                    if (File.Exists(xmlfile))
                    {
                        string xmlfile_result = System.IO.File.ReadAllText(xmlfile);
                        DebugLog("File exists! Read file: \n\n " + xmlfile_result + "\n Done reading: ");
                    }
                    else
                    {
                        xmlfile = TempDirectory() + array_command[1] + ".xml";
                        DebugLog("Try opening: " + xmlfile);
                        if (File.Exists(xmlfile))
                        {
                            string xmlfile_result = System.IO.File.ReadAllText(xmlfile);
                            DebugLog("File exists! Read file: \n\n " + xmlfile_result + "\n Done reading: ");
                        } 
                        else
                        {
                            xmlfile = ConfigDirectory() + array_command[1] + ".xml";
                            DebugLog("Try opening: " + xmlfile);
                            if (File.Exists(xmlfile))
                            {
                                string xmlfile_result = System.IO.File.ReadAllText(xmlfile);
                                DebugLog("File exists! Read file: \n\n " + xmlfile_result + "\n Done reading: ");
                            } else
                            {
                                DebugLogNoDate("File " + array_command[1] + ".xml" + " NOT FOUND!");
                            }
                        }
                    }
                }
                else
                {
                    DebugLog("Parameter Error. Read command need valid folder, ex: xml [DIR/FILE]. ");
                }
            }
            else if (array_command[0] == "log")
            {
                if (array_command.Length == 2)
                {
                    DebugLogClear();
                    string logfile = LogDirectory() + array_command[1] + ".log";
                    DebugLog("Try opening: " + logfile);
                    if (File.Exists(logfile))
                    {
                        string logfile_result = System.IO.File.ReadAllText(logfile);
                        DebugLog("File exists! Read file: \n\n " + logfile_result + "\n Done reading: ");
                    } else
                    {
                        DebugLogNoDate("File " + array_command[1] + ".log" + " NOT FOUND!");
                    }
                }
                else
                {
                    DebugLog("Parameter Error. Read command need valid folder, ex: log [DIR/FILE]. ");
                }
            }
            else if (array_command[0] == "axis")
            {
                if (array_command.Length == 2)
                {
                    if (array_command[1] == "on")
                    {
                        DebugLogClear();
                        AxisDebug = true;
                        DebugLogNoDate("Horizontal/Vertical Input Debugger: ON");
                    }
                    if (array_command[1] == "off")
                    {
                        AxisDebug = false;
                        DebugLogNoDate("Horizontal/Vertical Input Debugger: OFF");
                    }
                }
                else
                {
                    DebugLog("Parameter Error. Axis command need 2 parameter, ex: axis [ON/OFF]. ");
                }
            }
            else if (array_command[0] == "graph")
            {
                if (array_command.Length == 2)
                {
                    if (array_command[1] == "on")
                    {
                        DebugLogClear();
                        TargetGraph.SetActive(true);
                        TerminalCanvas.GetComponent<Canvas>().enabled = false;
                        DebugLogReset();
                    }
                    if (array_command[1] == "off")
                    {
                        TargetGraph.SetActive(false);
                        DebugLogNoDate("Game Statistic Graph: OFF");
                    }
                }
                else
                {
                    DebugLog("Parameter Error. Axis command need 2 parameter, ex: axis [ON/OFF]. ");
                }
            }
            else if (array_command[0] == "find")
            {
                if (array_command.Length == 3)
                {
                    foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
                    {
                        if (obj.name == array_command[1])
                        {
                            if (array_command[2] == "true")
                            {
                                obj.SetActive(true);
                            }
                            else if (array_command[2] == "false")
                            {
                                obj.SetActive(false);
                            }
                        }
                    }
                }
                else
                {
                    DebugLogNoDate("Parameter Error. Find command need 3 parameter, ex: find name [GAMEOBJECT] active true. ");
                }
            }
            else
            {
                DebugLogNoDate("Unknown command. Press ? for showing all possible command. ");
            }

        }
    }
}
