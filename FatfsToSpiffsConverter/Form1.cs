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
        public MainForm()
        {
            InitializeComponent();

            MainHandler.FormInstance = this;

            m_Proto = MessagesProto.Instance;            

            UserSettings userSet = Settings.Instance.UsSettings;
            MainSettings mainSet = Settings.Instance.MnSettings;

            label_profileName.Text = userSet.currentProfile;

            profilesList = Settings.Instance.GetProfilesList();
            comboBox_Profile.DataSource = profilesList;
            comboBox_Profile.SelectedItem = userSet.currentProfile;

            comPortsList = ComPortProcess.GetPorts();
            comboBox_ComPorts.DataSource = comPortsList;
            comboBox_ComPorts.SelectedItem = userSet.portName;

        }

        private void ComboBox_ComPorts_DropDown(object sender, EventArgs e)
        {
            comPortsList = ComPortProcess.GetPorts();
        }

        private void ComboBox_ComPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserSettings userSet = Settings.Instance.UsSettings;
            userSet.portName = comboBox_ComPorts.SelectedItem.ToString();
            Settings.Instance.UsSettings = userSet;
            ComPortProcess.Instance.portChanged = true;
        }

        private void button_StartFlash_Click(object sender, EventArgs e)
        {
            MainSettings mainSet = Settings.Instance.MnSettings;
            MessageSettings msg;
            msg.flashSize = mainSet.flashSize;
            msg.spiffsAddr = mainSet.spiffsAddress;
            msg.eraseSize = mainSet.eraseSize;
            msg.pageSize = mainSet.logPageSize;
            msg.logBlockSize = mainSet.blockSize;
            msg.allowFormating = Convert.ToUInt32(mainSet.allowFormating);            

            MainHandler.isStarted = true;
            m_Proto.SendMessageSettings(msg);
        }

        private void comboBox_Profile_DropDown(object sender, EventArgs e)
        {
            profilesList = Settings.Instance.GetProfilesList();
        }

        private void comboBox_Profile_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserSettings userSet = Settings.Instance.UsSettings;
            userSet.currentProfile = comboBox_Profile.SelectedItem.ToString();
            Settings.Instance.UsSettings = userSet;
            label_profileName.Text = userSet.currentProfile;
        }
    }
}
