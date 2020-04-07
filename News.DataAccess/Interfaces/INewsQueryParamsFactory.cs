using System;

namespace News.DataAccess.Interfaces
{
    public interface INewsQueryParamsFactory
    {
        string Create();

        string Create(int page);

        string Create(int page, DateTime from);

        string Create(int page, DateTime from, DateTime to);
    }
}
