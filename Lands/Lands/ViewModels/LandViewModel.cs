using Lands.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Lands.ViewModels
{
    public class LandViewModel : BaseViewModel
    {
        #region Attributes

        private ObservableCollection<Border> _borders;
        private ObservableCollection<Currency> _currencies;
        private ObservableCollection<Language> _languages;

        #endregion

        #region Properties

        public ObservableCollection<Border> Borders
        {
            get { return this._borders; }
            set
            {
                this.SetValue(ref this._borders, value);           
            }
        }

        public ObservableCollection<Currency> Currencies
        {
            get { return this._currencies; }
            set
            {
                this.SetValue(ref this._currencies, value);
            }
        }

        public ObservableCollection<Language> Languages
        {
            get { return this._languages; }
            set
            {
                this.SetValue(ref this._languages, value);
            }
        }

        public Land Land { get; set; }

        #endregion

        #region Constructors
        public LandViewModel(Land land)
        {
            this.Land = land;
            this.LoadBorders();
            this.Currencies = new ObservableCollection<Currency>(this.Land.Currencies);
            this.Languages = new ObservableCollection<Language>(this.Land.Languages);
        }


        #endregion

        #region Methods

        private void LoadBorders()
        {
            this.Borders = new ObservableCollection<Border>();
            foreach (var border in this.Land.Borders)
            {
                var land = MainViewModel.GetInstance().LandList
                    .Where(x => x.Alpha3Code == border)
                    .FirstOrDefault();
                if(land != null)
                {
                    this.Borders.Add(new Border
                    {
                        Code = land.Alpha3Code,
                        Name = land.Name
                    });
                }
            }
        }

        #endregion

    }
}
