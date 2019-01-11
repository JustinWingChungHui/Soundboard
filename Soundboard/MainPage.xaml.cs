using SoundBoard.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SoundBoard
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ContactsCVS.Source = Contact.GetContactsGrouped(250);
        }
    }
}
