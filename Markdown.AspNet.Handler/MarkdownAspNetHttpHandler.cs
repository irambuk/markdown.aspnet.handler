using System.IO;
using System.Web;

namespace Markdown.AspNet.Handler
{
    public class MarkdownAspNetHttpHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            var requestedFile = context.Request.PhysicalPath;

            if (string.IsNullOrEmpty(requestedFile) || !File.Exists(requestedFile))
            {                
                context.Response.StatusCode = 404;
                return;
            }

            var fileContents = File.ReadAllText(requestedFile);

            var markdown = new HeyRed.MarkdownSharp.Markdown();
            var content = markdown.Transform(fileContents);

            context.Response.Write(content);
        }
    }
}
