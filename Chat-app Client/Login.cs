using System.Net.Sockets;
using System.Net;
using System.Security.Principal;
using System.Text.Json;
using Communicator;
using System.Text;
using System.Windows.Forms;

namespace Chat_app_Client
{
    public partial class Login : Form
    {
        private IPEndPoint ipe;
        private TcpClient server;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        private String name;

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có trường nào còn trống không.
            if (txtLoginPassword.Text == "" || txtLoginIP.Text == "" || txtLoginUsername.Text == "")
            {
                MessageBox.Show("Empty Fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tạo đối tượng IPEndPoint với IP và port của server.
                ipe = new IPEndPoint(IPAddress.Parse(txtLoginIP.Text), 2009);
                server = new TcpClient();

                // Kết nối tới server.
                server.Connect(ipe);

                // Lấy tên người dùng từ textbox.
                name = txtLoginUsername.Text;
                streamReader = new StreamReader(server.GetStream());
                streamWriter = new StreamWriter(server.GetStream());

                // Khởi động luồng để đợi phản hồi đăng nhập từ server.
                var threadLog = new Thread(new ThreadStart(waitForLoginFeedback));
                threadLog.IsBackground = true;
                threadLog.Start();
            }
            catch
            {
                // Hiển thị thông báo lỗi nếu không kết nối được tới server.
                MessageBox.Show("Cannot connect to server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void waitForLoginFeedback()
        {
            Account account = new Account(txtLoginUsername.Text, txtLoginPassword.Text);
            String accountJson = JsonSerializer.Serialize(account);
            Json json = new Json("LOGIN", accountJson);

            // Gửi dữ liệu đăng nhập tới server.
            sendJson(json, server);

            // Nhận phản hồi từ server.
            accountJson = streamReader.ReadLine();
            Json? feedback = JsonSerializer.Deserialize<Json?>(accountJson);

            try
            {
                if (feedback != null)
                {
                    switch (feedback.type)
                    {
                        case "LOGIN_FEEDBACK":
                            if (feedback.content == "TRUE")
                            {
                                // Mở form ChatBox và đóng form Login nếu đăng nhập thành công.
                                new Thread(() => Application.Run(new ChatBox(server, this.name))).Start();
                                this.Invoke((MethodInvoker)delegate
                                {
                                    this.Close();
                                });
                                break;
                            }
                            if (feedback.content == "FALSE")
                            {
                                MessageBox.Show("Login failed!!", "Notification");
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void sendJson(Json json, TcpClient client)
        {
            byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(json);
            String S = Encoding.ASCII.GetString(jsonUtf8Bytes, 0, jsonUtf8Bytes.Length);
            streamWriter.WriteLine(S);
            streamWriter.Flush();
        }

        private void lblSignin_Click(object sender, EventArgs e)
        {
            // Mở form Signin khi người dùng nhấp vào nút "Đăng ký".
            new Thread(() => Application.Run(new Signin())).Start();
            this.Close();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

    }

}