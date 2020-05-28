using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.DAO
{
    public abstract class BaseDao
    {
        protected string connStr;
        public BaseDao(string _connStr)
        {
            this.connStr = _connStr;
        }
    }
}
