﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DigIn.API.Startup))]

namespace DigIn.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }
    }
}
