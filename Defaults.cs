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
    class Rdefaults
    {
        // Default values and settings are stored here

        // User session defaults
        public const string defaultMasterPassword = "root"; // default master password is root
        public const int defaultLanguage = 1; // en = 0 or ro = 1, default is en
        public const string defaultUserName = "root"; // default name is root
        public const string defaultSalt = "20031104"; // minimum 8 bytes req for salt
        public const string defaultIcon = "gray"; // default icon
        public const bool defaultIfDarkTheme = true; // default theme type is white
        
        // User data structures
        public struct user
        {
            public string masterPassword;
            public int language;
            public string name;
            public string salt;
            public string icon;
            public bool ifDarkTheme;
        };
        
    }
}
