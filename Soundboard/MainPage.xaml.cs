using System;
using SoundBoard.Model;
using Windows.Media.Core;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SoundBoard
{
    public sealed partial class MainPage : Page
    {
        private Guid _currentlyPlaying;

        public MainPage()
        {
            this.InitializeComponent();
            //SamplesCVS.Source = Contact.GetContactsGrouped(250);
            SamplesCVS.Source = DataSource.GetSamplesGrouped();
        }


        /// <summary>
        /// Invoked when an item within a group is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        private async void itemGridView_ItemClick(object sender, ItemClickEventArgs e)
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

                //Play new sample
                //var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                //var file = await folder.GetFileAsync(sampleItem.AudioPath);

                var file = await StorageFile.GetFileFromPathAsync(sampleItem.AudioPath);
               

                IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);

                this.AudioPlayer.Source = MediaSource.CreateFromStorageFile(file);
                _currentlyPlaying = sampleItem.UniqueID;
                this.AudioPlayer.MediaPlayer.Play();
                this.AudioPlayer.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.playBackErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                this.AudioPlayer.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                this.playBackErrorMessage.Text = ex.Message;
                this.playBackErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }

        }

        private void RemoveSampleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddSampleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void itemGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.RemoveSampleButton.Visibility = e.AddedItems.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
