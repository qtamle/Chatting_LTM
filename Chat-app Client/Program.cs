namespace Chat_app_Client
{
    internal static class Program
    {
        /// <summary>
        /// Điểm bắt đầu chính của ứng dụng.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Khởi tạo các cấu hình ứng dụng như thiết lập DPI cao hoặc font mặc định.
            ApplicationConfiguration.Initialize();
            // Chạy form đăng nhập đầu tiên.
            Application.Run(new Login());
        }
    }
}