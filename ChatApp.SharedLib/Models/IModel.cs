using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.SharedLib.Models
{
    interface IModel
    {
        void ParseSqlReader(SqlDataReader reader);
    }
}
