using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoard.Model
{
    /// <summary>
    /// Base class for model entities.  
    /// Implements the INotifyPropertyChanged so each class already has a OnPropertyChanged method
    /// </summary>
    public abstract class ModelEntityBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
