using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

using Newtonsoft.Json;
using Windows.Storage;
using Windows.Media.Core;

namespace SoundBoard.Model
{

    public class Sample : ModelEntityBase
    {


        private Guid _uid;
        private string _title;
        private string _audioPath;
        private string _imagePath;
        private BitmapImage _image;

        /// <summary>
        /// Creates a new instance of a sample
        /// </summary>
        public Sample()
        {
            _uid = Guid.NewGuid();
        }

        /// <summary>
        /// Creates a new instance of sample
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="title"></param>
        /// <param name="audiopath"></param>
        /// <param name="imagepath"></param>
        public Sample(Guid uid, string title, string audiopath, string imagepath)
        {
            _uid = uid;
            _title = title;
            _audioPath = audiopath;
            _imagePath = imagepath;
        }


        public Guid UniqueID
        {
            get { return _uid; }
            set
            {
                _uid = value;
                OnPropertyChanged("UniqueID");
            }
        }

        private string _groupKey;
        private MediaSource _mediaSource;

        public string GroupKey
        {
            get { return _groupKey; }
            set
            { 
                _groupKey = value;
                OnPropertyChanged("GroupKey");
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }



        public string AudioPath
        {
            get { return _audioPath; }
            set
            {
                _audioPath = value;
                OnPropertyChanged("AudioPath");
            }
        }


        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }



        [JsonIgnore]
        public BitmapImage Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        [JsonIgnore]
        public MediaSource MediaSource
        {
            get { return _mediaSource; }
            set
            {
                _mediaSource = value;
                OnPropertyChanged("MediaSource");
            }
        }

        public override string ToString()
        {
            return this.Title;
        }

        public async Task LoadSample()
        {
            var file = await StorageFile.GetFileFromPathAsync(this.AudioPath);

            this.MediaSource = MediaSource.CreateFromStorageFile(file);
        }
    }
}
