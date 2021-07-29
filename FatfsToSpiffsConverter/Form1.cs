using FatfsToSpiffsConverter.Communication;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace FatfsToSpiffsConverter
{
    public partial class MainForm : Form
    {
        private MessagesProto m_Proto;
        private List<string> comPortsList;
        private List<string> profilesList;

        private bool isDropDownComPorts = false;

        public MainForm()
        {
            InitializeComponent();

            MainHandler.FormInstance = this;

            m_Proto = MessagesProto.Instance;            

            UserSettings userSet = Settings.UsSettings;
            MainSettings mainSet = Settings.MnSettings;

            label_profileName.Text = userSet.currentProfile;

            Profiles_UpdateList();
            SetSpiffsValues(mainSet);

            comPortsList = ComPortProcess.GetPorts();
            comboBox_ComPorts.DataSource = comPortsList;
            comboBox_ComPorts.SelectedItem = userSet.portName;

            CreateProfile_UpdateUI(false);
            SetProfile_UpdateUI();
            comboBox_FlashType.SelectedIndex = 2;

            label_version.Text = "version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void ComboBox_ComPorts_DropDown(object sender, EventArgs e)
        {
            isDropDownComPorts = true;
            comPortsList = ComPortProcess.GetPorts();
            comboBox_ComPorts.DataSource = comPortsList;
            comboBox_ComPorts.SelectedItem = Settings.UsSettings.portName;
            isDropDownComPorts = false;
        }

        private void ComboBox_ComPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isDropDownComPorts) return;
            UserSettings userSet = Settings.UsSettings;
            userSet.portName = comboBox_ComPorts.SelectedItem.ToString();
            Settings.UsSettings = userSet;
            ComPortProcess.Instance.portChanged = true;
        }

        private void button_StartFlash_Click(object sender, EventArgs e)
        {
            MainHandler.StartFlash();
        }

        private void button_imageTabStart_Click(object sender, EventArgs e)
        {
            MainHandler.StartCreateImage();
        }

        private void comboBox_Profile_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserSettings userSet = Settings.UsSettings;
            userSet.currentProfile = comboBox_Profile.SelectedItem.ToString();
            Settings.UsSettings = userSet;

            label_profileName.Text = userSet.currentProfile;

            MainSettings mainSet = Settings.MnSettings;
            SetSpiffsValues(mainSet);
            SetProfile_UpdateUI();
        }
        private void radioButton_Profile_CheckedChanged(object sender, EventArgs e)
        {
            CreateProfile_UpdateUI(radioButton_CreateProfile.Checked);
            SetProfile_UpdateUI();
            SetSpiffsValues(Settings.MnSettings);
        }

        private void button_AddToProfile_Click(object sender, EventArgs e)
        {
            if(textBox_profileName.Text == "" || textBox_profileName.Text == "введитя имя профиля")
            {
                MessageBox.Show("Введите имя профиля", Program.programName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                try
                {
                    MainSettings set = new MainSettings
                    {
                        flashSize = uint.Parse(textBox_FlashSize.Text, System.Globalization.NumberStyles.HexNumber),
                        spiffsAddress = uint.Parse(textBox_StartAddress.Text, System.Globalization.NumberStyles.HexNumber),
                        eraseSize = uint.Parse(textBox_EraseSize.Text, System.Globalization.NumberStyles.HexNumber),
                        logPageSize = uint.Parse(textBox_PageSize.Text, System.Globalization.NumberStyles.HexNumber),
                        blockSize = uint.Parse(textBox_BlockSize.Text, System.Globalization.NumberStyles.HexNumber),
                        allowFormating = checkBox_FormatingSpiffs.Checked,
                        useSpiffs = checkBox_useSpiffs.Checked,
                        pathSpiffs = textBox_PathSpiffs.Text,
                        pathFatfs = textBox_PathFatfs.Text
                    };
                    if (Settings.SaveProfile(set, textBox_profileName.Text))
                    {
                        Profiles_UpdateList();
                        MessageBox.Show("Профиль успешно добавлен", Program.programName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    }
                    else
                    {
                        MessageBox.Show("Имя профиля уже существует!", Program.programName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Program.programName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                textBox_profileName.Text = "введитя имя профиля";
            }
        }

        private void button_RemoveProfile_Click(object sender, EventArgs e)
        {
            var profName = Settings.UsSettings.currentProfile;
            var res = MessageBox.Show("Удалить профиль: " + profName, Program.programName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (res == DialogResult.Yes)
            {
                Settings.RemoveProfile(comboBox_Profile.SelectedItem.ToString());
                Profiles_UpdateList();
                MessageBox.Show("Профиль успешно удалён", Program.programName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void textBox_profileName_GotFocus(object sender, EventArgs e)
        {
            textBox_profileName.Text = "";
        }

        private void textBox_profileName_LostFocus(object sender, EventArgs e)
        {
            if(textBox_profileName.Text == "") textBox_profileName.Text = "введитя имя профиля";
        }

        private void checkBox_useSpiffs_CheckedChanged(object sender, EventArgs e)
        {
            CreateProfile_UpdateUI(radioButton_CreateProfile.Checked);
        }

        private void CreateProfile_UpdateUI(bool st)
        {
            var isEnSpiffsVal = checkBox_useSpiffs.Checked && st;

            label3.Enabled = isEnSpiffsVal;
            label4.Enabled = isEnSpiffsVal;
            label5.Enabled = isEnSpiffsVal;
            label6.Enabled = isEnSpiffsVal;
            label7.Enabled = isEnSpiffsVal;
            label8.Enabled = isEnSpiffsVal;
                      

            textBox_BlockSize.Enabled = isEnSpiffsVal;
            textBox_EraseSize.Enabled = isEnSpiffsVal;
            textBox_FlashSize.Enabled = isEnSpiffsVal;
            textBox_PageSize.Enabled = isEnSpiffsVal;            
            textBox_PathSpiffs.Enabled = isEnSpiffsVal;
            textBox_StartAddress.Enabled = isEnSpiffsVal;
            checkBox_FormatingSpiffs.Enabled = isEnSpiffsVal;


            label10.Enabled = st;
            label11.Enabled = st;
            textBox_PathFatfs.Enabled = st;
            textBox_profileName.Enabled = st;

            checkBox_useSpiffs.Enabled = st;
            
            button_AddToProfile.Enabled = st;
            if (st) button_AddToProfile.BackColor = System.Drawing.Color.LightGreen;
            else button_AddToProfile.BackColor = System.Drawing.Color.LightGray;
        }

        private void SetSpiffsValues(MainSettings set)
        {
            textBox_BlockSize.Text = set.blockSize.ToString("X");
            textBox_EraseSize.Text = set.eraseSize.ToString("X");
            textBox_FlashSize.Text = set.flashSize.ToString("X");
            textBox_PageSize.Text = set.logPageSize.ToString("X");
            textBox_StartAddress.Text = set.spiffsAddress.ToString("X");
            textBox_PathFatfs.Text = set.pathFatfs;
            textBox_PathSpiffs.Text = set.pathSpiffs;
            checkBox_FormatingSpiffs.Checked = set.allowFormating;
            checkBox_useSpiffs.Checked = set.useSpiffs;
        }

        private void Profiles_UpdateList()
        {
            UserSettings userSet = Settings.UsSettings;
            profilesList = Settings.GetProfilesList();
            comboBox_Profile.DataSource = profilesList;
            comboBox_Profile.SelectedItem = userSet.currentProfile;
        }

        private void SetProfile_UpdateUI()
        {
            if (radioButton_Profile.Checked)
            {
                comboBox_Profile.Enabled = true;
                button_RemoveProfile.BackColor = System.Drawing.Color.Red;

                UserSettings userSet = Settings.UsSettings;
                button_RemoveProfile.Enabled = userSet.currentProfile != Settings.indoorTagerProfileName &&
                userSet.currentProfile != Settings.indoorVestProfileName &&
                userSet.currentProfile != Settings.outdoorTagerProfileName;
                if(button_RemoveProfile.Enabled) button_RemoveProfile.BackColor = System.Drawing.Color.Red;
                else button_RemoveProfile.BackColor = System.Drawing.Color.LightGray;
            }
            else
            {
                comboBox_Profile.Enabled = false;
                button_RemoveProfile.BackColor = System.Drawing.Color.LightGray;
                button_RemoveProfile.Enabled = false;
            }
        }

        private bool StringIsOk(string str)
        {
            var symbolsOk = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890_";

            if (str.Length > 1)
            {
                var last = str.Remove(0, str.Length - 1);
                return symbolsOk.Contains(last);
            }
            else
            {
                return symbolsOk.Contains(str);
            }
        }

        private void textBox_imageTabPath_TextChanged(object sender, EventArgs e)
        {
            var text = textBox_imageTabPath.Text;
            if (!StringIsOk(text))
            {
                textBox_imageTabPath.Text = text.Remove(text.Length - 1);
                MessageBox.Show("Недопустимый символ в имени файла");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var msg = Help.OpenFile();
            if(msg != "") MessageBox.Show(msg, Program.programName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }
    }
}
