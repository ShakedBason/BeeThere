using System;
using BeThere.Views;

namespace BeThere;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(ChatPage), typeof(ChatPage)); //gives a string of chatpage to navigate.
        Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
    }
}
