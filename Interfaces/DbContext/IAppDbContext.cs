﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DbContext
{
    public interface IAppDbContext
    {
        public SqlConnection CreateConnection();
    }
}
