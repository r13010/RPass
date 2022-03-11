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
    class Rlang
    {
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
            "Settings",
            "Setări"
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
        public string[] minititle18eula = new string[2] // minititle18 eula
        {
            "eula placeholder\r\n",
            "eula placeholder\r\n"
        };
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
        public string[] minititle24about = new string[2] // minititle24 about
        {
            "about placeholder" +
            "\r\n" +
            "\r\nVersion beta 1.00100322",
            "about placeholder" +
            "\r\n" +
            "\r\nVersiune beta 1.00100322"
        };
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
    }
}
