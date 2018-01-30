using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Linq_05_LINQ_to_SQL.Model
{
    [Table (Name = "AccessUser")]
    public class AccessUser
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int intAccessID { get; set; }
        [Column]
        public int intUserId { get; set; }
        [Column]
        public DateTime dCreated { get; set; }
        [Column]
        public int intTabID { get; set; }
        [Column]
        public int intGroupAccessId { get; set; }
        [Column]
        public int intLocationId { get; set; }
        [Association(ThisKey = "intTabID", OtherKey = "intTabID")]
        public EntitySet<AccessTab> AccessTabs { get; set; }
    }
}
