using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using News.CoreModule.ViewModels;
using News.DataAccess.Interfaces;
using News.Domain.Models;
using News.Interfaces;
using News.Utils;
using News.ViewModels.Items;

namespace News.ViewModels
{
    public class NewsViewModel : ViewModelBase, INotifyAboutNews
    {
        private readonly INewsRepository _newsRepository;
        private readonly IUpdateNewsService _newsService;

        public IEnumerable<NewsItemViewModel> OrderedNews => News.OrderByDescending(i => i.PublishedDate);

        public ObservableCollection<NewsItemViewModel> News { get; } = new ObservableCollection<NewsItemViewModel>();

        public event EventHandler<IReadOnlyCollection<NewsModel>> NewsAdded;

        public NewsViewModel(INewsRepository newsRepository, IUpdateNewsService newsService)
        {
            _newsRepository = newsRepository;
            _newsService = newsService;

            News.CollectionChanged += OnNewsCollectionChanged;
            _newsService.NewsAdded += OnNewsAdded;
        }

        public async Task InitializeAsync(CancellationToken token)
        {
            _newsService.Stop();

            var news = await _newsRepository.GetNewsAsync(token);
            token.ThrowIfCancellationRequested();

            News.Clear();
            AddNews(news);

            _newsService.Start(News.FirstOrDefault()?.PublishedDate ?? DateTime.Now);
        }

        private void OnNewsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(OrderedNews));
        }

        private void OnNewsAdded(object sender, IReadOnlyCollection<NewsModel> e)
        {
            NewsAdded?.Invoke(this, e);

            AddNews(e);
        }

        private void AddNews(IReadOnlyCollection<NewsModel> e)
        {
            Tools.DispatchedInvoke(() =>
            {
                News.AddRange(e.OrderByDescending(i => i.PublishedDate).Select(i =>
                {
                    var newsItem = new NewsItemViewModel();
                    newsItem.Initialize(i);
                    return newsItem;
                }));
            });
        }
    }
}
