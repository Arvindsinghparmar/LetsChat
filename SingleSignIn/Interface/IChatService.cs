namespace SingleSignIn.Interface
{
    public interface IChatService
    {
        event Action<string, string, string> OnReceiveMessage;
        Task InitializeHubConnection();
        Task SendMessage(string username, string message, string userType);
    }
}
