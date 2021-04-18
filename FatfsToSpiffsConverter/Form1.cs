using FatfsToSpiffsConverter.Communication;
using System;
using System.Windows.Forms;

namespace FatfsToSpiffsConverter
{
    public partial class MainForm : Form
    {
        private MessagesProto m_Proto;
        public MainForm()
        {
            InitializeComponent();

            m_Proto = MessagesProto.Instance;

            m_Proto.SetForm(this);

            UserSettings userSet = Settings.Instance.UsSettings;
            MainSettings mainSet = Settings.Instance.MnSettings;

            label_profileName.Text = userSet.currentProfile;

            var profilesList = Settings.Instance.GetProfilesList();
            comboBox_Profile.DataSource = profilesList;
            comboBox_Profile.SelectedItem = userSet.currentProfile;

        }

        private void ComboBox_ComPorts_DropDown(object sender, EventArgs e)
        {
            var comPortsList = ComPortProcess.GetPorts();
            ComboBox_ComPorts.DataSource = comPortsList;
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

            //msg.flashSize = 0x100000;
            //msg.spiffsAddr = 0x100000;
            //msg.eraseSize = 0x1000;
            //msg.pageSize = 0x100;
            //msg.logBlockSize = 0x1000;
            //msg.allowFormating = 0;

            m_Proto.isStarted = true;
            m_Proto.SendMessageSettings(msg);
        }

        private void comboBox_Profile_DropDown(object sender, EventArgs e)
        {
            var profilesList = Settings.Instance.GetProfilesList();
            comboBox_Profile.DataSource = profilesList;
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
