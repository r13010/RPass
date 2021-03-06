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
using ComponentFactory.Krypton.Toolkit;
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
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
            // Disable console
            Console("interface disable console");
            // Reset
            Console("reset");
        }

        // Other scripts stuff
        rpass.Rencrypt Rencrypt = new rpass.Rencrypt();
        rpass.Rlang Rlang = new rpass.Rlang();
        rpass.Rdefaults Rdefaults = new rpass.Rdefaults();
        rpass.Rfiles Rfile = new rpass.Rfiles();

        // Creating user sessions
        Rdefaults.user defaultUser;
        Rdefaults.user currentUser;

        public bool passwordShown = false;
        public bool isSure = false;
        public bool isChecksumCorrect = false;
        public bool isConsoleEnabled = false;
        public bool isConsoleToggleEnabled = false;

        public string currentNotificationTitle = "";
        public string currentNotificationContents = "";
        public string currentNotificationCommand = "";
        
        public int currentGeneratorSize = 15;

        public string currentAuthCommand = "";
        public string currentAuthFailCommand = "";

        // Creating passwords pool
        public List<string> passwordsPool_name = new List<string>();
        public List<string> passwordsPool_password = new List<string>();
        public List<string> passwordsPool_sitelink = new List<string>();
        public List<string> passwordsPool_description = new List<string>();
        public List<string> passwordsPool_oldpassword1 = new List<string>();
        public List<string> passwordsPool_oldpassword2 = new List<string>();
        public List<string> passwordsPool_oldpassword3 = new List<string>();
        
        // FUNCTIONS

        // BUTTONS
        private void Form1_Load(object sender, EventArgs e)
        {

        }
            // other elements
        private void kryptonListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console("interface show infopassword");
        }
        private void kryptonButtonCentralDash_Click(object sender, EventArgs e)
        {
            Console("interface show dashboard");
        }
            // create password
        private void kryptonButtonCreatepassCancel_Click(object sender, EventArgs e)
        {
            Console("interface show dashboard");
        }
        private void kryptonButtonCreatepassSave_Click(object sender, EventArgs e)
        {
            if (kryptonTextBoxPassword.Text != "" &&
                    kryptonTextBoxMasterPassword.Text != "" &&
                    kryptonTextBoxSiteLink.Text != "" &&
                    kryptonTextBoxDesc.Text != "")
            {
                // Verify master password
                if (kryptonTextBoxMasterPassword.Text == currentUser.masterPassword)
                {
                    // Reset color
                    kryptonLabelReq1.StateCommon.ShortText.Color1 = Color.Gray;
                    kryptonLabelReq1.StateCommon.ShortText.Color2 = Color.Gray;
                    kryptonLabelReq1.StateCommon.LongText.Color1 = Color.Gray;
                    kryptonLabelReq1.StateCommon.LongText.Color2 = Color.Gray;
                    // Save password in pool
                    kryptonListBox1.Items.Add(kryptonTextBoxName.Text);
                    passwordsPool_name.Add(Rencrypt.EncryptInterface(kryptonTextBoxName.Text, kryptonTextBoxMasterPassword.Text, currentUser.salt));
                    passwordsPool_password.Add(Rencrypt.EncryptInterface(kryptonTextBoxPassword.Text, kryptonTextBoxMasterPassword.Text, currentUser.salt));
                    passwordsPool_sitelink.Add(Rencrypt.EncryptInterface(kryptonTextBoxSiteLink.Text, kryptonTextBoxMasterPassword.Text, currentUser.salt));
                    passwordsPool_description.Add(Rencrypt.EncryptInterface(kryptonTextBoxDesc.Text, kryptonTextBoxMasterPassword.Text, currentUser.salt));
                    passwordsPool_oldpassword1.Add(Rencrypt.EncryptInterface("-", kryptonTextBoxMasterPassword.Text, currentUser.salt));
                    passwordsPool_oldpassword2.Add(Rencrypt.EncryptInterface("-", kryptonTextBoxMasterPassword.Text, currentUser.salt));
                    passwordsPool_oldpassword3.Add(Rencrypt.EncryptInterface("-", kryptonTextBoxMasterPassword.Text, currentUser.salt));
                    // Dashboard 
                    Console("interface show dashboard");
                }
                else
                {
                    Console("error createpassword reqforms");
                    kryptonTextBoxMasterPassword.Text = Rlang.error1[currentUser.language];
                }
            }
            else
            {
                for (int i = 2; i != 0; i--)
                {
                    Console("error createpassword reqforms");
                }
            }
        }
            // dashboard
        private void kryptonButtonDashboardCreate_Click(object sender, EventArgs e)
        {
            Console("interface show createpassword");
        }
        private void kryptonButtonDashboardForceexit_Click(object sender, EventArgs e)
        {
            if (isSure == false)
            {
                isSure = true;
                kryptonButtonForceexit.Text = Rlang.button14sure[currentUser.language];
                kryptonButtonForceexit.Refresh();
            }
            else
            {
                isSure = false;
                // log out
                Console("interface disable passwordlist");
                Console("interface disable dashbutton");
                Console("reset");
            }
        }
        private void kryptonButtonDashboardUsersettings_Click(object sender, EventArgs e)
        {
            Console("interface show settings");
        }
        private void kryptonButtonDashboardSaveandExit_Click(object sender, EventArgs e)
        {
            Console("auth save");
        }
        private void kryptonButtonRPassInfo_Click(object sender, EventArgs e)
        {
            Console("interface show about");
        }
        private void kryptonButtonGeneratepass_Click(object sender, EventArgs e)
        {
            Console("interface show generate");
        }
            // info password
        private void kryptonButtonInfopassShowHidd_Click(object sender, EventArgs e)
        {
            isSure = false;
            kryptonButtonInfopassDelete.Text = Rlang.button12delete[currentUser.language];
            kryptonButtonInfopassDelete.Refresh();
            if (passwordShown == false)
            {
                passwordShown = true;
                // show password
                kryptonTextBoxPasswordInfoshow.Text = Rencrypt.DecryptInterface(passwordsPool_password[kryptonListBox1.SelectedIndex], currentUser.masterPassword, currentUser.language, currentUser.salt);
                kryptonLabelSitelinkInfoshow.Text = Rencrypt.DecryptInterface(passwordsPool_sitelink[kryptonListBox1.SelectedIndex], currentUser.masterPassword, currentUser.language, currentUser.salt);
                kryptonLabelOldpass1.Text = Rencrypt.DecryptInterface(passwordsPool_oldpassword1[kryptonListBox1.SelectedIndex], currentUser.masterPassword, currentUser.language, currentUser.salt);
                kryptonLabelOldpass2.Text = Rencrypt.DecryptInterface(passwordsPool_oldpassword2[kryptonListBox1.SelectedIndex], currentUser.masterPassword, currentUser.language, currentUser.salt);
                kryptonLabelOldpass3.Text = Rencrypt.DecryptInterface(passwordsPool_oldpassword3[kryptonListBox1.SelectedIndex], currentUser.masterPassword, currentUser.language, currentUser.salt);
                kryptonLabelYourDescInfoshow.Text = Rencrypt.DecryptInterface(passwordsPool_description[kryptonListBox1.SelectedIndex], currentUser.masterPassword, currentUser.language, currentUser.salt);

                kryptonButtonInfopassShowHidd.Text = Rlang.button10hide[currentUser.language];
            }
            else
            {
                passwordShown = false;
                // hide password
                kryptonTextBoxPasswordInfoshow.Text = Rlang.minititle11hidden[currentUser.language];
                kryptonLabelSitelinkInfoshow.Text = Rlang.minititle11hidden[currentUser.language];
                kryptonLabelOldpass1.Text = Rlang.minititle11hidden[currentUser.language];
                kryptonLabelOldpass2.Text = Rlang.minititle11hidden[currentUser.language];
                kryptonLabelOldpass3.Text = Rlang.minititle11hidden[currentUser.language];
                kryptonLabelYourDescInfoshow.Text = Rlang.minititle11hidden[currentUser.language];

                kryptonButtonInfopassShowHidd.Text = Rlang.button9show[currentUser.language];
            }
        }
        private void kryptonButtonInfopassCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Rencrypt.DecryptInterface(passwordsPool_password[kryptonListBox1.SelectedIndex], currentUser.masterPassword, currentUser.language, currentUser.salt));
            kryptonTextBoxPasswordInfoshow.Text = Rlang.minititle12copied[currentUser.language];
            isSure = false;
            kryptonButtonInfopassDelete.Text = Rlang.button12delete[currentUser.language];
            kryptonButtonInfopassDelete.Refresh();
        }
        private void kryptonButtonInfopassDelete_Click(object sender, EventArgs e)
        {
            if (isSure == false)
            {
                isSure = true;
                kryptonButtonInfopassDelete.Text = Rlang.button14sure[currentUser.language];
                kryptonButtonInfopassDelete.Refresh();
            }
            else
            {
                isSure = false;
                kryptonButtonInfopassDelete.Text = Rlang.button12delete[currentUser.language];
                kryptonButtonInfopassDelete.Refresh();
                // delete current password
                passwordsPool_name.RemoveAt(kryptonListBox1.SelectedIndex);
                passwordsPool_password.RemoveAt(kryptonListBox1.SelectedIndex);
                passwordsPool_sitelink.RemoveAt(kryptonListBox1.SelectedIndex);
                passwordsPool_description.RemoveAt(kryptonListBox1.SelectedIndex);
                passwordsPool_oldpassword1.RemoveAt(kryptonListBox1.SelectedIndex);
                passwordsPool_oldpassword2.RemoveAt(kryptonListBox1.SelectedIndex);
                passwordsPool_oldpassword3.RemoveAt(kryptonListBox1.SelectedIndex);
                kryptonListBox1.Items.RemoveAt(kryptonListBox1.SelectedIndex);
                Console("interface show dashboard");
            }
        }
        private void kryptonButtonInfopassEdit_Click(object sender, EventArgs e)
        {
            isSure = false;
            kryptonButtonInfopassDelete.Text = Rlang.button12delete[currentUser.language];
            kryptonButtonInfopassDelete.Refresh();

            Console("interface show editpassword");
        }
        private void kryptonButtonInfopassOpenLink_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Rencrypt.DecryptInterface(passwordsPool_sitelink[kryptonListBox1.SelectedIndex], currentUser.masterPassword, currentUser.language, currentUser.salt));
            }
            catch
            {
                kryptonTextBoxPasswordInfoshow.Text = Rlang.minititle49invalidlink[currentUser.language];
            }
        }
        // edit password
        private void kryptonButtonEditpassSave_Click(object sender, EventArgs e)
        {
            if (kryptonTextBoxPassword.Text != "" &&
                    kryptonTextBoxMasterPassword.Text != "" &&
                    kryptonTextBoxSiteLink.Text != "" &&
                    kryptonTextBoxDesc.Text != "")
            {
                // Verify master password
                if (kryptonTextBoxMasterPassword.Text == currentUser.masterPassword)
                {
                    // Reset color
                    kryptonLabelReq1.StateCommon.ShortText.Color1 = Color.Gray;
                    kryptonLabelReq1.StateCommon.ShortText.Color2 = Color.Gray;
                    kryptonLabelReq1.StateCommon.LongText.Color1 = Color.Gray;
                    kryptonLabelReq1.StateCommon.LongText.Color2 = Color.Gray;
                    // Save edited password in pool
                    passwordsPool_name[kryptonListBox1.SelectedIndex] = Rencrypt.EncryptInterface(kryptonTextBoxName.Text, kryptonTextBoxMasterPassword.Text, currentUser.salt);
                    passwordsPool_oldpassword3[kryptonListBox1.SelectedIndex] = passwordsPool_oldpassword2[kryptonListBox1.SelectedIndex];
                    passwordsPool_oldpassword2[kryptonListBox1.SelectedIndex] = passwordsPool_oldpassword1[kryptonListBox1.SelectedIndex];
                    passwordsPool_oldpassword1[kryptonListBox1.SelectedIndex] = passwordsPool_password[kryptonListBox1.SelectedIndex];
                    passwordsPool_password[kryptonListBox1.SelectedIndex] = Rencrypt.EncryptInterface(kryptonTextBoxPassword.Text, kryptonTextBoxMasterPassword.Text, currentUser.salt);
                    passwordsPool_sitelink[kryptonListBox1.SelectedIndex] = Rencrypt.EncryptInterface(kryptonTextBoxSiteLink.Text, kryptonTextBoxMasterPassword.Text, currentUser.salt);
                    passwordsPool_description[kryptonListBox1.SelectedIndex] = Rencrypt.EncryptInterface(kryptonTextBoxDesc.Text, kryptonTextBoxMasterPassword.Text, currentUser.salt);
                    // Back to Infopassword
                    Console("interface show infopassword");
                }
                else
                {
                    for (int i = 2; i != 0; i--)
                    {
                        System.Threading.Thread.Sleep(100);
                        kryptonLabelReq1.StateCommon.ShortText.Color1 = Color.Gray;
                        kryptonLabelReq1.StateCommon.ShortText.Color2 = Color.Gray;
                        kryptonLabelReq1.StateCommon.LongText.Color1 = Color.Gray;
                        kryptonLabelReq1.StateCommon.LongText.Color2 = Color.Gray;
                        kryptonLabelReq1.Refresh();
                        System.Threading.Thread.Sleep(100);
                        kryptonLabelReq1.StateCommon.ShortText.Color1 = Color.Red;
                        kryptonLabelReq1.StateCommon.ShortText.Color2 = Color.Red;
                        kryptonLabelReq1.StateCommon.LongText.Color1 = Color.Red;
                        kryptonLabelReq1.StateCommon.LongText.Color2 = Color.Red;
                        kryptonLabelReq1.Refresh();
                    }
                    kryptonTextBoxMasterPassword.Text = Rlang.error1[currentUser.language];
                }

            }
            else
            {
                for (int i = 2; i != 0; i--)
                {
                    System.Threading.Thread.Sleep(100);
                    kryptonLabelReq1.StateCommon.ShortText.Color1 = Color.Gray;
                    kryptonLabelReq1.StateCommon.ShortText.Color2 = Color.Gray;
                    kryptonLabelReq1.StateCommon.LongText.Color1 = Color.Gray;
                    kryptonLabelReq1.StateCommon.LongText.Color2 = Color.Gray;
                    kryptonLabelReq1.Refresh();
                    System.Threading.Thread.Sleep(100);
                    kryptonLabelReq1.StateCommon.ShortText.Color1 = Color.Red;
                    kryptonLabelReq1.StateCommon.ShortText.Color2 = Color.Red;
                    kryptonLabelReq1.StateCommon.LongText.Color1 = Color.Red;
                    kryptonLabelReq1.StateCommon.LongText.Color2 = Color.Red;
                    kryptonLabelReq1.Refresh();
                }
            }
        }
        private void kryptonButtonEditpassCancel_Click(object sender, EventArgs e)
        {
            Console("interface show infopassword");
        }
            // settings
        private void kryptonComboBoxUsersettLangSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentUser.language = kryptonComboBoxLangSelector.SelectedIndex;
            Console("interface show settings");
        }
        private void kryptonComboBoxUsersettIfDarkTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (kryptonComboBoxIfDarkTheme.SelectedIndex == 0)
            {
                currentUser.ifDarkTheme = true;
            }
            else if (kryptonComboBoxIfDarkTheme.SelectedIndex == 1)
            {
                currentUser.ifDarkTheme = false;
            }
            // Reinit interface
            InterfaceHide_All();
            Console("interface disable dashbutton");
            Console("interface theme");
            Console("interface show settings");
        }
        private void kryptonButtonAccountChangeName_Click(object sender, EventArgs e)
        {
            Console("auth changename");
        }
        private void kryptonButtonAccountDel_Click(object sender, EventArgs e)
        {
            Console("auth deleteaccount1");
        }
        private void kryptonComboBoxProfileColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(kryptonComboBoxProfileColor.SelectedIndex)
            {
                case 0:
                    currentUser.icon = "blue";
                    break;

                case 1:
                    currentUser.icon = "gray";
                    break;

                case 2:
                    currentUser.icon = "green";
                    break;

                case 3:
                    currentUser.icon = "red";
                    break;

                case 4:
                    currentUser.icon = "yellow";
                    break;

                default:
                    currentUser.icon = "gray";
                    break;
            }
        }
            // login
        private void kryptonComboBoxLoginLangSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentUser.language = kryptonComboBoxLoginLangSelector.SelectedIndex;
            Console("interface show login");
            Console("save settings");
        }
        private void kryptonButtonLogin_Click(object sender, EventArgs e)
        {
            Console("login");
        }
        private void kryptonButtonLoginExit_Click(object sender, EventArgs e)
        {
            Console("quit");
        }
        private void kryptonButtonRegister_Click(object sender, EventArgs e)
        {
            Console("interface show eula");
        }
        private void kryptonButtonLoginDefaultSettings_Click(object sender, EventArgs e)
        {
            Console("interface show defaultsettings");
        }
            // eula
        private void kryptonButtonEulaCancel_Click(object sender, EventArgs e)
        {
            Console("reset");
        }
        private void kryptonButtonEulaAccept_Click(object sender, EventArgs e)
        {
            Console("interface show register");
        }
            // register
        private void kryptonButtonRegisterCancel_Click(object sender, EventArgs e)
        {
            Console("reset");
        }
        private void kryptonButtonRegisterReg_Click(object sender, EventArgs e)
        {
            Console("register");
        }
            // notification
        private void kryptonButtonNotification_Click(object sender, EventArgs e)
        {
            Console(currentNotificationCommand);
            currentNotificationCommand = "";
        }
            // generate
        private void kryptonButtonGenerateCreate_Click(object sender, EventArgs e)
        {
            Console("interface show createpassword");
            kryptonTextBoxPassword.Text = kryptonTextBoxGenerated.Text;
        }
        private void kryptonButtonGenerate_Click(object sender, EventArgs e)
        {
            Console("generate");
        }
        private void kryptonRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            currentGeneratorSize = 16;
        }
        private void kryptonRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            currentGeneratorSize = 32;
        }
        private void kryptonRadioButtonGeneratorSize3_CheckedChanged(object sender, EventArgs e)
        {
            currentGeneratorSize = 64;
        }
        private void kryptonRadioButtonGeneratorSize4_CheckedChanged(object sender, EventArgs e)
        {
            currentGeneratorSize = 128;
        }
        private void kryptonRadioButtonGeneratorSize5_CheckedChanged(object sender, EventArgs e)
        {
            currentGeneratorSize = 512;
        }
            // auth
        private void kryptonButtonAuth_Click(object sender, EventArgs e)
        {
            Console("error login reset");
            if (kryptonTextBoxLoginName.Text == currentUser.name && kryptonTextBoxLoginMasterpassword.Text == currentUser.masterPassword)
            {
                Console("error login reset");
                Console(currentAuthCommand);
            }
            else
            {
                Console("error login credentials");
            }
        }
        private void kryptonButtonAuthCancel_Click(object sender, EventArgs e)
        {
            Console(currentAuthFailCommand);
        }
            // changename
        private void kryptonButtonChangenameCancel_Click(object sender, EventArgs e)
        {
            Console("interface show settings");
        }
        private void kryptonButtonChangenameSave_Click(object sender, EventArgs e)
        {
            Console("error changename reset");
            if (kryptonTextBoxChangename.Text == "")
            {
                Console("error changename blank");
            }
            else
            {
                // Change name
                if (Rfile.UserExists(kryptonTextBoxChangename.Text))
                {
                    Console("error changename blank");
                }
                else
                {
                    try
                    {
                        File.Delete(Rfile.savesPath + @"\" + currentUser.name + ".rcrypt");
                    }
                    catch
                    { }
                    currentUser.name = kryptonTextBoxChangename.Text;
                    kryptonTextBoxLoginName.Text = kryptonTextBoxChangename.Text;
                    Console("save user");
                }
            }
        }
            // defaultsettings
        private void kryptonButtonDefaultSettingsBack_Click(object sender, EventArgs e)
        {
            Console("reset");
        }
        private void kryptonButtonConsoleToggle_Click(object sender, EventArgs e)
        {
            if (isConsoleEnabled)
            {
                Console("interface disable console");
                isConsoleEnabled = false;
            }
            else
            {
                Console("interface enable console");
                isConsoleEnabled = true;
            }
        }
        private void kryptonComboBoxDefaultSettingsIfDarkTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (kryptonComboBoxDefaultSettingsIfDarkTheme.SelectedIndex == 0)
            {
                currentUser.ifDarkTheme = true;
            }
            else if (kryptonComboBoxDefaultSettingsIfDarkTheme.SelectedIndex == 1)
            {
                currentUser.ifDarkTheme = false;
            }
            // Reinit interface
            InterfaceHide_All();
            Console("interface theme");
            Console("interface show defaultsettings");
            Console("save settings");
        }
        private void kryptonButtonDefaultSettingsResetsalt_Click(object sender, EventArgs e)
        {
            defaultUser.salt = Rdefaults.defaultSalt;
            Console("save settings");
            Console("notification saltreset");
        }
        private void kryptonButtonDefaultSettingsOpensaves_Click(object sender, EventArgs e)
        {
            Console("open savesfolder");
        }
        private void kryptonButtonDefaultSettingsReset_Click(object sender, EventArgs e)
        {
            // Reset
            Console("load defaultsettings");
            kryptonTextBoxLoginName.Text = "";
            defaultUser.salt = Rdefaults.defaultSalt;
            Console("save settings");
            // Restart
            Console("reset");
        }
        private void kryptonButtonDefaultSettingsChangesalt_Click(object sender, EventArgs e)
        {
            Console("interface show changesalt");
        }
        // console
        private void kryptonButtonConsoleSend_Click(object sender, EventArgs e)
        {
            Console(kryptonTextBoxConsoleCommand.Text);
        }
        private void kryptonButtonDefaultSettingsOpenbackups_Click(object sender, EventArgs e)
        {
            Console("open backupsfolder");
        }
            // changesalt
        private void kryptonButtonChangeSaltCancel_Click(object sender, EventArgs e)
        {
            Console("interface show defaultsettings");
        }
        private void kryptonButtonChangeSaltSave_Click(object sender, EventArgs e)
        {
            Console("changesalt");
        }






        // INTERFACE
        public void InterfaceHide_All()
        {
            // other elements
            kryptonButtonInfopassDelete.Text = Rlang.button12delete[currentUser.language];
            kryptonButtonInfopassDelete.Refresh();
            passwordShown = false;
            isSure = false;

            Console("interface hide createpassword");
            Console("interface hide dashboard");
            Console("interface hide settings");
            Console("interface hide infopassword");
            Console("interface hide editpassword");
            Console("interface hide login");
            Console("interface hide eula");
            Console("interface hide register");
            Console("interface hide about");
            Console("interface hide notification");
            Console("interface hide generate");
            Console("interface hide auth");
            Console("interface hide changename");
            Console("interface hide defaultsettings");
            Console("interface hide changesalt");
        }

        // CONSOLE
        public void Console(string command)
        {
            switch (command)
            {
                default:
                    kryptonTextBoxConsoleCommand.Text = "invalid command";
                    break;

                case "quit":
                    Environment.Exit(0);
                    break;

                case "interface show createpassword":
                    // hide all interfaces
                    Console("interface enable dashbutton");
                    Console("interface enable passwordlist");

                    InterfaceHide_All();
                    // language
                    kryptonLabelBigTitlePass.Text = Rlang.title1createnewpass[currentUser.language];
                    kryptonLabelName.Text = Rlang.minititle6name[currentUser.language];
                    kryptonLabelPasswordtobesaved.Text = Rlang.minititle1passtobesaved[currentUser.language];
                    kryptonLabelMasterpassword.Text = Rlang.minititle2masterpass[currentUser.language];
                    kryptonLabelSitelink.Text = Rlang.minititle3sitelnk[currentUser.language];
                    kryptonLabelDescription.Text = Rlang.minititle4desc[currentUser.language];
                    kryptonLabelReq1.Text = Rlang.minititle5req1[currentUser.language];

                    kryptonButtonSave.Text = Rlang.button1save[currentUser.language];
                    kryptonButtonCancel.Text = Rlang.button2cancel[currentUser.language];
                    // show
                    kryptonLabelBigTitlePass.Location = new System.Drawing.Point(276, 12);
                    kryptonLabelBigTitlePass.Visible = true;

                    kryptonLabelName.Location = new System.Drawing.Point(276, 95);
                    kryptonLabelName.Visible = true;
                    kryptonTextBoxName.Location = new System.Drawing.Point(276, 121);
                    kryptonTextBoxName.Text = "";
                    kryptonTextBoxName.Visible = true;

                    kryptonLabelPasswordtobesaved.Location = new System.Drawing.Point(276, 160);
                    kryptonLabelPasswordtobesaved.Visible = true;
                    kryptonTextBoxPassword.Location = new System.Drawing.Point(276, 186);
                    kryptonTextBoxPassword.Text = "";
                    kryptonTextBoxPassword.Visible = true;

                    kryptonLabelMasterpassword.Location = new System.Drawing.Point(276, 225);
                    kryptonLabelMasterpassword.Visible = true;
                    kryptonTextBoxMasterPassword.Location = new System.Drawing.Point(276, 251);
                    kryptonTextBoxMasterPassword.Text = "";
                    kryptonTextBoxMasterPassword.Visible = true;

                    kryptonLabelSitelink.Location = new System.Drawing.Point(276, 290);
                    kryptonLabelSitelink.Visible = true;
                    kryptonTextBoxSiteLink.Location = new System.Drawing.Point(276, 316);
                    kryptonTextBoxSiteLink.Text = "";
                    kryptonTextBoxSiteLink.Visible = true;

                    kryptonLabelDescription.Location = new System.Drawing.Point(276, 355);
                    kryptonLabelDescription.Visible = true;
                    kryptonTextBoxDesc.Location = new System.Drawing.Point(276, 381);
                    kryptonTextBoxDesc.Text = "";
                    kryptonTextBoxDesc.Visible = true;

                    kryptonLabelReq1.Location = new System.Drawing.Point(276, 525);
                    kryptonLabelReq1.Visible = true;
                    kryptonLabelReq1.StateCommon.ShortText.Color1 = Color.Gray;
                    kryptonLabelReq1.StateCommon.ShortText.Color2 = Color.Gray;
                    kryptonLabelReq1.StateCommon.LongText.Color1 = Color.Gray;
                    kryptonLabelReq1.StateCommon.LongText.Color2 = Color.Gray;

                    kryptonButtonSave.Location = new System.Drawing.Point(276, 56);
                    kryptonButtonSave.Visible = true;
                    kryptonButtonCancel.Location = new System.Drawing.Point(363, 56);
                    kryptonButtonCancel.Visible = true;
                    break;

                case "interface hide createpassword":
                    kryptonLabelBigTitlePass.Visible = false;
                    kryptonTextBoxPassword.Visible = false;

                    kryptonLabelName.Visible = false;
                    kryptonTextBoxName.Visible = false;

                    kryptonLabelPasswordtobesaved.Visible = false;
                    kryptonTextBoxPassword.Visible = false;

                    kryptonLabelMasterpassword.Visible = false;
                    kryptonTextBoxMasterPassword.Visible = false;

                    kryptonLabelSitelink.Visible = false;
                    kryptonTextBoxSiteLink.Visible = false;

                    kryptonLabelDescription.Visible = false;
                    kryptonTextBoxDesc.Visible = false;

                    kryptonLabelReq1.Visible = false;

                    kryptonButtonSave.Visible = false;
                    kryptonButtonCancel.Visible = false;
                    break;

                case "interface enable dashbutton":
                    kryptonButtonCentralDash.Text = Rlang.button8centraldashboard[currentUser.language];
                    kryptonButtonCentralDash.Location = new System.Drawing.Point(0, 12);
                    kryptonButtonCentralDash.Visible = true;
                    break;

                case "interface disable dashbutton":
                    kryptonButtonCentralDash.Visible = false;
                    break;

                case "interface enable passwordlist":
                    kryptonListBox1.Location = new System.Drawing.Point(0, 51);
                    kryptonListBox1.Visible = true;
                    break;

                case "interface disable passwordlist":
                    kryptonListBox1.Visible = false;
                    break;

                case "interface show dashboard":
                    // hide all interfaces
                    Console("interface disable dashbutton");
                    Console("interface enable passwordlist");

                    InterfaceHide_All();
                    // language
                    kryptonButtonCentralDash.Text = Rlang.button8centraldashboard[currentUser.language];

                    kryptonLabelBigTitleDashboard.Text = Rlang.title2dashboard[currentUser.language];
                    kryptonLabelDashboardpass.Text = Rlang.minititle7dashpass[currentUser.language];
                    kryptonLabelDashboardaccount.Text = Rlang.minititle8dashaccount[currentUser.language];

                    kryptonButtonGeneratepass.Text = Rlang.button3generate[currentUser.language];
                    kryptonButtonDashboardCreate.Text = Rlang.button4createpass[currentUser.language];
                    kryptonButtonUsersettings.Text = Rlang.button5usersettings[currentUser.language];
                    kryptonButtonDashboardSaveandExit.Text = Rlang.button6saveexit[currentUser.language];
                    kryptonButtonForceexit.Text = Rlang.button7forceexit[currentUser.language];

                    kryptonLabelDashboardusername.Text = currentUser.name;
                    // show
                    kryptonLabelBigTitleDashboard.Location = new System.Drawing.Point(276, 12);
                    kryptonLabelBigTitleDashboard.Visible = true;

                    kryptonLabelDashboardusername.Location = new System.Drawing.Point(276, 148);
                    kryptonLabelDashboardusername.Visible = true;

                    pictureBoxUsergray.Location = new System.Drawing.Point(276, 56);
                    pictureBoxUserblue.Location = new System.Drawing.Point(276, 56);
                    pictureBoxUsergreen.Location = new System.Drawing.Point(276, 56);
                    pictureBoxUserred.Location = new System.Drawing.Point(276, 56);
                    pictureBoxUseryellow.Location = new System.Drawing.Point(276, 56);
                    Console("interface enable profilepicture");

                    kryptonLabelDashboardpass.Location = new System.Drawing.Point(276, 197);
                    kryptonLabelDashboardpass.Visible = true;

                    kryptonLabelDashboardaccount.Location = new System.Drawing.Point(276, 320);
                    kryptonLabelDashboardaccount.Visible = true;

                    kryptonButtonGeneratepass.Location = new System.Drawing.Point(276, 223);
                    kryptonButtonGeneratepass.Visible = true;

                    kryptonButtonDashboardCreate.Location = new System.Drawing.Point(276, 262);
                    kryptonButtonDashboardCreate.Visible = true;

                    kryptonButtonUsersettings.Location = new System.Drawing.Point(276, 346);
                    kryptonButtonUsersettings.Visible = true;

                    kryptonButtonDashboardSaveandExit.Location = new System.Drawing.Point(276, 385);
                    kryptonButtonDashboardSaveandExit.Visible = true;

                    kryptonButtonForceexit.Location = new System.Drawing.Point(276, 424);
                    kryptonButtonForceexit.Visible = true;
                    break;

                case "interface hide dashboard":
                    kryptonLabelBigTitleDashboard.Visible = false;
                    kryptonLabelDashboardusername.Visible = false;

                    Console("interface disable profilepicture");

                    kryptonLabelDashboardpass.Visible = false;
                    kryptonLabelDashboardaccount.Visible = false;
                    kryptonButtonGeneratepass.Visible = false;
                    kryptonButtonDashboardCreate.Visible = false;
                    kryptonButtonUsersettings.Visible = false;
                    kryptonButtonDashboardSaveandExit.Visible = false;
                    kryptonButtonForceexit.Visible = false;
                    break;

                case "interface show settings":
                    // hide all interfaces
                    Console("interface enable dashbutton");
                    Console("interface disable passwordlist");

                    InterfaceHide_All();
                    // language
                    kryptonLabelBigTitleSett.Text = Rlang.title5sett[currentUser.language];

                    kryptonLabelSettLang.Text = Rlang.minititle14lang[currentUser.language];
                    kryptonLabelSettTheme.Text = Rlang.minititle15theme[currentUser.language];
                    kryptonLabelSettIcon.Text = Rlang.minititle16profilec[currentUser.language];

                    kryptonButtonAccountChangeName.Text = Rlang.button15accountname[currentUser.language];
                    kryptonButtonAccountDel.Text = Rlang.button16accountdel[currentUser.language];
                    kryptonButtonRPassInfo.Text = Rlang.button17softwareinfo[currentUser.language];
                    // show
                    kryptonLabelBigTitleSett.Location = new System.Drawing.Point(276, 12);
                    kryptonLabelBigTitleSett.Visible = true;

                    kryptonLabelSettLang.Location = new System.Drawing.Point(276, 56);
                    kryptonLabelSettLang.Visible = true;
                    kryptonLabelSettTheme.Location = new System.Drawing.Point(276, 117);
                    kryptonLabelSettTheme.Visible = true;
                    kryptonLabelSettIcon.Location = new System.Drawing.Point(276, 178);
                    kryptonLabelSettIcon.Visible = true;
                    kryptonComboBoxLangSelector.Location = new System.Drawing.Point(276, 82);
                    kryptonComboBoxLangSelector.Visible = true;
                    kryptonComboBoxIfDarkTheme.Location = new System.Drawing.Point(276, 143);
                    kryptonComboBoxIfDarkTheme.Visible = true;
                    kryptonComboBoxProfileColor.Location = new System.Drawing.Point(276, 204);
                    kryptonComboBoxProfileColor.Visible = true;

                    if(currentUser.name == "root")
                    {
                        // Don't show
                    }
                    else
                    {
                        kryptonButtonAccountChangeName.Location = new System.Drawing.Point(276, 329);
                        kryptonButtonAccountChangeName.Visible = true;
                        kryptonButtonAccountDel.Location = new System.Drawing.Point(276, 368);
                        kryptonButtonAccountDel.Visible = true;
                    }
                    kryptonButtonRPassInfo.Location = new System.Drawing.Point(276, 441);
                    kryptonButtonRPassInfo.Visible = true;
                    break;

                case "interface hide settings":
                    kryptonLabelBigTitleSett.Visible = false;

                    kryptonLabelSettLang.Visible = false;
                    kryptonLabelSettTheme.Visible = false;
                    kryptonLabelSettIcon.Visible = false;
                    kryptonComboBoxLangSelector.Visible = false;
                    kryptonComboBoxIfDarkTheme.Visible = false;
                    kryptonComboBoxProfileColor.Visible = false;

                    kryptonButtonAccountChangeName.Visible = false;
                    kryptonButtonAccountDel.Visible = false;
                    kryptonButtonRPassInfo.Visible = false;
                    break;

                case "interface show infopassword":
                    // hide all interfaces
                    Console("interface enable dashbutton");
                    Console("interface enable passwordlist");

                    InterfaceHide_All();
                    // language
                    try
                    {
                        kryptonLabelBigTitleInfoshow.Text = Rlang.title3password[currentUser.language] + kryptonListBox1.Items[kryptonListBox1.SelectedIndex];
                    }
                    catch
                    {
                        kryptonLabelBigTitleInfoshow.Text = Rlang.title3password[currentUser.language];
                    }
                    kryptonLabelPasswordsavedInfoshow.Text = Rlang.minititle9savedpassword[currentUser.language];
                    kryptonTextBoxPasswordInfoshow.Text = Rlang.minititle11hidden[currentUser.language];
                    kryptonLabelSitelinkInfoshow.Text = Rlang.minititle11hidden[currentUser.language];
                    kryptonLabelOldpasswordsInfoshow.Text = Rlang.minititle10oldpasswords[currentUser.language];
                    kryptonLabelOldpass1.Text = Rlang.minititle11hidden[currentUser.language];
                    kryptonLabelOldpass2.Text = Rlang.minititle11hidden[currentUser.language];
                    kryptonLabelOldpass3.Text = Rlang.minititle11hidden[currentUser.language];
                    kryptonLabelDescInfoshow.Text = Rlang.minititle4desc[currentUser.language];
                    kryptonLabelYourDescInfoshow.Text = Rlang.minititle11hidden[currentUser.language];

                    kryptonButtonInfopassShowHidd.Text = Rlang.button9show[currentUser.language];
                    kryptonButtonInfopassEdit.Text = Rlang.button11edit[currentUser.language];
                    kryptonButtonInfopassDelete.Text = Rlang.button12delete[currentUser.language];
                    kryptonButtonInfopassCopy.Text = Rlang.button13copy[currentUser.language];
                    // show
                    kryptonLabelBigTitleInfoshow.Location = new System.Drawing.Point(276, 12);
                    kryptonLabelBigTitleInfoshow.Visible = true;
                    kryptonLabelPasswordsavedInfoshow.Location = new System.Drawing.Point(276, 95);
                    kryptonLabelPasswordsavedInfoshow.Visible = true;
                    kryptonTextBoxPasswordInfoshow.Location = new System.Drawing.Point(276, 121);
                    kryptonTextBoxPasswordInfoshow.Visible = true;
                    kryptonLabelSitelinkInfoshow.Location = new System.Drawing.Point(276, 160);
                    kryptonLabelSitelinkInfoshow.Visible = true;
                    kryptonLabelOldpasswordsInfoshow.Location = new System.Drawing.Point(276, 186);
                    kryptonLabelOldpasswordsInfoshow.Visible = true;
                    kryptonLabelOldpass1.Location = new System.Drawing.Point(276, 212);
                    kryptonLabelOldpass1.Visible = true;
                    kryptonLabelOldpass2.Location = new System.Drawing.Point(276, 238);
                    kryptonLabelOldpass2.Visible = true;
                    kryptonLabelOldpass3.Location = new System.Drawing.Point(276, 264);
                    kryptonLabelOldpass3.Visible = true;
                    kryptonLabelDescInfoshow.Location = new System.Drawing.Point(276, 290);
                    kryptonLabelDescInfoshow.Visible = true;
                    kryptonLabelYourDescInfoshow.Location = new System.Drawing.Point(276, 316);
                    kryptonLabelYourDescInfoshow.Visible = true;

                    kryptonButtonInfopassShowHidd.Location = new System.Drawing.Point(276, 56);
                    kryptonButtonInfopassShowHidd.Visible = true;
                    kryptonButtonInfopassEdit.Location = new System.Drawing.Point(363, 56);
                    kryptonButtonInfopassEdit.Visible = true;
                    kryptonButtonInfopassDelete.Location = new System.Drawing.Point(450, 56);
                    kryptonButtonInfopassDelete.Visible = true;
                    kryptonButtonInfopassCopy.Location = new System.Drawing.Point(537, 56);
                    kryptonButtonInfopassCopy.Visible = true;
                    kryptonButtonInfopassOpenLink.Location = new System.Drawing.Point(624, 56);
                    kryptonButtonInfopassOpenLink.Visible = true;
                    break;

                case "interface hide infopassword":
                    kryptonLabelBigTitleInfoshow.Visible = false;
                    kryptonLabelPasswordsavedInfoshow.Visible = false;
                    kryptonTextBoxPasswordInfoshow.Visible = false;
                    kryptonLabelSitelinkInfoshow.Visible = false;
                    kryptonLabelOldpasswordsInfoshow.Visible = false;
                    kryptonLabelOldpass1.Visible = false;
                    kryptonLabelOldpass2.Visible = false;
                    kryptonLabelOldpass3.Visible = false;
                    kryptonLabelDescInfoshow.Visible = false;
                    kryptonLabelYourDescInfoshow.Visible = false;

                    kryptonButtonInfopassShowHidd.Visible = false;
                    kryptonButtonInfopassEdit.Visible = false;
                    kryptonButtonInfopassDelete.Visible = false;
                    kryptonButtonInfopassCopy.Visible = false;
                    kryptonButtonInfopassOpenLink.Visible = false;
                    break;

                case "interface show editpassword":
                    // hide all interfaces
                    Console("interface enable dashbutton");
                    Console("interface disable passwordlist");

                    InterfaceHide_All();
                    // language
                    try
                    {
                        kryptonLabelBigTitlePass.Text = Rlang.title4edit[currentUser.language] + kryptonListBox1.Items[kryptonListBox1.SelectedIndex];
                    }
                    catch
                    {
                        kryptonLabelBigTitlePass.Text = Rlang.title4edit[currentUser.language];
                    }
                    kryptonLabelName.Text = Rlang.minititle6name[currentUser.language];
                    kryptonLabelPasswordtobesaved.Text = Rlang.minititle1passtobesaved[currentUser.language];
                    kryptonLabelMasterpassword.Text = Rlang.minititle2masterpass[currentUser.language];
                    kryptonLabelSitelink.Text = Rlang.minititle3sitelnk[currentUser.language];
                    kryptonLabelDescription.Text = Rlang.minititle4desc[currentUser.language];
                    kryptonLabelReq1.Text = Rlang.minititle13req2[currentUser.language];

                    kryptonButtonEditSave.Text = Rlang.button1save[currentUser.language];
                    kryptonButtonEditCancel.Text = Rlang.button2cancel[currentUser.language];
                    // show
                    kryptonLabelBigTitlePass.Location = new System.Drawing.Point(276, 12);
                    kryptonLabelBigTitlePass.Visible = true;

                    kryptonLabelName.Location = new System.Drawing.Point(276, 95);
                    kryptonLabelName.Visible = true;
                    kryptonTextBoxName.Location = new System.Drawing.Point(276, 121);
                    kryptonTextBoxName.Text = Rencrypt.DecryptInterface(passwordsPool_name[kryptonListBox1.SelectedIndex], currentUser.masterPassword, currentUser.language, currentUser.salt);
                    kryptonTextBoxName.Visible = true;

                    kryptonLabelPasswordtobesaved.Location = new System.Drawing.Point(276, 160);
                    kryptonLabelPasswordtobesaved.Visible = true;
                    kryptonTextBoxPassword.Location = new System.Drawing.Point(276, 186);
                    kryptonTextBoxPassword.Text = Rencrypt.DecryptInterface(passwordsPool_password[kryptonListBox1.SelectedIndex], currentUser.masterPassword, currentUser.language, currentUser.salt);
                    kryptonTextBoxPassword.Visible = true;

                    kryptonLabelMasterpassword.Location = new System.Drawing.Point(276, 225);
                    kryptonLabelMasterpassword.Visible = true;
                    kryptonTextBoxMasterPassword.Location = new System.Drawing.Point(276, 251);
                    kryptonTextBoxMasterPassword.Text = "";
                    kryptonTextBoxMasterPassword.Visible = true;

                    kryptonLabelSitelink.Location = new System.Drawing.Point(276, 290);
                    kryptonLabelSitelink.Visible = true;
                    kryptonTextBoxSiteLink.Location = new System.Drawing.Point(276, 316);
                    kryptonTextBoxSiteLink.Text = Rencrypt.DecryptInterface(passwordsPool_sitelink[kryptonListBox1.SelectedIndex], currentUser.masterPassword, currentUser.language, currentUser.salt);
                    kryptonTextBoxSiteLink.Visible = true;

                    kryptonLabelDescription.Location = new System.Drawing.Point(276, 355);
                    kryptonLabelDescription.Visible = true;
                    kryptonTextBoxDesc.Location = new System.Drawing.Point(276, 381);
                    kryptonTextBoxDesc.Text = Rencrypt.DecryptInterface(passwordsPool_description[kryptonListBox1.SelectedIndex], currentUser.masterPassword, currentUser.language, currentUser.salt);
                    kryptonTextBoxDesc.Visible = true;

                    kryptonLabelReq1.Location = new System.Drawing.Point(276, 525);
                    kryptonLabelReq1.Visible = true;
                    kryptonLabelReq1.StateCommon.ShortText.Color1 = Color.Gray;
                    kryptonLabelReq1.StateCommon.ShortText.Color2 = Color.Gray;
                    kryptonLabelReq1.StateCommon.LongText.Color1 = Color.Gray;
                    kryptonLabelReq1.StateCommon.LongText.Color2 = Color.Gray;

                    kryptonButtonEditSave.Location = new System.Drawing.Point(276, 56);
                    kryptonButtonEditSave.Visible = true;
                    kryptonButtonEditCancel.Location = new System.Drawing.Point(363, 56);
                    kryptonButtonEditCancel.Visible = true;
                    break;

                case "interface hide editpassword":
                    kryptonLabelBigTitleInfoshow.Visible = false;
                    kryptonLabelPasswordsavedInfoshow.Visible = false;
                    kryptonTextBoxPasswordInfoshow.Visible = false;
                    kryptonLabelSitelinkInfoshow.Visible = false;
                    kryptonLabelOldpasswordsInfoshow.Visible = false;
                    kryptonLabelOldpass1.Visible = false;
                    kryptonLabelOldpass2.Visible = false;
                    kryptonLabelOldpass3.Visible = false;
                    kryptonLabelDescInfoshow.Visible = false;
                    kryptonLabelYourDescInfoshow.Visible = false;

                    kryptonButtonEditSave.Visible = false;
                    kryptonButtonEditCancel.Visible = false;
                    break;

                case "interface show login":
                    // hide all interfaces
                    Console("interface disable dashbutton");
                    Console("interface disable passwordlist");

                    InterfaceHide_All();
                    // language
                    kryptonLabelBigTitleLogin.Text = Rlang.title6login[currentUser.language];
                    kryptonLabelLoginName.Text = Rlang.minititle6name[currentUser.language];
                    kryptonLabelLoginMasterpassword.Text = Rlang.minititle2masterpass[currentUser.language];
                    kryptonLabelLoginLang.Text = Rlang.minititle17langlang[currentUser.language];

                    kryptonButtonLogin.Text = Rlang.button18login[currentUser.language];
                    kryptonButtonRegister.Text = Rlang.button19register[currentUser.language];
                    kryptonButtonLoginExit.Text = Rlang.button20loginexit[currentUser.language];
                    kryptonButtonLoginDefaultSettings.Text = Rlang.title14defsettings[currentUser.language];

                    kryptonLabelLoginError.Text = Rlang.error2[currentUser.language];
                    // show
                    pictureBoxLoginIcon.Location = new System.Drawing.Point(384, 13);
                    pictureBoxLoginIcon.Visible = true;

                    kryptonLabelBigTitleLogin.Location = new System.Drawing.Point(322, 103);
                    kryptonLabelBigTitleLogin.Visible = true;
                    kryptonLabelLoginName.Location = new System.Drawing.Point(322, 147);
                    kryptonLabelLoginName.Visible = true;
                    kryptonLabelLoginMasterpassword.Location = new System.Drawing.Point(322, 212);
                    kryptonLabelLoginMasterpassword.Visible = true;
                    kryptonLabelLoginLang.Location = new System.Drawing.Point(322, 382);
                    kryptonLabelLoginLang.Visible = true;

                    kryptonButtonLogin.Location = new System.Drawing.Point(322, 277);
                    kryptonButtonLogin.Visible = true;
                    kryptonButtonRegister.Location = new System.Drawing.Point(322, 343);
                    kryptonButtonRegister.Visible = true;
                    kryptonButtonLoginExit.Location = new System.Drawing.Point(457, 343);
                    kryptonButtonLoginExit.Visible = true;
                    kryptonButtonLoginDefaultSettings.Location = new System.Drawing.Point(322, 445);
                    kryptonButtonLoginDefaultSettings.Visible = true;

                    kryptonTextBoxLoginName.Location = new System.Drawing.Point(322, 173);
                    kryptonTextBoxLoginName.Visible = true;
                    kryptonTextBoxLoginMasterpassword.Location = new System.Drawing.Point(322, 238);
                    kryptonTextBoxLoginMasterpassword.Text = "";
                    kryptonTextBoxLoginMasterpassword.Visible = true;

                    kryptonComboBoxLoginLangSelector.Location = new System.Drawing.Point(322, 407);
                    kryptonComboBoxLoginLangSelector.Visible = true;
                    kryptonLabelLoginError.Location = new System.Drawing.Point(322, 318);
                    kryptonLabelLoginError.Visible = false;
                    break;

                case "interface hide login":
                    pictureBoxLoginIcon.Visible = false;

                    kryptonLabelBigTitleLogin.Visible = false;
                    kryptonLabelLoginName.Visible = false;
                    kryptonLabelLoginMasterpassword.Visible = false;
                    kryptonLabelLoginLang.Visible = false;

                    kryptonButtonLogin.Visible = false;
                    kryptonButtonRegister.Visible = false;
                    kryptonButtonLoginExit.Visible = false;
                    kryptonButtonLoginDefaultSettings.Visible = false;

                    kryptonTextBoxLoginName.Visible = false;
                    kryptonTextBoxLoginMasterpassword.Text = "";
                    kryptonTextBoxLoginMasterpassword.Visible = false;

                    kryptonComboBoxLoginLangSelector.Visible = false;
                    kryptonLabelLoginError.Visible = false;
                    break;

                case "interface show eula":
                    // hide all interfaces
                    Console("interface disable dashbutton");
                    Console("interface disable passwordlist");

                    InterfaceHide_All();
                    // language
                    kryptonLabelBigTitleEula.Text = Rlang.title7eula[currentUser.language];

                    kryptonButtonEulaAccept.Text = Rlang.button21eulaaccept[currentUser.language];
                    kryptonButtonEulaCancel.Text = Rlang.button22eularefuse[currentUser.language];

                    kryptonTextBoxEulaterms.Text = Rlang.minititle18eula[currentUser.language];
                    // show
                    kryptonLabelBigTitleEula.Location = new System.Drawing.Point(12, 12);
                    kryptonLabelBigTitleEula.Visible = true;

                    kryptonButtonEulaAccept.Location = new System.Drawing.Point(12, 56);
                    kryptonButtonEulaAccept.Visible = true;
                    kryptonButtonEulaCancel.Location = new System.Drawing.Point(178, 56);
                    kryptonButtonEulaCancel.Visible = true;

                    kryptonTextBoxEulaterms.Location = new System.Drawing.Point(12, 95);
                    kryptonTextBoxEulaterms.Visible = true;
                    break;

                case "interface hide eula":
                    kryptonLabelBigTitleEula.Visible = false;

                    kryptonButtonEulaAccept.Visible = false;
                    kryptonButtonEulaCancel.Visible = false;

                    kryptonTextBoxEulaterms.Visible = false;
                    break;

                case "reset":
                    // Initialize default user settings
                    defaultUser.masterPassword = Rdefaults.defaultMasterPassword;
                    defaultUser.language = Rdefaults.defaultLanguage;
                    defaultUser.name = Rdefaults.defaultUserName;
                    defaultUser.salt = Rdefaults.defaultSalt;
                    defaultUser.icon = Rdefaults.defaultIcon;
                    defaultUser.ifDarkTheme = Rdefaults.defaultIfDarkTheme;
                    // Initialize current user settings
                    Console("load settings");
                    // Clear password pool
                    kryptonListBox1.Items.Clear();
                    passwordsPool_name.Clear();
                    passwordsPool_password.Clear();
                    passwordsPool_sitelink.Clear();
                    passwordsPool_description.Clear();
                    passwordsPool_oldpassword1.Clear();
                    passwordsPool_oldpassword2.Clear();
                    passwordsPool_oldpassword3.Clear();
                    kryptonListBox1.ClearSelected();
                    // Interface and other functions
                    InterfaceHide_All();
                    Console("interface theme");
                    Console("interface show login");
                    Rfile.CreatePaths();
                    break;

                case "save user":
                    if (currentUser.name == "root")
                    {
                        // Can't save root
                        Console("notification rootsave");
                    }
                    else
                    {
                        // SAVE USER
                        // Create structure
                        List<string> linesToSave = new List<string>();
                        linesToSave.Clear();

                        linesToSave.Add(Rencrypt.EncryptInterface(Rfile.key1, Rfile.key1, defaultUser.salt)); // Line 1: First key, user info
                        linesToSave.Add(Rencrypt.EncryptInterface(currentUser.masterPassword, currentUser.masterPassword, defaultUser.salt)); // Line 2: User’s master password
                        linesToSave.Add(Rencrypt.EncryptInterface(currentUser.language.ToString(), currentUser.masterPassword, currentUser.salt)); // Line 3: User’s language setting
                        linesToSave.Add(Rencrypt.EncryptInterface(currentUser.name, currentUser.masterPassword, currentUser.salt)); // Line 4: User’s name
                        linesToSave.Add(Rencrypt.EncryptInterface(currentUser.salt, currentUser.masterPassword, defaultUser.salt)); // Line 5: User’s salt
                        linesToSave.Add(Rencrypt.EncryptInterface(currentUser.icon, currentUser.masterPassword, currentUser.salt)); // Line 6: User’s icon color setting
                        linesToSave.Add(Rencrypt.EncryptInterface(currentUser.ifDarkTheme.ToString(), currentUser.masterPassword, currentUser.salt)); // Line 7: User’s theme setting

                        linesToSave.Add(Rencrypt.EncryptInterface(Rfile.key2, Rfile.key2, defaultUser.salt)); // Line 8: Second key, user passwords
                        if (passwordsPool_name.Count == 0)
                        {
                            // No passwords stored, nothing to save
                        }
                        else
                        {
                            // Saves all the passwords found in the pool
                            for (int poolIndex1 = 0; passwordsPool_name.Count > poolIndex1; poolIndex1++)
                            {
                                linesToSave.Add(passwordsPool_name[poolIndex1]);
                                linesToSave.Add(passwordsPool_password[poolIndex1]);
                                linesToSave.Add(passwordsPool_sitelink[poolIndex1]);
                                linesToSave.Add(passwordsPool_description[poolIndex1]);
                                linesToSave.Add(passwordsPool_oldpassword1[poolIndex1]);
                                linesToSave.Add(passwordsPool_oldpassword2[poolIndex1]);
                                linesToSave.Add(passwordsPool_oldpassword3[poolIndex1]);
                            }
                        }

                        linesToSave.Add(Rencrypt.EncryptInterface(Rfile.key3, Rfile.key3, defaultUser.salt)); // Line last-1: Third key, checksum
                                                                                                              // Create checksum
                        string checksum1 = "";
                        for (int checksumIndex1 = 0; linesToSave.Count != checksumIndex1; checksumIndex1++)
                        {
                            checksum1 = checksum1 + linesToSave[checksumIndex1];
                        }
                        checksum1 = checksum1 + kryptonTextBoxLoginName.Text;
                        linesToSave.Add(Rencrypt.EncryptInterface(checksum1, currentUser.masterPassword, defaultUser.salt)); // Line last-1: Third key, checksum
                                                                                                                             // Make a backup
                        Rfile.CreateBackup(currentUser.name);
                        // Save structure
                        try
                        {
                            File.WriteAllLines(Rfile.savesPath + @"\" + currentUser.name + ".rcrypt", linesToSave);
                            Console("notification saved");
                        }
                        catch
                        {
                            Console("notification notsaved");
                        }
                    }
                    break;

                case "load user":
                    // Load structure
                    string[] linesToLoad = File.ReadAllLines(Rfile.savesPath + @"\" + kryptonTextBoxLoginName.Text + ".rcrypt");
                    // Initialize current user settings
                    if (linesToLoad[0] == Rencrypt.EncryptInterface(Rfile.key1, Rfile.key1, defaultUser.salt))
                    {
                        currentUser.masterPassword = kryptonTextBoxLoginMasterpassword.Text;
                        currentUser.salt = Rencrypt.DecryptInterface(linesToLoad[4], currentUser.masterPassword, currentUser.language, defaultUser.salt);
                        if (Rencrypt.DecryptInterface(linesToLoad[2], currentUser.masterPassword, currentUser.language, currentUser.salt) == "0")
                        {
                            currentUser.language = 0;
                        }
                        else if (Rencrypt.DecryptInterface(linesToLoad[2], currentUser.masterPassword, currentUser.language, currentUser.salt) == "1")
                        {
                            currentUser.language = 1;
                        }
                        else
                        {
                            currentUser.language = defaultUser.language;
                        }
                        currentUser.name = Rencrypt.DecryptInterface(linesToLoad[3], currentUser.masterPassword, currentUser.language, currentUser.salt);
                        currentUser.icon = Rencrypt.DecryptInterface(linesToLoad[5], currentUser.masterPassword, currentUser.language, currentUser.salt);
                        if (Rencrypt.DecryptInterface(linesToLoad[6], currentUser.masterPassword, currentUser.language, currentUser.salt) == "False")
                        {
                            currentUser.ifDarkTheme = false;
                        }
                        else if (Rencrypt.DecryptInterface(linesToLoad[6], currentUser.masterPassword, currentUser.language, currentUser.salt) == "True")
                        {
                            currentUser.ifDarkTheme = true;
                        }
                        else
                        {
                            currentUser.ifDarkTheme = defaultUser.ifDarkTheme;
                        }
                    }
                    // Load all user passwords
                    if (linesToLoad[7] == Rencrypt.EncryptInterface(Rfile.key2, Rfile.key2, defaultUser.salt))
                    {
                        // Verify if at least one password is stored
                        if (linesToLoad[8] == Rencrypt.EncryptInterface(Rfile.key3, Rfile.key3, defaultUser.salt))
                        {
                            // No passwords found
                        }
                        else
                        {
                            // Load all found passwords
                            int poolIndex2 = 0;
                            while (linesToLoad[8 + poolIndex2 + 0] != Rencrypt.EncryptInterface(Rfile.key3, Rfile.key3, defaultUser.salt))
                            {
                                kryptonListBox1.Items.Add(Rencrypt.DecryptInterface(linesToLoad[8 + poolIndex2 + 0], currentUser.masterPassword, currentUser.language, currentUser.salt));
                                passwordsPool_name.Add(linesToLoad[8 + poolIndex2 + 0]);
                                passwordsPool_password.Add(linesToLoad[8 + poolIndex2 + 1]);
                                passwordsPool_sitelink.Add(linesToLoad[8 + poolIndex2 + 2]);
                                passwordsPool_description.Add(linesToLoad[8 + poolIndex2 + 3]);
                                passwordsPool_oldpassword1.Add(linesToLoad[8 + poolIndex2 + 4]);
                                passwordsPool_oldpassword2.Add(linesToLoad[8 + poolIndex2 + 5]);
                                passwordsPool_oldpassword3.Add(linesToLoad[8 + poolIndex2 + 6]);
                                poolIndex2 = poolIndex2 + 7;
                            }
                        }
                    }
                    Array.Clear(linesToLoad, 0, linesToLoad.Length);
                    break;

                case "checksum":
                    // Load structure to checksum
                    string[] linesToChecksum1 = File.ReadAllLines(Rfile.savesPath + @"\" + kryptonTextBoxLoginName.Text + ".rcrypt");
                    List<string> linesToChecksum2 = new List<string>(linesToChecksum1); // Clone array to a list bcs C# is shit
                    List<string> linesToChecksum3 = new List<string>(linesToChecksum1); // Clone it again bcs I dont want to see arrays again in my life
                    string checksum2 = "";
                    // Replace the last line with the login name
                    linesToChecksum2[linesToChecksum2.Count - 1] = kryptonTextBoxLoginName.Text;
                    // Create checksum
                    for (int checksumIndex2 = 0; linesToChecksum2.Count != checksumIndex2; checksumIndex2++)
                    {
                        checksum2 = checksum2 + linesToChecksum2[checksumIndex2];
                    }
                    // Check and compare checksum
                    if (Rencrypt.EncryptInterface(checksum2, kryptonTextBoxLoginMasterpassword.Text, defaultUser.salt) == linesToChecksum3[linesToChecksum3.Count - 1])
                    {
                        // If equal, checksum correct
                        Array.Clear(linesToChecksum1, 0, linesToChecksum1.Length);
                        linesToChecksum3.Clear();
                        linesToChecksum2.Clear();
                        isChecksumCorrect = true;
                    }
                    else
                    {
                        // Not equal, checksum is different
                        Array.Clear(linesToChecksum1, 0, linesToChecksum1.Length);
                        linesToChecksum3.Clear();
                        linesToChecksum2.Clear();
                        isChecksumCorrect = false;
                    }
                    break;

                case "interface theme":
                    // This is a nightmare I know.. I wrote it myself
                    // set colors
                    int userColor1 = 0;     // default is black
                    int userColor2 = 255;   // default is white
                                            // int userColor1 = 255;     // darkthm is white
                                            // int userColor2 = 0;     // darkthm is black
                    if (currentUser.ifDarkTheme == true)
                    {
                        userColor1 = 255;
                        userColor2 = 40;
                    }
                    else if (currentUser.ifDarkTheme == false)
                    {
                        userColor1 = 0;
                        userColor2 = 255;
                    }
                    // set interface
                    // layout
                    this.BackColor = System.Drawing.Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonPalette1.FormStyles.FormCommon.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonPalette1.FormStyles.FormCommon.StateCommon.Back.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonPalette1.FormStyles.FormCommon.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonPalette1.FormStyles.FormCommon.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Content.ShortText.Color2 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonListBox1.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonListBox1.StateCommon.Back.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonListBox1.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonListBox1.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonListBox1.StateCommon.Item.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonListBox1.StateCommon.Item.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonListBox1.StateCommon.Item.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonListBox1.StateCommon.Item.Content.ShortText.Color2 = Color.FromArgb(userColor1, userColor1, userColor1);

                    // createpassword
                    kryptonLabelBigTitlePass.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelName.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelPasswordtobesaved.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelMasterpassword.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelSitelink.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelDescription.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelReq1.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonTextBoxName.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxName.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonTextBoxPassword.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxPassword.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonTextBoxMasterPassword.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxMasterPassword.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonTextBoxSiteLink.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxSiteLink.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonTextBoxDesc.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxDesc.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonButtonCancel.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonCancel.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonCancel.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonCancel.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonCancel.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonCancel.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonCancel.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonCancel.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonSave.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonSave.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonSave.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonSave.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonSave.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonSave.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonSave.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonSave.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    // dashboard
                    kryptonLabelBigTitleDashboard.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelDashboardpass.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelDashboardaccount.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelDashboardusername.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonButtonGeneratepass.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonGeneratepass.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonGeneratepass.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonGeneratepass.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonGeneratepass.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonGeneratepass.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonGeneratepass.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonGeneratepass.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonDashboardCreate.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDashboardCreate.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDashboardCreate.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDashboardCreate.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDashboardCreate.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDashboardCreate.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDashboardCreate.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDashboardCreate.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonUsersettings.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonUsersettings.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonUsersettings.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonUsersettings.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonUsersettings.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonUsersettings.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonUsersettings.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonUsersettings.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonDashboardSaveandExit.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDashboardSaveandExit.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDashboardSaveandExit.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDashboardSaveandExit.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDashboardSaveandExit.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDashboardSaveandExit.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDashboardSaveandExit.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDashboardSaveandExit.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonForceexit.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonForceexit.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonForceexit.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonForceexit.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonForceexit.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonForceexit.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonForceexit.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonForceexit.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonCentralDash.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonCentralDash.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonCentralDash.StateCommon.Content.ShortText.Color1 = Color.FromArgb(0, 0, 0);
                    kryptonButtonCentralDash.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonCentralDash.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonCentralDash.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonCentralDash.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonCentralDash.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    // infopassword
                    kryptonLabelBigTitleInfoshow.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelPasswordsavedInfoshow.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelOldpasswordsInfoshow.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelDescInfoshow.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonTextBoxPasswordInfoshow.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxPasswordInfoshow.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonButtonInfopassShowHidd.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassShowHidd.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassShowHidd.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonInfopassShowHidd.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassShowHidd.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassShowHidd.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonInfopassShowHidd.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonInfopassShowHidd.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonInfopassEdit.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassEdit.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassEdit.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonInfopassEdit.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassEdit.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassEdit.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonInfopassEdit.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonInfopassEdit.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonInfopassDelete.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassDelete.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassDelete.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonInfopassDelete.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassDelete.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassDelete.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonInfopassDelete.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonInfopassDelete.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonInfopassCopy.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassCopy.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassCopy.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonInfopassCopy.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassCopy.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassCopy.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonInfopassCopy.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonInfopassCopy.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonInfopassOpenLink.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassOpenLink.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassOpenLink.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonInfopassOpenLink.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassOpenLink.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonInfopassOpenLink.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonInfopassOpenLink.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonInfopassOpenLink.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    // editpassword
                    kryptonButtonEditSave.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonEditSave.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonEditSave.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonEditSave.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonEditSave.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonEditSave.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonEditSave.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonEditSave.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonEditCancel.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonEditCancel.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonEditCancel.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonEditCancel.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonEditCancel.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonEditCancel.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonEditCancel.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonEditCancel.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    // settings
                    kryptonLabelBigTitleSett.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelSettLang.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelSettTheme.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelSettIcon.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonComboBoxLangSelector.StateCommon.ComboBox.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonComboBoxLangSelector.StateCommon.ComboBox.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonComboBoxLangSelector.StateCommon.ComboBox.Border.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonComboBoxIfDarkTheme.StateCommon.ComboBox.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonComboBoxIfDarkTheme.StateCommon.ComboBox.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonComboBoxIfDarkTheme.StateCommon.ComboBox.Border.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonComboBoxProfileColor.StateCommon.ComboBox.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonComboBoxProfileColor.StateCommon.ComboBox.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonComboBoxProfileColor.StateCommon.ComboBox.Border.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonButtonAccountChangeName.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonAccountChangeName.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonAccountChangeName.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonAccountChangeName.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonAccountChangeName.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonAccountChangeName.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonAccountChangeName.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonAccountChangeName.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonAccountDel.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonAccountDel.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonAccountDel.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonAccountDel.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonAccountDel.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonAccountDel.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonAccountDel.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonAccountDel.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonRPassInfo.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonRPassInfo.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonRPassInfo.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonRPassInfo.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonRPassInfo.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonRPassInfo.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonRPassInfo.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonRPassInfo.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    // login
                    kryptonLabelBigTitleLogin.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelLoginName.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelLoginMasterpassword.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelLoginLang.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonTextBoxLoginName.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxLoginName.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonTextBoxLoginMasterpassword.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxLoginMasterpassword.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonComboBoxLoginLangSelector.StateCommon.ComboBox.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonComboBoxLoginLangSelector.StateCommon.ComboBox.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonComboBoxLoginLangSelector.StateCommon.ComboBox.Border.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonButtonLogin.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonLogin.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonLogin.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonLogin.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonLogin.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonLogin.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonLogin.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonLogin.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonRegister.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonRegister.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonRegister.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonRegister.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonRegister.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonRegister.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonRegister.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonRegister.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonLoginExit.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonLoginExit.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonLoginExit.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonLoginExit.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonLoginExit.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonLoginExit.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonLoginExit.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonLoginExit.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonLoginDefaultSettings.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonLoginDefaultSettings.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonLoginDefaultSettings.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonLoginDefaultSettings.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonLoginDefaultSettings.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonLoginDefaultSettings.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonLoginDefaultSettings.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonLoginDefaultSettings.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    // eula
                    kryptonLabelBigTitleEula.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonButtonEulaAccept.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonEulaAccept.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonEulaAccept.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonEulaAccept.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonEulaAccept.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonEulaAccept.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonEulaAccept.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonEulaAccept.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonEulaCancel.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonEulaCancel.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonEulaCancel.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonEulaCancel.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonEulaCancel.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonEulaCancel.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonEulaCancel.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonEulaCancel.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonTextBoxEulaterms.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxEulaterms.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    // register
                    kryptonLabelBigTitleRegister.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelRegisterName.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelRegisterMasterpass.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelRegisterConfirmMasterpass.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelRegisterSalt.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonTextBoxRegisterName.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxRegisterName.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonTextBoxRegisterMasterpass.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxRegisterMasterpass.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonTextBoxRegisterConfirmMasterpass.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxRegisterConfirmMasterpass.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonTextBoxRegisterSalt1.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxRegisterSalt1.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonTextBoxRegisterSalt2.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxRegisterSalt2.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonTextBoxRegisterSalt3.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxRegisterSalt3.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonTextBoxRegisterSalt4.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxRegisterSalt4.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonTextBoxRegisterSalt5.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxRegisterSalt5.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonTextBoxRegisterSalt6.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxRegisterSalt6.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonTextBoxRegisterSalt7.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxRegisterSalt7.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonTextBoxRegisterSalt8.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxRegisterSalt8.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonButtonRegisterReg.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonRegisterReg.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonRegisterReg.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonRegisterReg.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonRegisterReg.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonRegisterReg.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonRegisterReg.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonRegisterReg.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonRegisterCancel.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonRegisterCancel.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonRegisterCancel.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonRegisterCancel.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonRegisterCancel.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonRegisterCancel.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonRegisterCancel.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonRegisterCancel.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    // about
                    kryptonLabelBigTitleAbout.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonTextBoxAbout.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxAbout.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    // notification
                    kryptonLabelBigTitleNotification.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelNotificationContents.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonButtonNotification.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonNotification.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonNotification.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonNotification.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonNotification.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonNotification.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonNotification.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonNotification.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    // generate
                    kryptonLabelBigTitleGenerate.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelGenerateGenerated.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelGenerateSett.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelGenerateSize.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonRadioButtonGeneratorSize1.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonRadioButtonGeneratorSize2.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonRadioButtonGeneratorSize3.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonRadioButtonGeneratorSize4.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonRadioButtonGeneratorSize5.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonTextBoxGenerated.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxGenerated.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonButtonGenerate.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonGenerate.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonGenerate.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonGenerate.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonGenerate.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonGenerate.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonGenerate.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonGenerate.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonGenerateCreate.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonGenerateCreate.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonGenerateCreate.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonGenerateCreate.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonGenerateCreate.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonGenerateCreate.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonGenerateCreate.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonGenerateCreate.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    // auth
                    kryptonButtonAuthContinue.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonAuthContinue.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonAuthContinue.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonAuthContinue.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonAuthContinue.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonAuthContinue.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonAuthContinue.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonAuthContinue.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonAuthCancel.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonAuthCancel.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonAuthCancel.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonAuthCancel.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonAuthCancel.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonAuthCancel.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonAuthCancel.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonAuthCancel.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    // changename
                    kryptonLabelBigTitleChangename.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelChangenameEnter.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonTextBoxChangename.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxChangename.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonButtonChangenameSave.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonChangenameSave.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonChangenameSave.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonChangenameSave.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonChangenameSave.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonChangenameSave.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonChangenameSave.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonChangenameSave.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonChangenameCancel.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonChangenameCancel.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonChangenameCancel.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonChangenameCancel.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonChangenameCancel.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonChangenameCancel.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonChangenameCancel.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonChangenameCancel.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    // defaultsettings
                    kryptonLabelBigTitleDefaultsettings.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelDefaultSettingsLang.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelDefaultSettingsTheme.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonLabelDefaultSettingsOther.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonComboBoxDefaultSettingsIfDarkTheme.StateCommon.ComboBox.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonComboBoxDefaultSettingsIfDarkTheme.StateCommon.ComboBox.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonComboBoxDefaultSettingsIfDarkTheme.StateCommon.ComboBox.Border.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonButtonDefaultSettingsResetsalt.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsResetsalt.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsResetsalt.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsResetsalt.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsResetsalt.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsResetsalt.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsResetsalt.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsResetsalt.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonDefaultSettingsChangesalt.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsChangesalt.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsChangesalt.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsChangesalt.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsChangesalt.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsChangesalt.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsChangesalt.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsChangesalt.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonConsoleToggle.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonConsoleToggle.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonConsoleToggle.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonConsoleToggle.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonConsoleToggle.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonConsoleToggle.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonConsoleToggle.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonConsoleToggle.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonDefaultSettingsBack.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsBack.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsBack.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsBack.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsBack.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsBack.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsBack.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsBack.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonDefaultSettingsOpenbackups.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsOpenbackups.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsOpenbackups.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsOpenbackups.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsOpenbackups.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsOpenbackups.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsOpenbackups.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsOpenbackups.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonDefaultSettingsOpensaves.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsOpensaves.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsOpensaves.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsOpensaves.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsOpensaves.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsOpensaves.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsOpensaves.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsOpensaves.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonDefaultSettingsReset.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsReset.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsReset.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsReset.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsReset.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonDefaultSettingsReset.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsReset.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonDefaultSettingsReset.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    // console
                    kryptonTextBoxConsoleCommand.StateCommon.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonTextBoxConsoleCommand.StateCommon.Content.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonButtonConsoleSend.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonConsoleSend.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonConsoleSend.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonConsoleSend.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonConsoleSend.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonConsoleSend.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonConsoleSend.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonConsoleSend.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    // changesalt
                    kryptonLabelBigTitleChangeSalt.StateCommon.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);

                    kryptonButtonChangeSaltSave.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonChangeSaltSave.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonChangeSaltSave.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonChangeSaltSave.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonChangeSaltSave.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonChangeSaltSave.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonChangeSaltSave.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonChangeSaltSave.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);

                    kryptonButtonChangeSaltCancel.StateCommon.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonChangeSaltCancel.StateCommon.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonChangeSaltCancel.StateCommon.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonChangeSaltCancel.StatePressed.Border.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonChangeSaltCancel.StatePressed.Border.Color2 = Color.FromArgb(userColor2, userColor2, userColor2);
                    kryptonButtonChangeSaltCancel.StatePressed.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonChangeSaltCancel.StateTracking.Content.ShortText.Color1 = Color.FromArgb(userColor1, userColor1, userColor1);
                    kryptonButtonChangeSaltCancel.StateTracking.Back.Color1 = Color.FromArgb(userColor2, userColor2, userColor2);
                    break;

                case "error login credentials":
                    // error2 wrong credentials
                    kryptonTextBoxLoginMasterpassword.Text = "";
                    kryptonLabelLoginError.Visible = true;
                    break;

                case "error login reset":
                    // error1 reset
                    kryptonLabelLoginError.Visible = false;
                    kryptonLabelLoginError.StateCommon.ShortText.Color1 = Color.Red;
                    kryptonLabelLoginError.Text = Rlang.error2[currentUser.language];
                    break;

                case "error login logged":
                    // error3 login credentials pass
                    kryptonLabelLoginError.StateCommon.ShortText.Color1 = Color.Gray;
                    kryptonLabelLoginError.Text = Rlang.error3[currentUser.language];
                    kryptonLabelLoginError.Visible = true;
                    kryptonLabelLoginError.Refresh();
                    // hide other stuff while logging in
                    kryptonButtonLogin.Visible = false;
                    kryptonButtonRegister.Visible = false;
                    kryptonButtonLoginExit.Visible = false;
                    kryptonButtonLoginDefaultSettings.Visible = false;

                    kryptonLabelLoginMasterpassword.Visible = false;
                    kryptonTextBoxLoginMasterpassword.Visible = false;
                    kryptonLabelLoginLang.Visible = false;
                    kryptonComboBoxLoginLangSelector.Visible = false;
                    System.Threading.Thread.Sleep(1000);
                    break;

                case "error login badfile":
                    // error4 bad file
                    Console("interface show login");
                    kryptonLabelLoginError.StateCommon.ShortText.Color1 = Color.Red;
                    kryptonLabelLoginError.Text = Rlang.error4[currentUser.language];
                    kryptonLabelLoginError.Visible = true;
                    kryptonLabelLoginError.Refresh();
                    break;

                case "interface show register":
                    // hide all interfaces
                    Console("interface disable dashbutton");
                    Console("interface disable passwordlist");
                    Console("error register reset");

                    InterfaceHide_All();
                    // language
                    kryptonLabelBigTitleRegister.Text = Rlang.title8register[currentUser.language];

                    kryptonLabelRegisterName.Text = Rlang.minititle19newname[currentUser.language];
                    kryptonLabelRegisterMasterpass.Text = Rlang.minititle20newmasterpass[currentUser.language];
                    kryptonLabelRegisterConfirmMasterpass.Text = Rlang.minititle21confmasterpass[currentUser.language];
                    kryptonLabelRegisterSalt.Text = Rlang.minititle22newsecretcode[currentUser.language];

                    kryptonButtonRegisterReg.Text = Rlang.button19register[currentUser.language];
                    kryptonButtonRegisterCancel.Text = Rlang.button2cancel[currentUser.language];

                    kryptonLabelRegisterReq.Text = Rlang.minititle23req2[currentUser.language];
                    // show
                    pictureBoxRPassIcon.Location = new System.Drawing.Point(384, 13);
                    pictureBoxRPassIcon.Visible = true;
                    kryptonLabelBigTitleRegister.Location = new System.Drawing.Point(322, 105);
                    kryptonLabelBigTitleRegister.Visible = true;

                    kryptonLabelRegisterName.Location = new System.Drawing.Point(322, 149);
                    kryptonLabelRegisterName.Visible = true;
                    kryptonTextBoxRegisterName.Location = new System.Drawing.Point(322, 175);
                    kryptonTextBoxRegisterName.Text = "";
                    kryptonTextBoxRegisterName.Visible = true;

                    kryptonLabelRegisterMasterpass.Location = new System.Drawing.Point(322, 214);
                    kryptonLabelRegisterMasterpass.Visible = true;
                    kryptonTextBoxRegisterMasterpass.Location = new System.Drawing.Point(322, 240);
                    kryptonTextBoxRegisterMasterpass.Text = "";
                    kryptonTextBoxRegisterMasterpass.Visible = true;
                    kryptonLabelRegisterConfirmMasterpass.Location = new System.Drawing.Point(322, 279);
                    kryptonLabelRegisterConfirmMasterpass.Visible = true;
                    kryptonTextBoxRegisterConfirmMasterpass.Location = new System.Drawing.Point(322, 305);
                    kryptonTextBoxRegisterConfirmMasterpass.Text = "";
                    kryptonTextBoxRegisterConfirmMasterpass.Visible = true;

                    kryptonLabelRegisterSalt.Location = new System.Drawing.Point(322, 344);
                    kryptonLabelRegisterSalt.Visible = true;
                    kryptonTextBoxRegisterSalt1.Location = new System.Drawing.Point(322, 370);
                    kryptonTextBoxRegisterSalt1.Text = "";
                    kryptonTextBoxRegisterSalt1.Visible = true;
                    kryptonTextBoxRegisterSalt2.Location = new System.Drawing.Point(350, 370);
                    kryptonTextBoxRegisterSalt2.Text = "";
                    kryptonTextBoxRegisterSalt2.Visible = true;
                    kryptonTextBoxRegisterSalt3.Location = new System.Drawing.Point(378, 370);
                    kryptonTextBoxRegisterSalt3.Text = "";
                    kryptonTextBoxRegisterSalt3.Visible = true;
                    kryptonTextBoxRegisterSalt4.Location = new System.Drawing.Point(406, 370);
                    kryptonTextBoxRegisterSalt4.Text = "";
                    kryptonTextBoxRegisterSalt4.Visible = true;
                    kryptonTextBoxRegisterSalt5.Location = new System.Drawing.Point(434, 370);
                    kryptonTextBoxRegisterSalt5.Text = "";
                    kryptonTextBoxRegisterSalt5.Visible = true;
                    kryptonTextBoxRegisterSalt6.Location = new System.Drawing.Point(462, 370);
                    kryptonTextBoxRegisterSalt6.Text = "";
                    kryptonTextBoxRegisterSalt6.Visible = true;
                    kryptonTextBoxRegisterSalt7.Location = new System.Drawing.Point(490, 370);
                    kryptonTextBoxRegisterSalt7.Text = "";
                    kryptonTextBoxRegisterSalt7.Visible = true;
                    kryptonTextBoxRegisterSalt8.Location = new System.Drawing.Point(518, 370);
                    kryptonTextBoxRegisterSalt8.Text = "";
                    kryptonTextBoxRegisterSalt8.Visible = true;

                    kryptonButtonRegisterReg.Location = new System.Drawing.Point(322, 409);
                    kryptonButtonRegisterReg.Visible = true;
                    kryptonButtonRegisterCancel.Location = new System.Drawing.Point(457, 409);
                    kryptonButtonRegisterCancel.Visible = true;
                    kryptonLabelRegisterReq.Location = new System.Drawing.Point(322, 448);
                    kryptonLabelRegisterReq.Visible = true;
                    break;

                case "interface hide register":
                    pictureBoxRPassIcon.Visible = false;
                    kryptonLabelBigTitleRegister.Visible = false;

                    kryptonLabelRegisterName.Visible = false;
                    kryptonTextBoxRegisterName.Visible = false;

                    kryptonLabelRegisterMasterpass.Visible = false;
                    kryptonTextBoxRegisterMasterpass.Visible = false;
                    kryptonLabelRegisterConfirmMasterpass.Visible = false;
                    kryptonTextBoxRegisterConfirmMasterpass.Visible = false;

                    kryptonLabelRegisterSalt.Visible = false;
                    kryptonTextBoxRegisterSalt1.Visible = false;
                    kryptonTextBoxRegisterSalt2.Visible = false;
                    kryptonTextBoxRegisterSalt3.Visible = false;
                    kryptonTextBoxRegisterSalt4.Visible = false;
                    kryptonTextBoxRegisterSalt5.Visible = false;
                    kryptonTextBoxRegisterSalt6.Visible = false;
                    kryptonTextBoxRegisterSalt7.Visible = false;
                    kryptonTextBoxRegisterSalt8.Visible = false;

                    kryptonButtonRegisterReg.Visible = false;
                    kryptonButtonRegisterCancel.Visible = false;
                    kryptonLabelRegisterReq.Visible = false;
                    break;

                case "error createpassword reqforms":
                    // error5 empty required forms
                    for (int i = 2; i != 0; i--)
                    {
                        System.Threading.Thread.Sleep(100);
                        kryptonLabelReq1.StateCommon.ShortText.Color1 = Color.Gray;
                        kryptonLabelReq1.StateCommon.ShortText.Color2 = Color.Gray;
                        kryptonLabelReq1.Refresh();
                        System.Threading.Thread.Sleep(100);
                        kryptonLabelReq1.StateCommon.ShortText.Color1 = Color.Red;
                        kryptonLabelReq1.StateCommon.ShortText.Color2 = Color.Red;
                        kryptonLabelReq1.Refresh();
                    }
                    break;

                case "error register reset":
                    // error6 reset
                    kryptonLabelRegisterReq.StateCommon.ShortText.Color1 = Color.Gray;
                    kryptonLabelRegisterReq.StateCommon.ShortText.Color2 = Color.Gray;
                    kryptonLabelRegisterReq.Refresh();
                    kryptonLabelRegisterReq.Text = Rlang.minititle23req2[currentUser.language];
                    break;

                case "error register reqforms":
                    // error7 empty required forms
                    kryptonLabelRegisterReq.Text = Rlang.minititle23req2[currentUser.language];
                    for (int i = 2; i != 0; i--)
                    {
                        System.Threading.Thread.Sleep(100);
                        kryptonLabelRegisterReq.StateCommon.ShortText.Color1 = Color.Gray;
                        kryptonLabelRegisterReq.StateCommon.ShortText.Color2 = Color.Gray;
                        kryptonLabelRegisterReq.Refresh();
                        System.Threading.Thread.Sleep(100);
                        kryptonLabelRegisterReq.StateCommon.ShortText.Color1 = Color.Red;
                        kryptonLabelRegisterReq.StateCommon.ShortText.Color2 = Color.Red;
                        kryptonLabelRegisterReq.Refresh();
                    }
                    break;

                case "error register userexists":
                    // error8 user already exists
                    kryptonLabelRegisterReq.Text = Rlang.error8[currentUser.language];
                    for (int i = 2; i != 0; i--)
                    {
                        System.Threading.Thread.Sleep(100);
                        kryptonLabelRegisterReq.StateCommon.ShortText.Color1 = Color.Gray;
                        kryptonLabelRegisterReq.StateCommon.ShortText.Color2 = Color.Gray;
                        kryptonLabelRegisterReq.Refresh();
                        System.Threading.Thread.Sleep(100);
                        kryptonLabelRegisterReq.StateCommon.ShortText.Color1 = Color.Red;
                        kryptonLabelRegisterReq.StateCommon.ShortText.Color2 = Color.Red;
                        kryptonLabelRegisterReq.Refresh();
                    }
                    break;

                case "error register wrongpassword":
                    // error9 passwords doesn't match or they are less than 8 characters
                    kryptonLabelRegisterReq.Text = Rlang.error9[currentUser.language];
                    for (int i = 2; i != 0; i--)
                    {
                        System.Threading.Thread.Sleep(100);
                        kryptonLabelRegisterReq.StateCommon.ShortText.Color1 = Color.Gray;
                        kryptonLabelRegisterReq.StateCommon.ShortText.Color2 = Color.Gray;
                        kryptonLabelRegisterReq.Refresh();
                        System.Threading.Thread.Sleep(100);
                        kryptonLabelRegisterReq.StateCommon.ShortText.Color1 = Color.Red;
                        kryptonLabelRegisterReq.StateCommon.ShortText.Color2 = Color.Red;
                        kryptonLabelRegisterReq.Refresh();
                    }
                    break;

                case "error register wrongcode":
                    // error10 super secret code isn't valid
                    kryptonLabelRegisterReq.Text = Rlang.error10[currentUser.language];
                    for (int i = 2; i != 0; i--)
                    {
                        System.Threading.Thread.Sleep(100);
                        kryptonLabelRegisterReq.StateCommon.ShortText.Color1 = Color.Gray;
                        kryptonLabelRegisterReq.StateCommon.ShortText.Color2 = Color.Gray;
                        kryptonLabelRegisterReq.Refresh();
                        System.Threading.Thread.Sleep(100);
                        kryptonLabelRegisterReq.StateCommon.ShortText.Color1 = Color.Red;
                        kryptonLabelRegisterReq.StateCommon.ShortText.Color2 = Color.Red;
                        kryptonLabelRegisterReq.Refresh();
                    }
                    break;

                case "error register logged":
                    // error11 registering succes
                    kryptonLabelRegisterReq.Text = Rlang.error11[currentUser.language];
                    kryptonLabelRegisterReq.StateCommon.ShortText.Color1 = Color.Gray;
                    kryptonLabelRegisterReq.StateCommon.ShortText.Color2 = Color.Gray;
                    kryptonLabelRegisterReq.Refresh();
                    kryptonLabelRegisterName.Text = Rlang.minititle6name[currentUser.language];
                    kryptonLabelRegisterName.Refresh();

                    // hide elements while registering
                    kryptonLabelRegisterMasterpass.Visible = false;
                    kryptonTextBoxRegisterMasterpass.Visible = false;
                    kryptonLabelRegisterConfirmMasterpass.Visible = false;
                    kryptonTextBoxRegisterConfirmMasterpass.Visible = false;

                    kryptonLabelRegisterSalt.Visible = false;
                    kryptonTextBoxRegisterSalt1.Visible = false;
                    kryptonTextBoxRegisterSalt2.Visible = false;
                    kryptonTextBoxRegisterSalt3.Visible = false;
                    kryptonTextBoxRegisterSalt4.Visible = false;
                    kryptonTextBoxRegisterSalt5.Visible = false;
                    kryptonTextBoxRegisterSalt6.Visible = false;
                    kryptonTextBoxRegisterSalt7.Visible = false;
                    kryptonTextBoxRegisterSalt8.Visible = false;

                    kryptonButtonRegisterReg.Visible = false;
                    kryptonButtonRegisterCancel.Visible = false;
                    System.Threading.Thread.Sleep(1000);
                    break;

                case "register":
                    // Reset error
                    Console("error register reset");
                    // Checking if there's empty forms
                    if (kryptonTextBoxRegisterName.Text == "" ||
                        kryptonTextBoxRegisterMasterpass.Text == "" ||
                        kryptonTextBoxRegisterConfirmMasterpass.Text == "" ||
                        kryptonTextBoxRegisterSalt1.Text == "" ||
                        kryptonTextBoxRegisterSalt2.Text == "" ||
                        kryptonTextBoxRegisterSalt3.Text == "" ||
                        kryptonTextBoxRegisterSalt4.Text == "" ||
                        kryptonTextBoxRegisterSalt5.Text == "" ||
                        kryptonTextBoxRegisterSalt6.Text == "" ||
                        kryptonTextBoxRegisterSalt7.Text == "" ||
                        kryptonTextBoxRegisterSalt8.Text == "")
                    {
                        // Empty forms, return error
                        Console("error register reqforms");
                    }
                    else
                    {
                        // There's not empty forms
                        // Checking if user exists
                        if (kryptonTextBoxRegisterName.Text == "root")
                        {
                            // Can't use name "root", return error
                            Console("error register userexists");
                        }
                        else if (Rfile.UserExists(kryptonTextBoxRegisterName.Text))
                        {
                            // User already exists, return error
                            Console("error register userexists");
                        }
                        else
                        {
                            // User doesn't exist
                            // Checking if master password match and is bigger than 8 characters
                            if (kryptonTextBoxRegisterMasterpass.TextLength >= 8 && kryptonTextBoxRegisterMasterpass.Text == kryptonTextBoxRegisterConfirmMasterpass.Text)
                            {
                                // Master password is bigger than 8 characters and matches
                                // Checking if salt is in valid form

                                // Create a list for an easier comparison
                                List<string> registerCompareSalts = new List<string>();
                                registerCompareSalts.Clear();
                                registerCompareSalts.Add(kryptonTextBoxRegisterSalt1.Text);
                                registerCompareSalts.Add(kryptonTextBoxRegisterSalt2.Text);
                                registerCompareSalts.Add(kryptonTextBoxRegisterSalt3.Text);
                                registerCompareSalts.Add(kryptonTextBoxRegisterSalt4.Text);
                                registerCompareSalts.Add(kryptonTextBoxRegisterSalt5.Text);
                                registerCompareSalts.Add(kryptonTextBoxRegisterSalt6.Text);
                                registerCompareSalts.Add(kryptonTextBoxRegisterSalt7.Text);
                                registerCompareSalts.Add(kryptonTextBoxRegisterSalt8.Text);

                                // Compare the list
                                bool registerIsSaltValid = true;
                                for (int index = 0; index <= 7; index++)
                                {
                                    if (!(registerCompareSalts[index] == "0" ||
                                    registerCompareSalts[index] == "1" ||
                                    registerCompareSalts[index] == "2" ||
                                    registerCompareSalts[index] == "3" ||
                                    registerCompareSalts[index] == "4" ||
                                    registerCompareSalts[index] == "5" ||
                                    registerCompareSalts[index] == "6" ||
                                    registerCompareSalts[index] == "7" ||
                                    registerCompareSalts[index] == "8" ||
                                    registerCompareSalts[index] == "9"))
                                    {
                                        // Form invalid, returning error
                                        registerIsSaltValid = false;
                                    }
                                }
                                registerCompareSalts.Clear();

                                // Continue
                                if (registerIsSaltValid)
                                {
                                    // All forms valid, continue
                                    Console("error register logged");
                                    // REGISTER USER

                                    currentUser.masterPassword = kryptonTextBoxRegisterMasterpass.Text;
                                    kryptonTextBoxLoginName.Text = kryptonTextBoxRegisterName.Text;
                                    currentUser.name = kryptonTextBoxRegisterName.Text;
                                    // Leave current language as is
                                    currentUser.salt = kryptonTextBoxRegisterSalt1.Text;
                                    currentUser.salt = currentUser.salt + kryptonTextBoxRegisterSalt2.Text;
                                    currentUser.salt = currentUser.salt + kryptonTextBoxRegisterSalt3.Text;
                                    currentUser.salt = currentUser.salt + kryptonTextBoxRegisterSalt4.Text;
                                    currentUser.salt = currentUser.salt + kryptonTextBoxRegisterSalt5.Text;
                                    currentUser.salt = currentUser.salt + kryptonTextBoxRegisterSalt6.Text;
                                    currentUser.salt = currentUser.salt + kryptonTextBoxRegisterSalt7.Text;
                                    currentUser.salt = currentUser.salt + kryptonTextBoxRegisterSalt8.Text;
                                    currentUser.icon = "gray";
                                    currentUser.ifDarkTheme = false;

                                    // Save user
                                    Console("save user");
                                    Console("reset");
                                }
                                else
                                {
                                    // Form invalid, returning error
                                    Console("error register wrongcode");
                                }
                            }
                            else
                            {
                                // Master password is less than 8 or isn't matching
                                Console("error register wrongpassword");
                            }
                        }
                    }
                    break;

                case "login":
                    // Reset error
                    Console("error login reset");
                    // Login
                    if (kryptonTextBoxLoginName.Text == "root")
                    {
                        // user exists, now check password match
                        if (kryptonTextBoxLoginMasterpassword.Text == "root")
                        {
                            // checking successful, continue logging
                            Console("error login logged");
                            // LOGIN AS ROOT

                            // checksum user file
                            // root user have no file to checksum

                            // set current user settings
                            // root user settings already set

                            // load password pool
                            // root user have no passwords

                            // show dashboard
                            InterfaceHide_All();
                            Console("interface theme");
                            Console("interface show dashboard");
                        }
                        else
                        {
                            Console("error login credentials");
                        }
                    }
                    else
                    {
                        // search user
                        if (Rfile.UserExists(kryptonTextBoxLoginName.Text))
                        {
                            // user exists, now check password match
                            if (Rfile.MasterpasswordMatch(Rencrypt.EncryptInterface(kryptonTextBoxLoginMasterpassword.Text, kryptonTextBoxLoginMasterpassword.Text, defaultUser.salt), kryptonTextBoxLoginName.Text))
                            {
                                // checking successful, continue logging
                                Console("error login logged");
                                // LOGIN AS USER

                                // checksum user file
                                isChecksumCorrect = false;
                                Console("checksum");
                                if (isChecksumCorrect == true)
                                {
                                    // set current user settings and load password pool
                                    Console("load user");
                                    // show dashboard
                                    InterfaceHide_All();
                                    Console("interface theme");
                                    Console("interface show dashboard");
                                }
                                else
                                {
                                    // checksum is different
                                    Console("error login badfile");
                                }
                            }
                            else
                            {
                                // master password doesn't match
                                Console("error login credentials");
                            }
                        }
                        else
                        {
                            // user doesn't exits
                            Console("error login credentials");
                        }
                    }
                    break;

                case "interface show about":
                    // hide all interfaces
                    Console("interface enable dashbutton");
                    Console("interface disable passwordlist");

                    InterfaceHide_All();
                    // language
                    kryptonLabelBigTitleAbout.Text = Rlang.title9about[currentUser.language];
                    kryptonTextBoxAbout.Text = Rlang.minititle24about[currentUser.language] + Rlang.version;
                    // show
                    kryptonLabelBigTitleAbout.Location = new System.Drawing.Point(276, 12);
                    kryptonLabelBigTitleAbout.Visible = true;
                    kryptonTextBoxAbout.Location = new System.Drawing.Point(276, 56);
                    kryptonTextBoxAbout.Visible = true;
                    break;

                case "interface hide about":
                    kryptonLabelBigTitleAbout.Visible = false;
                    kryptonTextBoxAbout.Visible = false;
                    break;

                case "interface show notification":
                    // hide all interfaces
                    Console("interface disable dashbutton");
                    Console("interface disable passwordlist");

                    InterfaceHide_All();
                    // language
                    kryptonButtonNotification.Text = Rlang.button23notification[currentUser.language];

                    kryptonLabelBigTitleNotification.Text = currentNotificationTitle;
                    kryptonLabelNotificationContents.Text = currentNotificationContents;
                    // reset notification text
                    currentNotificationTitle = Rlang.title10notification[currentUser.language];
                    currentNotificationContents = Rlang.minititle25notif[currentUser.language];
                    // show
                    kryptonLabelBigTitleNotification.Location = new System.Drawing.Point(276, 12);
                    kryptonLabelBigTitleNotification.Visible = true;
                    kryptonButtonNotification.Location = new System.Drawing.Point(276, 56);
                    kryptonButtonNotification.Visible = true;
                    kryptonLabelNotificationContents.Location = new System.Drawing.Point(276, 120);
                    kryptonLabelNotificationContents.Visible = true;
                    break;

                case "interface hide notification":
                    kryptonLabelBigTitleNotification.Visible = false;
                    kryptonButtonNotification.Visible = false;
                    kryptonLabelNotificationContents.Visible = false;
                    break;

                case "notification saved":
                    // Set notification
                    currentNotificationTitle = Rlang.minititle26notifsaved[currentUser.language];
                    currentNotificationContents = Rlang.minititle27notifsaved[currentUser.language];
                    currentNotificationCommand = "reset";
                    // Show it
                    Console("interface show notification");
                    break;

                case "notification notsaved":
                    // Set notification
                    currentNotificationTitle = Rlang.minititle28notifnotsaved[currentUser.language];
                    currentNotificationContents = Rlang.minititle29notifnotsaved[currentUser.language];
                    currentNotificationCommand = "interface show dashboard";
                    // Show it
                    Console("interface show notification");
                    break;

                case "notification rootsave":
                    // Set notification
                    currentNotificationTitle = Rlang.minititle28notifnotsaved[currentUser.language];
                    currentNotificationContents = Rlang.minititle30notifrootsave[currentUser.language];
                    currentNotificationCommand = "interface show dashboard";
                    // Show it
                    Console("interface show notification");
                    break;

                case "generate":
                    // Generate passsword
                    var generate = new Random();
                    string generatedPassword = "";
                    var value = generate.Next();
                    // Make it big
                    for (int gen = 0; gen < 50; gen++)
                    {
                        generatedPassword = generatedPassword + value.ToString();
                        value = generate.Next();
                    }
                    generatedPassword = Rencrypt.EncryptInterface(generatedPassword, generatedPassword, currentUser.salt);
                    // Display it
                    kryptonTextBoxGenerated.Text = generatedPassword.Substring(0, currentGeneratorSize);
                    break;

                case "interface show generate":
                    // hide all interfaces
                    Console("interface enable dashbutton");
                    Console("interface enable passwordlist");

                    InterfaceHide_All();
                    // language
                    kryptonLabelBigTitleGenerate.Text = Rlang.title11generate[currentUser.language];
                    kryptonLabelGenerateGenerated.Text = Rlang.minititle31generated[currentUser.language];
                    kryptonLabelGenerateSett.Text = Rlang.minititle32generatedsett[currentUser.language];
                    kryptonLabelGenerateSize.Text = Rlang.minititle33generatedsize[currentUser.language];

                    kryptonRadioButtonGeneratorSize1.Text = "16" + Rlang.minititle34genchar[currentUser.language];
                    kryptonRadioButtonGeneratorSize2.Text = "32" + Rlang.minititle34genchar[currentUser.language];
                    kryptonRadioButtonGeneratorSize3.Text = "64" + Rlang.minititle34genchar[currentUser.language];
                    kryptonRadioButtonGeneratorSize4.Text = "128" + Rlang.minititle34genchar[currentUser.language];
                    kryptonRadioButtonGeneratorSize5.Text = "512" + Rlang.minititle34genchar[currentUser.language];

                    kryptonButtonGenerate.Text = Rlang.button24generate[currentUser.language];
                    kryptonButtonGenerateCreate.Text = Rlang.button4createpass[currentUser.language];
                    // show
                    kryptonLabelBigTitleGenerate.Location = new System.Drawing.Point(276, 12);
                    kryptonLabelBigTitleGenerate.Visible = true;
                    kryptonLabelGenerateGenerated.Location = new System.Drawing.Point(276, 95);
                    kryptonLabelGenerateGenerated.Visible = true;
                    kryptonLabelGenerateSett.Location = new System.Drawing.Point(276, 206);
                    kryptonLabelGenerateSett.Visible = true;
                    kryptonLabelGenerateSize.Location = new System.Drawing.Point(276, 240);
                    kryptonLabelGenerateSize.Visible = true;

                    kryptonRadioButtonGeneratorSize1.Location = new System.Drawing.Point(279, 266);
                    kryptonRadioButtonGeneratorSize1.Visible = true;
                    kryptonRadioButtonGeneratorSize2.Location = new System.Drawing.Point(279, 292);
                    kryptonRadioButtonGeneratorSize2.Visible = true;
                    kryptonRadioButtonGeneratorSize3.Location = new System.Drawing.Point(279, 318);
                    kryptonRadioButtonGeneratorSize3.Visible = true;
                    kryptonRadioButtonGeneratorSize4.Location = new System.Drawing.Point(279, 344);
                    kryptonRadioButtonGeneratorSize4.Visible = true;
                    kryptonRadioButtonGeneratorSize5.Location = new System.Drawing.Point(279, 370);
                    kryptonRadioButtonGeneratorSize5.Visible = true;

                    kryptonTextBoxGenerated.Text = "";
                    kryptonTextBoxGenerated.Location = new System.Drawing.Point(276, 121);
                    kryptonTextBoxGenerated.Visible = true;
                    kryptonButtonGenerate.Location = new System.Drawing.Point(276, 56);
                    kryptonButtonGenerate.Visible = true;
                    kryptonButtonGenerateCreate.Location = new System.Drawing.Point(392, 56);
                    kryptonButtonGenerateCreate.Visible = true;
                    currentGeneratorSize = 16;
                    break;

                case "interface hide generate":
                    kryptonLabelBigTitleGenerate.Visible = false;
                    kryptonLabelGenerateGenerated.Visible = false;
                    kryptonLabelGenerateSett.Visible = false;
                    kryptonLabelGenerateSize.Visible = false;

                    kryptonRadioButtonGeneratorSize1.Visible = false;
                    kryptonRadioButtonGeneratorSize2.Visible = false;
                    kryptonRadioButtonGeneratorSize3.Visible = false;
                    kryptonRadioButtonGeneratorSize4.Visible = false;
                    kryptonRadioButtonGeneratorSize5.Visible = false;

                    kryptonTextBoxGenerated.Visible = false;
                    kryptonButtonGenerate.Visible = false;
                    kryptonButtonGenerateCreate.Visible = false;
                    break;

                case "interface show auth":
                    // hide all interfaces
                    Console("interface disable dashbutton");
                    Console("interface disable passwordlist");

                    InterfaceHide_All();
                    // language
                    kryptonLabelBigTitleLogin.Text = Rlang.title12auth[currentUser.language];
                    kryptonLabelLoginName.Text = Rlang.minititle6name[currentUser.language];
                    kryptonLabelLoginMasterpassword.Text = Rlang.minititle2masterpass[currentUser.language];
                    kryptonLabelLoginLang.Text = Rlang.minititle17langlang[currentUser.language];

                    kryptonButtonAuthContinue.Text = Rlang.button25continue[currentUser.language];
                    kryptonButtonAuthCancel.Text = Rlang.button2cancel[currentUser.language];

                    kryptonLabelLoginError.Text = Rlang.error2[currentUser.language];
                    // show
                    pictureBoxLoginIcon.Location = new System.Drawing.Point(384, 70);
                    pictureBoxLoginIcon.Visible = true;

                    kryptonLabelBigTitleLogin.Location = new System.Drawing.Point(322, 160);
                    kryptonLabelBigTitleLogin.Visible = true;
                    kryptonLabelLoginName.Location = new System.Drawing.Point(322, 204);
                    kryptonLabelLoginName.Visible = true;
                    kryptonLabelLoginMasterpassword.Location = new System.Drawing.Point(322, 269);
                    kryptonLabelLoginMasterpassword.Visible = true;

                    kryptonButtonAuthContinue.Location = new System.Drawing.Point(322, 334);
                    kryptonButtonAuthContinue.Visible = true;
                    kryptonButtonAuthCancel.Location = new System.Drawing.Point(457, 400);
                    kryptonButtonAuthCancel.Visible = true;

                    kryptonTextBoxLoginName.Location = new System.Drawing.Point(322, 230);
                    kryptonTextBoxLoginName.Visible = true;
                    kryptonTextBoxLoginMasterpassword.Location = new System.Drawing.Point(322, 295);
                    kryptonTextBoxLoginMasterpassword.Text = "";
                    kryptonTextBoxLoginMasterpassword.Visible = true;

                    kryptonLabelLoginError.Location = new System.Drawing.Point(322, 375);
                    kryptonLabelLoginError.Visible = false;
                    break;

                case "interface hide auth":
                    pictureBoxLoginIcon.Visible = false;
                    pictureBoxSave.Visible = false;

                    kryptonLabelBigTitleLogin.Visible = false;
                    kryptonLabelLoginName.Visible = false;
                    kryptonLabelLoginMasterpassword.Visible = false;

                    kryptonButtonAuthContinue.Visible = false;
                    kryptonButtonAuthCancel.Visible = false;

                    kryptonTextBoxLoginName.Visible = false;
                    kryptonTextBoxLoginMasterpassword.Text = "";
                    kryptonTextBoxLoginMasterpassword.Visible = false;

                    kryptonLabelLoginError.Visible = false;
                    break;

                case "auth save":
                    // Set auth
                    currentAuthCommand = "save user";
                    currentAuthFailCommand = "interface show dashboard";
                    // Show it
                    Console("interface show auth");
                    pictureBoxLoginIcon.Visible = false;
                    pictureBoxSave.Location = new System.Drawing.Point(384, 70);
                    pictureBoxSave.Visible = true;
                    break;

                case "interface enable profilepicture":
                    switch (currentUser.icon)
                    {
                        default:
                            pictureBoxUsergray.Visible = true;
                            break;

                        case "gray":
                            pictureBoxUsergray.Visible = true;
                            break;

                        case "blue":
                            pictureBoxUserblue.Visible = true;
                            break;

                        case "green":
                            pictureBoxUsergreen.Visible = true;
                            break;

                        case "red":
                            pictureBoxUserred.Visible = true;
                            break;

                        case "yellow":
                            pictureBoxUseryellow.Visible = true;
                            break;
                    }
                    break;

                case "interface disable profilepicture":
                    pictureBoxUsergray.Visible = false;
                    pictureBoxUserblue.Visible = false;
                    pictureBoxUsergreen.Visible = false;
                    pictureBoxUserred.Visible = false;
                    pictureBoxUseryellow.Visible = false;
                    break;

                case "auth changename":
                    // Set auth
                    currentAuthCommand = "interface show changename";
                    currentAuthFailCommand = "interface show settings";
                    // Show it
                    Console("interface show auth");
                    break;

                case "auth deleteaccount1":
                    // Set auth
                    currentAuthCommand = "notification deleteaccount";
                    currentAuthFailCommand = "interface show settings";
                    // Show it
                    Console("interface show auth");
                    break;

                case "interface show changename":
                    // hide all interfaces
                    Console("interface disable dashbutton");
                    Console("interface disable passwordlist");

                    InterfaceHide_All();
                    // language
                    kryptonLabelBigTitleChangename.Text = Rlang.title13changename[currentUser.language];
                    kryptonLabelChangenameEnter.Text = Rlang.minititle35newname[currentUser.language];
                    kryptonLabelChangenameReq.Text = Rlang.minititle36namereq[currentUser.language];

                    kryptonButtonChangenameSave.Text = Rlang.button1save[currentUser.language];
                    kryptonButtonChangenameCancel.Text = Rlang.button2cancel[currentUser.language];
                    // show
                    Console("error changename reset");
                    kryptonLabelBigTitleChangename.Location = new System.Drawing.Point(276, 12);
                    kryptonLabelBigTitleChangename.Visible = true;
                    kryptonLabelChangenameEnter.Location = new System.Drawing.Point(276, 115);
                    kryptonLabelChangenameEnter.Visible = true;
                    kryptonLabelChangenameReq.Location = new System.Drawing.Point(276, 214);
                    kryptonLabelChangenameReq.Visible = true;

                    kryptonTextBoxChangename.Location = new System.Drawing.Point(276, 141);
                    kryptonTextBoxChangename.Text = "";
                    kryptonTextBoxChangename.Visible = true;

                    kryptonButtonChangenameSave.Location = new System.Drawing.Point(276, 56);
                    kryptonButtonChangenameSave.Visible = true;
                    kryptonButtonChangenameCancel.Location = new System.Drawing.Point(363, 56);
                    kryptonButtonChangenameCancel.Visible = true;
                    break;

                case "interface hide changename":
                    kryptonLabelBigTitleChangename.Visible = false;
                    kryptonLabelChangenameEnter.Visible = false;
                    kryptonLabelChangenameReq.Visible = false;
                    kryptonTextBoxChangename.Visible = false;
                    kryptonButtonChangenameSave.Visible = false;
                    kryptonButtonChangenameCancel.Visible = false;
                    break;

                case "error changename blank":
                    // error the field is blank
                    for (int i = 2; i != 0; i--)
                    {
                        System.Threading.Thread.Sleep(100);
                        kryptonLabelChangenameReq.StateCommon.ShortText.Color1 = Color.Gray;
                        kryptonLabelChangenameReq.StateCommon.ShortText.Color2 = Color.Gray;
                        kryptonLabelChangenameReq.Refresh();
                        System.Threading.Thread.Sleep(100);
                        kryptonLabelChangenameReq.StateCommon.ShortText.Color1 = Color.Red;
                        kryptonLabelChangenameReq.StateCommon.ShortText.Color2 = Color.Red;
                        kryptonLabelChangenameReq.Refresh();
                    }
                    break;

                case "error changename reset":
                    // error change name reset
                    kryptonLabelChangenameReq.StateCommon.ShortText.Color1 = Color.Gray;
                    kryptonLabelChangenameReq.StateCommon.ShortText.Color2 = Color.Gray;
                    kryptonLabelChangenameReq.Refresh();
                    break;

                case "notification deleteaccount":
                    // Set notification
                    currentNotificationTitle = Rlang.minititle37deleteaccount[currentUser.language];
                    currentNotificationContents = Rlang.minititle38deleteaccount[currentUser.language];
                    currentNotificationCommand = "auth deleteaccount2";
                    // Show it
                    Console("interface show notification");
                    break;

                case "notification deletedaccount":
                    // Set notification
                    currentNotificationTitle = Rlang.minititle39deletedaccount[currentUser.language];
                    currentNotificationContents = Rlang.minititle40deletedaccount[currentUser.language];
                    currentNotificationCommand = "reset";
                    // Show it
                    Console("interface show notification");
                    break;

                case "auth deleteaccount2":
                    // Set auth
                    currentAuthCommand = "delete";
                    currentAuthFailCommand = "interface show settings";
                    // Show it
                    Console("interface show auth");
                    break;

                case "delete":
                    // Delete current account
                    try
                    {
                        File.Delete(Rfile.savesPath + @"\" + currentUser.name + ".rcrypt");
                    }
                    catch
                    { }
                    Console("notification deletedaccount");
                    break;

                case "interface show defaultsettings":
                    // hide all interfaces
                    Console("interface disable dashbutton");
                    Console("interface disable passwordlist");

                    InterfaceHide_All();
                    // language
                    kryptonLabelBigTitleDefaultsettings.Text = Rlang.title14defsettings[currentUser.language];
                    kryptonLabelDefaultSettingsLang.Text = Rlang.minititle41deflang[currentUser.language];
                    kryptonLabelDefaultSettingsTheme.Text = Rlang.minititle42deftheme[currentUser.language];
                    kryptonLabelDefaultSettingsOther.Text = Rlang.minititle43defother[currentUser.language];
                    kryptonLabelDefaultSettingsWarn.Text = Rlang.minititle44defwarn[currentUser.language];

                    kryptonButtonDefaultSettingsResetsalt.Text = Rlang.button26resetsalt[currentUser.language];
                    kryptonButtonDefaultSettingsChangesalt.Text = Rlang.button27changesalt[currentUser.language];
                    kryptonButtonConsoleToggle.Text = Rlang.button28toggleconsole[currentUser.language];
                    kryptonButtonDefaultSettingsBack.Text = Rlang.button29defsettingsback[currentUser.language];
                    kryptonButtonDefaultSettingsOpensaves.Text = Rlang.button30opensaves[currentUser.language];
                    kryptonButtonDefaultSettingsOpenbackups.Text = Rlang.button31openbackups[currentUser.language];
                    kryptonButtonDefaultSettingsReset.Text = Rlang.button32globalreset[currentUser.language];
                    // show
                    kryptonLabelBigTitleDefaultsettings.Location = new System.Drawing.Point(276, 12);
                    kryptonLabelBigTitleDefaultsettings.Visible = true;
                    kryptonLabelDefaultSettingsLang.Location = new System.Drawing.Point(276, 56);
                    kryptonLabelDefaultSettingsLang.Visible = true;
                    kryptonLabelDefaultSettingsTheme.Location = new System.Drawing.Point(276, 117);
                    kryptonLabelDefaultSettingsTheme.Visible = true;
                    kryptonLabelDefaultSettingsOther.Location = new System.Drawing.Point(276, 267);
                    kryptonLabelDefaultSettingsOther.Visible = true;
                    kryptonLabelDefaultSettingsWarn.Location = new System.Drawing.Point(276, 301);
                    kryptonLabelDefaultSettingsWarn.Visible = true;

                    kryptonComboBoxLoginLangSelector.Location = new System.Drawing.Point(276, 82);
                    kryptonComboBoxLoginLangSelector.Visible = true;
                    kryptonComboBoxDefaultSettingsIfDarkTheme.Location = new System.Drawing.Point(276, 143);
                    kryptonComboBoxDefaultSettingsIfDarkTheme.Visible = true;

                    kryptonButtonDefaultSettingsResetsalt.Location = new System.Drawing.Point(276, 327);
                    kryptonButtonDefaultSettingsResetsalt.Visible = true;
                    kryptonButtonDefaultSettingsChangesalt.Location = new System.Drawing.Point(276, 366);
                    kryptonButtonDefaultSettingsChangesalt.Visible = true;
                    kryptonButtonConsoleToggle.Location = new System.Drawing.Point(276, 405);
                    if (isConsoleToggleEnabled)
                    {
                        kryptonButtonConsoleToggle.Visible = true;
                    }
                    kryptonButtonDefaultSettingsBack.Location = new System.Drawing.Point(0, 12);
                    kryptonButtonDefaultSettingsBack.Visible = true;
                    kryptonButtonDefaultSettingsOpensaves.Location = new System.Drawing.Point(276, 178);
                    kryptonButtonDefaultSettingsOpensaves.Visible = true;
                    kryptonButtonDefaultSettingsOpenbackups.Location = new System.Drawing.Point(276, 217);
                    kryptonButtonDefaultSettingsOpenbackups.Visible = true;
                    kryptonButtonDefaultSettingsReset.Location = new System.Drawing.Point(276, 444);
                    kryptonButtonDefaultSettingsReset.Visible = true;
                    break;

                case "interface hide defaultsettings":
                    kryptonLabelBigTitleDefaultsettings.Visible = false;
                    kryptonLabelDefaultSettingsLang.Visible = false;
                    kryptonLabelDefaultSettingsTheme.Visible = false;
                    kryptonLabelDefaultSettingsOther.Visible = false;
                    kryptonLabelDefaultSettingsWarn.Visible = false;

                    kryptonComboBoxLoginLangSelector.Visible = false;
                    kryptonComboBoxDefaultSettingsIfDarkTheme.Visible = false;

                    kryptonButtonDefaultSettingsResetsalt.Visible = false;
                    kryptonButtonDefaultSettingsChangesalt.Visible = false;
                    kryptonButtonConsoleToggle.Visible = false;
                    kryptonButtonDefaultSettingsBack.Visible = false;
                    kryptonButtonDefaultSettingsOpensaves.Visible = false;
                    kryptonButtonDefaultSettingsOpenbackups.Visible = false;
                    kryptonButtonDefaultSettingsReset.Visible = false;
                    break;

                case "interface enable console":
                    kryptonTextBoxConsoleCommand.Location = new System.Drawing.Point(588, 12);
                    kryptonTextBoxConsoleCommand.Visible = true;
                    kryptonButtonConsoleSend.Location = new System.Drawing.Point(842, 12);
                    kryptonButtonConsoleSend.Visible = true;
                    break;

                case "interface disable console":
                    kryptonTextBoxConsoleCommand.Visible = false;
                    kryptonButtonConsoleSend.Visible = false;
                    break;

                case "load settings":
                    Console("load defaultsettings");
                    if (File.Exists(Rfile.defaultPath + @"\" + "settings.rpass"))
                    {
                        try
                        {
                            // Load structure
                            string[] linesToSett1 = File.ReadAllLines(Rfile.defaultPath + @"\" + "settings.rpass");
                            List<string> linesToSett2 = new List<string>(linesToSett1);
                            Array.Clear(linesToSett1, 0, linesToSett1.Length);
                            // Initialize current user settings
                            // language
                            currentUser.language = Convert.ToInt32(linesToSett2[0]);
                            // theme
                            if (linesToSett2[1] == "False")
                            {
                                currentUser.ifDarkTheme = false;
                            }
                            else if (linesToSett2[1] == "True")
                            {
                                currentUser.ifDarkTheme = true;
                            }
                            else
                            {
                                currentUser.ifDarkTheme = defaultUser.ifDarkTheme;
                            }
                            // salt
                            if (int.TryParse(linesToSett2[2], out _) && linesToSett2[2].Length == 8)
                            {
                                defaultUser.salt = linesToSett2[2];
                            }
                            else
                            {
                                defaultUser.salt = Rdefaults.defaultSalt;
                            }
                            // console
                            if (linesToSett2[3] == "2")
                            {
                                Console("interface enable console");
                                isConsoleToggleEnabled = true;
                            }
                            else if (linesToSett2[3] == "1")
                            {
                                isConsoleToggleEnabled = true;
                            }
                            else
                            {
                                Console("interface disable console");
                                isConsoleToggleEnabled = false;
                            }
                            linesToSett2.Clear();
                        }
                        catch
                        {
                            Console("load defaultsettings");
                        }

                    }
                    else
                    {
                        Console("load defaultsettings");
                    }
                    break;

                case "load defaultsettings":
                    currentUser.masterPassword = defaultUser.masterPassword;
                    currentUser.language = defaultUser.language;
                    currentUser.name = defaultUser.name;
                    currentUser.salt = defaultUser.salt;
                    currentUser.icon = defaultUser.icon;
                    currentUser.ifDarkTheme = defaultUser.ifDarkTheme;
                    break;

                case "save settings":
                    // Create structure
                    List<string> linesToSett3 = new List<string>();
                    linesToSett3.Clear();

                    linesToSett3.Add(currentUser.language.ToString()); // Line 1: Default language
                    linesToSett3.Add(currentUser.ifDarkTheme.ToString()); // Line 2: Default theme
                    linesToSett3.Add(defaultUser.salt.ToString()); // Line 3: Default salt
                    linesToSett3.Add("0"); // Line 4: Console // usually turned off

                    linesToSett3.Add("");
                    linesToSett3.Add("RPASS SETTINGS: THESE SETTINGS NEED TO BE IN THIS FORMAT:");
                    linesToSett3.Add("Line 1: Default language // DEFAULT VALUE = 0");
                    linesToSett3.Add("Line 2: Default theme // DEFAULT VALUE = False");
                    linesToSett3.Add("Line 3: Default salt // DEFAULT VALUE = empty");
                    linesToSett3.Add("Line 4: Console // DEFAULT VALUE = 0");
                    // Save structure
                    try
                    {
                        File.WriteAllLines(Rfile.defaultPath + @"\" + "settings.rpass", linesToSett3);
                    }
                    catch
                    { }
                    break;

                case "notification saltreset":
                    // Set notification
                    currentNotificationTitle = Rlang.minititle45notifsaltreset[currentUser.language];
                    currentNotificationContents = Rlang.minititle46notifsaltreset[currentUser.language];
                    currentNotificationCommand = "reset";
                    // Show it
                    Console("interface show notification");
                    break;

                case "open savesfolder":
                    try
                    {
                        System.Diagnostics.Process.Start("explorer.exe", Rfile.savesPath);
                    }
                    catch
                    { }
                    break;

                case "open defaultfolder":
                    try
                    {
                        System.Diagnostics.Process.Start("explorer.exe", Rfile.defaultPath);
                    }
                    catch
                    { }
                    break;

                case "open backupsfolder":
                    try
                    {
                        System.Diagnostics.Process.Start("explorer.exe", Rfile.backupsPath);
                    }
                    catch
                    { }
                    break;

                case "interface show changesalt":
                    // hide all interfaces
                    Console("interface disable dashbutton");
                    Console("interface disable passwordlist");
                    Console("error register reset");

                    InterfaceHide_All();
                    // language
                    kryptonLabelBigTitleChangeSalt.Text = Rlang.title15changedefsalt[currentUser.language];

                    kryptonButtonChangeSaltSave.Text = Rlang.button1save[currentUser.language];
                    kryptonButtonChangeSaltCancel.Text = Rlang.button2cancel[currentUser.language];

                    kryptonLabelRegisterSalt.Text = Rlang.minititle22newsecretcode[currentUser.language];
                    kryptonLabelRegisterReq.Text = Rlang.minititle23req2[currentUser.language];
                    // show
                    kryptonLabelBigTitleChangeSalt.Location = new System.Drawing.Point(276, 12);
                    kryptonLabelBigTitleChangeSalt.Visible = true;

                    kryptonButtonChangeSaltSave.Location = new System.Drawing.Point(276, 56);
                    kryptonButtonChangeSaltSave.Visible = true;
                    kryptonButtonChangeSaltCancel.Location = new System.Drawing.Point(363, 56);
                    kryptonButtonChangeSaltCancel.Visible = true;

                    kryptonLabelRegisterSalt.Location = new System.Drawing.Point(276, 113);
                    kryptonLabelRegisterSalt.Visible = true;
                    kryptonLabelRegisterReq.Location = new System.Drawing.Point(276, 207);
                    kryptonLabelRegisterReq.Visible = true;

                    kryptonTextBoxRegisterSalt1.Location = new System.Drawing.Point(276, 139);
                    kryptonTextBoxRegisterSalt1.Text = "";
                    kryptonTextBoxRegisterSalt1.Visible = true;
                    kryptonTextBoxRegisterSalt2.Location = new System.Drawing.Point(304, 139);
                    kryptonTextBoxRegisterSalt2.Text = "";
                    kryptonTextBoxRegisterSalt2.Visible = true;
                    kryptonTextBoxRegisterSalt3.Location = new System.Drawing.Point(332, 139);
                    kryptonTextBoxRegisterSalt3.Text = "";
                    kryptonTextBoxRegisterSalt3.Visible = true;
                    kryptonTextBoxRegisterSalt4.Location = new System.Drawing.Point(360, 139);
                    kryptonTextBoxRegisterSalt4.Text = "";
                    kryptonTextBoxRegisterSalt4.Visible = true;
                    kryptonTextBoxRegisterSalt5.Location = new System.Drawing.Point(388, 139);
                    kryptonTextBoxRegisterSalt5.Text = "";
                    kryptonTextBoxRegisterSalt5.Visible = true;
                    kryptonTextBoxRegisterSalt6.Location = new System.Drawing.Point(416, 139);
                    kryptonTextBoxRegisterSalt6.Text = "";
                    kryptonTextBoxRegisterSalt6.Visible = true;
                    kryptonTextBoxRegisterSalt7.Location = new System.Drawing.Point(444, 139);
                    kryptonTextBoxRegisterSalt7.Text = "";
                    kryptonTextBoxRegisterSalt7.Visible = true;
                    kryptonTextBoxRegisterSalt8.Location = new System.Drawing.Point(472, 139);
                    kryptonTextBoxRegisterSalt8.Text = "";
                    kryptonTextBoxRegisterSalt8.Visible = true;
                    break;

                case "interface hide changesalt":
                    kryptonLabelBigTitleChangeSalt.Visible = false;

                    kryptonButtonChangeSaltSave.Visible = false;
                    kryptonButtonChangeSaltCancel.Visible = false;

                    kryptonLabelRegisterSalt.Visible = false;
                    kryptonLabelRegisterReq.Visible = false;

                    kryptonTextBoxRegisterSalt1.Visible = false;
                    kryptonTextBoxRegisterSalt2.Visible = false;
                    kryptonTextBoxRegisterSalt3.Visible = false;
                    kryptonTextBoxRegisterSalt4.Visible = false;
                    kryptonTextBoxRegisterSalt5.Visible = false;
                    kryptonTextBoxRegisterSalt6.Visible = false;
                    kryptonTextBoxRegisterSalt7.Visible = false;
                    kryptonTextBoxRegisterSalt8.Visible = false;
                    break;

                case "changesalt":
                    // Reset error
                    Console("error register reset");
                    // Checking if there's empty forms
                    if (kryptonTextBoxRegisterSalt1.Text == "" ||
                        kryptonTextBoxRegisterSalt2.Text == "" ||
                        kryptonTextBoxRegisterSalt3.Text == "" ||
                        kryptonTextBoxRegisterSalt4.Text == "" ||
                        kryptonTextBoxRegisterSalt5.Text == "" ||
                        kryptonTextBoxRegisterSalt6.Text == "" ||
                        kryptonTextBoxRegisterSalt7.Text == "" ||
                        kryptonTextBoxRegisterSalt8.Text == "")
                    {
                        // Empty forms, return error
                        Console("error register reqforms");
                    }
                    else
                    {
                        // There's not empty forms
                        // Checking if salt is in valid form

                        // Create a list for an easier comparison
                        List<string> registerCompareSalts = new List<string>();
                        registerCompareSalts.Clear();
                        registerCompareSalts.Add(kryptonTextBoxRegisterSalt1.Text);
                        registerCompareSalts.Add(kryptonTextBoxRegisterSalt2.Text);
                        registerCompareSalts.Add(kryptonTextBoxRegisterSalt3.Text);
                        registerCompareSalts.Add(kryptonTextBoxRegisterSalt4.Text);
                        registerCompareSalts.Add(kryptonTextBoxRegisterSalt5.Text);
                        registerCompareSalts.Add(kryptonTextBoxRegisterSalt6.Text);
                        registerCompareSalts.Add(kryptonTextBoxRegisterSalt7.Text);
                        registerCompareSalts.Add(kryptonTextBoxRegisterSalt8.Text);

                        // Compare the list
                        bool registerIsSaltValid = true;
                        for (int index = 0; index <= 7; index++)
                        {
                            if (!(registerCompareSalts[index] == "0" ||
                            registerCompareSalts[index] == "1" ||
                            registerCompareSalts[index] == "2" ||
                            registerCompareSalts[index] == "3" ||
                            registerCompareSalts[index] == "4" ||
                            registerCompareSalts[index] == "5" ||
                            registerCompareSalts[index] == "6" ||
                            registerCompareSalts[index] == "7" ||
                            registerCompareSalts[index] == "8" ||
                            registerCompareSalts[index] == "9"))
                            {
                                // Form invalid, returning error
                                registerIsSaltValid = false;
                            }
                        }
                        registerCompareSalts.Clear();

                        // Continue
                        if (registerIsSaltValid)
                        {
                            // Save new default salt

                            // Prepare it for saving
                            int crDefaultSalt = 0;
                            crDefaultSalt = crDefaultSalt + Convert.ToInt32(kryptonTextBoxRegisterSalt1.Text); // index 0
                            crDefaultSalt = crDefaultSalt + (Convert.ToInt32(kryptonTextBoxRegisterSalt2.Text) * 10); // index 1
                            crDefaultSalt = crDefaultSalt + (Convert.ToInt32(kryptonTextBoxRegisterSalt3.Text) * 100); // index 2
                            crDefaultSalt = crDefaultSalt + (Convert.ToInt32(kryptonTextBoxRegisterSalt4.Text) * 1000); // index 3
                            crDefaultSalt = crDefaultSalt + (Convert.ToInt32(kryptonTextBoxRegisterSalt5.Text) * 10000); // index 4
                            crDefaultSalt = crDefaultSalt + (Convert.ToInt32(kryptonTextBoxRegisterSalt6.Text) * 100000); // index 5
                            crDefaultSalt = crDefaultSalt + (Convert.ToInt32(kryptonTextBoxRegisterSalt7.Text) * 1000000); // index 6
                            crDefaultSalt = crDefaultSalt + (Convert.ToInt32(kryptonTextBoxRegisterSalt8.Text) * 10000000); // index 7
                            // Save it
                            defaultUser.salt = crDefaultSalt.ToString();
                            crDefaultSalt = 0;
                            Console("save settings");
                            // Continue
                            Console("notification saltchanged");
                        }
                        else
                        {
                            // Form invalid, returning error
                            Console("error register reqforms");
                        }
                    }
                    break;

                case "notification saltchanged":
                    // Set notification
                    currentNotificationTitle = Rlang.minititle48notifsaltchanged[currentUser.language];
                    currentNotificationContents = Rlang.minititle47notifsaltchanged[currentUser.language];
                    currentNotificationCommand = "interface show defaultsettings";
                    // Show it
                    Console("interface show notification");
                    break;
            }
        }
    }
}
