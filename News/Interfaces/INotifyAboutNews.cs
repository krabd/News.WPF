using System;
using System.Collections.Generic;
using News.Domain.Models;

namespace News.Interfaces
{
    public interface INotifyAboutNews
    {
        event EventHandler<IReadOnlyCollection<NewsModel>> NewsAdded;
    }
}
