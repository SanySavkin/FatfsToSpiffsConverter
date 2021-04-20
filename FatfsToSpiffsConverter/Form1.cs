using FatfsToSpiffsConverter.Communication;
using System;
using System.Collections.Generic;
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

            UserSettings userSet = Settings.Instance.UsSettings;
            MainSettings mainSet = Settings.Instance.MnSettings;

            label_profileName.Text = userSet.currentProfile;

            Profiles_UpdateList();
            SetParametersValues(mainSet);

            comPortsList = ComPortProcess.GetPorts();
            comboBox_ComPorts.DataSource = comPortsList;
            comboBox_ComPorts.SelectedItem = userSet.portName;

            CreateProfile_UpdateUI(false);
            SetProfile_UpdateUI();
        }

        private void ComboBox_ComPorts_DropDown(object sender, EventArgs e)
        {
            isDropDownComPorts = true;
            comPortsList = ComPortProcess.GetPorts();
            comboBox_ComPorts.DataSource = comPortsList;
            comboBox_ComPorts.SelectedItem = Settings.Instance.UsSettings.portName;
            isDropDownComPorts = false;
        }

        private void ComboBox_ComPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isDropDownComPorts) return;
            UserSettings userSet = Settings.Instance.UsSettings;
            userSet.portName = comboBox_ComPorts.SelectedItem.ToString();
            Settings.Instance.UsSettings = userSet;
            ComPortProcess.Instance.portChanged = true;
        }

        private void button_StartFlash_Click(object sender, EventArgs e)
        {
            MainHandler.StartFlash();
        }

        private void button_imageTabStart_Click(object sender, EventArgs e)
        {
            MainHandler.StartWriteImage();
        }

        private void comboBox_Profile_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserSettings userSet = Settings.Instance.UsSettings;
            userSet.currentProfile = comboBox_Profile.SelectedItem.ToString();
            Settings.Instance.UsSettings = userSet;

            label_profileName.Text = userSet.currentProfile;

            MainSettings mainSet = Settings.Instance.MnSettings;
            SetParametersValues(mainSet);
            SetProfile_UpdateUI();
        }
        private void radioButton_Profile_CheckedChanged(object sender, EventArgs e)
        {
            CreateProfile_UpdateUI(!radioButton_Profile.Checked);
            SetProfile_UpdateUI();
        }

        private void button_AddToProfile_Click(object sender, EventArgs e)
        {
            if(textBox_profileName.Text == "" || textBox_profileName.Text == "введитя имя профиля")
            {
                MessageBox.Show("Введите имя профиля", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
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
                        pathSpiffs = textBox_PathSpiffs.Text,
                        pathFatfs = textBox_PathFatfs.Text
                    };
                    if (Settings.Instance.SaveProfile(set, textBox_profileName.Text))
                    {
                        Profiles_UpdateList();
                        MessageBox.Show("Профиль успешно добавлен", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                textBox_profileName.Text = "введитя имя профиля";
            }
        }
        private void button_RemoveProfile_Click(object sender, EventArgs e)
        {
            Settings.Instance.RemoveProfile(comboBox_Profile.SelectedItem.ToString());
            Profiles_UpdateList();
            MessageBox.Show("Профиль успешно удалён", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }


        private void textBox_profileName_GotFocus(object sender, EventArgs e)
        {
            textBox_profileName.Text = "";
        }

        private void textBox_profileName_LostFocus(object sender, EventArgs e)
        {
            if(textBox_profileName.Text == "") textBox_profileName.Text = "введитя имя профиля";
        }        

        private void CreateProfile_UpdateUI(bool st)
        {
            label3.Enabled = st;
            label4.Enabled = st;
            label5.Enabled = st;
            label6.Enabled = st;
            label7.Enabled = st;
            label8.Enabled = st;
            label10.Enabled = st;
            label11.Enabled = st;

            textBox_BlockSize.Enabled = st;
            textBox_EraseSize.Enabled = st;
            textBox_FlashSize.Enabled = st;
            textBox_PageSize.Enabled = st;
            textBox_PathFatfs.Enabled = st;
            textBox_PathSpiffs.Enabled = st;
            textBox_StartAddress.Enabled = st;
            textBox_profileName.Enabled = st;

            checkBox_FormatingSpiffs.Enabled = st;
            button_AddToProfile.Enabled = st;
            if (st) button_AddToProfile.BackColor = System.Drawing.Color.LightGreen;
            else button_AddToProfile.BackColor = System.Drawing.Color.LightGray;
        }

        private void SetParametersValues(MainSettings set)
        {
            textBox_BlockSize.Text = set.blockSize.ToString("X");
            textBox_EraseSize.Text = set.eraseSize.ToString("X");
            textBox_FlashSize.Text = set.flashSize.ToString("X");
            textBox_PageSize.Text = set.logPageSize.ToString("X");
            textBox_StartAddress.Text = set.spiffsAddress.ToString("X");
            textBox_PathFatfs.Text = set.pathFatfs;
            textBox_PathSpiffs.Text = set.pathSpiffs;
            checkBox_FormatingSpiffs.Checked = set.allowFormating;
        }

        private void Profiles_UpdateList()
        {
            UserSettings userSet = Settings.Instance.UsSettings;
            profilesList = Settings.Instance.GetProfilesList();
            comboBox_Profile.DataSource = profilesList;
            comboBox_Profile.SelectedItem = userSet.currentProfile;
        }

        private void SetProfile_UpdateUI()
        {
            if (radioButton_Profile.Checked)
            {
                comboBox_Profile.Enabled = true;
                button_RemoveProfile.BackColor = System.Drawing.Color.Red;

                UserSettings userSet = Settings.Instance.UsSettings;
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

        
    }
}
