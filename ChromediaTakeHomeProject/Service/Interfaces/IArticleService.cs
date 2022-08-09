using ChromediaTakeHomeProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChromediaTakeHomeProject.Service.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleResponse> GetArticles(int page);

        Task<List<ArticleResponse>> GetAllArticles();

        Task<List<string>> GetTopArticleTitles(int count);
    }
}
