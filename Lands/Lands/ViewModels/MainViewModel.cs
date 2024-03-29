﻿namespace Lands.ViewModels
{

    using Lands.Models;
    using System.Collections.Generic;
    public class MainViewModel
    {
        #region Properties

        public List<Land> LandList { get; set; }

        #endregion

        #region ViewModels

        public LoginViewModel Login { get; set; }
        public LandsViewModel Lands { get; set; }
        public LandViewModel Land { get; set; }

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
