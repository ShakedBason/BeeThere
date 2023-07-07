using CommunityToolkit.Mvvm.ComponentModel;
using System;
using BeThere.Views;
using BeThere.Services;

namespace BeThere.ViewModels
{
    public partial class ConnectToAppViewModle : BaseViewModels
    {
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }


        private ConnectToAppService m_LoginService;

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string loginFailureMessage;

        public ConnectToAppViewModle(ConnectToAppService i_LoginService)
        {
            Title = "Login!!";
            m_LoginService = i_LoginService;
            LoginCommand = new Command(async () => await LoginAsync());
            RegisterCommand = new Command(async () => await registerClicked());
        }


        private async Task LoginAsync()
        {
            if (IsBusy == true)
            {
                return;
            }

            try
            {
                LoginFailureMessage = string.Empty;
                IsBusy = true;
                ResultUnit<string> response = await m_LoginService.TryToLogin(UserName, Password);
                if (response.IsSuccess == true)
                {
                    await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                }
                else
                {
                    HandleIncorrectLogin(response.ResultMessage);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;

            }
        }

        private async Task registerClicked()
        {
            UserName = string.Empty;
            Password = string.Empty;
            LoginFailureMessage = string.Empty;
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }

        private void HandleIncorrectLogin(string i_resultMessage)
        {
            switch (i_resultMessage)
            {
                case "No user found":
                    UserName = string.Empty;
                    Password = string.Empty;
                    LoginFailureMessage = $"{i_resultMessage},try again or register";
                    break;
                case "Incorect password":
                    Password = string.Empty;
                    LoginFailureMessage = $"{i_resultMessage},try again";
                    break;
                default:
                    break;
            }
        }

    }
}
