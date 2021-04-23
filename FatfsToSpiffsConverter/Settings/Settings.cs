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
        public bool useSpiffs;
        public string pathSpiffs;
        public string pathFatfs;
    }

    public struct UserSettings
    {
        public string currentProfile;
        public string portName;
    }




    public static class Settings
    {

        private static readonly object m_lock = new object();
        private static readonly object lockMainSet = new object();
        private static readonly object lockUserSet = new object();


        private static readonly string settingsFolder = "W25Qxxx_Flasher\\Settings";
        private static readonly string mainSettingsFileName = "Settings.xml";
        private static readonly string userSettingsFileName = "UserSettings.xml";

        public static readonly string indoorTagerProfileName = "indoor_tager_3_0";
        public static readonly string indoorVestProfileName = "indoor_vest_3_0";
        public static readonly string outdoorTagerProfileName = "outdoor_tager";

        private static readonly string rootElementNameMain = "Profiles";
        private static readonly string rootElementNameUser = "Settings";

        private static UserSettings userSet = new UserSettings();
        private static MainSettings mainSet = new MainSettings();


        public static UserSettings UsSettings
        {
            get
            {
                lock (lockUserSet)
                {
                    ReadSettings();
                    return userSet;
                }
            }
            set
            {
                lock (lockUserSet)
                {
                    SaveSettings(value);
                }
            }
        }

        public static MainSettings MnSettings {
            get
            {
                lock (lockMainSet)
                {
                    ReadSettings(UsSettings.currentProfile);
                    return mainSet;
                }
            }
            set
            {
                lock (lockMainSet)
                {
                    SaveProfile(value, UsSettings.currentProfile);
                }
            }
        }       

        public static string GetSettingsDir
        {
            get
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), settingsFolder);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }

        private static void CreateDefXmlDocUser(string path)
        {
            XDocument xdoc = new XDocument();
            XElement root = new XElement(rootElementNameUser);
            xdoc.Add(root);
            root.Add(new XElement("currentProfile", outdoorTagerProfileName),
                        new XElement("portName", ""));                     
            xdoc.Save(path);
        }

        private static void CreateDefXmlDocMain(string path)
        {
            XDocument xdoc = new XDocument();
            XElement root = new XElement(rootElementNameMain);
            xdoc.Add(root);
            xdoc.Save(path);
            SaveProfile(DefaultSettings.indoorTager3_0 ,indoorTagerProfileName);
            SaveProfile(DefaultSettings.indoorVest3_0, indoorVestProfileName);
            SaveProfile(DefaultSettings.outdoorTager, outdoorTagerProfileName);
        }

        private static bool IsExistElementName(XDocument xdoc, string name)
        {
            XElement root = xdoc.Element(rootElementNameMain);
            foreach (XElement xe in root.Elements("profile").ToList())
            {
                if (xe.Attribute("name").Value == name)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool SaveProfile(MainSettings set, string profileName)
        {
            try
            {
                XDocument xdoc;
                var path = Path.Combine(GetSettingsDir, mainSettingsFileName);
                if (!File.Exists(path))
                {
                    CreateDefXmlDocMain(path);
                }
                xdoc = XDocument.Load(path);
                XElement root = xdoc.Element(rootElementNameMain);
                if (IsExistElementName(xdoc, profileName)) return false;

                root.Add(new XElement("profile",
                                new XAttribute("name", profileName),
                                new XElement("flashSize", set.flashSize),
                                new XElement("spiffsAddress", set.spiffsAddress),
                                new XElement("logPageSize", set.logPageSize),
                                new XElement("blockSize", set.blockSize),
                                new XElement("eraseSize", set.eraseSize),
                                new XElement("allowFormating", set.allowFormating),
                                new XElement("useSpiffs", set.useSpiffs),
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

        public static bool RemoveProfile(string profileName)
        {
            XDocument xdoc;
            var path = Path.Combine(GetSettingsDir, mainSettingsFileName);
            if (!File.Exists(path))
            {
                CreateDefXmlDocMain(path);
                return false;
            }
            xdoc = XDocument.Load(path);
            XElement root = xdoc.Element(rootElementNameMain);
            if (!IsExistElementName(xdoc, profileName)) return false;

            foreach (XElement xe in root.Elements("profile").ToList())
            {
                if (xe.Attribute("name").Value == profileName)
                {
                    xe.Remove();
                    break;
                }
            }

            xdoc.Save(path);
            return true;
        }

        private static bool SaveSettings(UserSettings set)
        {
            XDocument xdoc;
            var path = Path.Combine(GetSettingsDir, userSettingsFileName);
            if (!File.Exists(path))
            {
                CreateDefXmlDocUser(path);
            }
            xdoc = XDocument.Load(path);
            XElement root = xdoc.Element(rootElementNameUser);

            root.Element("currentProfile").Value = set.currentProfile;
            root.Element("portName").Value = set.portName;
            xdoc.Save(path);
            return true;
        }

        private static void ReadSettings()
        {
            XDocument xdoc;
            var path = Path.Combine(GetSettingsDir, userSettingsFileName);
            if (!File.Exists(path))
            {
                CreateDefXmlDocUser(path);
            }
            xdoc = XDocument.Load(path);
            XElement root = xdoc.Element(rootElementNameUser);
            userSet.currentProfile = root.Element("currentProfile").Value;
            userSet.portName = root.Element("portName").Value;
        }

        private static MainSettings ReadSettings(string profileName)
        {
            XDocument xdoc;
            var path = Path.Combine(GetSettingsDir, mainSettingsFileName);
            if (!File.Exists(path))
            {
                CreateDefXmlDocMain(path);
            }
            xdoc = XDocument.Load(path);
            XElement root = xdoc.Element(rootElementNameMain);

            foreach (XElement xe in root.Elements("profile").ToList())
            {
                if (xe.Attribute("name").Value == profileName)
                {
                    mainSet.flashSize = Convert.ToUInt32(xe.Element("flashSize").Value);
                    mainSet.spiffsAddress = Convert.ToUInt32(xe.Element("spiffsAddress").Value);
                    mainSet.logPageSize = Convert.ToUInt32(xe.Element("logPageSize").Value);
                    mainSet.blockSize = Convert.ToUInt32(xe.Element("blockSize").Value);
                    mainSet.eraseSize = Convert.ToUInt32(xe.Element("eraseSize").Value);
                    mainSet.allowFormating = Convert.ToBoolean(xe.Element("allowFormating").Value);
                    mainSet.useSpiffs = Convert.ToBoolean(xe.Element("useSpiffs").Value);
                    mainSet.pathSpiffs = xe.Element("pathSpiffs").Value;
                    mainSet.pathFatfs = xe.Element("pathFatfs").Value;
                }
            }

            return mainSet;
        }

        public static List<string> GetProfilesList()
        {
            XDocument xdoc;
            var path = Path.Combine(GetSettingsDir, mainSettingsFileName);
            if (!File.Exists(path))
            {
                CreateDefXmlDocMain(path);
            }
            xdoc = XDocument.Load(path);
            XElement root = xdoc.Element(rootElementNameMain);
            List<string> resList = new List<string>();
            foreach (XElement xe in root.Elements("profile").ToList())
            {
                resList.Add(xe.Attribute("name").Value);
            }
            return resList;
        }
    }
}
