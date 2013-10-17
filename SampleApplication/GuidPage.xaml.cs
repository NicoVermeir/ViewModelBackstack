using System.ComponentModel;
using Microsoft.Phone.Controls;
using SampleApplication.ViewModel;
using ViewModelBackstack;

namespace SampleApplication
{
    public partial class GuidPage : PhoneApplicationPage
    {
        public GuidPage()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (ViewModelBackStack.CanGoBack())
            {
                DataContext = ViewModelBackStack.GoBack<GuidViewModel>();
                e.Cancel = true;
                return;
            }

            base.OnBackKeyPress(e);
        }
    }
}