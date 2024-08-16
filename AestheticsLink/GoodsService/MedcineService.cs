using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebCommon.Database;
using WebModel.Entity;
using MedcineService.Dto;
using Microsoft.Extensions.Hosting;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;


namespace MedcineService
{
    public interface IMedicineService
    {
        Task<int> CheckStorageAndReplenish(string HOS_ID, string G_ID);
        Task DiscardExpiredMedicines();
    }
    public class ExpiredMedicineCheckerService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ExpiredMedicineCheckerService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var medicineService = scope.ServiceProvider.GetRequiredService<IMedicineService>();

                    // 调用丢弃过期药品的方法
                    await medicineService.DiscardExpiredMedicines();
                }

                // 等待24小时后再次执行任务
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }


    public class MedicineService : IMedicineService
    {
        public async Task<int> CheckStorageAndReplenish(string hosId, string gId)
        {

            var inventory = await DbContext.db.Queryable<INVENTORY>()
                                               .Where(i => i.HOS_ID == hosId && i.G_ID == gId)
                                               .FirstAsync();

            if (inventory != null)
            {
                // 如果库存小于10，自动补货
                if (inventory.STORAGE < 10)
                {
                    var good = await DbContext.db.Queryable<GOODS>()
                                                  .Where(g => g.G_ID == gId)
                                                  .FirstAsync();

                    if (good != null)
                    {
                        var replenishAmount = 10 - inventory.STORAGE;
                        inventory.STORAGE += replenishAmount;
                        var replenishCost = good.PRICE * replenishAmount;

                        await DbContext.db.Updateable(inventory).ExecuteCommandAsync();
                        await RecordHospitalOutcome(hosId, replenishCost);
                    }
                }

                return inventory.STORAGE;
            }

            return 0; // 若库存不存在，返回 0
        }

        public async Task DiscardExpiredMedicines()
        {
            var today = DateTime.Today;
            var expiredInventories = await DbContext.db.Queryable<INVENTORY>()
                                                       .Where(i => i.SELL_BY_DATE < today)
                                                       .ToListAsync();

            foreach (var inventory in expiredInventories)
            {
                inventory.STORAGE = 0;
                await DbContext.db.Updateable(inventory).ExecuteCommandAsync();
            }
        }

        private async Task RecordHospitalOutcome(string hosId, decimal amount)
        {
            var currentMonth = DateTime.Now.ToString("yyyy-MM");
            var bill = await DbContext.db.Queryable<HOSPITALBILL>()
                                          .Where(b => b.HOS_ID == hosId && b.FOUND_DATE.ToString("yyyy-MM") == currentMonth)
                                          .FirstAsync();

            if (bill != null)
            {
                bill.OUTCOME += amount;
                await DbContext.db.Updateable(bill).ExecuteCommandAsync();
            }
            else
            {
                bill = new HOSPITALBILL
                {
                    HOS_ID = hosId,
                    FOUND_DATE = DateTime.Now,
                    OUTCOME = amount,
                    INCOME = 0 // 如果需要初始化收入
                };
                await DbContext.db.Insertable(bill).ExecuteCommandAsync();
            }
        }
    }
}
