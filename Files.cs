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
//
//RPass - Encrypted Password Manager - locally, offline, secure
//    Copyright (C) 2022 Alexăndroae Valentin
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see:
//
//		https://www.gnu.org/licenses/
//
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
