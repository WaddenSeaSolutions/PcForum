using backend;
using backend.DAL;
using backend.Service;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString,
        dataSourceBuilder => dataSourceBuilder.EnableParameterLogging());
}

if (builder.Environment.IsProduction())
{
    builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString);
}

builder.Services.AddSingleton<ForumDAL>();
builder.Services.AddSingleton<ForumService>();
builder.Services.AddSingleton<FrontpageService>();
builder.Services.AddSingleton<FrontpageDAL>();
builder.Services.AddSingleton<EmailService>();

builder.Services.AddSingleton<TokenService>();
builder.Services.AddSingleton<TokenDal>();

builder.Services.AddSingleton<ThreadService>();
builder.Services.AddSingleton<ThreadDAL>();

builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<UserDal>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});


app.MapControllers();
app.Run();