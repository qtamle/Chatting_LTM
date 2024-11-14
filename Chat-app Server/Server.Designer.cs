namespace Chat_app_Server
{
    partial class Server
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            txtIP = new TextBox();
            label3 = new Label();
            txtPort = new TextBox();
            btnStart = new Button();
            rtbDialog = new RichTextBox();
            btnStop = new Button();
            groupBox1 = new GroupBox();
            dgvUsers = new DataGridView();
            groupBox2 = new GroupBox();
            dgvGroups = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGroups).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label1.ForeColor = Color.LightSeaGreen;
            label1.Location = new Point(12, 18);
            label1.Name = "label1";
            label1.Size = new Size(238, 39);
            label1.TabIndex = 0;
            label1.Text = "Server_Nhóm 2";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 9.75F, FontStyle.Italic, GraphicsUnit.Point);
            label2.Location = new Point(604, 66);
            label2.Name = "label2";
            label2.Size = new Size(85, 19);
            label2.TabIndex = 1;
            label2.Text = "IP Address";
            // 
            // txtIP
            // 
            txtIP.Font = new Font("Times New Roman", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtIP.Location = new Point(604, 84);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(225, 26);
            txtIP.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 9.75F, FontStyle.Italic, GraphicsUnit.Point);
            label3.Location = new Point(604, 114);
            label3.Name = "label3";
            label3.Size = new Size(40, 19);
            label3.TabIndex = 1;
            label3.Text = "Port";
            // 
            // txtPort
            // 
            txtPort.Font = new Font("Times New Roman", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtPort.Location = new Point(604, 132);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(225, 26);
            txtPort.TabIndex = 2;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.DarkSlateGray;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnStart.ForeColor = Color.LightSeaGreen;
            btnStart.Location = new Point(604, 171);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(225, 32);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // rtbDialog
            // 
            rtbDialog.Enabled = false;
            rtbDialog.Location = new Point(604, 229);
            rtbDialog.Name = "rtbDialog";
            rtbDialog.Size = new Size(225, 248);
            rtbDialog.TabIndex = 4;
            rtbDialog.Text = "";
            // 
            // btnStop
            // 
            btnStop.BackColor = Color.DarkSlateGray;
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnStop.ForeColor = Color.Aqua;
            btnStop.Location = new Point(604, 489);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(225, 32);
            btnStop.TabIndex = 3;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += btnStop_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvUsers);
            groupBox1.Location = new Point(12, 84);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(560, 214);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Người dùng";
            // 
            // dgvUsers
            // 
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.Location = new Point(3, 23);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.RowTemplate.Height = 29;
            dgvUsers.Size = new Size(554, 188);
            dgvUsers.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvGroups);
            groupBox2.Location = new Point(12, 307);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(560, 214);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Nhóm";
            // 
            // dgvGroups
            // 
            dgvGroups.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGroups.Dock = DockStyle.Fill;
            dgvGroups.Location = new Point(3, 23);
            dgvGroups.Name = "dgvGroups";
            dgvGroups.RowHeadersWidth = 51;
            dgvGroups.RowTemplate.Height = 29;
            dgvGroups.Size = new Size(554, 188);
            dgvGroups.TabIndex = 0;
            // 
            // Server
            // 
            BackColor = SystemColors.Control;
            ClientSize = new Size(878, 533);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(rtbDialog);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(txtPort);
            Controls.Add(label3);
            Controls.Add(txtIP);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Server";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Server";
            Load += Server_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvGroups).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtIP;
        private Label label3;
        private TextBox txtPort;
        private Button btnStart;
        private RichTextBox rtbDialog;
        private Button btnStop;
        private GroupBox groupBox1;
        private DataGridView dgvUsers;
        private GroupBox groupBox2;
        private DataGridView dgvGroups;
    }
}