using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoundBoard.Model;

namespace SoundBoard.MockData
{
    public static class MockData
    {
        public static List<Sample> GetMockData()
        {
            var appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;           

            return new List<Sample>
            {
                new Sample
                {
                    Title = "I Want to Break Free",
                    AudioPath= $@"{appInstalledFolder.Path}\MockData\BreakFree.mp3",
                    ImagePath = $@"{appInstalledFolder.Path}\MockData\BreakFreeIntro.png",
                    GroupKey = "Intros"
                },
                new Sample
                {
                    Title = "Flash",
                    AudioPath= $@"{appInstalledFolder.Path}\MockData\Flash.mp3",
                    ImagePath = $@"{appInstalledFolder.Path}\MockData\FlashIntro.png",
                    GroupKey = "Intros"
                },
                new Sample
                {
                    Title = "One Vision",
                    AudioPath= $@"{appInstalledFolder.Path}\MockData\OneVisionIntro.mp3",
                    ImagePath = $@"{appInstalledFolder.Path}\MockData\OneVisionIntro.png",
                    GroupKey = "Intros"
                },
                new Sample
                {
                    Title = "One Vision M8",
                    AudioPath= $@"{appInstalledFolder.Path}\MockData\OneVisionMiddle8.mp3",
                    ImagePath = $@"{appInstalledFolder.Path}\MockData\OneVisionMiddle8.png",
                    GroupKey = "Middle8"
                }
            };
        }
    }
}
