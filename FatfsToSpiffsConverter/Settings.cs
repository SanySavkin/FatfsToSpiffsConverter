using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace FatfsToSpiffsConverter
{

    public struct MainSettings
    {
        public uint flashSize;
        public uint spiffsAddress;
        public uint logPageSize;
        public uint blockSize;
        public uint eraseSize;
        public bool allowFormating;
        public string pathSpiffs;
        public string pathFatfs;
    }




    public static class Settings
    {
        private static readonly string folderSettings = "Settings";
        //private static readonly string defSettingsFileName = "default_settings";
        private static readonly string settingsFileName = "Settings.xml";

        private static readonly string indoorTagerProfileName = "indoor_tager_3_0";
        private static readonly string indoorVestProfileName = "indoor_vest_3_0";
        private static readonly string outdoorTagerProfileName = "outdoor_tager";

        private static readonly string rootElementName = "Profiles";


        public static string GetSettingsDir
        {
            get
            {
                var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var path = Path.Combine(assemblyFolder, folderSettings);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }

        private static void CreateDefXmlDoc(string path)
        {
            XDocument xdoc = new XDocument();
            XElement root = new XElement(rootElementName);
            xdoc.Add(root);
            xdoc.Save(path);
            //WriteSettings(indoorTagerProfileName);
            //WriteSettings(indoorVestProfileName);
            //WriteSettings(outdoorTagerProfileName);
        }

        private static bool IsExistElementName(XDocument xdoc, string name)
        {
            XElement root = xdoc.Element(rootElementName);
            foreach (XElement xe in root.Elements("profile").ToList())
            {
                if (xe.Attribute("name").Value == name)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool WriteSettings(MainSettings set, string profileName)
        {
            try
            {
                XDocument xdoc;
                var path = Path.Combine(GetSettingsDir, settingsFileName);
                if (!File.Exists(path))
                {
                    CreateDefXmlDoc(path);
                }
                xdoc = XDocument.Load(path);
                XElement root = xdoc.Element(rootElementName);
                if (IsExistElementName(xdoc, profileName)) return false;

                root.Add(new XElement("profile",
                                new XAttribute("name", profileName),
                                new XElement("flashSize", set.flashSize),
                                new XElement("spiffsAddress", set.spiffsAddress),
                                new XElement("logPageSize", set.logPageSize),
                                new XElement("blockSize", set.blockSize),
                                new XElement("eraseSize", set.eraseSize),
                                new XElement("allowFormating", set.allowFormating),
                                new XElement("pathSpiffs", set.pathSpiffs),
                                new XElement("pathFatfs", set.pathFatfs)
                                ));

                xdoc.Save(path);
                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }

        public static MainSettings ReadSettings(string profileName)
        {
            MainSettings set = new MainSettings();
            XDocument xdoc;
            var path = Path.Combine(GetSettingsDir, settingsFileName);
            if (!File.Exists(path))
            {
                CreateDefXmlDoc(path);
            }
            xdoc = XDocument.Load(path);
            XElement root = xdoc.Element(rootElementName);

            foreach (XElement xe in root.Elements("profile").ToList())
            {
                if (xe.Attribute("name").Value == profileName)
                {
                    set.flashSize = Convert.ToUInt32(xe.Element("flashSize").Value);
                    set.spiffsAddress = Convert.ToUInt32(xe.Element("spiffsAddress").Value);
                    set.logPageSize = Convert.ToUInt32(xe.Element("logPageSize").Value);
                    set.blockSize = Convert.ToUInt32(xe.Element("blockSize").Value);
                    set.eraseSize = Convert.ToUInt32(xe.Element("eraseSize").Value);
                    set.allowFormating = Convert.ToBoolean(xe.Element("allowFormating").Value);
                    set.pathSpiffs = xe.Element("pathSpiffs").Value;
                    set.pathFatfs = xe.Element("pathFatfs").Value;
                }
            }

            return set;
        }

        public static bool CheckDefaultSettings()
        {
            var pathFile = Path.Combine(GetSettingsDir, indoorTagerProfileName);
            return true;
        }

        public static bool CreateDefSettings()
        {
            return true;
        }

    }
}
