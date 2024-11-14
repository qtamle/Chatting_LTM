namespace Communicator
{
    public class CommandMsg
    {
        public String type { get; set; }
        public String content { get; set; }
        public CommandMsg(String type, String content)
        {
            this.type = type;
            this.content = content;
        }
    }
}