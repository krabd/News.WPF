using System;
using System.Collections.Generic;

namespace News.DataAccess.Interfaces
{
    public interface INewsQueryParamsFactory
    {
        List<string> Create();

        List<string> Create(int page);

        List<string> Create(int page, DateTime from);

        List<string> Create(int page, DateTime from, DateTime to);
    }
}
