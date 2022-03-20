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
    class Rlang
    {
        // Version number
        public string version = "beta 1.00200322a"; // a = features; b = fixing;

        // Language set goes here

        // value of 0 = english
        // value of 1 = romanian


        // errors
        public string[] error1 = new string[2] // error1 = wrong master password
        {
            "Incorrect master password",
            "Parolă principală incorectă"
        };
        public string[] error2 = new string[2] // error2 = login error
        {
            "Incorrect name or password",
            "Nume sau parolă incorectă"
        };
        public string[] error3 = new string[2] // error3 = login credential pass
        {
            "Logging in...",
            "Se autentifică..."
        };
        public string[] error4 = new string[2] // error3 = bad file
        {
            "Account data may be corrupt",
            "Datele contului pot fi corupte"
        };
        public string[] error8 = new string[2] // error8 = user already exists
        {
            "*All forms are required\n" +
            "*There is already an user registered with this name",
            "*Toate câmpurile sunt necesare\n" +
            "*Există deja un utilizator înregistrat cu acest nume"
        };
        public string[] error9 = new string[2] // error8 = passwords doesn't match or they are less than 8 characters
        {
            "*All forms are required\n" +
            "*Passwords doesn't match or they are less than 8 characters",
            "*Toate câmpurile sunt necesare\n" +
            "*Parolele nu corespund sau sunt mai mici de 8 caractere"
        };
        public string[] error10 = new string[2] // error8 = super secret code isn't valid
        {
            "*All forms are required\n" +
            "*Super secret code must be made up of numbers only",
            "*Toate câmpurile sunt necesare\n" +
            "*Codul super secret trebuie sa fie facut doar din cifre"
        };
        public string[] error11 = new string[2] // error8 = registering success
        {
            "Registering...",
            "Se înregistrează..."
        };
        // titles
        public string[] title1createnewpass = new string[2] // title1 create new password
        {
            "Create a new password",
            "Crează o parolă nouă"
        };
        public string[] title2dashboard = new string[2] // title2 dashboard
        {
            "Dashboard",
            "Tablou de bord"

        };
        public string[] title3password = new string[2] // title3 password
        {
            "Password > ",
            "Parolă > "
        };
        public string[] title4edit = new string[2] // title4 edit
        {
            "Edit > ",
            "Editează > "
        };
        public string[] title5sett = new string[2] // title5 sett
        {
            "User settings",
            "Setări utilizator"
        };
        public string[] title6login = new string[2] // title6 login
        {
            "Login",
            "Autentificare"
        };
        public string[] title7eula = new string[2] // title7 eula
        {
            "End-user license agreement RPass",
            "Acord de licență pentru utilizatorul final RPass"
        };
        public string[] title8register = new string[2] // title8 register
        {
            "Register",
            "Înregistrează-te"
        };
        public string[] title9about = new string[2] // title9 about
        {
            "About RPass",
            "Despre RPass"
        };
        public string[] title10notification = new string[2] // title10 notification
        {
            "Notification",
            "Notificare"
        };
        public string[] title11generate = new string[2] // title11 generate
        {
            "Generate a password",
            "Generează o parolă"
        };
        public string[] title12auth = new string[2] // title12 auth
        {
            "Confirm",
            "Confirmă"
        };
        public string[] title13changename = new string[2] // title12 auth
        {
            "Change your name",
            "Schimbă-ți numele"
        };
        public string[] title14defsettings = new string[2] // title13 default settings
        {
            "Default settings",
            "Setări implicite"
        };
        // minititles
        public string[] minititle1passtobesaved = new string[2] // minititle1 password to be saved
        {
            "New password:",
            "Parolă nouă:"
        };
        public string[] minititle2masterpass = new string[2] // minititle2 masterpassword
        {
            "Master password:",
            "Parolă principală:"
        };
        public string[] minititle3sitelnk = new string[2] // minititle3 sitelink
        {
            "Site link:",
            "Link-ul siteului:"
        };
        public string[] minititle4desc = new string[2] // minititle4 description
        {
            "Description:",
            "Descriere:"
        };
        public string[] minititle5req1 = new string[2] // minititle5 req1
        {
            "*All forms are required in order to create the password",
            "*Toate câmpurile sunt necesare pentru a crea parola"
        };
        public string[] minititle6name = new string[2] // minititle6 name
        {
            "Name:",
            "Nume:"
        };
        public string[] minititle7dashpass = new string[2] // minititle7 passwords
        {
            "Passwords",
            "Parole"
        };
        public string[] minititle8dashaccount = new string[2] // minititle8 your account
        {
            "Your account",
            "Contul tău"
        };
        public string[] minititle9savedpassword = new string[2] // minititle9 saved password
        {
            "Saved password:",
            "Parolă salvată:"
        };
        public string[] minititle10oldpasswords = new string[2] // minititle10 old passwords
        {
            "Older passwords:",
            "Parole mai vechi:"
        };
        public string[] minititle11hidden = new string[2] // minititle11 hidden
        {
            "[hidden]",
            "[ascuns]"
        };
        public string[] minititle12copied = new string[2] // minititle12 copied to clipboard
        {
            "[copied to clipboard]",
            "[copiat în clipboard]"
        };
        public string[] minititle13req2 = new string[2] // minititle13 req2
        {
            "*All forms are required in order to edit the password\n" +
            "**Name can't be updated unless you restart RPass",
            "*Toate câmpurile sunt necesare pentru a edita parola\n" +
            "**Numele nu poate fi actualizat decât dacă reporniți RPass"
        };
        public string[] minititle14lang = new string[2] // minititle14 lang
        {
            "Language:",
            "Limbă:"
        };
        public string[] minititle15theme = new string[2] // minititle15theme
        {
            "Theme:",
            "Temă:"
        };
        public string[] minititle16profilec = new string[2] // minititle16proflec
        {
            "Profile color:",
            "Culoarea profilului:"
        };
        public string[] minititle17langlang = new string[2] // minititle17 double lang
        {
            "Language/Limbă:",
            "Language/Limbă:"
        };
        // EULA
        public string[] minititle18eula = new string[2] // minititle18 eula
        {
            "Agreeing to the license / Continuing/ Using the program implies" +
            "\r\nyou're accepting this terms and conditions in his entirety as follows:" +
            "\r\n" +
            "\r\nUsing this software does not guarantee the security of your data in its entirety" +
            "\r\n" +
            "\r\n   This program is free software: you can redistribute it and/or modify" +
            "\r\nit under the terms of the GNU General Public License as published by" +
            "\r\nthe Free Software Foundation, either version 3 of the License, or" +
            "\r\n(at your option) any later version." +
            "\r\n" +
            "\r\n   This program is distributed in the hope that it will be useful," +
            "\r\nbut WITHOUT ANY WARRANTY; without even the implied warranty of" +
            "\r\nMERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the" +
            "\r\nGNU General Public License for more details." +
            "\r\n" +
            "\r\n   You should have received a copy of the GNU General Public License" +
            "\r\nalong with this program.  If not, see:" +
            "\r\n" +
            "\r\n		https://www.gnu.org/licenses/" +
            "\r\n" +
            "\r\nRPass - Encrypted Password Manager - locally, offline, secure" +
            "\r\nCopyright (C) 2022 Alexăndroae Valentin-Grigore",

            "Acceptarea licenței / Continuarea / Utilizarea programului implică" +
            "\r\nacceptarea pe deplin a acești termeni și condiții, după cum urmează:" +
            "\r\n" +
            "\r\nUtilizarea acestui software nu garantează securitatea datelor dumneavoastră în întregime" +
            "\r\n" +
            "\r\n   Acest program este software liber: îl puteți redistribui și/sau modifica pe" +
            "\r\nacesta în conformitate cu termenii Licenței Publice Generale GNU publicate de" +
            "\r\nFree Software Foundation, fie versiunea 3 a Licenței, fie" +
            "\r\n(la alegerea dvs.) orice versiune ulterioară." +
            "\r\n" +
            "\r\n   Acest program este distribuit în speranța că va fi util," +
            "\r\ndar FĂRĂ NICIO GARANȚIE; fără nici măcar garanția implicită de" +
            "\r\nVANTABILITATE sau DE A SE CONFORMA UNUI UN ANUMIT SCOP. Vezi" +
            "\r\nLicența Publică Generală GNU pentru mai multe detalii." +
            "\r\n" +
            "\r\n   Ar fi trebuit să primiți o copie a Licenței Publice Generale GNU" +
            "\r\nîmpreună cu acest program. Daca nu, vezi:" +
            "\r\n" +
            "\r\n		https://www.gnu.org/licenses/" +
            "\r\n" +
            "\r\nRPass - Manager de parole criptate - local, offline, securizat" +
            "\r\nDrepturi de autor (C) 2022 Alexăndroae Valentin"
        };
        // END OF EULA
        public string[] minititle19newname = new string[2] // minititle19 newname
        {
            "Enter a new name:",
            "Introduce-ți un nume nou:"
        };
        public string[] minititle20newmasterpass = new string[2] // minititle20 newmasterpass
        {
            "Enter a new master password:",
            "Introduce-ți o parolă principală nouă:"
        };
        public string[] minititle21confmasterpass = new string[2] // minititle21 confmasterpass
        {
            "Confirm master password:",
            "Confirmați parola principală:"
        };
        public string[] minititle22newsecretcode = new string[2] // minititle22 newsecretcode
        {
            "Enter a super secret code:",
            "Introduce-ți un cod super secret:"
        };
        public string[] minititle23req2 = new string[2] // minititle23 req2
        {
            "*All forms are required",
            "*Toate câmpurile sunt necesare"
        };
        // ABOUT
        public string[] minititle24about = new string[2] // minititle24 about
        {
            "RPass - Encrypted Password Manager - locally, offline, secure" +
            "\r\nCopyright (C) 2022 Alexăndroae Valentin-Grigore" +
            "\r\nABOUT:" +
            "\r\n   This is a software used to manage passwords for various sites and places," +
            "\r\nsaving them locally, away from the internet, encrypted with AES-256," +
            "\r\nprotected by a master password of choice." +
            "\r\n" +
            "\r\nLICENSE:" +
            "\r\n   This program is free software: you can redistribute it and/or modify" +
            "\r\nit under the terms of the GNU General Public License as published by" +
            "\r\nthe Free Software Foundation, either version 3 of the License, or" +
            "\r\n(at your option) any later version." +
            "\r\n" +
            "\r\n   You should have received a copy of the GNU General Public License" +
            "\r\nalong with this program.  If not, see:" +
            "\r\n" +
            "\r\n		https://www.gnu.org/licenses/" +
            "\r\n" +
            "\r\nFOLLOW THE PROJECT:" +
            "\r\n   All about the project, including updated versions, can be found here:" +
            "\r\n" +
            "\r\n		https://github.com/r13010/RPass" +
            "\r\n" +
            "\r\nVersion ",

            "RPass - Manager de parole criptate - local, offline, securizat" +
            "\r\nDrepturi de autor (C) 2022 Alexăndroae Valentin" +
            "\r\n" +
            "\r\nDESPRE:" +
            "\r\n   Acesta este un software folosit pentru a gestiona parolele pentru diverse" +
            "\r\nsite-uri și locuri, salvându-le local, departe de internet, criptat cu AES-256," +
            "\r\nprotejat de o parolă principală la alegere." +
            "\r\n" +
            "\r\nLICENȚĂ:" +
            "\r\n   Acest program este software liber: îl puteți redistribui și/sau modifica pe" +
            "\r\nacesta în conformitate cu termenii Licenței Publice Generale GNU publicate de" +
            "\r\nFree Software Foundation, fie versiunea 3 a Licenței, fie" +
            "\r\n(la alegerea dvs.) orice versiune ulterioară." +
            "\r\n" +
            "\r\n   Ar fi trebuit să primiți o copie a Licenței Publice Generale GNU" +
            "\r\nîmpreună cu acest program. Daca nu, vezi:" +
            "\r\n" +
            "\r\n		https://www.gnu.org/licenses/" +
            "\r\n" +
            "\r\nURMĂRIȚI PROIECTUL:" +
            "\r\n   Totul despre proiect, inclusiv versiunile actualizate, pot fi găsite aici:" +
            "\r\n" +
            "\r\n		https://github.com/r13010/RPass" +
            "\r\n" +
            "\r\nVersiune "
        };
        // END OF ABOUT
        public string[] minititle25notif = new string[2] // minititle25 notif
        {
            "[contents]",
            "[contents]"
        };
        public string[] minititle26notifsaved = new string[2] // minititle26 notif saved
        {
            "Saved",
            "Salvat"
        };
        public string[] minititle27notifsaved = new string[2] // minititle27 notif saved
        {
            "Your changes have been successfully saved",
            "Modificările tale au fost salvate cu succes"
        };
        public string[] minititle28notifnotsaved = new string[2] // minititle28 notif notsaved
        {
            "Couldn't save",
            "Nu s-a putut salva"
        };
        public string[] minititle29notifnotsaved = new string[2] // minititle29 not saved
        {
            "Your changes have not been saved\n" +
            "The file may not be writable",
            "Modificările tale nu au fost salvate\n" +
            "Este posibil ca fisierul sa nu poata fi scris"
        };
        public string[] minititle30notifrootsave = new string[2] // minititle30 root save
        {
            "Your changes have not been saved\n" +
            "The root user can't be saved",
            "Modificările tale nu au fost salvate\n" +
            "Utilizatorul root nu poate fi salvat"
        };
        public string[] minititle31generated = new string[2] // minititle31 generated
        {
            "Generated password:",
            "Parolă generată:"
        };
        public string[] minititle32generatedsett = new string[2] // minititle32 generated settings
        {
            "Settings",
            "Setări"
        };
        public string[] minititle33generatedsize = new string[2] // minititle33 generated size
        {
            "Size:",
            "Mărime:"
        };
        public string[] minititle34genchar = new string[2] // minititle34 generated char
        {
            " Characters",
            " Caractere"
        };
        public string[] minititle35newname = new string[2] // minititle35 new name
        {
            "Enter a new name:",
            "Introduce-ți un nume nou:"
        };
        public string[] minititle36namereq = new string[2] // minititle36 name req
        {
            "*Name can't be blank\n" +
            "*Can't use a name of an already existing user",
            "*Numele nu poate fi necompletat\n" +
            "*Nu se poate folosi numele unui utilizator deja existent"
        };
        public string[] minititle37deleteaccount = new string[2] // minititle37 delete account
        {
            "Delete account",
            "Ștergere cont"
        };
        public string[] minititle38deleteaccount = new string[2] // minititle38 delete account contents
        {
            "You're about to delete your account!\nConfirm again for approval.",
            "Sunteți pe cale să vă ștergeți contul!\nConfirmați din nou pentru aprobare."
        };
        public string[] minititle39deletedaccount = new string[2] // minititle39 deleted account
        {
            "Deleted account",
            "Cont șters"
        };
        public string[] minititle40deletedaccount = new string[2] // minititle40 delete account contents
        {
            "This account was deleted.",
            "Acest cont a fost șters."
        };
        public string[] minititle41deflang = new string[2] // minititle41 defsettings lang
        {
            "Default language: (when you open RPass)",
            "Limba implicită: (când deschideți RPass)"
        };
        public string[] minititle42deftheme = new string[2] // minititle42 defsettings theme
        {
            "Default theme: (when you open RPass)",
            "Tema implicită: (când deschideți RPass)"
        };
        public string[] minititle43defother = new string[2] // minititle43 defsettings theme
        {
            "Other settings",
            "Alte setări"
        };
        public string[] minititle44defwarn = new string[2] // minititle44 defsettings theme
        {
            "!! DON'T USE UNLESS YOU KNOW WHAT ARE YOU DOING !!",
            "!! NU UTILIZAȚI DECĂ DACĂ ȘTIȚI CE FACEȚI !!"
        };
        //buttons
        public string[] button1save = new string[2] // button1 save
        {
            "Save",
            "Salvează"
        };
        public string[] button2cancel = new string[2] // button2 cancel
        {
            "Cancel",
            "Anulează"
        };
        public string[] button3generate = new string[2] // button3 generate a new password
        {
            "Generate a random password",
            "Generează o parolă aleatorie"
        };
        public string[] button4createpass = new string[2] // button4 create a new password
        {
            "Create a new password",
            "Crează o parolă nouă"
        };
        public string[] button5usersettings = new string[2] // button5 user settings
        {
            "Account settings",
            "Setări cont"
        };
        public string[] button6saveexit = new string[2] // button6 save and exit
        {
            "Save and log out",
            "Salvează și deconectează-te"
        };
        public string[] button7forceexit = new string[2] // button7 exit without saving
        {
            "Log out",
            "Deconectează-te"
        };
        public string[] button8centraldashboard = new string[2] // button8 dashboard
        {
            "← Dashboard",
            "← Tablou de bord"
        };
        public string[] button9show = new string[2] // button9 show hidd
        {
            "Show",
            "Arată"
        };
        public string[] button10hide = new string[2] // button10 show hidd
        {
            "Hide",
            "Ascunde"
        };
        public string[] button11edit = new string[2] // button11 edit
        {
            "Edit",
            "Editează"
        };
        public string[] button12delete = new string[2] // button12 delete
        {
            "Delete",
            "Șterge"
        };
        public string[] button13copy = new string[2] // button13 copy
        {
            "Copy",
            "Copiază"
        };
        public string[] button14sure = new string[2] // minititle14 sure
        {
            "Sure?",
            "Sigur?"
        };
        public string[] button15accountname = new string[2] // minititle15 account name
        {
            "Change account name",
            "Schimbă numele contului"
        };
        public string[] button16accountdel = new string[2] // minititle16 account del
        {
            "Delete account",
            "Șterge contul"
        };
        public string[] button17softwareinfo = new string[2] // minititle17 software info   
        {
            "Software info",
            "Despre software"
        };
        public string[] button18login = new string[2] // minititle18 login
        {
            "Login",
            "Autentifică-te"
        };
        public string[] button19register = new string[2] // minititle19 register
        {
            "Register",
            "Înregistrează-te"
        };
        public string[] button20loginexit = new string[2] // minititle20 login exit
        {
            "Exit",
            "Ieși"
        };
        public string[] button21eulaaccept = new string[2] // minititle21 eula accept
        {
            "I agree",
            "Sunt de accord"
        };
        public string[] button22eularefuse = new string[2] // minititle22 eula refuse
        {
            "I don't agree",
            "Nu sunt de accord"
        };
        public string[] button23notification = new string[2] // minititle23 notification
        {
            "OK",
            "OK"
        };
        public string[] button24generate = new string[2] // minititle24 generate
        {
            "Generate",
            "Generează"
        };
        public string[] button25continue = new string[2] // minititle25 continue
        {
            "Continue",
            "Continuă"
        };
        public string[] button26resetsalt = new string[2] // minititle26 reset salt
        {
            "Reset to default salt",
            "Resetați la salt-ul implicit"
        };
        public string[] button27changesalt = new string[2] // minititle27 change salt
        {
            "Change default salt",
            "Schimbați salt-ul implicit"
        };
        public string[] button28toggleconsole = new string[2] // minititle27 change salt
        {
            "TOGGLE CONSOLE",
            "COMUTĂ CONSOLĂ"
        };
        public string[] button29defsettingsback = new string[2] // button29 defsettings back
        {
            "← Back",
            "← Înapoi"
        };
    }
}
