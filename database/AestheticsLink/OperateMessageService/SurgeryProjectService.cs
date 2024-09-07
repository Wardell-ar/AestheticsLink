using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SqlSugar;
using SurgeryProjectService.Dto;
using WebModel.Entity;
using WebCommon.Database;

namespace SurgeryProjectService
{
    public class SurgeryProjectService : ISurgeryProjectService
    {
        public async Task<List<SurgeryProjectDto>> GetSurgeryProjectDetailsAsync(string cusId)
        {
            // 通过 CUS_ID 查询所有 BILL_ID
            var bills = await DbContext.db.Queryable<BILL>()
                                          .Where(b => b.CUS_ID == cusId)
                                          .ToListAsync();

            if (bills == null || bills.Count == 0) return null;

            var surgeryProjects = new List<SurgeryProjectDto>();

            foreach (var bill in bills)
            {
                // 通过 BILL_ID 查询所有相关的 PROJ_ID
                var operations = await DbContext.db.Queryable<OPERATE>()
                                                   .Where(o => o.BILL_ID == bill.BILL_ID)
                                                   .ToListAsync();

                foreach (var operation in operations)
                {
                    // 查询项目详情
                    var project = await DbContext.db.Queryable<PROJECT>()
                                                    .Where(p => p.PROJ_ID == operation.PROJ_ID)
                                                    .FirstAsync();

                    if (project == null) continue;

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
                    .Where(pg => pg.PROJ_ID == project.PROJ_ID)
                    .Select((pg, g) => new { g.NAME, g.PRICE })
                    .FirstAsync();

                    // 查询医院名字
                    var hospital = await DbContext.db.Queryable<HOSPITAL>()
                                                     .Where(h => h.HOS_ID == bill.HOS_ID)
                                                     .Select(h => h.NAME)
                                                     .FirstAsync();

                    // 计算手术持续时间
                    TimeSpan? duration = null;
                    if (operationTime?.START_TIME != null && operationTime?.END_TIME != null)
                    {
                        duration = operationTime.END_TIME - operationTime.START_TIME;
                    }

                    // 构建 DTO 并加入列表
                    var surgeryProjectDto = new SurgeryProjectDto
                    {
                        hospital = hospital,
                        name = project.NAME,
                        //ServerName = server,
                        //GoodsName = projGoods?.NAME,
                        //GoodsPrice = projGoods?.PRICE ?? 0,
                        year = operationTime.DAY.Year.ToString(),
                        month = operationTime.DAY.Month.ToString(),
                        day = operationTime.DAY.Day.ToString(),
                        startTime = operationTime?.START_TIME.ToString("HH:mm:ss"),
                        endTime = operationTime?.END_TIME.ToString("HH:mm:ss"),
                        duration = duration?.ToString(@"hh\:mm\:ss"),
                        room = operationTime?.ROOM_ID,
                        status = operation.EXE_STATE,
                        billid = bill.BILL_ID
                    };

                    surgeryProjects.Add(surgeryProjectDto);
                }
            }

            return surgeryProjects;
        }

        public async Task<List<ProjectDto>> GetProjectInfoAsync(ProjectInfoRequest dto)
        {
            var query = DbContext.db.Queryable<PROJECT>();

            if (dto.proj_id != "null" && dto.name == "null")
            {
                query = query.Where(p => p.PROJ_ID == dto.proj_id);
            }
            else if (dto.name != "null" && dto.proj_id == "null")
            {
                query = query.Where(p => p.NAME == dto.name);
            }
            else if (dto.name != "null" && dto.proj_id != "null")
            {
                query = query.Where(p => p.PROJ_ID == dto.proj_id && p.NAME == dto.name);
            }
            else if (dto.proj_id == "null" && dto.name == "null")
            {
                var allProjects = await query.Select(p => new ProjectDto
                {
                    proj_id = p.PROJ_ID,
                    name = p.NAME,
                    price = p.PRICE,
                    found_date = p.FOUND_DATE.ToString("yyyy-MM-dd")
                }).ToListAsync();

                return allProjects.Any() ? allProjects : null;  // 修改这里
            }

            var project = await query.FirstAsync();
            if (project == null)
            {
                return null;  // 修改这里
            }

            return new List<ProjectDto>
    {
        new ProjectDto
        {
            proj_id = project.PROJ_ID,
            name = project.NAME,
            price = project.PRICE,
            found_date = project.FOUND_DATE.ToString("yyyy-MM-dd")
        }
    };
        }

    }
}
