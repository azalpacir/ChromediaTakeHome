using ChromediaTakeHomeProject.Models;
using ChromediaTakeHomeProject.Service.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChromediaTakeHomeProject.Service
{
    public class ArticleService : IArticleService
    {
        private const string ARTICLE_URL = "https://jsonmock.hackerrank.com/api/articles";

        private readonly HttpClient _httpClient;

        public ArticleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ArticleResponse> GetArticles(int page=1)
        {
            var result = await _httpClient.SendAsync(new HttpRequestMessage
            {
                RequestUri = new Uri($"{ARTICLE_URL}?page={page}"),
                Method = HttpMethod.Get
            });

            var resultStr = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ArticleResponse>(resultStr);
        }

        public async Task<List<ArticleResponse>> GetAllArticles()
        {
            var initialQuery = await GetArticles();
            var tasks = new List<Task<ArticleResponse>>();
            for (var start = 2; start <= initialQuery.TotalPages; start++)
            {
                tasks.Add(GetArticles(start));
            }

            var articleResponses = (await Task.WhenAll(tasks)).ToList();
            articleResponses.Add(initialQuery);

            return articleResponses;
        }

        public async Task<List<string>> GetTopArticleTitles(int count)
        {
            var allArticleResponses = await GetAllArticles();
            return allArticleResponses.SelectMany(x => x.Articles)
                                      .OrderByDescending(x => x.CommentCount)
                                      .ThenByDescending(x => x.FinalTitle)
                                      .Take(count)
                                      .Select(x => x.FinalTitle)
                                      .ToList();
        }
    }
}
