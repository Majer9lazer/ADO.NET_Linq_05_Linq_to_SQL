using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Linq_05_LINQ_to_SQL.Model
{
    public class MCSModel:DataContext
    {
        public MCSModel():base("Data Source=192.168.111.107; Initial Catalog=MCS; User ID=sa;Password=Mc123456")
        {

           
        }
        public Table<AccessTab> AccessTab { get; set; }
        public Table<AccessUser> AccessUser { get; set; }

    }
}
