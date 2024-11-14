using Chat_app_Server.DATA;
using Communicator;
using LiteDB;
using Microsoft.VisualBasic.ApplicationServices;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Chat_app_Server
{
    public partial class Server : Form
    {

        private bool active = true;
        private IPEndPoint iep;
        private TcpListener server;
        //private Dictionary<String, String> USER;
        //private Dictionary<String, List<String>> GROUP;
        private Dictionary<String, TcpClient> TCP_CLIENTS;

        // Danh sách người dùng
        private ILiteCollection<Account> _accounts;
        // Danh sách nhóm
        private ILiteCollection<Group> _groups;
        private ILiteCollection<ChatMsg> _chatMessages;

        public Server()
        {
            InitializeComponent();
        }

        BindingSource _srcUsers = new BindingSource();
        BindingSource _srcGroups = new BindingSource();

        private void Server_Load(object sender, EventArgs e)
        {
            _accounts = LiteDbContext.Db.GetCollection<Account>();
            _groups = LiteDbContext.Db.GetCollection<Group>();
            _chatMessages = LiteDbContext.Db.GetCollection<ChatMsg>();

            String IP = null;
            // Lấy thông số IP hiện tại của máy
            var host = Dns.GetHostByName(Dns.GetHostName());
            // Duyệt qua danh sách IP
            foreach (var ip in host.AddressList)
            {
                // Chọn ra IP dạng IPv4
                if (ip.ToString().Contains('.'))
                {
                    IP = ip.ToString();
                }
            }
            if (IP == null)
            {
                MessageBox.Show("No network adapters with an IPv4 address in the system!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtIP.Text = IP;
            txtPort.Text = "2009";

            userInitialize();

            // Load danh sách người dùng
            dgvUsers.DataSource = _srcUsers;
            dgvUsers.ReadOnly = true;
            dgvUsers.AllowUserToAddRows = false;
            LoadGridUsers();

            // Load danh sách nhóm
            dgvGroups.DataSource = _srcGroups;
            dgvGroups.ReadOnly = true;
            dgvGroups.AllowUserToAddRows = false;
            LoadGridGroups();

            // 
            btnStart_Click(sender, e);
        }

        private void LoadGridGroups()
        {
            var groups = _groups
                .Query().Select(x => new Group()
                {
                    name = x.name,
                    members = x.members
                }).ToList();

            _srcGroups.DataSource = groups;
            _srcGroups.ResetBindings(true);
        }

        private void LoadGridUsers()
        {
            var accounts = _accounts
               .Query().Select(x => new Account
               {
                   userName = x.userName,
                   password = x.password,
                   status = "Offline"
               }).ToList();

            _srcUsers.DataSource = accounts;
            _srcUsers.ResetBindings(true);
        }

        private void updateLogInStatus(string userName)
        {
            var accounts = _srcUsers.DataSource as List<Account>;
            
            if (accounts == null)
                return;
            
            foreach (var item in accounts)
            {
                if (item.userName == userName)
                {
                    item.status = "Online";
                    break;
                }
            }

            _srcUsers.DataSource = accounts;
            _srcUsers.ResetBindings(true);
        }

        private void updateLogoutStatus(string userName)
        {
            var accounts = _srcUsers.DataSource as List<Account>;

            if (accounts == null)
                return;

            foreach (var item in accounts)
            {
                if (item.userName == userName)
                {
                    item.status = "Offline";
                    break;
                }
            }

            _srcUsers.DataSource = accounts;
            _srcUsers.ResetBindings(true);
        }

        private void userInitialize()
        {
            TCP_CLIENTS = new Dictionary<String, TcpClient>();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            iep = new IPEndPoint(IPAddress.Parse(txtIP.Text), int.Parse(txtPort.Text));
            server = new TcpListener(iep);

            try
            {
                server.Start();

                Thread ServerThread = new Thread(new ThreadStart(ServerStart));
                ServerThread.IsBackground = true;
                ServerThread.Start();
            } catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra ! Không thể start máy chủ !", "Thông báo"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ServerStart()
        {
            try
            {
                AppendRichTextBox("Start accept connect from client!");
                changeButtonEnable(btnStart, false);
                changeButtonEnable(btnStop, true);
                //Clipboard.SetText(txtIP.Text);
                while (active)
                {
                    TcpClient client = server.AcceptTcpClient();
                    var clientThread = new Thread(() => clientService(client));
                    clientThread.Start();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clientService(TcpClient client)
        {
            StreamReader streamReader = new StreamReader(client.GetStream());
            String msg = streamReader.ReadLine();
            CommandMsg infoJson = JsonSerializer.Deserialize<CommandMsg>(msg);

            if (infoJson != null)
            {
                switch (infoJson.type)
                {
                    case "SIGNIN":
                        reponseSignin(infoJson, client);
                        break;
                    case "LOGIN":
                        reponseLogin(infoJson, client);
                        break;
                }
            }

            try
            {
                bool threadActive = true;
                while (threadActive && client != null)
                {
                    msg = streamReader.ReadLine();
                    infoJson = JsonSerializer.Deserialize<CommandMsg>(msg);
                    
                    if (infoJson != null && infoJson.content != null)
                    {
                        switch (infoJson.type)
                        {
                            case "MESSAGE":
                                if (infoJson.content != null)
                                {
                                    reponseMessage(infoJson);
                                }
                                break;
                            case "CREATE_GROUP":
                                if (infoJson.content != null)
                                {
                                    createGroup(infoJson);
                                }
                                break;
                            case "FILE":
                                if (infoJson.content != null)
                                {
                                    reponseFile(infoJson, client);
                                }
                                break;
                            case "GET_OLD_MESSAGES":
                                if (infoJson.content != null)
                                {
                                    responseOldMessage(infoJson, client);
                                }
                                break;
                            case "LOGOUT":
                                if (infoJson.content != null)
                                {
                                    TCP_CLIENTS[infoJson.content].Close();
                                    TCP_CLIENTS.Remove(infoJson.content);
                                    AppendRichTextBox(infoJson.content + " logged out.");
                                    
                                    // Cập nhật trạng thái logout
                                    if (InvokeRequired)
                                    {
                                        this.Invoke(new MethodInvoker(delegate () {
                                            updateLogoutStatus(infoJson.content);
                                        }));
                                    }
                                    
                                    threadActive = false;

                                    foreach (String key in TCP_CLIENTS.Keys)
                                    {
                                        startupClient(TCP_CLIENTS[key], key);
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            catch
            {
                //client.Close();
            }
        }

        private void responseOldMessage(CommandMsg infoJson, TcpClient client)
        {
            OldMsgRequest oldMsgRequest = JsonSerializer.Deserialize<OldMsgRequest>(infoJson.content);
            
            if (oldMsgRequest == null)
                return;

            List<ChatMsg> messages = new List<ChatMsg>();

            // Chỉ lấy tin nhắn nhóm
            if (oldMsgRequest.is_group_msg)
            {
                // Lấy tất cả tin người nhận là nhóm
                var groupMessages = _chatMessages
                   .Query()
                   .Where(x => x.is_group_receive == true
                       && (x.receiver == oldMsgRequest.receiver ))
                   .ToList();
                messages.AddRange(groupMessages);
            }
            else
            {
                // Lấy tất cả message thông thường được gửi tới người gửi
                var msgs = _chatMessages
                    .Query()
                    .Where(x => x.is_group_receive != true
                        && ((x.receiver == oldMsgRequest.sender && x.sender == oldMsgRequest.receiver) || (x.sender == oldMsgRequest.sender && x.receiver == oldMsgRequest.receiver)))
                .ToList();
                messages.AddRange(msgs);
            }

            messages = messages
                .OrderBy(x => x.time)
                .ToList();

            String json = JsonSerializer.Serialize(messages);
            CommandMsg notification = new CommandMsg("GET_OLD_MESSAGES_FEEDBACK", json);
            // gởi thông điệp cho client
            if (TCP_CLIENTS.ContainsKey(oldMsgRequest.sender))
                sendJson(notification, client);
        }

        private void reponseSignin(CommandMsg infoJson, TcpClient client)
        {
            // Convert data nhận được thành đối tượng Account
            Account account = JsonSerializer.Deserialize<Account>(infoJson.content);

            if (account == null || account.userName == null)
                return;

            // Kiểm tra nếu không tồn tại tài khoản này trên hệ thống
            // Lấy tài khoản có trong CSDL
            Account dbAccount = _accounts.FindOne(x => x.userName == account.userName);

            if (dbAccount == null)
            {
                // Thêm tài khoản mới đăng ký vào CSDL

                _accounts.Insert(account);
                _accounts.EnsureIndex(x => x.userName);

                // Hiển thị danh sách user mới lên grid
                // Do hàm chạy trên thread nên cần invoke để thay
                // đổi giá trị ở form chính
                if (InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate () {
                        LoadGridUsers();
                    }));
                }
            } 
            
            // Nếu chưa tồn tại client

            if (!TCP_CLIENTS.ContainsKey(account.userName))
            {

                TCP_CLIENTS.Add(account.userName, client);

                // Tạo mới đối tượng Json để phản hồi cho Client
                CommandMsg notification = new CommandMsg("SIGNIN_FEEDBACK", "TRUE");
                // gởi thông điệp cho client
                sendJson(notification, client);
                // thêm vào textbox
                AppendRichTextBox(account.userName + " signed in!");

                // Cập nhật trạng thái Login cho user

                if (InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate () {
                        updateLogInStatus(account.userName);
                    }));
                }

                foreach (String key in TCP_CLIENTS.Keys)
                {
                    startupClient(TCP_CLIENTS[key], key);
                }
            }
        }

        private void reponseLogin(CommandMsg infoJson, TcpClient client)
        {
            Account account = JsonSerializer.Deserialize<Account>(infoJson.content);

            if (account == null || account.userName == null)
                return;

            // Kiểm tra nếu không tồn tại tài khoản này trên hệ thống
            // Lấy tài khoản có trong CSDL
            Account dbAccount = _accounts.FindOne(x => x.userName == account.userName);

            if (dbAccount == null || dbAccount.password != account.password)
            {
                CommandMsg notification = new CommandMsg("LOGIN_FEEDBACK", "FALSE");
                sendJson(notification, client);
                AppendRichTextBox(account.userName + " can not login!");
            } 

            // Nếu client này chưa kết nối
            if (!TCP_CLIENTS.ContainsKey(account.userName))
            {
                CommandMsg notification = new CommandMsg("LOGIN_FEEDBACK", "TRUE");
                sendJson(notification, client);
                AppendRichTextBox(account.userName + " logged in!");

                //TCP_CLIENTS.Remove(account.userName);

                TCP_CLIENTS.Add(account.userName, client);

                // Cập nhật trạng thái Login cho user

                if (InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate () {
                        updateLogInStatus(account.userName);
                    }));
                }

                foreach (String key in TCP_CLIENTS.Keys)
                {
                    startupClient(TCP_CLIENTS[key], key);
                }
            }
            else // Ngược lại thì client này đã kết nối
            {
                CommandMsg notification = new CommandMsg("LOGIN_FEEDBACK", "FALSE");
                sendJson(notification, client);
                AppendRichTextBox(account.userName + " can not login!");
            }
        }

        private void startupClient(TcpClient client, String clientName)
        {
            // Lấy danh sách user online
            List<String> onlUser = new List<string>(TCP_CLIENTS.Keys);

            onlUser.Remove(clientName);

            // Lấy danh sách group
            List<String> group = new List<string>();

            var localGroups = _groups
                .Query()
                .ToList();

            foreach (Group item in localGroups)
            {
                var groupMembers = item.members
                       .Split(",", StringSplitOptions.RemoveEmptyEntries);

                if (groupMembers.Contains(clientName))
                {
                    group.Add(item.name);
                }
            }

            string jsonUser = JsonSerializer.Serialize<List<String>>(onlUser);
            string jsonGroup = JsonSerializer.Serialize<List<String>>(group);

            Startup startup = new Startup(jsonUser, jsonGroup);
            String startupJson = JsonSerializer.Serialize(startup);
            CommandMsg json = new CommandMsg("STARTUP_FEEDBACK", startupJson);
            sendJson(json, client);
        }

        private void reponseMessage(CommandMsg infoJson)
        {
            ChatMsg msg = JsonSerializer.Deserialize<ChatMsg>(infoJson.content);

            if (msg == null) return;

            if (!msg.is_group_receive 
                && TCP_CLIENTS.ContainsKey(msg.receiver))
            {
                AppendRichTextBox(msg.sender + " to " + msg.receiver + ": " + msg.message);

                // Gửi thông điệp cho receiver
                TcpClient receiver = TCP_CLIENTS[msg.receiver];
                sendJson(infoJson, receiver);

                // Gửi thông điệp cho sender
                receiver = TCP_CLIENTS[msg.sender];
                sendJson(infoJson, receiver);

                // Lưu chat message vào Db
                _chatMessages.Insert(msg);


            }
            
            if (msg.is_group_receive)
            {
                var localGroup = _groups.FindOne(x => x.name == msg.receiver);

                if (localGroup != null)
                {
                    AppendRichTextBox(msg.sender + " to " + msg.receiver + ": " + msg.message);
                    
                    var groupMembers = localGroup.members
                        .Split(",", StringSplitOptions.RemoveEmptyEntries);

                    // Duyệt qua tất cả thành viên trong nhóm

                    foreach (String user in groupMembers)
                    {
                        if (TCP_CLIENTS.ContainsKey(user))
                        {
                            TcpClient receiver = TCP_CLIENTS[user];
                            sendJson(infoJson, receiver);
                            _chatMessages.Insert(msg);
                        }
                    }
                }
            }
        }

        private void createGroup(CommandMsg infoJson)
        {

            Group group = JsonSerializer.Deserialize<Group>(infoJson.content);

            string[] groupMembers = group
                .members
                .Split(',', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < groupMembers.Length; i++)
            {
                groupMembers[i] = groupMembers[i].Trim();
            }

            Group localGroup = _groups.FindOne(x => x.name == group.name);
            
            if (localGroup == null)
            {
                _groups.Insert(group);
                _groups.EnsureIndex(x => x.name);
            }

            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate () {
                    LoadGridGroups();
                }));
            }

            //GROUP.Add(group.name, groupMembers.ToList());

            foreach (String key in TCP_CLIENTS.Keys)
            {
                startupClient(TCP_CLIENTS[key], key);
            }
        }

        private void reponseFile(CommandMsg infoJson, TcpClient client)
        {
            FileMsg fileMessage = JsonSerializer.Deserialize<FileMsg>(infoJson.content);

            try
            {

                int length = Convert.ToInt32(fileMessage.lenght);
                byte[] buffer = new byte[length];
                int received = 0;
                int read = 0;
                int size = 1024;
                int remaining = 0;

                // Read bytes from the client using the length sent from the client    
                while (received < length)
                {
                    remaining = length - received;
                    if (remaining < size)
                    {
                        size = remaining;
                    }

                    read = client.GetStream().Read(buffer, received, size);
                    received += read;
                }

                BufferFile bufferFile = new BufferFile(fileMessage.sender, fileMessage.receiver, buffer, fileMessage.extension);

                String jsonString = JsonSerializer.Serialize(bufferFile);
                CommandMsg json = new CommandMsg("FILE", jsonString);

                if (TCP_CLIENTS.ContainsKey(fileMessage.receiver))
                {
                    TcpClient receiver = TCP_CLIENTS[fileMessage.receiver];
                    sendJson(json, receiver);
                }

                else
                {
                    var localGroup = _groups.FindOne(x => x.name == fileMessage.receiver);

                    if (localGroup != null)
                    {
                        var groupMembers = localGroup.members
                            .Split(",", StringSplitOptions.RemoveEmptyEntries);

                        foreach (String user in groupMembers)
                        {
                            if (TCP_CLIENTS.ContainsKey(user))
                            {
                                TcpClient receiver = TCP_CLIENTS[user];
                                sendJson(json, receiver);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }

        private void sendJson(CommandMsg json, TcpClient client)
        {
            byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(json);
            StreamWriter streamWriter = new StreamWriter(client.GetStream());

            String S = Encoding.ASCII.GetString(jsonUtf8Bytes, 0, jsonUtf8Bytes.Length);

            streamWriter.WriteLine(S);
            streamWriter.Flush();
        }

        private void AppendRichTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendRichTextBox), new object[] { value });
                return;
            }
            rtbDialog.AppendText(value);
            rtbDialog.AppendText(Environment.NewLine);
        }

        private void changeButtonEnable(Button btn, bool enable)
        {
            btn.BeginInvoke(new MethodInvoker(() =>
            {
                btn.Enabled = enable;
            }));
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (TCP_CLIENTS.Count() > 0)
            {
                MessageBox.Show("The server has " + TCP_CLIENTS.Count + " user(s) logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            active = false;
            Environment.Exit(0);
        }
    }
}