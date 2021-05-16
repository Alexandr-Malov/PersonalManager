using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalManager.Models
{
    class ContactsModel : INotifyPropertyChanged
    {
        private string _nameContact { get; set; }
        private string _phoneNumber { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name_Contact 
        {
            get 
            {
                return _nameContact;
            }
            set
            {
                if (_nameContact == value)
                {
                    return;
                }
                _nameContact = value;
                OnPropertyChanged("Name_Contact");
            }
        }

        public string Phone_Number
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                if (_phoneNumber == value)
                {
                    return;
                }
                _phoneNumber = value;
                OnPropertyChanged("Phone_Number");
            }

        }
        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
