using System.Text.Json.Serialization;

namespace Communicator
{
    public class ChatMsg
    {
        public DateTime time { get; set; }
        public String sender { get; set; }
        public String receiver { get; set; }
        public bool is_group_receive { get; set; }
        public String message { get; set; }
        public ChatMsg(String sender, String recerver, String message, bool is_group)
        {
            time = DateTime.Now;
            this.sender = sender;
            this.receiver = recerver;
            this.message = message;
            this.is_group_receive = is_group;
        }

        [JsonConstructor]
        public ChatMsg() { }
    }
}
