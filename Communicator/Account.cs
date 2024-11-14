namespace Communicator
{
    public class Account
    {
        public string userName { get; set; }
        public string password { get; set; }

        public string status { get; set; } = "Offline";

        public Account()
        {
            
        }

        public Account(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
            this.status = "Offline";
        }
    }
}
