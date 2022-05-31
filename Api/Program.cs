using Api.Services;
using Microsoft.EntityFrameworkCore;
using SoapCore;
using DbContext = Api.DbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSoapCore();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbContext>(opts => opts.UseSqlite("Data Source=Posts.db"));


builder.Services.AddHttpClient<IContactsService, ContactsService>(client =>
{
    client.BaseAddress = new Uri("http://contacts:5000/");
});

builder.Services.AddScoped<IPostsService, PostsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => {
    endpoints.UseSoapEndpoint<IPostsService>(opt =>
    {
        opt.Path = "/posts.asmx";
        opt.SoapSerializer = SoapSerializer.DataContractSerializer;
    });
});

app.MapControllers();

app.Run();
