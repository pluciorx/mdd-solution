using mddPostFetcher.Interfaces;
using System.Diagnostics;

namespace mddPostFetcher
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, .mdd !");
            var commentURL = "https://jsonplaceholder.typicode.com/comments";
            var postURL = "https://jsonplaceholder.typicode.com/posts";
            
            //for potential DI usage I relay on the interface
            IPostFetcher postFetcher = new URLPostDataFetcher(commentURL, postURL);

            var sut = await postFetcher.GetPostWithCommentsAsync();
            
            Debug.Assert(sut != null);
            Debug.Assert(sut.Count() == 100);
            
            
            var sut2 = await postFetcher.GetPostWithCommentsLinqAsync();

            Debug.Assert(sut2 != null);
            Debug.Assert(sut2.Count() == 100);


        }
    }
}
