using ControlzEx.Standard;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Converters;
using Microsoft.Xaml.Behaviors.Core;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp6.Message.Classes;
using WpfApp6.Model;
using WpfApp6.Service.Classes;
using WpfApp6.Service.Interface;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp6.ViewModel
{
    public class SignUpViewModel : ViewModelBase
    {
        public string UserText { get; set; }
        public string? PasswordText { get; set; }
        public string? ConfirmText { get; set; }
        public string? SerialText { get; set; }
        public string? AdressText { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FINText { get; set; }
        public string? ErrorText { get; set; }
        public Admin_UserContentModel? AllUser { get; set; }

        private readonly INavigationService _service;

        public SignUpViewModel(INavigationService service, IMessenger messenger)
        {
            UserText = "";
            _service = service;
            messenger.Register<ParameterMessage>(this, param => {
                    AllUser = param?.Message as Admin_UserContentModel;
            });
        }

        public RelayCommand MouseClickReturnButton => new(() => { _service.NavigateTo<SignInViewModel>(); });
        public RelayCommand MouseClickRegisterButton => new(() =>
        {
            if ( AllUser != null && !AllUser.AllUser.ContainsKey(UserText))
            {
                if (PasswordText == ConfirmText)
                {
                    if (!string.IsNullOrWhiteSpace(PasswordText) && !string.IsNullOrWhiteSpace(UserText) && !string.IsNullOrWhiteSpace(SerialText)
                       && !string.IsNullOrWhiteSpace(AdressText) && !string.IsNullOrWhiteSpace(PhoneNumber) && !string.IsNullOrWhiteSpace(FINText))
                    {
                        long number;
                        if (long.TryParse(PhoneNumber, out number))
                        {
                            ErrorText = "";
                            var NewUser = new UserPageModel()
                            {
                                Address = AdressText,
                                FIN = FINText, 
                                Number = number,
                                Password = MD5HashService.GetHash(PasswordText),
                                Serial = SerialText,
                                UserName = UserText,
                            };
                            AllUser.AllUser.Add(UserText, new UserCargoModel() { User = NewUser });

                            _service.NavigateTo<SignInViewModel>();
                        }
                        else ErrorText = "Number incorrectly";
                    }
                    else ErrorText = "Entered incorrectly";
                }
                else ErrorText = "Password is incorrect";
            }
            else ErrorText = "Such an account exists";
        });
    }
} 