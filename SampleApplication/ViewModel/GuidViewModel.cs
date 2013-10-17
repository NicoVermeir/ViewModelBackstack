using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SampleApplication.Messages;
using ViewModelBackstack;

namespace SampleApplication.ViewModel
{
    public class GuidViewModel : ViewModelBase
    {
        private string _guidString;
        private RelayCommand _newGuidCommand;

        public string GuidString
        {
            get { return _guidString; }
            set
            {
                if (_guidString == value) return;

                _guidString = value;
                RaisePropertyChanged(() => GuidString);
            }
        }

        public RelayCommand NewGuidCommand
        {
            get
            {
                return _newGuidCommand ?? (_newGuidCommand = new RelayCommand(LoadNewData));
            }
        }

        public GuidViewModel()
        {
            Messenger.Default.Register<GenerateNewGuidMessage>(this, msg => GenerateGuid());            
        }

        private void GenerateGuid()
        {
            GuidString = Guid.NewGuid().ToString();
        }

        private void LoadNewData()
        {
            if (ViewModelBackStack.ContainsKey(GuidString))
                ViewModelBackStack.Replace(GuidString, this);
            else
                ViewModelBackStack.Add(GuidString, this);

            Messenger.Default.Send(new GenerateNewGuidMessage());
        }
    }
}
