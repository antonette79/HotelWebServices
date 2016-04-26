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
        #region For Add new Guest
        private int _guestNo;
        private string _name;
        private string _address;


        public int Guest_No
        {
            get { return _guestNo; }
            set { _guestNo = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(); }
        } 
        #endregion

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
            GetGuests();
        }
        #endregion

        
        public async void PutGuestAsync()
        {
            var msg = await GuestFacade.PutGuestAsync(SelectedGuest, SelectedGuest.Guest_No);

            MessageDialog message = new MessageDialog(msg);
            await message.ShowAsync();
            GetGuests();
        }

        public async void DeleteGuestAsync()
        {
            var msg = await GuestFacade.DeleteGuestAsync(SelectedGuest);

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
        public async void PostGuestAsync()
        {
            Guest newGuest = new Guest(HighestGuestNo() +1, Name, Address);
            var msg = await GuestFacade.PostGuestAsync(newGuest);

            MessageDialog message = new MessageDialog(msg);
            await message.ShowAsync();
            GetGuests();
        }
        private int HighestGuestNo()
        {
            int i = -1;
            foreach (var guest in Guests.Guests)
            {
                if (guest.Guest_No > i)
                {
                    i = guest.Guest_No;
                }
            }
            return i;
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
