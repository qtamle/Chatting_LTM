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
            label2.Location = new Point(12, 66);
            label2.Name = "label2";
            label2.Size = new Size(84, 19);
            label2.TabIndex = 1;
            label2.Text = "IP Address";
            // 
            // txtIP
            // 
            txtIP.Font = new Font("Times New Roman", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtIP.Location = new Point(12, 84);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(225, 26);
            txtIP.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 9.75F, FontStyle.Italic, GraphicsUnit.Point);
            label3.Location = new Point(12, 114);
            label3.Name = "label3";
            label3.Size = new Size(40, 19);
            label3.TabIndex = 1;
            label3.Text = "Port";
            // 
            // txtPort
            // 
            txtPort.Font = new Font("Times New Roman", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtPort.Location = new Point(12, 132);
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
            btnStart.Location = new Point(12, 171);
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
            rtbDialog.Location = new Point(12, 229);
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
            btnStop.Location = new Point(12, 489);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(225, 32);
            btnStop.TabIndex = 3;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += btnStop_Click;
            // 
            // Server
            // 
            BackColor = SystemColors.Control;
            ClientSize = new Size(249, 533);
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
    }
}