﻿namespace Chat_app_Client
{
    partial class ChatBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatBox));
            tblUser = new DataGridView();
            Online = new DataGridViewButtonColumn();
            tblGroup = new DataGridView();
            Group = new DataGridViewButtonColumn();
            rtbDialog = new RichTextBox();
            btnPicture = new PictureBox();
            txtMessage = new TextBox();
            btnSend = new PictureBox();
            lblWelcome = new Label();
            txtReceiver = new TextBox();
            btnCreateGroup = new Button();
            button1 = new Button();
            btnLike = new PictureBox();
            btnLove = new PictureBox();
            btnLaugh = new PictureBox();
            btnCry = new PictureBox();
            btnDevil = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)tblUser).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tblGroup).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnSend).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnLike).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnLove).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnLaugh).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnCry).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnDevil).BeginInit();
            SuspendLayout();
            // 
            // tblUser
            // 
            tblUser.AllowUserToAddRows = false;
            tblUser.AllowUserToDeleteRows = false;
            tblUser.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tblUser.Columns.AddRange(new DataGridViewColumn[] { Online });
            tblUser.Location = new Point(14, 57);
            tblUser.Margin = new Padding(3, 4, 3, 4);
            tblUser.Name = "tblUser";
            tblUser.ReadOnly = true;
            tblUser.RowHeadersWidth = 51;
            tblUser.RowTemplate.Height = 25;
            tblUser.Size = new Size(174, 268);
            tblUser.TabIndex = 4;
            tblUser.CellContentClick += tblUser_CellContentClick;
            // 
            // Online
            // 
            Online.HeaderText = "Online";
            Online.MinimumWidth = 6;
            Online.Name = "Online";
            Online.ReadOnly = true;
            Online.Resizable = DataGridViewTriState.False;
            Online.SortMode = DataGridViewColumnSortMode.Automatic;
            Online.Width = 120;
            // 
            // tblGroup
            // 
            tblGroup.AllowUserToAddRows = false;
            tblGroup.AllowUserToDeleteRows = false;
            tblGroup.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tblGroup.Columns.AddRange(new DataGridViewColumn[] { Group });
            tblGroup.Location = new Point(14, 333);
            tblGroup.Margin = new Padding(3, 4, 3, 4);
            tblGroup.Name = "tblGroup";
            tblGroup.ReadOnly = true;
            tblGroup.RowHeadersWidth = 51;
            tblGroup.RowTemplate.Height = 25;
            tblGroup.Size = new Size(174, 268);
            tblGroup.TabIndex = 3;
            tblGroup.CellContentClick += tblGroup_CellContentClick;
            // 
            // Group
            // 
            Group.HeaderText = "Group";
            Group.MinimumWidth = 6;
            Group.Name = "Group";
            Group.ReadOnly = true;
            Group.Resizable = DataGridViewTriState.False;
            Group.SortMode = DataGridViewColumnSortMode.Automatic;
            Group.Width = 120;
            // 
            // rtbDialog
            // 
            rtbDialog.BackColor = Color.LightGray;
            rtbDialog.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            rtbDialog.Location = new Point(205, 57);
            rtbDialog.Margin = new Padding(3, 4, 3, 4);
            rtbDialog.Name = "rtbDialog";
            rtbDialog.Size = new Size(695, 495);
            rtbDialog.TabIndex = 2;
            rtbDialog.Text = "";
            // 
            // btnPicture
            // 
            btnPicture.Cursor = Cursors.Hand;
            btnPicture.Image = Properties.Resources.file;
            btnPicture.Location = new Point(205, 609);
            btnPicture.Margin = new Padding(3, 4, 3, 4);
            btnPicture.Name = "btnPicture";
            btnPicture.Size = new Size(34, 40);
            btnPicture.SizeMode = PictureBoxSizeMode.Zoom;
            btnPicture.TabIndex = 2;
            btnPicture.TabStop = false;
            btnPicture.Click += btnPicture_Click;
            // 
            // txtMessage
            // 
            txtMessage.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtMessage.Location = new Point(246, 611);
            txtMessage.Margin = new Padding(3, 4, 3, 4);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(614, 35);
            txtMessage.TabIndex = 1;
            txtMessage.KeyPress += txtMessage_KeyPress;
            // 
            // btnSend
            // 
            btnSend.Cursor = Cursors.Hand;
            btnSend.Image = Properties.Resources.send;
            btnSend.Location = new Point(867, 609);
            btnSend.Margin = new Padding(3, 4, 3, 4);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(34, 40);
            btnSend.SizeMode = PictureBoxSizeMode.Zoom;
            btnSend.TabIndex = 2;
            btnSend.TabStop = false;
            btnSend.Click += btnSend_Click;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            lblWelcome.ForeColor = Color.DarkSlateGray;
            lblWelcome.Location = new Point(14, 12);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(133, 28);
            lblWelcome.TabIndex = 4;
            lblWelcome.Text = "Welcome, ...";
            // 
            // txtReceiver
            // 
            txtReceiver.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtReceiver.Location = new Point(205, 9);
            txtReceiver.Margin = new Padding(3, 4, 3, 4);
            txtReceiver.Name = "txtReceiver";
            txtReceiver.Size = new Size(126, 35);
            txtReceiver.TabIndex = 0;
            // 
            // btnCreateGroup
            // 
            btnCreateGroup.BackColor = Color.DarkSlateGray;
            btnCreateGroup.FlatStyle = FlatStyle.Flat;
            btnCreateGroup.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnCreateGroup.ForeColor = Color.LightCyan;
            btnCreateGroup.Location = new Point(14, 611);
            btnCreateGroup.Margin = new Padding(3, 4, 3, 4);
            btnCreateGroup.Name = "btnCreateGroup";
            btnCreateGroup.Size = new Size(174, 39);
            btnCreateGroup.TabIndex = 5;
            btnCreateGroup.Text = "Create Group";
            btnCreateGroup.UseVisualStyleBackColor = false;
            btnCreateGroup.Click += btnCreateGroup_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.DarkSlateGray;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = Color.LightSeaGreen;
            button1.Location = new Point(801, 8);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(101, 39);
            button1.TabIndex = 29;
            button1.Text = "Logout";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // btnLike
            // 
            btnLike.Cursor = Cursors.Hand;
            btnLike.Image = (Image)resources.GetObject("btnLike.Image");
            btnLike.Location = new Point(205, 561);
            btnLike.Margin = new Padding(3, 4, 3, 4);
            btnLike.Name = "btnLike";
            btnLike.Size = new Size(34, 40);
            btnLike.SizeMode = PictureBoxSizeMode.Zoom;
            btnLike.TabIndex = 2;
            btnLike.TabStop = false;
            btnLike.Click += btnLike_Click;
            // 
            // btnLove
            // 
            btnLove.Cursor = Cursors.Hand;
            btnLove.Image = (Image)resources.GetObject("btnLove.Image");
            btnLove.Location = new Point(262, 561);
            btnLove.Margin = new Padding(3, 4, 3, 4);
            btnLove.Name = "btnLove";
            btnLove.Size = new Size(34, 40);
            btnLove.SizeMode = PictureBoxSizeMode.Zoom;
            btnLove.TabIndex = 2;
            btnLove.TabStop = false;
            btnLove.Click += btnLove_Click;
            // 
            // btnLaugh
            // 
            btnLaugh.Cursor = Cursors.Hand;
            btnLaugh.Image = (Image)resources.GetObject("btnLaugh.Image");
            btnLaugh.Location = new Point(319, 561);
            btnLaugh.Margin = new Padding(3, 4, 3, 4);
            btnLaugh.Name = "btnLaugh";
            btnLaugh.Size = new Size(34, 40);
            btnLaugh.SizeMode = PictureBoxSizeMode.Zoom;
            btnLaugh.TabIndex = 2;
            btnLaugh.TabStop = false;
            btnLaugh.Click += btnLaugh_Click;
            // 
            // btnCry
            // 
            btnCry.Cursor = Cursors.Hand;
            btnCry.Image = (Image)resources.GetObject("btnCry.Image");
            btnCry.Location = new Point(376, 561);
            btnCry.Margin = new Padding(3, 4, 3, 4);
            btnCry.Name = "btnCry";
            btnCry.Size = new Size(34, 40);
            btnCry.SizeMode = PictureBoxSizeMode.Zoom;
            btnCry.TabIndex = 2;
            btnCry.TabStop = false;
            btnCry.Click += btnCry_Click;
            // 
            // btnDevil
            // 
            btnDevil.Cursor = Cursors.Hand;
            btnDevil.Image = (Image)resources.GetObject("btnDevil.Image");
            btnDevil.Location = new Point(433, 561);
            btnDevil.Margin = new Padding(3, 4, 3, 4);
            btnDevil.Name = "btnDevil";
            btnDevil.Size = new Size(34, 40);
            btnDevil.SizeMode = PictureBoxSizeMode.Zoom;
            btnDevil.TabIndex = 2;
            btnDevil.TabStop = false;
            btnDevil.Click += btnDevil_Click;
            // 
            // ChatBox
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 663);
            Controls.Add(button1);
            Controls.Add(btnCreateGroup);
            Controls.Add(lblWelcome);
            Controls.Add(txtReceiver);
            Controls.Add(txtMessage);
            Controls.Add(btnSend);
            Controls.Add(btnDevil);
            Controls.Add(btnCry);
            Controls.Add(btnLaugh);
            Controls.Add(btnLove);
            Controls.Add(btnLike);
            Controls.Add(btnPicture);
            Controls.Add(rtbDialog);
            Controls.Add(tblGroup);
            Controls.Add(tblUser);
            Margin = new Padding(3, 4, 3, 4);
            Name = "ChatBox";
            Text = "ChatBox";
            FormClosing += ChatBox_FormClosing;
            Load += ChatBox_Load;
            ((System.ComponentModel.ISupportInitialize)tblUser).EndInit();
            ((System.ComponentModel.ISupportInitialize)tblGroup).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnSend).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnLike).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnLove).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnLaugh).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnCry).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnDevil).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView tblUser;
        private DataGridView tblGroup;
        private RichTextBox rtbDialog;
        private PictureBox btnPicture;
        private TextBox txtMessage;
        private PictureBox btnSend;
        private Label lblWelcome;
        private TextBox txtReceiver;
        private Button btnCreateGroup;
        private Button button1;
        private DataGridViewButtonColumn Online;
        private DataGridViewButtonColumn Group;
        private PictureBox btnLike;
        private PictureBox btnLove;
        private PictureBox btnLaugh;
        private PictureBox btnCry;
        private PictureBox btnDevil;
    }
}