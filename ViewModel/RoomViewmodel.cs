using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ViewModel
{
    public class RoomViewmodel : INotifyPropertyChanged
    {
        private RoomInformation _room;
        public RoomViewmodel()
        {
            _room = new RoomInformation();
        }

        public int RoomId
        {
            get => _room.RoomId;
            set
            {
                if (_room.RoomId != value)
                {
                    _room.RoomId = value;
                    OnPropertyChange();
                }
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChange([CallerMemberName] string ?  propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
