using Microsoft.AspNetCore.Builder;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace HomeworkMVCPageStatistics
{
    public class PathFinder
    {
        private readonly RequestDelegate _requestDelegate;

        public PathFinder(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate; 
        }

        public Task Invoke(HttpContext context)
        {
            using (FileStream fstream = new FileStream("path.txt", FileMode.OpenOrCreate))
            {
                //string url = HttpContext.Current.Request.Url.AbsoluteUri;
                // http://localhost:1302/TESTERS/Default6.aspx

                //string path = HttpContext.Current.Request.Url.AbsolutePath;
                // /TESTERS/Default6.aspx

                //string host = HttpContext.Current.Request.Url.Host;
                // localhost

                string path = Path.Combine(context.Request.Scheme, context.Request.Protocol, context.Request.Path);

                byte[] buffer = Encoding.Default.GetBytes(path);

                fstream.Write(buffer);
            }

            return _requestDelegate.Invoke(context);    
        }
    }

    public static class PathFinderExtension
    {
        public static IApplicationBuilder mw(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PathFinder>();
        }
    }
}
