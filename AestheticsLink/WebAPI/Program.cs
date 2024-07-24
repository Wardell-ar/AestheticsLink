using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerSigninService.Signin;
using System.Text;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using UpdateAt0amService.UpdateAt0am;
using LogRegService;
using QuartzJobLibrary;
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
//注册员工信息请求服务
builder.Services.AddScoped<IServerInfoService, ServerInfoService>();
//注册查询所有客户服务
builder.Services.AddScoped<IQueryAllCustomersService, QueryAllCustomersService>();
// 添加Quartz.NET服务
builder.Services.AddQuartzServices();
//添加跨域策略
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin() // 允许的前端域名
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});


var app = builder.Build();

var serviceProvider = app.Services;
await serviceProvider.UseQuartzServices();
/*await scheduler.Start();*/


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

/*static IJobDetail CreateJob(JobSchedule schedule)
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
}*/