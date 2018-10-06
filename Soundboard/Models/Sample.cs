using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundboard.Models
{
    public class Sample
    {
        public Guid Id { get; set; }

        public string Group { get; set; }

        public string Name { get; set; }

        public string ThumbnailPath { get; set; }

        public string AudioFilePath { get; set; }
    }
}
