﻿using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using MiniBlog;
using MiniBlog.Model;
using MiniBlog.Stores;
using Xunit;

namespace MiniBlogTest
{
    public class TestBase : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public TestBase(CustomWebApplicationFactory<Startup> factory)
        {
            this.Factory = factory;
        }

        protected CustomWebApplicationFactory<Startup> Factory { get; }

        protected HttpClient GetClient(ArticleStore articleStore = null, UserStore userStore = null)
        {
            return Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(
                    services =>
                    {
                        services.AddSingleton<ArticleStore>(provider =>
                        {
                            return articleStore;
                        });
                        services.AddSingleton<UserStore>(provider =>
                        {
                            return userStore;
                        });
                    });
            }).CreateDefaultClient();
        }
    }
}
