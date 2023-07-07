using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeThere.ViewModels
{
    public partial class BaseViewModels : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string title;

        public bool IsNotBusy => !IsBusy;

    }
}

