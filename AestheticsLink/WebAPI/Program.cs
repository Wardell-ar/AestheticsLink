using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerSigninService.Signin;
using System.Text;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using UpdateAt0amService.UpdateAt0am;
using LogRegService;
//using WebAPI.JWTService;
using ServerInformation;
using QueryAllUsersService.QueryAllCustomers;

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
//注册签到服务
builder.Services.AddTransient<ISigninService, SigninService>();
//注册每日更新数据服务
builder.Services.AddSingleton<UpdateSigninService>();//0am更新服务
//注册员工信息请求服务
builder.Services.AddScoped<IServerInfoService, ServerInfoService>();
//注册查询所有客户服务
builder.Services.AddScoped<IQueryAllCustomersService, QueryAllCustomersService>();


//添加跨域策略
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin() // 允许的前端域名
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});


/*
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Secret))

        };
    });
}*/

// 添加Quartz.NET服务
builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddSingleton<UpdateSigninService>();
builder.Services.AddSingleton(new JobSchedule(
    jobType: typeof(UpdateSigninService),
    cronExpression: "0 0 0 * * ?")); // 每天凌晨12点

var app = builder.Build();

// 配置Quartz.NET
var scheduler = await app.Services.GetRequiredService<ISchedulerFactory>().GetScheduler();
scheduler.JobFactory = app.Services.GetRequiredService<IJobFactory>();
var jobSchedules = app.Services.GetServices<JobSchedule>();
foreach (var jobSchedule in jobSchedules)
{
    var job = CreateJob(jobSchedule);
    var trigger = CreateTrigger(jobSchedule);

    await scheduler.ScheduleJob(job, trigger);
}

await scheduler.Start();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

// 配置请求管道
app.UseRouting();

//使用跨域策略
app.UseCors("AllowAll");

app.UseAuthentication(); // 授权验证中间件
app.UseAuthorization(); // 授权中间件

app.MapControllers();

app.Run();

static IJobDetail CreateJob(JobSchedule schedule)
{
    var jobType = schedule.JobType;
    return JobBuilder
        .Create(jobType)
        .WithIdentity(jobType.FullName)
        .WithDescription(jobType.Name)
        .Build();
}

static ITrigger CreateTrigger(JobSchedule schedule)
{
    return TriggerBuilder
        .Create()
        .WithIdentity($"{schedule.JobType.FullName}.trigger")
        .WithCronSchedule(schedule.CronExpression)
        .WithDescription(schedule.CronExpression)
        .Build();
}

public class SingletonJobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public SingletonJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
    }

    public void ReturnJob(IJob job) { }
}

public class JobSchedule
{
    public JobSchedule(Type jobType, string cronExpression)
    {
        JobType = jobType;
        CronExpression = cronExpression;
    }

    public Type JobType { get; }
    public string CronExpression { get; }
}