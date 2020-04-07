using System.Diagnostics;
using System.Windows.Input;
using News.CoreModule.ViewModels;
using Prism.Commands;

namespace News.ViewModels
{
    public class NewsDetailsViewModel : ViewModelBase
    {
        private string _url;
        private string _imageUrl;

        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }

        public ICommand GoToNewsCommand { get; }

        public NewsDetailsViewModel()
        {
            GoToNewsCommand = new DelegateCommand(OnGoToNews);
        }

        private void OnGoToNews()
        {
            Process.Start(new ProcessStartInfo(Url));
        }
    }
}
