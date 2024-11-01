using Communicator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_app_Client
{
    // Class GroupCreator: Biểu mẫu để người dùng tạo nhóm chat
    public partial class GroupCreator : Form
    {
        private TcpClient server; // Biến lưu trữ kết nối tới server

        // Constructor của GroupCreator, khởi tạo với một đối tượng TcpClient cho phép giao tiếp với server
        public GroupCreator(TcpClient server)
        {
            this.server = server;
            InitializeComponent(); // Khởi tạo các thành phần giao diện
        }

        // Sự kiện click của nút "Create" để tạo nhóm
        private void btnCreate_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng có để trống trường tên nhóm hoặc danh sách thành viên không
            if (txtGroupName.Text == "" || txtMembers.Text == "")
            {
                MessageBox.Show("Empty Fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng xử lý nếu có trường trống
            }

            // Tạo StreamWriter để gửi dữ liệu đến server thông qua luồng của server
            StreamWriter streamWriter = new StreamWriter(server.GetStream());

            // Tạo một đối tượng Group từ thông tin tên nhóm và danh sách thành viên
            Group group = new Group(txtGroupName.Text, txtMembers.Text);

            // Chuyển đổi đối tượng Group thành chuỗi JSON
            String jsonString = JsonSerializer.Serialize(group);

            // Tạo một đối tượng Json với loại "CREATE_GROUP" và dữ liệu là chuỗi JSON của đối tượng Group
            Json json = new Json("CREATE_GROUP", jsonString);

            // Chuyển đối tượng Json thành mảng byte UTF-8 để truyền qua mạng
            byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(json);
            String S = Encoding.ASCII.GetString(jsonUtf8Bytes, 0, jsonUtf8Bytes.Length);

            // Ghi chuỗi JSON dưới dạng dòng văn bản để gửi tới server
            streamWriter.WriteLine(S);
            streamWriter.Flush(); // Đẩy dữ liệu vào luồng để đảm bảo truyền đi thành công

            // Đóng biểu mẫu tạo nhóm sau khi hoàn thành
            this.Close();
        }

        // Sự kiện khi biểu mẫu GroupCreator được tải lên
        private void GroupCreator_Load(object sender, EventArgs e)
        {
            // (Hiện tại, không có xử lý đặc biệt khi biểu mẫu được tải)
        }
    }
}
