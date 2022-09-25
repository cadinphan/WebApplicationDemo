using System;
using System.IO;
using System.Threading.Tasks;
using elFinder.NetCore;
using elFinder.NetCore.Drivers.FileSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationDemo.Controllers
{
    [Route("demo-file-system")]
    public class FileSystemController : Controller
    {
        IWebHostEnvironment _env;
        public FileSystemController(IWebHostEnvironment env) => _env = env;

        // Url to client-side to connect
        // demo-file-system/connector
        [Route("connector")]
        public async Task<IActionResult> Connector()
        {
            var connector = GetConnector();
            return await connector.ProcessAsync(Request);
        }

        [Route("thumb/{hash}")]
        public async Task<IActionResult> Thumbs(string hash)
        {
            var connector = GetConnector();
            return await connector.GetThumbnailAsync(HttpContext.Request, HttpContext.Response, hash);
        }

        private Connector GetConnector()
        {
            // The storage root directory is wwwwroot/files
            string pathroot = "C:\\SystemLog\\Logs";

            var driver = new FileSystemDriver();

            string absoluteUrl = UriHelper.BuildAbsolute(Request.Scheme, Request.Host);
            var uri = new Uri(absoluteUrl);

            // .. ... wwww/fileManager
            //string rootDirectory = Path.Combine(_env.WebRootPath, pathroot);
            //string[] rootDirectory = Directory.GetFiles(pathroot);

            string rootDirectory = Path.Combine(pathroot);

            string url = $"{pathroot}";
            string urlthumb = $"{uri.Scheme}://{uri.Authority}/demo-file-system/thumb/";


            var root = new RootVolume(rootDirectory, url, urlthumb)
            {
                //IsReadOnly = !User.IsInRole("Administrators")
                IsReadOnly = false, 
                IsLocked = false, 
                Alias = "SystemLog", 
                MaxUploadSizeInKb = 2048, // Limit imposed to user uploaded file <= 2048 KB
                ThumbnailSize = 100,
            };


            driver.AddRoot(root);

            return new Connector(driver)
            {
                // This allows support for the "onlyMimes" option on the client.
                MimeDetect = MimeDetectOption.Internal
            };
        }
    }
}
