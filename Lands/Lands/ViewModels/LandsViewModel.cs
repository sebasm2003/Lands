namespace Lands.ViewModels
{
    using Lands.Models;
    using Lands.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class LandsViewModel : BaseViewModel 
    {

        #region Services

        private ApiService _apiService;

        #endregion

        #region Attributes

        private ObservableCollection<Land> _lands;

        #endregion

        #region Properties

        public ObservableCollection<Land> Lands
        {
            get { return this._lands; }
            set { SetValue(ref this._lands, value); }
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
            var response = await this._apiService.GetList<Land>(
                "http://restcountries.eu",
                "/rest",
                "/v2/all");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
            }

            var list = (List<Land>)response.Result;
            this.Lands = new ObservableCollection<Land>(list);
        }
        #endregion

    }
}
