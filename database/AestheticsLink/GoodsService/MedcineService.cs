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
using SqlSugar;

namespace MedcineService
{
    public interface IMedicineService
    {
        Task<int> GetStorageByName(string HOS_ID, string NAME);  // 根据药品名称查询库存
        Task<char> AutoReplenishStorage();
        Task DiscardExpiredMedicines();  // 丢弃过期药品
        Task<IEnumerable<MedicineInfoDto>> GetMedicineInfo(CheckStorageDto dto); // 根据传入的字段查询药品信息
    }

    public class MedicineService : IMedicineService
    {
        // 根据药品名称查询库存
        public async Task<int> GetStorageByName(string hosId, string name)
        {
            var inventory = await DbContext.db.Queryable<INVENTORY, GOODS>((i, g) => new object[]
                {
                JoinType.Inner, i.G_ID == g.G_ID
                })
                .Where((i, g) => i.HOS_ID == hosId && g.NAME == name)
                .Select((i, g) => i.STORAGE)
                .FirstAsync();

            return inventory;
        }

        // 自动补货
        public async Task<char> AutoReplenishStorage()
        {
            try
            {
                // 查询库存小于10的药品
                var lowStockInventories = await DbContext.db.Queryable<INVENTORY, GOODS>((i, g) => new object[]
                {
            JoinType.Inner, i.G_ID == g.G_ID
                })
                .Where((i, g) => i.STORAGE < 10)
                .Select((i, g) => new { i, g.PRICE })
                .ToListAsync();

                foreach (var item in lowStockInventories)
                {
                    var inventory = item.i;

                    var replenishAmount = 100;
                    inventory.STORAGE += replenishAmount;
                    var replenishCost = item.PRICE * replenishAmount;

                    await DbContext.db.Updateable(inventory).ExecuteCommandAsync();
                    await RecordHospitalOutcome(inventory.HOS_ID, replenishCost);
                }

                // 如果补货过程完成没有异常，返回 '1'
                return '1';
            }
            catch (Exception ex)
            {
                // 捕获并记录异常
                Console.WriteLine($"Error in AutoReplenishStorage: {ex.Message}");
                // 如果发生异常，返回 '0'
                return '0';
            }
        }

        // 更新过期药品的库存并延长过期日期
        public async Task DiscardExpiredMedicines()
        {
            var today = DateTime.Today;

            // 查询出过期药品的库存和相关信息
            var expiredInventories = await DbContext.db.Queryable<INVENTORY, GOODS>((i, g) => new object[]
            {
        JoinType.Inner, i.G_ID == g.G_ID
            })
            .Where((i, g) => i.SELL_BY_DATE < today)
            .Select((i, g) => new { Inventory = i, Goods = g })
            .ToListAsync();

            foreach (var item in expiredInventories)
            {
                var inventory = item.Inventory;
                var goods = item.Goods;

                // 假设补充的数量为100
                var replenishAmount = 100;
                // 计算补充的成本
                var replenishCost = goods.PRICE * replenishAmount;
                // 延长过期日期三年
                inventory.SELL_BY_DATE = inventory.SELL_BY_DATE.AddYears(3);
                // 更新库存
                inventory.STORAGE = replenishAmount;

                // 更新药品记录
                await DbContext.db.Updateable(inventory).ExecuteCommandAsync();

                // 记录医院支出
                await RecordHospitalOutcome(inventory.HOS_ID, replenishCost);
            }
        }



        // 记录医院支出
        private async Task RecordHospitalOutcome(string hosId, decimal amount)
        {
            var currentMonth = DateTime.Now.ToString("yyyy-MM");
            var bill = await DbContext.db.Queryable<FINANCIAL>()
                                          .Where(b => b.HOS_ID == hosId && b.FINANCE_MONTH.ToString("yyyy-MM") == currentMonth)
                                          .FirstAsync();

            if (bill != null)
            {
                bill.PAYOUT += amount;
                await DbContext.db.Updateable(bill).ExecuteCommandAsync();
            }
            else
            {
                bill = new FINANCIAL
                {
                    HOS_ID = hosId,
                    FINANCE_MONTH = DateTime.Now,
                    PAYOUT = amount,
                    INCOME = 0 // 如果需要初始化收入
                };
                await DbContext.db.Insertable(bill).ExecuteCommandAsync();
            }
        }

        // 根据传入的字段查询药品信息
        // 根据传入的字段查询药品信息
        // 根据传入的字段查询药品信息
        public async Task<IEnumerable<MedicineInfoDto>> GetMedicineInfo(CheckStorageDto dto)
        {
            var query = DbContext.db.Queryable<INVENTORY, GOODS>((i, g) => new object[]
            {
            JoinType.Inner, i.G_ID == g.G_ID
            })
            .WhereIF(dto.hos_id == "null" && dto.name == "null", (i, g) => true)
            .WhereIF(dto.hos_id != "null" && dto.name == "null", (i, g) => i.HOS_ID == dto.hos_id)
            .WhereIF(dto.name != "null" && dto.hos_id == "null", (i, g) => g.NAME == dto.name)
            .WhereIF(dto.hos_id != "null" && dto.name != "null", (i, g) => i.HOS_ID == dto.hos_id && g.NAME == dto.name);

            var result = await query.Select((i, g) => new MedicineInfoDto
            {
                g_id = g.G_ID,
                hos_id = i.HOS_ID,
                name = g.NAME,
                storage = i.STORAGE,
                producer = g.PRODUCER,
                sell_by_date = i.SELL_BY_DATE,
                price = g.PRICE
            }).ToListAsync();

            // 如果查询结果为空列表，返回null
            return result.Any() ? result : null;
        }

    }

    // 创建一个后台服务来定期检查和丢弃过期药品
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
}
