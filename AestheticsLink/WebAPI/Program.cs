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
using ORScheduleService;
using OperateService;
using OrderService;
using RechargeService;
using MedcineService;
using CustomerMessageService;
using SurgeryProjectService;
using Microsoft.Extensions.DependencyInjection;  // ����ע�����
using Microsoft.Extensions.Hosting;  // �����������
using QueryAllUsersService.QueryAllServers;
using HosAndDepService;
using ProjectChange;
using RebookOperateService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// ��־
builder.Logging.AddLog4Net("LogFile/log4net.Config");
// ע��LogAndReg����
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IServerService, ServerService>();
// ע��ǩ������
builder.Services.AddTransient<ISigninService, SigninService>();
// ע��Ա����Ϣ�������
builder.Services.AddScoped<IServerInfoService, ServerInfoService>();
// ע���ѯ���пͻ�����
builder.Services.AddScoped<IQueryAllCustomersService, QueryAllCustomersService>();
// ע���������Ű�����
builder.Services.AddScoped<IO_RScheduleService, O_RScheduleService>();
// ע���µ�����
builder.Services.AddTransient<IOrderService, OrdersService>();
// ע���ֵ����
builder.Services.AddTransient<IRechargeService, RechargeServices>();
// ���Quartz.NET����
builder.Services.AddQuartzServices();
// ���ҩƷ��ѯ
builder.Services.AddScoped<IMedicineService, MedicineService>();
// ע�� MedicineService ����
builder.Services.AddScoped<IMedicineService, MedicineService>();
// �Զ���������ҩƷ
builder.Services.AddHostedService<ExpiredMedicineCheckerService>();
// ��ѯ�˿���Ϣ���Ż�ȯ������
builder.Services.AddScoped<ICustomerMessageService, CustomerMessageService.CustomerMessageService>();
// ������Ϣ��ѯ
builder.Services.AddScoped<ISurgeryProjectService, SurgeryProjectService.SurgeryProjectService>();
// ע���ѯ����Ա������
builder.Services.AddScoped<IQueryAllServersService, QueryAllServersService>();
// ע���ѯ���з�Ժ�Ͳ��ŷ���
builder.Services.AddScoped<IHosAndDepInfoService, HosAndDepInfoService>();
//ע����Ŀ�޸ķ���
builder.Services.AddTransient<IProjectService, ProjectService>();
//ע���Ƴ���������
builder.Services.AddTransient<IRebookService, RebookService>();


//��ӿ������
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin() // �����ǰ������
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

// ��������ܵ�
app.UseRouting();

//ʹ�ÿ������
app.UseCors("AllowAll");

app.UseAuthentication(); // ��Ȩ��֤�м��
app.UseAuthorization(); // ��Ȩ�м��

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