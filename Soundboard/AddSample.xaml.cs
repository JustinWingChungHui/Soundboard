using SoundBoard.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SoundBoard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddSample : Page
    {
        public AddSample()
        {
            this.InitializeComponent();
        }

        public StorageFile PictureFile { get; private set; }
        public StorageFile AudioFile { get; private set; }

        public bool SaveEnabled
        {
            get
            {
                return !string.IsNullOrEmpty(this.sampleTitle.Text)
                    && !string.IsNullOrEmpty(this.groupName.Text)
                    && !string.IsNullOrEmpty(this.sampleFilename.Text);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.PictureFile = null;
            this.AudioFile = null;
            this.sampleTitle.Text = string.Empty;
            this.groupName.Text = string.Empty;
            this.sampleFilename.Text = string.Empty;
            this.pictureFilename.Text = string.Empty;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
        }

        private async void buttonBrowsePicture_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.CommitButtonText = "Select";
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");


            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                this.pictureFilename.Text = file.Path;
                this.PictureFile = file;
            }
        }

        private async void buttonBrowseSample_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            openPicker.CommitButtonText = "Select";
            openPicker.FileTypeFilter.Add(".wav");
            openPicker.FileTypeFilter.Add(".mp3");
            openPicker.FileTypeFilter.Add(".png");


            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                this.sampleFilename.Text = file.Path;
                this.AudioFile = file;
            }
        }

        private async void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            this.buttonSave.IsEnabled = false;
            this.ErrorMessages.Text = string.Empty;

            try
            {
                if (!this.SaveEnabled)
                {
                    throw new ArgumentException("Missing fields");
                }

                await DataSource.AddSample(
                    this.sampleTitle.Text.Trim()
                    ,this.groupName.Text.Trim()
                    ,this.PictureFile
                    ,this.AudioFile);

                ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
            }
            catch (Exception ex)
            {
                this.ErrorMessages.Text = ex.Message;
            }
            finally
            {
                this.buttonSave.IsEnabled = true;
            }
        }
    }
}
