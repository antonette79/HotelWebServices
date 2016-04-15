using System.Collections.ObjectModel;

namespace WebClient.Model
{
    public class GuestCatalogSingelton
    {
        private static GuestCatalogSingelton _instance;

        private GuestCatalogSingelton()
        {
            var guests = new ObservableCollection<Guest>();
        }

        public ObservableCollection<Guest> guests { get; set; }

        public static GuestCatalogSingelton Instance
        {
            get { return _instance ?? (_instance = new GuestCatalogSingelton()); }
            
        }

        public void Add(Guest newGuest)
        {
            guests.Add(newGuest);
        }

        public void Remove(Guest removeGuest)
        {
            guests.Remove(removeGuest);
        }
    }
}