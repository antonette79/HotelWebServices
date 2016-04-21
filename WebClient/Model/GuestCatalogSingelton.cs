using System.Collections.ObjectModel;

namespace WebClient.Model
{
    public class GuestCatalogSingelton
    {
        private static GuestCatalogSingelton _instance;

        private GuestCatalogSingelton()
        {
            Guests = new ObservableCollection<Guest>();
        }

        public ObservableCollection<Guest> Guests { get; set; }

        public static GuestCatalogSingelton Instance
        {
            get { return _instance ?? (_instance = new GuestCatalogSingelton()); }      
        }

        public void Add(Guest newGuest)
        {
            Guests.Add(newGuest);
        }

        public void Remove(Guest removeGuest)
        {
            Guests.Remove(removeGuest);
        }
    }
}