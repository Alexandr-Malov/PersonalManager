using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalManager.Models
{
    class ShipModel : INotifyPropertyChanged
    {
        private int _orderNumber;
        private int _orderedQuantity;
        private int _orderAmount;
        private string _manufacturer;
        private string _nomenclature;
        private int _priceoneproduct;
        /// <summary>
        /// Номер заказа
        /// </summary>
        public int OrderNumber
        {
            get
            {
                return _orderNumber;
            }
            set
            {
                if (_orderNumber == value)
                {
                    return;
                }
                _orderNumber = value;
                OnPropertyChanged("OrderNumber");
            }
        }
        /// <summary>
        /// Заказанное количество
        /// </summary>
        public int OrderedQuantity
        {
            get
            {
                return _orderedQuantity;
            }
            set
            {
                if (_orderedQuantity == value)
                {
                    return;
                }
                _orderedQuantity = value;
                OnPropertyChanged("OrderedQuantity");
            }
        }
        /// <summary>
        /// Сумма заказа
        /// </summary>
        public int OrderAmount
        {
            get
            {
                return _orderAmount;
            }
            set
            {
                if (_orderAmount == value)
                {
                    return;
                }
                _orderAmount = value;
                OnPropertyChanged("OrderAmount");
            }
        }
        /// <summary>
        /// Производитель
        /// </summary>
        public string Manufacturer
        {
            get
            {
                return _manufacturer;
            }
            set
            {
                if (_manufacturer == value)
                {
                    return;
                }
                _manufacturer = value;
                OnPropertyChanged("Manufacturer");
            }
        }
        /// <summary>
        /// Номенклатура
        /// </summary>
        public string Nomenclature
        {
            get
            {
                return _nomenclature;
            }
            set
            {
                if (_nomenclature == value)
                {
                    return;
                }
                _nomenclature = value;
                OnPropertyChanged("Nomenclature");
            }
        }
        /// <summary>
        /// Цена за один товар
        /// </summary>
        public int PriceOneProduct
        {
            get
            {
                return _priceoneproduct;
            }
            set
            {
                if (_priceoneproduct == value)
                {
                    return;
                }
                _priceoneproduct = value;
                OnPropertyChanged("PriceOneProduct");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
