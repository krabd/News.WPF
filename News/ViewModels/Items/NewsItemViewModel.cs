using System;
using News.CoreModule.ViewModels;
using News.Domain.Models;

namespace News.ViewModels.Items
{
    public class NewsItemViewModel : ViewModelBase
    {
        private string _author;
        private string _title;
        private string _description;
        private DateTime _publishedDate;

        public string Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public DateTime PublishedDate
        {
            get => _publishedDate;
            set => SetProperty(ref _publishedDate, value);
        }

        public void Initialize(NewsModel news)
        {
            Author = news.Author;
            Title = news.Title;
            Description = news.Description;
            PublishedDate = news.PublishedDate;
        }
    }
}
