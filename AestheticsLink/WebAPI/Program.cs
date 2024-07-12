using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.JWTService;
using LogRegService;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//日志
builder.Logging.AddLog4Net("LogFile/log4net.Config");
//注册LogAndReg服务
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IServerService, ServerService>();
//添加跨域策略

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin() // 允许的前端域名
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
//注册JWT服务
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));
builder.Services.AddTransient<IJWTService, JWTService>();

//JWT校验
{
    JWTTokenOptions tokenOptions = new JWTTokenOptions();
    builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            //是否验证属性
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = tokenOptions.Audience,
            ValidIssuer = tokenOptions.Issuer,
        };
    });
}

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

//使用跨域策略
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
