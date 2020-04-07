using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using News.Comparers;
using News.CoreModule.ViewModels;
using News.DataAccess.Interfaces;
using News.Domain.Models;
using News.Interfaces;
using News.Utils;
using News.Utils.Helpers;
using News.ViewModels.Items;
using Prism.Commands;

namespace News.ViewModels
{
    public class NewsViewModel : ViewModelBase, INotifyAboutNews
    {
        private readonly INewsRepository _newsRepository;
        private readonly IUpdateNewsService _newsService;

        private readonly NewsComparer _newsComparer;
        private readonly int _pageSize;

        private int _totalCount;

        public IEnumerable<NewsItemViewModel> OrderedNews => News.OrderByDescending(i => i.PublishedDate);

        public ObservableCollection<NewsItemViewModel> News { get; } = new ObservableCollection<NewsItemViewModel>();

        public event EventHandler<IReadOnlyCollection<NewsModel>> NewsAdded;

        public ICommand LoadNewPageCommand { get; }

        public NewsViewModel(INewsRepository newsRepository, IUpdateNewsService newsService)
        {
            _newsRepository = newsRepository;
            _newsService = newsService;

            LoadNewPageCommand = new DelegateCommand<int?>(OnLoadNewPageAsync);

            News.CollectionChanged += OnNewsCollectionChanged;
            _newsService.NewsAdded += OnNewsAdded;

            _newsComparer = new NewsComparer();
            _pageSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("newsLoadingPageSize"));
        }

        public async Task InitializeAsync(CancellationToken token)
        {
            _newsService.Stop();

            var news = await _newsRepository.GetNewsAsync(token, 1);
            token.ThrowIfCancellationRequested();

            if (news.Value == Status.Fail)
            {
                Debug.WriteLine(news.Message);
            }
            else
            {
                _totalCount = news.Model.TotalCount;

                News.Clear();
                AddNews(news.Model.News);

                _newsService.Start(News.FirstOrDefault()?.PublishedDate ?? DateTime.Now);
            }
        }

        private async void OnLoadNewPageAsync(int? itemsCount)
        {
            try
            {
                var diff = _totalCount - itemsCount;
                if (diff <= 0) return;

                var loadedPageCount = (int)Math.Truncate((decimal)itemsCount / _pageSize);
                var news = await _newsRepository.GetNewsAsync(default, loadedPageCount + 1);

                if (news.Value == Status.Fail)
                {
                    Debug.WriteLine(news.Message);
                }
                else
                {
                    _totalCount = news.Model.TotalCount;

                    AddNews(news.Model.News);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
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
                var unique = e.Select(i =>
                                        {
                                            var newsItem = new NewsItemViewModel();
                                            newsItem.Initialize(i);
                                            return newsItem;
                                        })
                    .Distinct(_newsComparer)
                    .Except(News.ToList(), _newsComparer)
                    .ToList();

                News.AddRange(unique);
            });
        }
    }
}
