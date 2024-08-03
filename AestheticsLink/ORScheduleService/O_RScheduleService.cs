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
            // 构建查询
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
                ROOM_ID = r.ROOM_ID,
                HOS_ID = r.HOS_ID,
                HOS_NAME = h.NAME,
                PROJ_ID = p.PROJ_ID,
                NAME = p.NAME,
                PRICE = p.PRICE,
                DAY = t.DAY,
                STATUS = t.STATUS,
                OP_TIME_ID = o.OP_TIME_ID,
                START_TIME = t.START_TIME,
                END_TIME = t.END_TIME,
                SER_ID = s.SER_ID,
                SER_NAME = s.NAME,
                CUS_ID = c.CUS_ID,
                CUS_NAME = c.NAME,
                BILL_ID = o.BILL_ID
            });

            // 添加查询条件
            if (criteria.ContainsKey("DATE") && criteria["DATE"] is DateTime date)
            {
                query = query.Where(t => t.DAY.Date == date.Date);
            }

            if (criteria.ContainsKey("ROOM_ID") && criteria["ROOM_ID"] is string roomId)
            {
                query = query.Where(r => r.ROOM_ID == roomId);
            }

            if (criteria.ContainsKey("SER_ID") && criteria["SER_ID"] is string serId)
            {
                query = query.Where(s => s.SER_ID == serId);
            }

            if (criteria.ContainsKey("CUS_ID") && criteria["CUS_ID"] is string cusId)
            {
                query = query.Where(c => c.CUS_ID == cusId);
            }

            if (criteria.ContainsKey("HOS_ID") && criteria["HOS_ID"] is string hosId)
            {
                query = query.Where(r => r.HOS_ID == hosId);
            }

            if (criteria.ContainsKey("STATUS") && criteria["STATUS"] is string exeState)
            {
                query = query.Where(o => o.STATUS == exeState);
            }

            // 执行查询
            var result = await query.ToListAsync();
            return result;
        }

    }
}
