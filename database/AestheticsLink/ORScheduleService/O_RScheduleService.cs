using ORScheduleService.Dto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommon.Database;
using WebModel.Entity;

namespace ORScheduleService
{
    public class O_RScheduleService : IO_RScheduleService
    {
        public async Task<List<QueryO_RScheduleDto>> GetO_RScheduleByCriteria(Dictionary<string, object> criteria)
        {
            // 从criteria字典中获取医院名称
            string? hospitalName = criteria.ContainsKey("hospitalname") ? criteria["hospitalname"].ToString() : string.Empty;

            // 结果列表初始化
            List<QueryO_RScheduleDto> result = new List<QueryO_RScheduleDto>();

            // 检查医院名称是否为空
            if (string.IsNullOrEmpty(hospitalName))
            {
                return result; // 如果医院名称为空，返回空结果
            }

            // 查找医院
            var hospital = await DbContext.db.Queryable<HOSPITAL>()
                                .Where(h => h.NAME == hospitalName)
                                .FirstAsync();

            // 如果医院不存在，返回空结果
            if (hospital == null)
            {
                return result;
            }

            // 查找医院下的所有手术室
            var operatingRooms = await DbContext.db.Queryable<OPERATING_ROOM>()
                                    .Where(or => or.HOS_ID == hospital.HOS_ID)
                                    .ToListAsync();

            // 查询所有手术室的排班信息
            foreach (var room in operatingRooms)
            {
                var slots = await DbContext.db.Queryable<OPERATE_TIME, OPERATE, PROJECT, SERVER, BILL, CUSTOMER>(
                    (ot, op, pr, se, bi, cu) => new object[] {
                JoinType.Left, ot.OP_TIME_ID == op.OP_TIME_ID,
                JoinType.Left, op.PROJ_ID == pr.PROJ_ID,
                JoinType.Left, op.SER_ID == se.SER_ID,
                JoinType.Left, op.BILL_ID == bi.BILL_ID,
                JoinType.Left, bi.CUS_ID == cu.CUS_ID
                    })
                    .Where((ot, op, pr, se, bi, cu) => ot.ROOM_ID == room.ROOM_ID && ot.DAY == DateTime.Today)
                    .Select((ot, op, pr, se, bi, cu) => new O_RScheduleSlotsDto
                    {
                        status = op.OP_TIME_ID == null ? "0" : "1", // 没有预约时状态为0，反之为1
                        info = pr.NAME ?? "无预约", // 没有预约时显示"无预约"
                        doctor = se.NAME ?? string.Empty,
                        customer = cu.NAME ?? string.Empty
                    })
                    .ToListAsync();

                // 处理所有四个时间段的情况
                var allSlots = new List<O_RScheduleSlotsDto>
                {
                    new O_RScheduleSlotsDto { status = "0", info = "无预约", doctor = string.Empty, customer = string.Empty },
                    new O_RScheduleSlotsDto { status = "0", info = "无预约", doctor = string.Empty, customer = string.Empty },
                    new O_RScheduleSlotsDto { status = "0", info = "无预约", doctor = string.Empty, customer = string.Empty },
                    new O_RScheduleSlotsDto { status = "0", info = "无预约", doctor = string.Empty, customer = string.Empty }
                };

                // 更新有预约的时间段
                foreach (var slot in slots)
                {
                    var index = 0; // 根据时间段（8点、12点、14点、16点）获取索引
                    if (index >= 0 && index < allSlots.Count && slot.status == "1")
                    {
                        allSlots[index] = slot;
                    }
                }

                result.Add(new QueryO_RScheduleDto
                {
                    roomId = room.ROOM_ID,
                    slots = allSlots
                });

            }

            return result;

            /*// 构建查询
            var query = DbContext.db.Queryable<OPERATE, OPERATE_TIME, OPERATING_ROOM, PROJECT, SERVER, BILL, CUSTOMER, HOSPITAL>((o, t, r, p, s, b, c, h) => new object[] {
                JoinType.Inner, o.OP_TIME_ID == t.OP_TIME_ID,
                JoinType.Inner, t.ROOM_ID == r.ROOM_ID,
                JoinType.Inner, o.PROJ_ID == p.PROJ_ID,
                JoinType.Inner, o.SER_ID == s.SER_ID,
                JoinType.Inner, o.BILL_ID == b.BILL_ID,
                JoinType.Inner, b.CUS_ID == c.CUS_ID,
                JoinType.Inner, r.HOS_ID == h.HOS_ID
            })
            .Select((o, t, r, p, s, b, c, h) => new QueryO_RScheduleDto
            {
                roomId = r.ROOM_ID,

            });

            

            if (criteria.ContainsKey("STATUS") && criteria["STATUS"] is string exeState)
            {
                query = query.Where(o => o.STATUS == exeState);
            }

            // 执行查询
            var result = await query.ToListAsync();
            return result;*/
        }

    }
}
