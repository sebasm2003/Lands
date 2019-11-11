namespace Lands.ViewModels
{
    public class MainViewModel
    {
        #region ViewModels

        public LoginViewModel Login { get; set; }
        public LandsViewModel Lands { get; set; }

        #endregion

        #region Constructors

        public MainViewModel()
        {
            _instance = this;
            this.Login = new LoginViewModel();
        }

        #endregion

        #region Singleton

        private static MainViewModel _instance;

        public static MainViewModel GetInstance()
        {
            if (_instance == null)
                return new MainViewModel();

            return _instance;
        }

        #endregion
    }
}
