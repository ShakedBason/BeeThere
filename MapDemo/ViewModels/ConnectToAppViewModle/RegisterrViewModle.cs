using CommunityToolkit.Mvvm.ComponentModel;
using System;
using BeThere.Views;
using BeThere.Models;
using BeThere.Services;
//using Android.Service.Autofill;


namespace BeThere.ViewModels
{
    public partial class RegisterViewModel : BaseViewModels
    {
        private ConnectToAppService m_LoginService;
        private User m_UserData;
        public Command RegisterCommand { get; }

        public RegisterViewModel(ConnectToAppService i_LoginService)
        {
            m_LoginService = i_LoginService;
            m_UserData = new User();
            RegisterCommand = new Command(async () => await RegisterAsync());

        }

        [ObservableProperty]
        private string invalidRegisterMessage;

        [ObservableProperty]
        private string? confirmPassword;

        public User User { get { return m_UserData; } }


        private async Task RegisterAsync()
        {
            bool canRegister = false;

            if (IsBusy == true)
            {
                return;
            }

            canRegister = validateMandatoryFieldsFilled() && validatePassword();

            if (canRegister == true)
            {

                try
                {
                    InvalidRegisterMessage = string.Empty;
                    IsBusy = true;
                    ResultUnit<string> response = await m_LoginService.TryToRegisterUser(m_UserData);
                    if (response.IsSuccess == true)
                    {
                        m_UserData.ClearAllFeilds();
                        await Shell.Current.DisplayAlert("Registertion", "Registration Successful!", "OK");
                        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                    }
                    else
                    {
                        HandleIncorrectRegistertion(response.ResultMessage);
                    }
                }
                catch (Exception ex)
                {
                    m_UserData.ClearAllFeilds();
                    await Shell.Current.DisplayAlert("Registertion", "Registration faild for some reason, please try again", "OK");
                }
                finally
                {
                    IsBusy = false;

                }
            }

        }

        private bool validateMandatoryFieldsFilled()
        {
            bool isValidData = true;

            if (m_UserData.Username is null || m_UserData.Password is null || m_UserData.Email is null || ConfirmPassword is null)
            {
                InvalidRegisterMessage = "Please fill all requered fileds";
                m_UserData.Password = string.Empty;
                ConfirmPassword = string.Empty;
                isValidData = false;
            }

            return isValidData;
        }

        private bool validatePassword()
        {
            bool isValidPassword = true;

            if (m_UserData.Password != ConfirmPassword)
            {
                isValidPassword = false;
                InvalidRegisterMessage = "Unmatched passwords, try again";

            }

            return isValidPassword;
        }

        private void HandleIncorrectRegistertion(string i_Message)
        {
            m_UserData.Username = string.Empty;
            m_UserData.Password = string.Empty;
            ConfirmPassword = string.Empty;
            InvalidRegisterMessage = "This user name is already in use, please try again";
        }

    }
}

