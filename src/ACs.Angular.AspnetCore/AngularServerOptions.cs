using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ACs.Angular.AspnetCore
{
    public class AngularServerOptions
    {
        public OptionsManager<StaticFileOptions> FileServerOptions { get; set; }

        public PathString EntryPath { get; set; }

        public bool Html5Mode
        {
            get
            {
                return EntryPath.HasValue;
            }
        }

        public AngularServerOptions(OptionsManager<StaticFileOptions> fileServerOptions, string entryPath)
        {
	        FileServerOptions = fileServerOptions;
			EntryPath = new PathString(entryPath);
        }
    }
}
