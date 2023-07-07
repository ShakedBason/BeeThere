using BeThere.ViewModels;

namespace BeThere.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel i_RegisterLogic)
    {
        InitializeComponent();
        BindingContext = i_RegisterLogic;
    }
}