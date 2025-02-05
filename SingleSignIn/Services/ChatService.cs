using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SingleSignIn.Interface;
using System;
using System.Threading.Tasks;

namespace SingleSignIn.Services
{
    public class ChatService : IChatService, IDisposable
    {
        public event Action<string, string, string>? OnReceiveMessage;
        private readonly HubConnection _hubConnection;
        private readonly NavigationManager _navigationManager;

        public ChatService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _hubConnection=new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/chathub"))
                .WithAutomaticReconnect()
                .Build();
            _hubConnection.On<string, string, string>("ReceiveMessage", (user, message, utype) =>
            {
                OnReceiveMessage?.Invoke(user, message, utype);
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

        public async Task SendMessage(string username, string message, string userType)
        {
            
            try
            {
                if (_hubConnection.State == HubConnectionState.Connected)
                {
                    await _hubConnection.SendAsync("SendMessage", username, message, userType);
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