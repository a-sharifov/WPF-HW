using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp6.Message.Classes;
using WpfApp6.Model;
using WpfApp6.Service.Interface;

namespace WpfApp6.ViewModel
{
    public class UserSettingsViewModel : ViewModelBase
    {
        public UserCargoModel? User { get; set; }
        public string? ErrorText { get; set; }

        private INavigationService? _service;
        public UserSettingsViewModel(INavigationService? service , IMessenger messenger)
        {
            _service = service;
            messenger.Register<ParameterMessage>(this, param => {
                User = param?.Message as UserCargoModel;
            });
        }
        public RelayCommand SaveSettings => new(() =>
        {
            if (!string.IsNullOrWhiteSpace(User?.User?.Address) && !string.IsNullOrWhiteSpace(User?.User?.FIN) && !string.IsNullOrWhiteSpace(User?.User?.Serial))
            {
                ErrorText = "";
                _service?.NavigateTo<UserMainViewModel>(new ParameterMessage { Message = User });
            }
            else ErrorText = "forgot to lead the field";
        });
    }
}

