﻿using BH.Adapter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;

namespace BH.Adapter.MidasCivil
{
    public partial class MidasCivilAdapter : BHoMAdapter
    {

        /***************************************************/
        /**** Constructors                              ****/
        /***************************************************/

        //Add any applicable constructors here, such as linking to a specific file or anything else as well as linking to that file through the (if existing) com link via the API
        public MidasCivilAdapter(string filePath, bool active = false, string version = "")
        { 
            if (active)
            {
                AdapterId = "MidasCivil_id";   //Set the "AdapterId" to "SoftwareName_id". Generally stored as a constant string in the convert class in the SoftwareName_Engine

                BH.Adapter.Modules.Structure.ModuleLoader.LoadModules(this);

                if (string.IsNullOrWhiteSpace(filePath))
                {
                    throw new ArgumentException("No file path given");
                }
                else if (IsApplicationRunning())
                {
                    throw new Exception("MidasCivil process already running");
                }
                else
                {
                    System.Diagnostics.Process.Start(filePath);
                    directory = Path.GetDirectoryName(filePath);
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    string txtFile = directory + "\\" + fileName + ".txt";
                    string mctFile = directory + "\\" + fileName + ".mct";

                    if (File.Exists(txtFile))
                    {
                        midasText = File.ReadAllLines(txtFile).ToList();
                        SetSectionText();
                    }
                    else if (File.Exists(mctFile))
                    {
                        midasText = File.ReadAllLines(mctFile).ToList();
                        SetSectionText();
                    }
                    string versionFile = directory + "\\TextFiles\\" + "VERSION" + ".txt";
                    midasCivilVersion = "8.8.1";

                    if (!(version == ""))
                    {
                        midasCivilVersion = version.Trim();
                        if(File.Exists(versionFile))
                        {
                            Engine.Reflection.Compute.RecordWarning("*VERSION file found, user input used to overide: version =  " + midasCivilVersion);
                        }
                    }
                    else if (File.Exists(versionFile))
                    {
                        List<string> versionText = GetSectionText("VERSION");
                        midasCivilVersion  = versionText[0].Trim();
                    }
                    else
                    {
                        Engine.Reflection.Compute.RecordWarning("*VERSION file not found in directory and no version specified, MidasCivil version assumed default value =  " + midasCivilVersion);
                    }
                }
            }
        }

        public static bool IsApplicationRunning()
        {
            return (Process.GetProcessesByName("CVlw").Length > 0) ? true : false;
        }

        public List<string> midasText;
        public string directory;
        public string midasCivilVersion;
        private Dictionary<Type, Dictionary<int, HashSet<string>>> m_tags = new Dictionary<Type, Dictionary<int, HashSet<string>>>();

        /***************************************************/
        /**** Private  Fields                           ****/
        /***************************************************/

        //Add any comlink object as a private field here, example named:

        //private SoftwareComLink m_softwareNameCom;

        /***************************************************/
    }
}
