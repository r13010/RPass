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
    class Rdefaults
    {
        // Default values and settings are stored here

        // User session defaults
        public const string defaultMasterPassword = "root"; // default master password is root
        public const int defaultLanguage = 0; // en = 0 or ro = 1, default is en
        public const string defaultUserName = "root"; // default name is root
        public const string defaultSalt = "20031104"; // minimum 8 bytes req for salt
        public const string defaultIcon = "gray"; // default icon
        public const bool defaultIfDarkTheme = false; // default theme type is white
        
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
