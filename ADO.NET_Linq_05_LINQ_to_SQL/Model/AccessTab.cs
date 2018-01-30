using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Linq_05_LINQ_to_SQL.Model
{
    [Table(Name = "AccessTab")]
    public class AccessTab
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int intTabID { get; set; }

        [Column(Name = "strTabName")]
        public string StrTabName { get; set; }
        [Column(Name = "strDescription")]
        public string StrDescription { get; set; }

        [Column(Name = "strTabUrl")]
        public string StrTabUrl { get; set; }

        private string StrTabGroupName;
        [Column(Storage = "StrTabGroupName")]
        public string strTabGroupName
        {
            get { return StrTabGroupName; }
            set { StrTabGroupName = value; }
        }
        [Association(ThisKey = "intTabID", OtherKey = "intTabID")]
        public EntitySet<AccessUser> AccessUsers{ get; set; }
    }
}
