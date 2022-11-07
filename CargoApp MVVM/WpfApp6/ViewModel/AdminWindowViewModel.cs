using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using WpfApp6.Message.Classes;
using WpfApp6.Model;
using WpfApp6.Service.Interface;

namespace WpfApp6.ViewModel
{
    public class AdminWindowViewModel : ViewModelBase
    {
        public int SelectedIndex { get; set; }
        public string Search { get; set; }
        public PreparationDeclerationModel? Selected { get; set; }
        public List<PreparationDeclerationModel>? Preparation { get; set; }
        public Admin_UserContentModel? ContentModel { get; set; }
        private readonly INavigationService? _service;

        public AdminWindowViewModel(INavigationService? service, IMessenger messenger)
        {
            Search = "";
            _service = service;
            messenger.Register<ParameterMessage>(this, param => {
                ContentModel = param?.Message as Admin_UserContentModel;
            });
        }

        public RelayCommand SearchCommand => new(() => {
            if (ContentModel != null && ContentModel.AllUser.ContainsKey(Search))
            {
                Preparation = ContentModel.AllUser[Search].UserOrder;
            }
        });

        public RelayCommand ReturnCommand => new(() => {
            _service?.NavigateTo<SignInViewModel>(new ParameterMessage() { Message = ContentModel });
        });

        public RelayCommand DeleteDeclerationCommand => new(() => {
            Preparation?.RemoveAt(SelectedIndex);
        });

    }
}
