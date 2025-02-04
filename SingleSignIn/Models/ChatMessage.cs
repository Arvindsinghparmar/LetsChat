namespace SingleSignIn.Models
{
    public class ChatMessage
    {
        public string Username { get; set; }
        public string Content { get; set; }

        public ChatMessage(string username, string content)
        {
            Username = username;
            Content = content;
        }
    }
}
