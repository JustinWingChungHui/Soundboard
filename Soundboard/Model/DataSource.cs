using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoard.Model
{
    public class DataSource
    {
        public static ObservableCollection<SampleGroup> GetSamplesGrouped()
        {
            var samples = GetSamples();

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

        private static IEnumerable<Sample> GetSamples()
        {
            var samples = MockData.MockData.GetMockData();

            return samples;
        }
    }
}
