﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModel.Entity
{
    public class INVENTORY
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string HOS_ID { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public string G_ID { get; set; }
        public int STORAGE { get; set; }
        public DateTime SELL_BY_DATE { get; set; }
    }
}
