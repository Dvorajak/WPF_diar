using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diar_WPF.ModelView
{
    public class ViewModelEvent : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string? NameofPropeprty)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(NameofPropeprty));
        }
    }
}
