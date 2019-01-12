using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoard.Model
{
    public class SampleGroup : ObservableCollection<Sample>
    {
        public string Key { get; set; }
    }
}
