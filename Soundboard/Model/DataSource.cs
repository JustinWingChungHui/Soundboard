using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SoundBoard.Model
{
    public class DataSource
    {
        private const string JSON_FILENAME = "SampleData.json";

        public static async Task<ObservableCollection<SampleGroup>> GetSamplesGrouped()
        {
            var samples = await GetSamples();

            // Load all samples into memory
            await Task.WhenAll(samples.Select(s => s.LoadSample()));

            var result = new ObservableCollection<SampleGroup>();

            var query = from item in samples
                        group item by item.GroupKey into g
                        orderby g.Key
                        select new { GroupName = g.Key, Items = g };


            foreach (var g in query)
            {
                SampleGroup group = new SampleGroup();
                group.Key = g.GroupName;
                foreach (var item in g.Items)
                {
                    group.Add(item);
                }
                result.Add(group);
            }

            return result;
        }


        public static async Task AddSample(string title, string group, StorageFile pictureFile, StorageFile audioFile)
        {
            var samples = await GetSamples();

            // Copy files to applicationdata folder
            var folder = ApplicationData.Current.LocalFolder;

            // Picture
            string newPicturePath = null;
            if (pictureFile != null)
            {
                var picBuffer = await FileIO.ReadBufferAsync(pictureFile);
                var newPictureFile = await folder.CreateFileAsync($"{Guid.NewGuid()}.{pictureFile.FileType}");
                await FileIO.WriteBytesAsync(newPictureFile, picBuffer.ToArray());
                newPicturePath = newPictureFile.Path;
            }

            // audio
            var audioBuffer = await FileIO.ReadBufferAsync(audioFile);
            var newAudioFile = await folder.CreateFileAsync($"{Guid.NewGuid()}.{audioFile.FileType}");
            await FileIO.WriteBytesAsync(newAudioFile, audioBuffer.ToArray());

            var formattedGroup = samples.FirstOrDefault(s => s.GroupKey.Equals(group, StringComparison.InvariantCultureIgnoreCase))?
                .GroupKey ?? group;

            samples.Add(new Sample
            {
                UniqueID = Guid.NewGuid(),
                Title = title,
                AudioPath = newAudioFile.Path,
                GroupKey = formattedGroup,
                ImagePath = newPicturePath
            });

            await SaveSamples(samples);

            // Load all samples into memory
            await Task.WhenAll(samples.Select(s => s.LoadSample()));
        }

        public static async Task RemoveSample(Guid uniqueID)
        {
            var samples = await GetSamples();
            samples.RemoveAll(s => s.UniqueID == uniqueID);

            await SaveSamples(samples);
        }

        private static async Task<List<Sample>> GetSamples()
        {
            List<Sample> samples = new List<Sample>();

            try
            {
                var json = await GetJSON_Text();
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Sample>>(json);
                if (obj != null)
                {
                    samples = obj;
                }
            }
            catch
            {
                samples = new List<Sample>();
            }


            return samples;
        }

        /// <summary>
        /// Gets the JSON file
        /// </summary>
        /// <returns></returns>
        private static async Task<string> GetJSON_Text()
        {

            try
            {
                var folder = ApplicationData.Current.LocalFolder;
                var file = await folder.GetFileAsync(JSON_FILENAME);
                return await FileIO.ReadTextAsync(file);
            }
            catch
            {
                return string.Empty;
            }
        }

        private static async Task SaveSamples(List<Sample> samples)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(samples);
            var folder = ApplicationData.Current.LocalFolder;

            StorageFile file;
            try
            {
                file = await folder.GetFileAsync(JSON_FILENAME);
            }
            catch
            {
                file = await folder.CreateFileAsync(JSON_FILENAME);
            }

            await FileIO.WriteTextAsync(file, json);
        }
    }
}
