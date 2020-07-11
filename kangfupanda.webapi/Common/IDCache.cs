using kangfupanda.dataentity.DAO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace kangfupanda.webapi.Common
{
    public class IDCache
    {
        private static List<int> allIDs;
        public static List<int> AllIDs {
            get {
                return allIDs;
            }
            private set { } }


        public static void UpdateRandomIDs() {
            var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            allIDs = dao.GetAllIds();
        }
    }
}