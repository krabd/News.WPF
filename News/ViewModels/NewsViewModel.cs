using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using News.CoreModule.ViewModels;
using News.DataAccess.Interfaces;
using News.Interfaces;
using News.ViewModels.Items;

namespace News.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        private readonly INewsRepository _newsRepository;
        private readonly IUpdateNewsService _newsService;

        public ObservableCollection<NewsItemViewModel> News { get; } = new ObservableCollection<NewsItemViewModel>();

        public NewsViewModel(INewsRepository newsRepository, IUpdateNewsService newsService)
        {
            _newsRepository = newsRepository;
            _newsService = newsService;
        }

        public async Task InitializeAsync(CancellationToken token)
        {
            _newsService.Stop();

            var news = await _newsRepository.GetNewsAsync(token);
            token.ThrowIfCancellationRequested();

            News.Clear();
            News.AddRange(news.OrderByDescending(i => i.PublishedDate).Select(i =>
            {
                var newsItem = new NewsItemViewModel();
                newsItem.Initialize(i);
                return newsItem;
            }));

            _newsService.Start(News.FirstOrDefault()?.PublishedDate ?? DateTime.Now);
        }
    }
}
