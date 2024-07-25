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
//��־
builder.Logging.AddLog4Net("LogFile/log4net.Config");
//ע��LogAndReg����
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IServerService, ServerService>();
<<<<<<< Updated upstream
=======
//ע��ǩ������
builder.Services.AddTransient<ISigninService, SigninService>();
//ע��ÿ�ո������ݷ���
builder.Services.AddSingleton<UpdateSigninService>();//0am���·���
//ע��Ա����Ϣ�������
builder.Services.AddScoped<IServerInfoService, ServerInfoService>();
//ע���ѯ���пͻ�����
builder.Services.AddScoped<IQueryAllCustomersService, QueryAllCustomersService>();
//ע���µ�����
builder.Services.AddTransient<IOrderService, OrdersService>();
//ע���ֵ����
builder.Services.AddTransient<IRechargeService, RechargeServices>();
//ע����������������
builder.Services.AddSingleton<OperateExcuteService>();

>>>>>>> Stashed changes
//��ӿ������

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin() // �����ǰ������
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
//ע��JWT����
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));
builder.Services.AddTransient<IJWTService, JWTService>();

//JWTУ��
{
    JWTTokenOptions tokenOptions = new JWTTokenOptions();
    builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            //�Ƿ���֤����
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

// ���Quartz.NET����
builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

builder.Services.AddSingleton<UpdateSigninService>();
builder.Services.AddSingleton<OperateExcuteService>();
builder.Services.AddSingleton<FinancialFound>();
builder.Services.AddSingleton<FinanBalance>();


builder.Services.AddSingleton(new JobSchedule(
    jobType: typeof(UpdateSigninService),
    cronExpression: "0 0 0 * * ?")); // ÿ���賿12��
//��������ĵ��ȷ���
builder.Services.AddSingleton(new JobSchedule(
    jobType: typeof(OperateExcuteService),
    cronExpression: "0 0 8,12,14,16 * * ?")); // ÿ��8��12��14��16��0 0 8,12,14,16 * * ?
builder.Services.AddSingleton(new JobSchedule(
    jobType: typeof(FinancialFound),
    cronExpression: "0��0 0 0 1 * ?")); // ÿ�µ�һ��0��0 0 0 1 * ?
builder.Services.AddSingleton(new JobSchedule(
    jobType: typeof(FinanBalance),
    cronExpression: "23��59��0 59 23 L * ?")); // ÿ�����һ��23��59��0 59 23 L * ?

var app = builder.Build();

// ����Quartz.NET
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

// �����Ȩ����
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

//ʹ�ÿ������
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
