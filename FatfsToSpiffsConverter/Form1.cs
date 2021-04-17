using FatfsToSpiffsConverter.Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
