using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/unityweb" });
});

var app = builder.Build();
app.UseResponseCompression();

#region StaticFileMiddleware
// Static File Middleware

var customContentTypeProvider = new FileExtensionContentTypeProvider();
customContentTypeProvider.Mappings.Add(".unityweb", "application/unityweb");

var staticFileOptions = new StaticFileOptions()
{
    ContentTypeProvider = customContentTypeProvider
};

app.UseStaticFiles(staticFileOptions);

// End Of Static File Middleware
#endregion

#region WithoutStaticFileMiddleware
// Without Static File Middleware

/*

app.Map("/unity", fileApp =>
{
    fileApp.Run(context =>
    {
        context.Response.ContentType = "application/unityweb";
        Console.WriteLine("sending unity custom");
        return context.Response.WriteAsync("doesntwork");
    });
});

 */

#endregion

app.Run();


