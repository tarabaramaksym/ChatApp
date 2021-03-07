using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server.Data.Models
{
    public interface IModel
    {
        string GetInsert();
        string GetTableName();
        void ParseSqlReader(SqlDataReader reader);
    }
}
