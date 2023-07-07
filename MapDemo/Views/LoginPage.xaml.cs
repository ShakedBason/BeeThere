using BeThere.ViewModels;
namespace BeThere.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(ConnectToAppViewModle i_LoginLogic)
    {
        InitializeComponent();
        BindingContext = i_LoginLogic;
    }
}