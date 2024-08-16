using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using SurgeryProjectService.Dto;
using WebModel.Entity;
using WebCommon.Database;

namespace SurgeryProjectService
{
    public class SurgeryProjectService : ISurgeryProjectService
    {

        public async Task<SurgeryProjectDto> GetSurgeryProjectDetailsAsync(string projId)
        {
            // 查询项目
            var project = await DbContext.db.Queryable<PROJECT>()
                                          .Where(p => p.PROJ_ID == projId)
                                          .FirstAsync();

            if (project == null) return null;

            // 查询对应的手术信息
            var operation = await DbContext.db.Queryable<OPERATE>()
                                            .Where(o => o.PROJ_ID == projId)
                                            .FirstAsync();

            // 查询手术时间
            var operationTime = await DbContext.db.Queryable<OPERATE_TIME>()
                                                .Where(ot => ot.OP_TIME_ID == operation.OP_TIME_ID)
                                                .FirstAsync();

            // 查询服务者名字
            var server = await DbContext.db.Queryable<SERVER>()
                                         .Where(s => s.SER_ID == operation.SER_ID)
                                         .Select(s => s.NAME)
                                         .FirstAsync();

            // 查询使用的物品信息
            var projGoods = await DbContext.db.Queryable<PROJ_GOOD, GOODS>((pg, g) => new object[]
            {
                JoinType.Inner, pg.G_ID == g.G_ID
            })
            .Where(pg => pg.PROJ_ID == projId)
            .Select((pg, g) => new { g.NAME, g.PRICE })
            .FirstAsync();


            // 构建 DTO
            var result = new SurgeryProjectDto
            {
                ProjectName = project.NAME,
                ServerName = server,
                GoodsName = projGoods?.NAME,
                GoodsPrice = projGoods?.PRICE ?? 0,
                FoundDate = project.FOUND_DATE,
                OperationDay = operationTime?.DAY ?? DateTime.MinValue,
                StartTime = operationTime?.START_TIME,
                EndTime = operationTime?.END_TIME,
                RoomId = operationTime?.ROOM_ID
            };

            return result;
        }
    }
}

