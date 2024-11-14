using Communicator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;
using Application = System.Windows.Forms.Application;

namespace Chat_app_Client
{
    public partial class ChatBox : Form
    {
        private TcpClient server; // Máy chủ TCP
        private String name;
        private bool threadActive = true; // Trạng thái luồng hoạt động
        private StreamReader streamReader; // Đọc luồng
        private StreamWriter streamWriter; // Ghi luồng


        public ChatBox(TcpClient server, String name)
        {
            this.server = server;
            this.name = name;
            InitializeComponent();
        }

        private void ChatBox_Load(object sender, EventArgs e)
        {
            streamReader = new StreamReader(server.GetStream());
            streamWriter = new StreamWriter(server.GetStream());

            this.Text = "Chat app - " + name;
            lblWelcome.Text = "Nhóm 2, " + name;

            var mainThread = new Thread(() => receiveTheard()); // Khởi tạo luồng nhận tin nhắn
            mainThread.Start(); 
            mainThread.IsBackground = true;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text == "" 
                || txtReceiver.Text == "")
            {
                MessageBox.Show("Empty Fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtReceiver.Text == this.Name)
            {
                MessageBox.Show("Could not send message to yourself", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChatMsg messages;
           messages = new ChatMsg(this.name, txtReceiver.Text, txtMessage.Text, is_group_msg); // Tạo đối tượng tin nhắn
           
            String messageJson = JsonSerializer.Serialize(messages); // Chuyển đổi tin nhắn sang định dạng JSON
            CommandMsg json = new CommandMsg("MESSAGE", messageJson); // Tạo đối tượng JSON cho tin nhắn
            sendJson(json);

            txtMessage.Clear();
        }

        // Hàm sự kiện khi nhấn phím Enter trong ô tin nhắn
        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                btnSend_Click(this.btnSend, e);
            }
        }

        private void getOldMessages(string sender, string receiver)
        {
            var jsonContent = JsonSerializer.Serialize(new OldMsgRequest()
            {
                sender = sender,
                receiver = receiver,
                is_group_msg = is_group_msg
            });

            CommandMsg cmd = new CommandMsg("GET_OLD_MESSAGES", jsonContent);
            sendJson(cmd);
        }

        // Đang chọn nhóm
        bool is_group_msg = false;

        // Hàm sự kiện khi nhấn vào một ô trong bảng nhóm
        private void tblGroup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtReceiver.Text = tblGroup.Rows[e.RowIndex].Cells[0].Value.ToString();
            is_group_msg = true;
            rtbDialog.Clear();
            getOldMessages(this.name, txtReceiver.Text);
        }

        // Hàm sự kiện khi nhấn vào một ô trong bảng người dùng
        private void tblUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtReceiver.Text = tblUser.Rows[e.RowIndex].Cells["Online"].Value.ToString();
            is_group_msg = false;
            rtbDialog.Clear();
            getOldMessages(this.name, txtReceiver.Text);
        }

        // Hàm sự kiện khi bấm nút Tạo nhóm
        private void btnCreateGroup_Click(object sender, EventArgs e)
        {
            new Thread(() => Application.Run(new GroupCreator(server))).Start();
        }

