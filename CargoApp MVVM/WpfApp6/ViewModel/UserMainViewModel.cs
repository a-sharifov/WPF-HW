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
using WpfApp6.Service.Classes;
using WpfApp6.Service.Interface;

namespace WpfApp6.ViewModel
{
    public class UserMainViewModel : ViewModelBase
    {
        public UserCargoModel? User { get; set; }
        public List<PreparationDeclerationModel>? UserPackages{ get; set; }
        public List<PreparationDeclerationModel>? UserOrders{ get; set; }
        public List<PreparationDeclerationModel>? UserInFillial{ get; set; }
        private readonly INavigationService? _service;
        public UserMainViewModel(INavigationService? service, IMessenger messenger)
        {
            _service = service;
            messenger.Register<ParameterMessage>(this, param =>
            {
                User = param?.Message as UserCargoModel;

                UserPackages = new();
                UserOrders = new();
                UserInFillial = new();

                for (int i = 0; i < User?.UserOrder?.Count; i++)
                {
                    switch (User.UserOrder[i].Status)
                    {
                        case OrderStatus.Packages:
                            UserPackages.Add(User.UserOrder[i]);
                            break;
                        case OrderStatus.Orders:
                            UserOrders.Add(User.UserOrder[i]);
                            break;
                        case OrderStatus.InFillial:
                            UserInFillial.Add(User.UserOrder[i]);
                            break;
                    }
                }
            });
        } 
        public RelayCommand ClickExit => new(() => { UserAccountSaveService.SaveUser(User); });
        public RelayCommand ClickSethings => new(() =>
        {
            _service?.NavigateTo<UserSettingsViewModel>(new ParameterMessage { Message = User});
        });

        public RelayCommand ClickAddDecleration => new(() =>
        {
            _service?.NavigateTo<DeclerationViewModel>(new ParameterMessage() { Message = User });
        });

    }
}
