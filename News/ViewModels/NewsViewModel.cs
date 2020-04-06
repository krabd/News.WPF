using System.Threading;
using System.Threading.Tasks;
using News.CoreModule.ViewModels;
using News.DataAccess.Interfaces;

namespace News.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        private readonly INewsRepository _newsRepository;

        public NewsViewModel(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task InitializeAsync(CancellationToken token)
        {
            var news = await _newsRepository.GetNewsAsync(token);
            token.ThrowIfCancellationRequested();

        }
    }
}
