using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.IO;

namespace rpass
{
    class Rfiles
    {
        // File paths and
        // Others
        public string defaultPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Roaming\.rpass";
        public string savesPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Roaming\.rpass" + @"\saves";
        public string backupsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Roaming\.rpass" + @"\backups";

        public string key1 = "Acum sau niciodată";
        public string key2 = "Azi este vechiul mâine";
        public string key3 = "Familia înainte de toate";

        public void CreatePaths()
        {
            Directory.CreateDirectory(defaultPath);
            Directory.CreateDirectory(savesPath);
            Directory.CreateDirectory(backupsPath);
        }
        public bool UserExists(string username)
        {
            if (File.Exists(savesPath + @"\" + username + ".rcrypt"))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public bool MasterpasswordMatch(string masterpassHash, string username)
        {
            string[] lines = File.ReadAllLines(savesPath + @"\" + username + ".rcrypt");
            try
            {
                if (masterpassHash == lines[1])
                {
                    Array.Clear(lines, 0, lines.Length);
                    return true;
                }
                else
                {
                    Array.Clear(lines, 0, lines.Length);
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public void CreateBackup(string username)
        {
            try
            {
                if (File.Exists(backupsPath + @"\" + username + "_backup2.rcrypt"))
                {
                    File.Copy(backupsPath + @"\" + username + "_backup1.rcrypt", backupsPath + @"\" + username + "_backup3.rcrypt", true);
                }
            }
            catch
            { }
            try
            {
                if (File.Exists(backupsPath + @"\" + username + "_backup1.rcrypt"))
                {
                    File.Copy(backupsPath + @"\" + username + "_backup1.rcrypt", backupsPath + @"\" + username + "_backup2.rcrypt", true);
                }
            }
            catch
            { }
            // Backup current user instance
            try
            {
                if (File.Exists(savesPath + @"\" + username + ".rcrypt"))
                {
                    File.Copy(savesPath + @"\" + username + ".rcrypt", backupsPath + @"\" + username + "_backup1.rcrypt", true);
                }
            }
            catch
            { }
        }
    }
}
