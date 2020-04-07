using System.Collections.Generic;
using News.ViewModels.Items;

namespace News.Comparers
{
    public class NewsComparer : IEqualityComparer<NewsItemViewModel>
    {
        public bool Equals(NewsItemViewModel x, NewsItemViewModel y)
        {
            if (x == null && y == null)
                return true;

            if (y == null || x == null)
                return false;

            if (string.Equals(y.Url, x.Url))
                return true;

            return false;
        }

        public int GetHashCode(NewsItemViewModel obj)
        {
            return obj.Url.GetHashCode() ^ obj.Title.GetHashCode();
        }
    }
}