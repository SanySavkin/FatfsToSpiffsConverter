
namespace FatfsToSpiffsConverter
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label9 = new System.Windows.Forms.Label();
            this.ComboBox_ComPorts = new System.Windows.Forms.ComboBox();
            this.connectionLabel = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label_profileName = new System.Windows.Forms.Label();
            this.button_StartFlash = new System.Windows.Forms.Button();
            this.label_Message = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radioButton_User = new System.Windows.Forms.RadioButton();
            this.radioButton_Profile = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_Profile = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_FlashSize = new System.Windows.Forms.TextBox();
            this.textBox_StartAddress = new System.Windows.Forms.TextBox();
            this.textBox_PageSize = new System.Windows.Forms.TextBox();
            this.textBox_BlockSize = new System.Windows.Forms.TextBox();
            this.textBox_EraseSize = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBox_FormatingSpiffs = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_PathFatfs = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_RemoveProfile = new System.Windows.Forms.Button();
            this.button_AddToProfile = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox_PathSpiffs = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.ItemSize = new System.Drawing.Size(70, 25);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(484, 451);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(476, 418);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Flash";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.progressBar1, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.ComboBox_ComPorts, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.connectionLabel, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label_profileName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.button_StartFlash, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label_Message, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 9;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(470, 412);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.progressBar1, 2);
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(50, 253);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(50, 3, 50, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(370, 24);
            this.progressBar1.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(187, 343);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 17);
            this.label9.TabIndex = 1;
            this.label9.Text = "Порт:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ComboBox_ComPorts
            // 
            this.ComboBox_ComPorts.FormattingEnabled = true;
            this.ComboBox_ComPorts.Location = new System.Drawing.Point(238, 343);
            this.ComboBox_ComPorts.MaximumSize = new System.Drawing.Size(90, 0);
            this.ComboBox_ComPorts.MinimumSize = new System.Drawing.Size(90, 0);
            this.ComboBox_ComPorts.Name = "ComboBox_ComPorts";
            this.ComboBox_ComPorts.Size = new System.Drawing.Size(90, 24);
            this.ComboBox_ComPorts.TabIndex = 2;
            this.ComboBox_ComPorts.DropDown += new System.EventHandler(this.ComboBox_ComPorts_DropDown);
            // 
            // connectionLabel
            // 
            this.connectionLabel.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.connectionLabel, 2);
            this.connectionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionLabel.Location = new System.Drawing.Point(3, 310);
            this.connectionLabel.Name = "connectionLabel";
            this.connectionLabel.Size = new System.Drawing.Size(464, 30);
            this.connectionLabel.TabIndex = 3;
            this.connectionLabel.Text = "Connected";
            this.connectionLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(100, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(132, 17);
            this.label13.TabIndex = 4;
            this.label13.Text = "Текущий профиль:";
            // 
            // label_profileName
            // 
            this.label_profileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_profileName.AutoSize = true;
            this.label_profileName.Location = new System.Drawing.Point(238, 13);
            this.label_profileName.Name = "label_profileName";
            this.label_profileName.Size = new System.Drawing.Size(96, 17);
            this.label_profileName.TabIndex = 5;
            this.label_profileName.Text = "имя профиля";
            // 
            // button_StartFlash
            // 
            this.button_StartFlash.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button_StartFlash.BackColor = System.Drawing.Color.SpringGreen;
            this.tableLayoutPanel2.SetColumnSpan(this.button_StartFlash, 2);
            this.button_StartFlash.Location = new System.Drawing.Point(161, 63);
            this.button_StartFlash.Name = "button_StartFlash";
            this.button_StartFlash.Size = new System.Drawing.Size(147, 64);
            this.button_StartFlash.TabIndex = 6;
            this.button_StartFlash.Text = "START";
            this.button_StartFlash.UseVisualStyleBackColor = false;
            this.button_StartFlash.Click += new System.EventHandler(this.button_StartFlash_Click);
            // 
            // label_Message
            // 
            this.label_Message.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Message.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.label_Message, 2);
            this.label_Message.Location = new System.Drawing.Point(235, 160);
            this.label_Message.Margin = new System.Windows.Forms.Padding(3, 30, 3, 0);
            this.label_Message.Name = "label_Message";
            this.label_Message.Size = new System.Drawing.Size(0, 17);
            this.label_Message.TabIndex = 7;
            this.label_Message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(476, 418);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.radioButton_User, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.radioButton_Profile, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_Profile, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.textBox_FlashSize, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBox_StartAddress, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBox_PageSize, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBox_BlockSize, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.textBox_EraseSize, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_FormatingSpiffs, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.textBox_PathFatfs, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.button_RemoveProfile, 2, 13);
            this.tableLayoutPanel1.Controls.Add(this.button_AddToProfile, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.textBox_PathSpiffs, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 12);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 14;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(470, 412);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // radioButton_User
            // 
            this.radioButton_User.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton_User.AutoSize = true;
            this.radioButton_User.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.radioButton_User, 2);
            this.radioButton_User.Location = new System.Drawing.Point(6, 32);
            this.radioButton_User.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.radioButton_User.Name = "radioButton_User";
            this.radioButton_User.Size = new System.Drawing.Size(279, 30);
            this.radioButton_User.TabIndex = 0;
            this.radioButton_User.TabStop = true;
            this.radioButton_User.Text = "Создать профиль";
            this.radioButton_User.UseVisualStyleBackColor = false;
            // 
            // radioButton_Profile
            // 
            this.radioButton_Profile.AutoSize = true;
            this.radioButton_Profile.Location = new System.Drawing.Point(291, 35);
            this.radioButton_Profile.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.radioButton_Profile.Name = "radioButton_Profile";
            this.radioButton_Profile.Size = new System.Drawing.Size(145, 21);
            this.radioButton_Profile.TabIndex = 1;
            this.radioButton_Profile.TabStop = true;
            this.radioButton_Profile.Text = "Выбрать профиль";
            this.radioButton_Profile.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 3);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(468, 30);
            this.label1.TabIndex = 2;
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.label2, 2);
            this.label2.Location = new System.Drawing.Point(1, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "Spiffs";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox_Profile
            // 
            this.comboBox_Profile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_Profile.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox_Profile.FormattingEnabled = true;
            this.comboBox_Profile.Location = new System.Drawing.Point(306, 66);
            this.comboBox_Profile.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.comboBox_Profile.Name = "comboBox_Profile";
            this.comboBox_Profile.Size = new System.Drawing.Size(143, 24);
            this.comboBox_Profile.TabIndex = 4;
            this.comboBox_Profile.DropDown += new System.EventHandler(this.comboBox_Profile_DropDown);
            this.comboBox_Profile.SelectedIndexChanged += new System.EventHandler(this.comboBox_Profile_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(4, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Flash size:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.label3, "Размер памяти выделенной \r\nпод SPIFFS");
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(4, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Start address:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.label4, "Адресс флеш-памяти с которого \r\nначинается SPIFFS");
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(4, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "Log. page size:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.label5, "Размер логической страницы файловой\r\nсистемы  SPIFFS");
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(4, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 25);
            this.label6.TabIndex = 8;
            this.label6.Text = "Block size:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.label6, "Размер логического блока\r\nфайловой системы SPIFFS");
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(4, 198);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 25);
            this.label7.TabIndex = 9;
            this.label7.Text = "Erase size:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.label7, "Размер стираемого блока");
            // 
            // textBox_FlashSize
            // 
            this.textBox_FlashSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox_FlashSize.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_FlashSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_FlashSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_FlashSize.Location = new System.Drawing.Point(105, 97);
            this.textBox_FlashSize.Name = "textBox_FlashSize";
            this.textBox_FlashSize.Size = new System.Drawing.Size(100, 19);
            this.textBox_FlashSize.TabIndex = 10;
            this.toolTip1.SetToolTip(this.textBox_FlashSize, "HEX формат без префикса 0x");
            // 
            // textBox_StartAddress
            // 
            this.textBox_StartAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox_StartAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_StartAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_StartAddress.Location = new System.Drawing.Point(105, 123);
            this.textBox_StartAddress.Name = "textBox_StartAddress";
            this.textBox_StartAddress.Size = new System.Drawing.Size(100, 19);
            this.textBox_StartAddress.TabIndex = 11;
            this.toolTip1.SetToolTip(this.textBox_StartAddress, "HEX формат без префикса 0x");
            // 
            // textBox_PageSize
            // 
            this.textBox_PageSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox_PageSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_PageSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_PageSize.Location = new System.Drawing.Point(105, 149);
            this.textBox_PageSize.Name = "textBox_PageSize";
            this.textBox_PageSize.Size = new System.Drawing.Size(100, 19);
            this.textBox_PageSize.TabIndex = 12;
            this.toolTip1.SetToolTip(this.textBox_PageSize, "HEX формат без префикса 0x");
            // 
            // textBox_BlockSize
            // 
            this.textBox_BlockSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox_BlockSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_BlockSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_BlockSize.Location = new System.Drawing.Point(105, 175);
            this.textBox_BlockSize.Name = "textBox_BlockSize";
            this.textBox_BlockSize.Size = new System.Drawing.Size(100, 19);
            this.textBox_BlockSize.TabIndex = 13;
            this.toolTip1.SetToolTip(this.textBox_BlockSize, "HEX формат без префикса 0x");
            // 
            // textBox_EraseSize
            // 
            this.textBox_EraseSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox_EraseSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_EraseSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_EraseSize.Location = new System.Drawing.Point(105, 201);
            this.textBox_EraseSize.Name = "textBox_EraseSize";
            this.textBox_EraseSize.Size = new System.Drawing.Size(100, 19);
            this.textBox_EraseSize.TabIndex = 14;
            this.toolTip1.SetToolTip(this.textBox_EraseSize, "HEX формат без префикса 0x");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(4, 224);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 25);
            this.label8.TabIndex = 15;
            this.label8.Text = "Path:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.label8, "Путь в который будут скопированы файлы");
            // 
            // checkBox_FormatingSpiffs
            // 
            this.checkBox_FormatingSpiffs.AutoSize = true;
            this.checkBox_FormatingSpiffs.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.checkBox_FormatingSpiffs, 2);
            this.checkBox_FormatingSpiffs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox_FormatingSpiffs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_FormatingSpiffs.Location = new System.Drawing.Point(9, 250);
            this.checkBox_FormatingSpiffs.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.checkBox_FormatingSpiffs.Name = "checkBox_FormatingSpiffs";
            this.checkBox_FormatingSpiffs.Size = new System.Drawing.Size(276, 25);
            this.checkBox_FormatingSpiffs.TabIndex = 18;
            this.checkBox_FormatingSpiffs.Text = "форматировать перед копированием";
            this.checkBox_FormatingSpiffs.UseVisualStyleBackColor = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.label10, 2);
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(1, 276);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(284, 30);
            this.label10.TabIndex = 19;
            this.label10.Text = "Fatfs";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(4, 307);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 25);
            this.label11.TabIndex = 20;
            this.label11.Text = "Path:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.label11, "Путь с которого будут скопированы\r\nфайлы");
            // 
            // textBox_PathFatfs
            // 
            this.textBox_PathFatfs.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox_PathFatfs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_PathFatfs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_PathFatfs.Location = new System.Drawing.Point(105, 310);
            this.textBox_PathFatfs.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.textBox_PathFatfs.Name = "textBox_PathFatfs";
            this.textBox_PathFatfs.Size = new System.Drawing.Size(100, 19);
            this.textBox_PathFatfs.TabIndex = 21;
            this.toolTip1.SetToolTip(this.textBox_PathFatfs, "Например:\r\nsnd/en\r\nsnd\r\n/");
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(286, 94);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBox1, 9);
            this.pictureBox1.Size = new System.Drawing.Size(183, 238);
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // button_RemoveProfile
            // 
            this.button_RemoveProfile.BackColor = System.Drawing.Color.Tomato;
            this.button_RemoveProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_RemoveProfile.Location = new System.Drawing.Point(289, 367);
            this.button_RemoveProfile.Name = "button_RemoveProfile";
            this.button_RemoveProfile.Size = new System.Drawing.Size(177, 41);
            this.button_RemoveProfile.TabIndex = 24;
            this.button_RemoveProfile.Text = "Удалить профиль";
            this.button_RemoveProfile.UseVisualStyleBackColor = false;
            // 
            // button_AddToProfile
            // 
            this.button_AddToProfile.BackColor = System.Drawing.Color.LightGreen;
            this.button_AddToProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_AddToProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_AddToProfile.Location = new System.Drawing.Point(4, 367);
            this.button_AddToProfile.Name = "button_AddToProfile";
            this.button_AddToProfile.Size = new System.Drawing.Size(94, 41);
            this.button_AddToProfile.TabIndex = 25;
            this.button_AddToProfile.Text = "Сохранить";
            this.button_AddToProfile.UseVisualStyleBackColor = false;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(110, 380);
            this.textBox2.Margin = new System.Windows.Forms.Padding(8, 5, 3, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(172, 16);
            this.textBox2.TabIndex = 27;
            this.textBox2.Tag = "";
            this.textBox2.Text = "введитя имя профиля";
            this.toolTip1.SetToolTip(this.textBox2, "Введите имя профиля");
            this.textBox2.WordWrap = false;
            // 
            // textBox_PathSpiffs
            // 
            this.textBox_PathSpiffs.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox_PathSpiffs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_PathSpiffs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_PathSpiffs.Location = new System.Drawing.Point(105, 227);
            this.textBox_PathSpiffs.Name = "textBox_PathSpiffs";
            this.textBox_PathSpiffs.Size = new System.Drawing.Size(100, 19);
            this.textBox_PathSpiffs.TabIndex = 28;
            this.toolTip1.SetToolTip(this.textBox_PathSpiffs, "Например:\r\nsnd/en\r\nsnd\r\n/");
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel1.SetColumnSpan(this.label14, 2);
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(4, 333);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(278, 30);
            this.label14.TabIndex = 29;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 451);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(500, 490);
            this.MinimumSize = new System.Drawing.Size(500, 490);
            this.Name = "MainForm";
            this.Text = "W25Qxxx_Flasher";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton radioButton_Profile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Profile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_FlashSize;
        private System.Windows.Forms.TextBox textBox_StartAddress;
        private System.Windows.Forms.TextBox textBox_PageSize;
        private System.Windows.Forms.TextBox textBox_BlockSize;
        private System.Windows.Forms.TextBox textBox_EraseSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBox_FormatingSpiffs;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_PathFatfs;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_RemoveProfile;
        private System.Windows.Forms.Button button_AddToProfile;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox_PathSpiffs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox ComboBox_ComPorts;
        public System.Windows.Forms.Label connectionLabel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label_profileName;
        private System.Windows.Forms.Button button_StartFlash;
        private System.Windows.Forms.RadioButton radioButton_User;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.Label label_Message;
    }
}

