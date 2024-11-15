### Chat-app: Phân tích và Xây dựng Sản phẩm ###
Tổng quan hệ thống
Chat-app Client:

ChatBox.cs: Là giao diện chính để người dùng có thể gửi và nhận tin nhắn trong ứng dụng chat.
GroupCreator.cs: Cho phép người dùng tạo nhóm chat mới và quản lý thành viên trong nhóm.
ImageView.cs: Quản lý và hiển thị các hình ảnh gửi trong chat.
Login.cs và Signin.cs: Các giao diện đăng nhập và đăng ký, hỗ trợ người dùng truy cập vào hệ thống.
Program.cs: Là điểm khởi đầu của ứng dụng client, thiết lập kết nối và khởi chạy các chức năng.
Chat-app Server:

Program.cs: Tệp khởi động của server, thiết lập và bắt đầu vận hành server.
Server.cs: Xử lý kết nối từ client, nhận và gửi tin nhắn, cùng với việc quản lý các nhóm và người dùng.
Communicator:

Account.cs: Quản lý thông tin tài khoản người dùng, hỗ trợ đăng nhập và đăng ký.
BufferFile.cs: Xử lý việc quản lý và truyền tải các tệp tin giữa client và server.
FileMessage.cs: Xử lý các tin nhắn có file đính kèm.
Group.cs: Quản lý thông tin nhóm chat, bao gồm các thành viên và cài đặt nhóm.
Json.cs: Đọc và ghi dữ liệu JSON giữa client và server.
Messages.cs: Quản lý các tin nhắn văn bản trong hệ thống.
Startup.cs: Cấu hình và khởi tạo môi trường cho hệ thống khi khởi chạy.

Hướng dẫn Cài đặt và Sử dụng
Ứng dụng này cho phép người dùng trò chuyện và gửi/nhận tệp tin qua mạng. Các bước cài đặt và sử dụng như sau:

Bước 1: Cài đặt
Clone repository:
git clone <https://github.com/qtamle/Chatting_LTM.git>

Bước 2: Cài đặt và chạy ứng dụng
Chạy Client:

Sử dụng Visual Studio hoặc dòng lệnh để chạy tệp Program.cs.
Đảm bảo đã thiết lập kết nối đến server bằng cách nhập địa chỉ IP và thông tin đăng nhập.
Chạy Server:

Khởi chạy server bằng cách chạy Program.cs trên máy chủ.
Server sẽ chờ kết nối từ client và xử lý các yêu cầu gửi/nhận tin nhắn.
Bước 3: Sử dụng
Đăng ký tài khoản:

Giao diện đăng ký yêu cầu người dùng nhập tên tài khoản và mật khẩu. Sau khi gửi, hệ thống sẽ gửi yêu cầu đăng ký đến server.
Đăng nhập:

Người dùng nhập thông tin đăng nhập (IP, tên tài khoản và mật khẩu) để kết nối với server.
Tạo nhóm chat:

Người dùng có thể tạo nhóm mới và mời bạn bè tham gia để trò chuyện.
Gửi và nhận tin nhắn:

Người dùng có thể gửi tin nhắn văn bản, hình ảnh, và tệp tin tới các thành viên trong nhóm hoặc chat cá nhân.
Đăng xuất:

Khi người dùng đăng xuất, ứng dụng sẽ ngắt kết nối và đưa họ quay lại màn hình đăng nhập.
Giao diện Sản phẩm
Giao diện Đăng ký
Tên chức năng: Đăng ký tài khoản
Mô tả chức năng: Cho phép người dùng nhập tên tài khoản và mật khẩu để tạo tài khoản mới trên hệ thống.
Chi tiết giao diện:
Trường "Server IP": Nhập địa chỉ IP của server.
Trường "Username": Nhập tên tài khoản.
Trường "Password": Nhập mật khẩu.
Nút "Đăng ký": Gửi yêu cầu đăng ký tới server và chuyển hướng đến màn hình chính nếu thành công.
Giao diện Đăng nhập
Tên chức năng: Đăng nhập vào hệ thống
Mô tả chức năng: Cho phép người dùng đăng nhập vào hệ thống thông qua tài khoản đã tạo trước đó.
Quy trình chức năng:
Kiểm tra đầu vào: Kiểm tra xem các trường có được điền đầy đủ không.
Kết nối tới server: Client kết nối đến server qua IP và cổng đã nhập.
Gửi thông tin đăng nhập và xử lý phản hồi: Server phản hồi với kết quả đăng nhập.

