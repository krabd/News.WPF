using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using News.CoreModule.ViewModels;
using News.DataAccess.Interfaces;
using News.ViewModels.Items;

namespace News.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        private readonly INewsRepository _newsRepository;

        public ObservableCollection<NewsItemViewModel> News { get; } = new ObservableCollection<NewsItemViewModel>();

        public NewsViewModel(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task InitializeAsync(CancellationToken token)
        {
            var news = await _newsRepository.GetNewsAsync(token);
            token.ThrowIfCancellationRequested();

            News.Clear();
            News.AddRange(news.Select(i =>
            {
                var newsItem = new NewsItemViewModel();
                newsItem.Initialize(i);
                return newsItem;
            }));
        }
    }
}
