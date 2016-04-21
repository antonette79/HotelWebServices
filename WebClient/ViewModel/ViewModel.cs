using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;
using WebClient.Annotations;
using WebClient.Model;

namespace WebClient.ViewModel
{
    class ViewModel : INotifyPropertyChanged
    {
        public GuestCatalogSingelton Guests { get; set; }
        public int Guest_No { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public ViewModel()
        {
            Guests = GuestCatalogSingelton.Instance;
            Guests.Add(new Guest(1, "Flunky Foe", "Roskildevej 98, 4000 Roskilde"));
            Guests.Add(new Guest(2, "Betta Boe", "Holbækvej 128, 4300 Holbæk"));
            Guests.Add(new Guest(3, "Hola Hup", "Ringstedvej 13, 4300 Holbæk"));
            Guests.Add(new Guest(4,"Skipping School", "Københavnsvej 456, 3400 Valby"));
        }

        #region PropertyChangedSupport
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
