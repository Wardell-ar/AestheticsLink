﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCommon.Database
{
    public static class DbContext
    {
        public static SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
        {

            ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=100.78.214.7)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=orcl)));User ID=XYYDATABASE;Password=XYY20040123;",
            DbType = SqlSugar.DbType.Oracle,//数据库类型
            InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
            IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了
        });
    }
}