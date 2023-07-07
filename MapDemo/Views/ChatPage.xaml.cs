using Microsoft.AspNetCore.SignalR.Client;

namespace BeThere.Views;

public partial class ChatPage : ContentPage
{
    private readonly HubConnection _connection;
    public ChatPage()
    {
        InitializeComponent();
        _connection = new HubConnectionBuilder().WithUrl("http://172.25.64.1:5266/chat").Build();

        _connection.On<string>("MessageReceived", (message) =>
        {
            Dispatcher.Dispatch(() => {
                chatMessages.Text += $"{Environment.NewLine}{message}";
            });
            //chatMessages.Text += $"{Environment.NewLine}{message}";
        });
        Task.Run(() =>
        {
            Dispatcher.Dispatch(async () => await _connection.StartAsync());
        });

    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        await _connection.InvokeCoreAsync("SendMessage", args: new[] { myChatMessage.Text });
        myChatMessage.Text = string.Empty;
    }
}