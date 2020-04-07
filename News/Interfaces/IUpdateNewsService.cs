using System;
using System.Threading.Tasks;

namespace News.Interfaces
{
    public interface IUpdateNewsService : INotifyAboutNews
    {
        Task Start(DateTime lastDate);

        void Stop();
    }
}
