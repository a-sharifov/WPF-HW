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
    public class DeclerationViewModel : ViewModelBase
    {
        public PreparationDeclerationModel? declerationModel { get; set; }

        public UserCargoModel? User { get; set; }
        public string? InvoicePrice { get; set; }
        public string? ErrorText { get; set; }

        private readonly INavigationService? _service;

        public DeclerationViewModel(INavigationService? service, IMessenger messenger)
        {
            _service = service;
            messenger.Register<ParameterMessage>(this, param => {
                declerationModel = new();
                InvoicePrice = null;
                ErrorText = null;
                User = param?.Message as UserCargoModel;
            });
        }

        public RelayCommand ClickReturn => new(() => { _service?.NavigateTo<UserMainViewModel>(new ParameterMessage { Message = User }); });
        public RelayCommand ClickSave => new(() =>
        {
            if (!string.IsNullOrWhiteSpace(InvoicePrice) && !string.IsNullOrWhiteSpace(declerationModel?.SiteName) && !string.IsNullOrWhiteSpace(declerationModel?.WareHouse)
              && !string.IsNullOrWhiteSpace(declerationModel?.TrackingNumber) && !string.IsNullOrWhiteSpace(declerationModel?.Quantity) && !string.IsNullOrWhiteSpace(declerationModel?.Note)
              && !string.IsNullOrWhiteSpace(declerationModel?.ProductCategory))
            {
                long Price;
                if (long.TryParse(InvoicePrice, out Price)) {
                    declerationModel.InvoicePrice = Price;
                    User?.UserOrder?.Add(declerationModel);
                    _service?.NavigateTo<UserMainViewModel>(new ParameterMessage { Message = User });
                }
                else ErrorText = " Invoice Price incorrectly";
            }
            else ErrorText = "forgot to lead the field";
        });
    }
}
