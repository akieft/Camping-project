using CampSiteC3.Models;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: OwinStartup(typeof(CampSiteC3.Startup))]

namespace CampSiteC3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }

    public static class Dumper
    {
        public static void Dump(this object obj)
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings() { 
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
        }
    }
}
