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
