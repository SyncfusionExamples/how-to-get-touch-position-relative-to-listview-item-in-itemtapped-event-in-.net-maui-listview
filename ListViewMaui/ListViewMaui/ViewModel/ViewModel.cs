using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ListViewMaui
{
    public class ContactInfo : INotifyPropertyChanged
    {
        #region Fields
        private ObservableCollection<Contact> contacts;
        #endregion

        #region Properties
        public ObservableCollection<Contact> Contacts
        {
            get { return contacts; }
            set
            {
                contacts = value;
                OnPropertyChanged("Contacts");
            }
        }
        #endregion

        #region Constructor
        public ContactInfo()
        {
            contacts = new ObservableCollection<Contact>();
            contacts.Add(new Contact() { Name = "John", Number = "1234567890" });
            contacts.Add(new Contact() { Name = "Steve", Number = "0987654321" });
            contacts.Add(new Contact() { Name = "David", Number = "6789054321" });
            contacts.Add(new Contact() { Name = "Michael", Number = "0987654321" });
            contacts.Add(new Contact() { Name = "Smith", Number = "1234567890" });
            contacts.Add(new Contact() { Name = "Johnson", Number = "0987654321" });
            contacts.Add(new Contact() { Name = "Williams", Number = "6789054321" });
            contacts.Add(new Contact() { Name = "Brown", Number = "0987654321" });
            contacts.Add(new Contact() { Name = "Jones", Number = "1234567890" });
            contacts.Add(new Contact() { Name = "Miller", Number = "0987654321" });
            contacts.Add(new Contact() { Name = "Davis", Number = "6789054321" });
            contacts.Add(new Contact() { Name = "Garcia", Number = "0987654321" });
            contacts.Add(new Contact() { Name = "Rodriguez", Number = "1234567890" });
            contacts.Add(new Contact() { Name = "Wilson", Number = "0987654321" });
            contacts.Add(new Contact() { Name = "Martinez", Number = "6789054321" });
            contacts.Add(new Contact() { Name = "Anderson", Number = "0987654321" });
            contacts.Add(new Contact() { Name = "Taylor", Number = "1234567890" });
            contacts.Add(new Contact() { Name = "Thomas", Number = "0987654321" });
            contacts.Add(new Contact() { Name = "Hernandez", Number = "6789054321" });
            contacts.Add(new Contact() { Name = "Moore", Number = "0987654321" });
            contacts.Add(new Contact() { Name = "Martin", Number = "1234567890" });
            contacts.Add(new Contact() { Name = "Jackson", Number = "0987654321" });
        }
        #endregion

        #region PropertyChange
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
