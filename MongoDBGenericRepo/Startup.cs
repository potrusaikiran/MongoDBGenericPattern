using System;
using System.Collections.Generic;
using System.Linq;
using Owin;
using Microsoft.Owin.Builder;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(MongoDBGenericRepo.Startup))]

namespace MongoDBGenericRepo
{
    public partial class Startup
    {
        //public void Configuration(AppBuilder app)
        //{
        //    //ConfigureAuth(app);
        //}
    }
}