        private void btnPicture_Click(object sender, EventArgs e)
        {
            if (txtReceiver.Text == "")
            {
                MessageBox.Show("Receiver field is empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Thread dialogThread = new Thread(() =>
            {
                try
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| Bitmap file(*.bmp)|*.bmp";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        String fName = ofd.FileName;
                        String path = "";
                        fName = fName.Replace("\\", "/");
                        while (fName.IndexOf("/") > -1)
                        {
                            path += fName.Substring(0, fName.IndexOf("/") + 1);
                            fName = fName.Substring(fName.IndexOf("/") + 1);
                        }

                        FileMsg message = new FileMsg(this.name, txtReceiver.Text, File.ReadAllBytes(path + fName).Length.ToString(), Path.GetExtension(ofd.FileName));

                        CommandMsg json = new CommandMsg("FILE", JsonSerializer.Serialize(message));
                        sendJson(json);

                        server.Client.SendFile(path + fName);

                        AppendRichTextBox(DateTime.Now, this.name, message.receiver, "The file was sent.", "");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
            dialogThread.SetApartmentState(ApartmentState.STA);
            dialogThread.Start();
            dialogThread.IsBackground = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CommandMsg json = new CommandMsg("LOGOUT", this.name);
            sendJson(json);
            new Thread(() => Application.Run(new Login())).Start();
            threadActive = false;
            this.Close();
        }

        private void receiveTheard()
        {
            while(server != null && threadActive)
            {
                try
                {
                    String jsonString = streamReader.ReadLine();
                    CommandMsg? infoJson = JsonSerializer.Deserialize<CommandMsg?>(jsonString);

                    switch (infoJson.type)
                    {
                        case "GET_OLD_MESSAGES_FEEDBACK":

                            if (infoJson.content != null)
                            {
                                List<ChatMsg> chatMsgs = JsonSerializer.Deserialize<List<ChatMsg>>(infoJson.content);
                                
                                if (chatMsgs != null)
                                {
                                    foreach (var message in chatMsgs)
                                    {

                                        if (message.sender != this.name)
                                        {
                                            AppendRichTextBox(message.time, message.sender, message.receiver, message.message, "");
                                        }
                                        else
                                            AppendRichTextBox(message.time, message.sender, message.receiver, message.message, "");
                                    }
                                } 
                            }
                            break;

                        case "STARTUP_FEEDBACK":
                            cleanDataGridView(tblGroup);
                            cleanDataGridView(tblUser);

                            Startup startup = JsonSerializer.Deserialize<Startup>(infoJson.content);

                            List<string> groups = JsonSerializer.Deserialize<List<String>>(startup.group);
                            foreach(String group in groups)
                            {
                                addDataInDataGridView(tblGroup, new string[] { group });
                            }

                            List<string> users = JsonSerializer.Deserialize<List<String>>(startup.onlUser);
                            foreach (String user in users)
                            {
                                addDataInDataGridView(tblUser, new string[] { user });
                            }
                            break;
                        case "MESSAGE":
                            if (infoJson.content != null)
                            {
                                ChatMsg message = JsonSerializer.Deserialize<ChatMsg?>(infoJson.content);
                                if (message != null)
                                {
                                    if (message.sender != this.name)
                                    {
                                        AppendRichTextBox(message.time, message.sender, message.receiver, message.message, "");
                                    }
                                    else AppendRichTextBox(message.time, message.sender, message.receiver, message.message, "");
                                }
                            }
                            break;
                        case "FILE":
                            if (infoJson.content != null)
                            {
                                BufferFile bufferFile = JsonSerializer.Deserialize<BufferFile>(infoJson.content);
                                List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };

                                if (ImageExtensions.Contains(bufferFile.extension.ToUpper()))
                                {
                                    var thread = new Thread(() => Application.Run(new ImageView(bufferFile)));
                                    thread.SetApartmentState(ApartmentState.STA);
                                    thread.Start() ;

                                    AppendRichTextBox(DateTime.Now, bufferFile.sender, bufferFile.receiver, "Shared the " + bufferFile.extension + " file in ", @Environment.CurrentDirectory);
                                }
                                else
                                {
                                    string fileName = @Environment.CurrentDirectory + "/" + String.Format("{0:yyyy-MM-dd HH-mm-ss}__{1}", DateTime.Now, bufferFile.sender) + bufferFile.extension;
                                    FileInfo fi = new FileInfo(fileName);

                                    try
                                    {
                                        // Check if file already exists. If yes, delete it.     
                                        if (fi.Exists)
                                        {
                                            fi.Delete();
                                        }

                                        using (FileStream fStream = File.Create(fileName))
                                        {
                                            fStream.Write(bufferFile.buffer, 0, bufferFile.buffer.Length);
                                            fStream.Flush();
                                            fStream.Close();
                                        }
                                    }
                                    catch (Exception Ex)
                                    {
                                        Console.WriteLine(Ex.ToString());
                                    }

                                    AppendRichTextBox(DateTime.Now, bufferFile.sender, bufferFile.receiver, "Shared the " + bufferFile.extension + " file in ", @Environment.CurrentDirectory);
                                }
                            }
                            break;
                    }

                }
                catch
                {
                    threadActive = false;
                }
            }
        }

        private void AppendRichTextBox(DateTime time, string sender, string receiver, string message, string link = "")
        {
            rtbDialog.BeginInvoke(new MethodInvoker(() =>
            {
                Font currentFont = rtbDialog.SelectionFont;

                // Time
                rtbDialog.SelectionStart = rtbDialog.TextLength;
                rtbDialog.SelectionLength = 0;
                rtbDialog.SelectionColor = Color.AliceBlue;
                rtbDialog.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, FontStyle.Bold);
                rtbDialog.AppendText(time.ToString("dd/MM/yyyy HH:MM:ss") + " ");
                rtbDialog.SelectionColor = rtbDialog.ForeColor;

                //Username
                rtbDialog.SelectionStart = rtbDialog.TextLength;
                rtbDialog.SelectionLength = 0;
                rtbDialog.SelectionColor = Color.Red;
                rtbDialog.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, FontStyle.Bold);
                rtbDialog.AppendText(sender + "<" + receiver + ">");
                rtbDialog.SelectionColor = rtbDialog.ForeColor;

                rtbDialog.AppendText(": ");

                //Message
                rtbDialog.SelectionStart = rtbDialog.TextLength;
                rtbDialog.SelectionLength = 0;
                rtbDialog.SelectionColor = Color.Green;
                rtbDialog.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, FontStyle.Regular);
                rtbDialog.AppendText(message);
                rtbDialog.SelectionColor = rtbDialog.ForeColor;

                rtbDialog.AppendText(" ");

                //link
                rtbDialog.SelectionStart = rtbDialog.TextLength;
                rtbDialog.SelectionLength = 0;
                rtbDialog.SelectionColor = Color.Blue;
                rtbDialog.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, FontStyle.Underline);
                rtbDialog.AppendText(link);
                rtbDialog.SelectionColor = rtbDialog.ForeColor;


                rtbDialog.SelectionStart = rtbDialog.GetFirstCharIndexOfCurrentLine();
                rtbDialog.SelectionLength = 0;

                if (sender == this.name)
                {
                    rtbDialog.SelectionAlignment = HorizontalAlignment.Left;
                }
                else rtbDialog.SelectionAlignment = HorizontalAlignment.Right;

                rtbDialog.AppendText(Environment.NewLine);
            }));
        }

        private void cleanDataGridView(DataGridView dataGridView)
        {
            dataGridView.BeginInvoke(new MethodInvoker(() =>
            {
                dataGridView.Rows.Clear();
            }));
            
        }

        private void addDataInDataGridView(DataGridView dataGridView, String[] array)
        {
            dataGridView.Invoke(new Action(() => { dataGridView.Rows.Add(array); }));
        }

        private void sendJson(CommandMsg json)
        {
            byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(json);
            String S = Encoding.ASCII.GetString(jsonUtf8Bytes, 0, jsonUtf8Bytes.Length);

            streamWriter.WriteLine(S);
            streamWriter.Flush();
        }

        private void btnLike_Click(object sender, EventArgs e)
        {
            
            if (txtReceiver.Text == "")
            {
                MessageBox.Show("Empty Fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtReceiver.Text == this.Name)
            {
                MessageBox.Show("Could not send message to yourself", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChatMsg messages = new ChatMsg(this.name, txtReceiver.Text, "👍", is_group_msg);
            String messageJson = JsonSerializer.Serialize(messages);
            CommandMsg json = new CommandMsg("MESSAGE", messageJson);
            sendJson(json);

            txtMessage.Clear();
        }

        private void btnLove_Click(object sender, EventArgs e)
        {
            if (txtReceiver.Text == "")
            {
                MessageBox.Show("Empty Fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtReceiver.Text == this.Name)
            {
                MessageBox.Show("Could not send message to yourself", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChatMsg messages = new ChatMsg(this.name, txtReceiver.Text, "🥰", is_group_msg);
            String messageJson = JsonSerializer.Serialize(messages);
            CommandMsg json = new CommandMsg("MESSAGE", messageJson);
            sendJson(json);

            txtMessage.Clear();
        }

        private void btnLaugh_Click(object sender, EventArgs e)
        {
            if (txtReceiver.Text == "")
            {
                MessageBox.Show("Empty Fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtReceiver.Text == this.Name)
            {
                MessageBox.Show("Could not send message to yourself", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChatMsg messages = new ChatMsg(this.name, txtReceiver.Text, "🤣", is_group_msg);
            String messageJson = JsonSerializer.Serialize(messages);
            CommandMsg json = new CommandMsg("MESSAGE", messageJson);
            sendJson(json);

            txtMessage.Clear();
        }

        private void btnCry_Click(object sender, EventArgs e)
        {
            if (txtReceiver.Text == "")
            {
                MessageBox.Show("Empty Fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtReceiver.Text == this.Name)
            {
                MessageBox.Show("Could not send message to yourself", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChatMsg messages = new ChatMsg(this.name, txtReceiver.Text, "😭", is_group_msg);
            String messageJson = JsonSerializer.Serialize(messages);
            CommandMsg json = new CommandMsg("MESSAGE", messageJson);
            sendJson(json);

            txtMessage.Clear();
        }

        private void btnDevil_Click(object sender, EventArgs e)
        {
            if (txtReceiver.Text == "")
            {
                MessageBox.Show("Empty Fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtReceiver.Text == this.Name)
            {
                MessageBox.Show("Could not send message to yourself", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChatMsg messages = new ChatMsg(this.name, txtReceiver.Text, "😈", is_group_msg);
            String messageJson = JsonSerializer.Serialize(messages);
            CommandMsg json = new CommandMsg("MESSAGE", messageJson);
            sendJson(json);

            txtMessage.Clear();
        }

        private void ChatBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommandMsg json = new CommandMsg("LOGOUT", this.name);
            sendJson(json);
            threadActive = false;
        }
    }
}
