using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Memorium.Common
{
    class NavigationService : INavigationService
    {
        public void GoBack()
        {
            ((Frame)Window.Current.Content).GoBack();
        }

        public void Navigate(Type sourcePageType)
        {
            ((Frame)Window.Current.Content).Navigate(sourcePageType);
        }

        public void Navigate(Type sourcePageType, object parameter)
        {
            ((Frame)Window.Current.Content).Navigate(sourcePageType, parameter);
        }

        private NavigationService()
        {

        }

        private static readonly Lazy<NavigationService> instance = new Lazy<NavigationService>(() => new NavigationService());

        public static NavigationService Instance => instance.Value;
    }
}
