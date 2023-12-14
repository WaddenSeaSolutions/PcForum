using System.Threading.RateLimiting;
using backend;
using backend.DAL;
using backend.Service;
using Microsoft.AspNetCore.RateLimiting;
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
builder.Services.AddSingleton<TokenDAL>();

builder.Services.AddSingleton<ThreadService>();
builder.Services.AddSingleton<ThreadDAL>();

builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<UserDal>();

builder.Services.AddSingleton<CommentService>();
builder.Services.AddSingleton<CommentDAL>();
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var fixedPolicy = "fixed";
var commentPolicy = "comment";
var getPolicy = "get";
var loginPolicy = "login";
var registerPolicy = "register";

builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: fixedPolicy, options =>
    {
        options.PermitLimit = 2;
        options.Window = TimeSpan.FromSeconds(30);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 1;
    }));

builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: commentPolicy, options =>
    {
        options.PermitLimit = 5;
        options.Window = TimeSpan.FromSeconds(120);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 1;
    }));

builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: getPolicy, options =>
    {
        options.PermitLimit = 75;
        options.Window = TimeSpan.FromSeconds(40);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 1;
    }));

builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: loginPolicy, options =>
    {
        options.PermitLimit = 5;
        options.Window = TimeSpan.FromSeconds(180);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 1;
    }));
builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: registerPolicy, options =>
    {
        options.PermitLimit = 1;
        options.Window = TimeSpan.FromSeconds(300);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 1;
    }));

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

app.UseRateLimiter();

app.MapControllers();
app.Run();