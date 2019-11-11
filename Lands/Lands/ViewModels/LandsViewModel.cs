namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Lands.Models;
    using Lands.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LandsViewModel : BaseViewModel 
    {

        #region Services

        private ApiService _apiService;

        #endregion

        #region Attributes
     
        private ObservableCollection<LandItemViewModel> _lands;
        private List<Land> _landsList;
        private string _filter;

        private bool _isRefreshing;

        #endregion

        #region Properties

        public string Filter
        {
            get { return this._filter; }
            set { 
                SetValue(ref this._filter, value);
                this.Search();
            }
        }

        public ObservableCollection<LandItemViewModel> Lands
        {
            get { return this._lands; }
            set { SetValue(ref this._lands, value); }
        }

        public bool IsRefreshing
        {
            get { return this._isRefreshing; }
            set { SetValue(ref this._isRefreshing, value); }
        }

        #endregion

        #region Constructors
        public LandsViewModel()
        {
            this._apiService = new ApiService();
            this.LoadLands();
        }


        #endregion

        #region Methods

        private async void LoadLands()
        {
            this.IsRefreshing = true;
            var connection = await this._apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   connection.Message,
                   "Accept");

                await Application.Current.MainPage.Navigation.PopAsync();
            }

            var response = await this._apiService.GetList<Land>(
                "http://restcountries.eu",
                "/rest",
                "/v2/all");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
            }

            this._landsList = (List<Land>)response.Result;
            this.Lands = new ObservableCollection<LandItemViewModel>(this.ToLandItemViewModel());
            this.IsRefreshing = false;           

        }


        #endregion

        #region Commands

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLands);   
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }



        #endregion

        #region Methods

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Lands = new ObservableCollection<LandItemViewModel>(this.ToLandItemViewModel());
            }
            else
            {
                this.Lands = new ObservableCollection<LandItemViewModel>(
                    this.ToLandItemViewModel().Where(x => x.Name.ToLower().Contains(this.Filter.ToLower())
                    || x.Capital.ToLower().Contains(this.Filter.ToLower())));
            }
        }

        private IEnumerable<LandItemViewModel> ToLandItemViewModel()
        {
            return this._landsList.Select(x => new LandItemViewModel
            {
                Alpha2Code = x.Alpha2Code,
                Alpha3Code = x.Alpha3Code,
                AltSpellings = x.AltSpellings,
                Area = x.Area,
                Borders = x.Borders,
                CallingCodes = x.CallingCodes,
                Capital = x.Capital,
                Cioc = x.Cioc,
                Currencies = x.Currencies,
                Demonym = x.Demonym,
                Flag = x.Flag,
                Gini = x.Gini,
                Languages = x.Languages,
                LatitudeLongitude = x.LatitudeLongitude,
                Name = x.Name,
                NativeName = x.NativeName,
                NumericCode = x.NumericCode,
                Population = x.Population,
                Region = x.Region,
                RegionalBlocs = x.RegionalBlocs,
                Subregion = x.Subregion,
                Timezones = x.Timezones,
                TopLevelDomain = x.TopLevelDomain,
                Translations = x.Translations
                
            }).ToList();
        }

        #endregion

    }
}
