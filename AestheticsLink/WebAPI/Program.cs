using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.JWTService;
using LogRegService;
<<<<<<< Updated upstream
=======
//using WebAPI.JWTService;
using ServerInformation;
using QueryAllUsersService.QueryAllCustomers;
using OrderService;
using RechargeService;
using OperateService;
using FinancialService;
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
=======
//注册签到服务
builder.Services.AddTransient<ISigninService, SigninService>();
//注册每日更新数据服务
builder.Services.AddSingleton<UpdateSigninService>();//0am更新服务
//注册员工信息请求服务
builder.Services.AddScoped<IServerInfoService, ServerInfoService>();
//注册查询所有客户服务
builder.Services.AddScoped<IQueryAllCustomersService, QueryAllCustomersService>();
//注册下单服务
builder.Services.AddTransient<IOrderService, OrdersService>();
//注册充值服务
builder.Services.AddTransient<IRechargeService, RechargeServices>();
//注册手术触发器服务
builder.Services.AddSingleton<OperateExcuteService>();

>>>>>>> Stashed changes
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
           // IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Secret))

        };
    });
<<<<<<< Updated upstream
=======
}*/

// 添加Quartz.NET服务
builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

builder.Services.AddSingleton<UpdateSigninService>();
builder.Services.AddSingleton<OperateExcuteService>();
builder.Services.AddSingleton<FinancialFound>();
builder.Services.AddSingleton<FinanBalance>();


builder.Services.AddSingleton(new JobSchedule(
    jobType: typeof(UpdateSigninService),
    cronExpression: "0 0 0 * * ?")); // 每天凌晨12点
//添加手术的调度服务
builder.Services.AddSingleton(new JobSchedule(
    jobType: typeof(OperateExcuteService),
    cronExpression: "0 0 8,12,14,16 * * ?")); // 每天8点12点14点16点0 0 8,12,14,16 * * ?
builder.Services.AddSingleton(new JobSchedule(
    jobType: typeof(FinancialFound),
    cronExpression: "0点0 0 0 1 * ?")); // 每月第一天0点0 0 0 1 * ?
builder.Services.AddSingleton(new JobSchedule(
    jobType: typeof(FinanBalance),
    cronExpression: "23点59分0 59 23 L * ?")); // 每月最后一天23点59分0 59 23 L * ?

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
>>>>>>> Stashed changes
}

// 添加授权服务
//builder.Services.AddAuthorization();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthentication();

app.UseAuthorization();

//使用跨域策略
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
