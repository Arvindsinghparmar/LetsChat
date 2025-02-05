namespace SingleSignIn.Models
{
    public class ChatMessage
    {
        public string Username { get; set; }
        public string Content { get; set; }
        public string UType { get; set; }

        public ChatMessage(string username, string content, string uType)
        {
            Username = username;
            Content = content;
            UType = uType;
        }
    }
}
