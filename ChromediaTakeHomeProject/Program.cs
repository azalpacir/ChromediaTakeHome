using ChromediaTakeHomeProject.Service;
using ChromediaTakeHomeProject.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ChromediaTakeHomeProject
{
    class Program
    {
        private static ServiceProvider _serviceProvider;

        static async Task Main(string[] args)
        {
            _serviceProvider = new ServiceCollection()
                .AddHttpClient()
                .AddScoped<IArticleService, ArticleService>()
                .BuildServiceProvider();


            var count = 2;
            var articles = await TopArticles(count);
            Console.WriteLine(String.Join(Environment.NewLine, articles));

            /*
             * Use this instead for testing multiple times.
             */

            //while (true)
            //{
            //    Console.WriteLine("Enter a number:");
            //    var input = Console.ReadLine();

            //    if(int.TryParse(input, out var count))
            //    {
            //        var articles = await TopArticles(count);
            //        Console.WriteLine("=====");
            //        Console.WriteLine(String.Join(Environment.NewLine, articles));
            //        Console.WriteLine("=====");
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}
        }

        private static async Task<string[]> TopArticles(int count)
        {
            var articleService = _serviceProvider.GetRequiredService<IArticleService>();
            return (await articleService.GetTopArticleTitles(count)).ToArray();
        }
    }
}
