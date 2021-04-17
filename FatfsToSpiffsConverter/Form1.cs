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
            MainSettings set = new MainSettings();
            set.blockSize = 0x01000;
            set.eraseSize = 0x1000;
            set.flashSize = 0x100000;
            set.allowFormating = true;
            set.pathFatfs = "snd/en";
            set.pathSpiffs = "snd/en";
            Settings.WriteSettings(set, "indoor_vest");
            Settings.ReadSettings("indoor_vest");
        }

        private void ComboBox_ComPorts_DropDown(object sender, EventArgs e)
        {

        }
    }
}
