using System;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SampleApplication.Messages;

namespace SampleApplication.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private RelayCommand _goToGuidPageCommand;

        public RelayCommand GoToGuidPageCommand
        {
            get
            {
                return _goToGuidPageCommand ?? (_goToGuidPageCommand = new RelayCommand(GoToGuidPage));
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private void GoToGuidPage()
        {
            Messenger.Default.Send(new GenerateNewGuidMessage());

            _navigationService.NavigateTo(new Uri("/GuidPage.xaml", UriKind.Relative));
        }
    }
}