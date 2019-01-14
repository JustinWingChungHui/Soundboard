using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SoundBoard.Model;
using Windows.UI.Xaml.Navigation;

namespace SoundBoard
{
    public sealed partial class MainPage : Page
    {
        private Guid _currentlyPlaying;

        public ObservableCollection<SampleGroup> Samples { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await LoadData();
        }

        private async void Page_Loading(FrameworkElement sender, object args)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            this.Samples = await DataSource.GetSamplesGrouped();
            SamplesCVS.Source = this.Samples;

            this.progressRing.Visibility = Visibility.Collapsed;
            this.itemGridView.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// Invoked when an item within a group is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        private void itemGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

            try
            {
                var sampleItem = ((Sample)e.ClickedItem);

                //Resume/pause existing sample
                if (_currentlyPlaying == sampleItem.UniqueID)
                {
                    if (this.AudioPlayer.MediaPlayer.PlaybackSession.PlaybackState == Windows.Media.Playback.MediaPlaybackState.Playing)
                    {
                        this.AudioPlayer.MediaPlayer.Pause();
                    }
                    else
                    {
                        this.AudioPlayer.MediaPlayer.Play();
                    }
                    return;
                }

                this.AudioPlayer.Source = sampleItem.MediaSource;
                _currentlyPlaying = sampleItem.UniqueID;
                this.AudioPlayer.MediaPlayer.Play();
                this.AudioPlayer.Visibility = Visibility.Visible;
                this.playBackErrorMessage.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                this.AudioPlayer.Visibility = Visibility.Collapsed;

                this.playBackErrorMessage.Text = ex.Message;
                this.playBackErrorMessage.Visibility = Visibility.Visible;
            }

        }

        private void RemoveSampleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddSampleButton_Click(object sender, RoutedEventArgs e)
        {
            //Navigate to next screen
            ((Frame)Window.Current.Content).Navigate(typeof(AddSample));
        }

        private void itemGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.RemoveSampleButton.Visibility = e.AddedItems.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }


    }
}
