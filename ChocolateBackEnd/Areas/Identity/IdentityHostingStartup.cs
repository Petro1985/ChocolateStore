﻿using ChocolateBackEnd.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace ChocolateBackEnd.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
