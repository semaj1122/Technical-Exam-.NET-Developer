using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Controllers;
using technical_exam.Server.DbAccess;
using technical_exam.Server.Handlers;
using technical_exam.Server.Interfaces;
using technical_exam.Server.Repositories;
using technical_exam.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// ===============================================================
//  -->> BEGIN: Using Basic Authentication
// ===============================================================

builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API",
        Version = "1.0.0.0",
        Description = "API"
    });
    o.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Basic Authorization header using the Bearer scheme.",
        Scheme = "basic",
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
          {
              new OpenApiSecurityScheme
                  {
                      Reference = new OpenApiReference
                          {
                              Type = ReferenceType.SecurityScheme,
                              Id = "basic"
                          }
                  },
              new string[] {}
        }
      });

    o.TagActionsBy(api =>
    {
        if (api.GroupName != null)
        {
            return new[] { api.GroupName };
        }

        var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
        if (controllerActionDescriptor != null)
        {
            return new[] { controllerActionDescriptor.ControllerName };
        }

        throw new InvalidOperationException("Unable to determine tag for endpoint.");
    });
    o.DocInclusionPredicate((name, api) => true);
});

// ===============================================================
//  -->> END: Using Basic Authentication
// ===============================================================
//FOR SWAGGER CONFIG SECURITY
builder.Services.AddAuthentication("Basic");
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuthentication("BasicAuthentication")
      .AddScheme<AuthenticationSchemeOptions,
                 BasicAuthenticationHandler>("BasicAuthentication", null);

//  Dependency Injection :: Repository
builder.Services.AddScoped<IApiServiceRepository, ApiService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ISQLDataAccess, SQLDataAccess>();

var app = builder.Build();

app.UseSwagger(c =>
{
    c.PreSerializeFilters.Add((swagger, httpReq) =>
    {
        swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
        //swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"https://i-cash.app:451" } };
    });
});
app.UseForwardedHeaders();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI(o =>
{
    o.DefaultModelsExpandDepth(-1); 
    o.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
});
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
var options = new DefaultFilesOptions();
options.DefaultFileNames.Clear();
options.DefaultFileNames.Add("index.html");
app.UseDefaultFiles(options);
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
});

app.Run();