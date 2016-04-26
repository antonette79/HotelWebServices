using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Animation;
using WebClient.Annotations;
using WebClient.Facade;
using WebClient.Model;

namespace WebClient.ViewModel
{
    class ViewModel : INotifyPropertyChanged
    {
        #region Propeties
        public GuestCatalogSingelton Guests { get; set; }
        private Guest _selectedGuest = new Guest();
        public Guest SelectedGuest
        {
            get { return _selectedGuest; }
            set
            {
                _selectedGuest = value;
                RaisePropertyChanged("SelectedGuest");
            }
        }
        #endregion

        #region Constructors
        public ViewModel()
        {
            Guests = GuestCatalogSingelton.Instance;

            //Guests.Add(new Guest(1, "Flunky Foe", "Roskildevej 98, 4000 Roskilde"));
            //Guests.Add(new Guest(2, "Betta Boe", "Holbækvej 128, 4300 Holbæk"));
            //Guests.Add(new Guest(3, "Hola Hup", "Ringstedvej 13, 4300 Holbæk"));
            //Guests.Add(new Guest(4,"Skipping School", "Københavnsvej 456, 3400 Valby"));

            GetGuests();
        }
        #endregion

        public async void PutGuestAsync()
        {
            //var guest = new Guest(SelectedGuest.Guest_No, Name, Address);
            var msg = await GuestFacade.PutGuestAsync(SelectedGuest, SelectedGuest.Guest_No);

            MessageDialog message = new MessageDialog(msg);
            await message.ShowAsync();
            GetGuests();


        }
        public async void GetGuests()
        {
            Guests.Guests.Clear();

            IEnumerable<Guest> guestListTemp = await Facade.GuestFacade.GetGuestsAsync();
            foreach (var item in guestListTemp)
            {
                Guests.Add(item);
            }
        }

        public void SelectGuest()
        {
            if (SelectedGuest != null)
            {
                Guest_No = SelectedGuest.Guest_No;
                Name = SelectedGuest.Name;
                Address = SelectedGuest.Address;

            }
            else
            {
                MessageDialog messageDialog = new MessageDialog("Fejl", "Kunne ikke sætte bruger til admin!!!");
            }
        }

        public void PopulateList()
        {
            Guests.Guests.Clear();
            GetGuests();
        }

        #region PropertyChangedSupport
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }
}
