using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FatfsToSpiffsConverter
{
    public static class Help
    {
        private static readonly string folderHelp = "W25Qxxx_Flasher\\Help";
        private static readonly string helpFileName = "W25Qxxx_Flasher.pdf";


        public static string GetHelpDir
        {
            get
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), folderHelp);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }

        public static string OpenFile()
        {
            string fileName = "";
            try
            {
                fileName = Path.Combine(GetHelpDir, helpFileName);
                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception)
            {
                return fileName + " файл не найден";
            }
            return "";
        }
    }
}
