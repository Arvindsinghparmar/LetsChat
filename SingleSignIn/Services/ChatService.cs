using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SingleSignIn.Interface;
using System;
using System.Threading.Tasks;

namespace SingleSignIn.Services
{
    public class ChatService : IChatService, IDisposable
    {
        public event Action<string, string>? OnReceiveMessage;
        private readonly HubConnection _hubConnection;
        private readonly NavigationManager _navigationManager;
        public event Action<string> OnUserTyping;
        public ChatService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _hubConnection=new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/chathub"))
                .WithAutomaticReconnect()
                .Build();
            _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                OnReceiveMessage?.Invoke(user, message);
                OnUserTyping?.Invoke("");
            });
            _hubConnection.On<string>("ReceiveTypingNotification", (username) =>
            {
                OnUserTyping?.Invoke(username);
            });
        }

        public async Task InitializeHubConnection()
        {
            try
            {
                if (_hubConnection.State == HubConnectionState.Disconnected)
                {
                    await _hubConnection.StartAsync();
                }
            }
            catch (Exception ex)
            {
                // Handle connection initialization error
                Console.WriteLine($"Error initializing hub connection: {ex.Message}");
            }
        }

        public async Task SendTypingNotification(string user)
        {
            if(_hubConnection.State==HubConnectionState.Connected)
            {
                await _hubConnection.SendAsync("UserTyping", user);
            }
        }

        public async Task SendMessage(string username, string message)
        {
            
            try
            {
                if (_hubConnection.State == HubConnectionState.Connected)
                {
                    await _hubConnection.SendAsync("SendMessage", username, message);
                }
                else
                {
                    // Handle message sending when not connected
                    Console.WriteLine("Cannot send message. Hub connection is not established.");
                }
            }
            catch (Exception ex)
            {
                // Handle message sending error
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _hubConnection.DisposeAsync().GetAwaiter().GetResult();
        }
    }
}