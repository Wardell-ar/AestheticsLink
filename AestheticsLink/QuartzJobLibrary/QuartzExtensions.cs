using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Reflection.Metadata;
using UpdateAt0amService;
using UpdateAt0amService.UpdateAt0am;
using Microsoft.Extensions.DependencyInjection;

namespace QuartzJobLibrary
{
    public static class QuartzExtensions
    {
        public static void AddQuartzServices(this IServiceCollection services)
        {
            // 添加Quartz.NET服务
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            //注册每日签到更新服务
            services.AddSingleton<UpdateSigninService>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(UpdateSigninService),
                cronExpression: "0 0/1 * * * ?")); // 每天凌晨12点

            //注册每月绩效更新服务
            services.AddSingleton<MonthlyPerformanceService>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(MonthlyPerformanceService),
                cronExpression: "0 0 1 1 * ?")); // 每月的1号凌晨1点

            // 注册每月工资更新服务
            services.AddSingleton<MonthlyTakeHomePayService>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(MonthlyTakeHomePayService),
                cronExpression: "0 0 12 1 * ?")); // 每月的1号中午12点

            // 注册每月优惠券更新服务
            services.AddSingleton<MonthlyCouponService>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(MonthlyCouponService),
                cronExpression: "0 5 0 1 * ?")); // 每月的1号凌晨12点5分
        }

        public static async Task UseQuartzServices(this IServiceProvider serviceProvider)
        {
            var scheduler = await serviceProvider.GetRequiredService<ISchedulerFactory>().GetScheduler();
            scheduler.JobFactory = serviceProvider.GetRequiredService<IJobFactory>();
            var jobSchedules = serviceProvider.GetServices<JobSchedule>();
            foreach (var jobSchedule in jobSchedules)
            {
                var job = CreateJob(jobSchedule);
                var trigger = CreateTrigger(jobSchedule);

                await scheduler.ScheduleJob(job, trigger);
            }

            await scheduler.Start();
        }

        private static IJobDetail CreateJob(JobSchedule schedule)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
                .Build();
        }

        private static ITrigger CreateTrigger(JobSchedule schedule)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.JobType.FullName}.trigger")
                .WithCronSchedule(schedule.CronExpression)
                .WithDescription(schedule.CronExpression)
                .Build();
        }
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
}