namespace SingleSignIn.Interface
{
    public interface IChatService
    {
        event Action<string, string> OnReceiveMessage;
        Task InitializeHubConnection();
        Task SendMessage(string username, string message);
        Task SendTypingNotification(string username);
        event Action<string> OnUserTyping;
    }
}
