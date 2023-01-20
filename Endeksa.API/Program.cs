using Autofac;
using Autofac.Extensions.DependencyInjection;
using Endeksa.API.Filters;
using Endeksa.Core.Utilities.Cache;
using Endeksa.Core.Utilities.Cache.RedisCache;
using Endeksa.Repository;
using Endeksa.Service.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.API.Modules;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;

});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddStackExchangeRedisCache(options => {
//    options.Configuration = "localhost:6379";
//});
//containerBuilder.Register<IRedisClientsManager>(c =>
//            new RedisManagerPool("localhost:6379"));
//containerBuilder.Register(c => c.Resolve<IRedisClientsManager>().GetCacheClient());

builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<TkgmDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(TkgmDbContext))?.GetName().Name);
    });
});

builder.Services.AddScoped<ICacheService, RedisCacheService>();

builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
