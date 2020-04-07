using System;

namespace News.DataAccess.Interfaces
{
    public interface INewsQueryParamsFactory
    {
        string Create(int pageSize);

        string Create(int page, int pageSize);

        string Create(int page, int pageSize, DateTime from);

        string Create(int page, int pageSize, DateTime from, DateTime to);
    }
}
