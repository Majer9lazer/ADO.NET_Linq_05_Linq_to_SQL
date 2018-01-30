using ADO.NET_Linq_05_LINQ_to_SQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using System.Transactions;

namespace ADO.NET_Linq_05_LINQ_to_SQL
{
    class Program
    {
        private static MCSModel db = new MCSModel();
        static void Main(string[] args)
        {
            Stopwatch stp = new Stopwatch();
            stp.Start();
            Example_06();
            stp.Stop();
            Console.WriteLine("Milliseconds : {0} , Seconds {1}",stp.ElapsedMilliseconds,stp.ElapsedMilliseconds/100);
        }
        static void Example_01()
        {

            Table<AccessTab> accesTAbles = db.GetTable<AccessTab>();
            //AccessTab tab = accesTAbles.FirstOrDefault(f => f.intTabId == 56);
            //tab.StrDescription = "New Description";
            //db.SubmitChanges(ConflictMode.FailOnFirstConflict);
            //Console.WriteLine();
            foreach (AccessTab tab in accesTAbles)
            {
                Console.WriteLine("Tab Name : {0}", tab.StrTabName);
            }



            \

        }
        static void Example_02()
        {
            try
            {
                Table<AccessTab> accesTAbles = db.GetTable<AccessTab>();
                AccessTab tab = accesTAbles.FirstOrDefault(f => f.intTabID == 56);
                tab.StrDescription = "Best New Description by Best";
                db.SubmitChanges(ConflictMode.FailOnFirstConflict);
               // Console.WriteLine();
                //foreach (AccessTab tab in accesTAbles)
                //{
                //    Console.WriteLine("Tab Name : {0}", tab.StrTabName);
                //}

            }
            catch (ChangeConflictException ex)
            {

                Console.WriteLine(ex.Message);
                foreach (ObjectChangeConflict item in db.ChangeConflicts)
                {
                    MetaTable metatable = db.Mapping.GetTable(item.Object.GetType());
                    AccessTab en = (AccessTab)item.Object;
                    Console.WriteLine("TABLE name : {0}", metatable.TableName);
                }
            }
        }
        //Отложенные выполнение запросов
        static void Example_03()
        {
            Table<AccessUser> users = db.GetTable<AccessUser>();
            var query = from u in users
                        where u.intUserId == 1
                        select
                        from t in u.AccessTabs
                        select new
                        {
                            u.intUserId,
                            t.StrTabName
                        };
            //Console.WriteLine("-----------------------------------");
            //db.Log = Console.Out;
            //Console.WriteLine("-----------------------------------");
   
            //var q2 = query.Select(s => s).ToList();
            foreach (var item in query)
            {
                foreach (var item2 in item)
                {
                    Console.WriteLine(item2.intUserId + " - " + item2.StrTabName);
                }
            }
            //Отложный запрос
            //получаем то что мы хотим , а не всё
            foreach (var user in users)
            {
                //пересылка данных туда и обратно
                foreach (AccessTab tab in user.AccessTabs)
                {
                    Console.WriteLine(user.intUserId + " - " + tab.StrTabName);
                }
            }
        }
        //dbConnection
        static void Example_04()
        {
            Console.WriteLine("Connection : {0}",db.Connection);
            Console.WriteLine("Connection.ConnectionString : {0}", db.Connection.ConnectionString);
            Console.WriteLine("Connection.ConnectionTimeout : {0}", db.Connection.ConnectionTimeout);
            Console.WriteLine("Connection.Database : {0}", db.Connection.Database);
            Console.WriteLine("Connection.DataSource : {0}", db.Connection.DataSource);

        }
        static void Example_05(MCSModel dataContext)
        {
            Table<AccessTab> tabs = dataContext.GetTable<AccessTab>();
            Table<AccessUser> users= dataContext.GetTable<AccessUser>();
            db.Refresh(RefreshMode.OverwriteCurrentValues);
            AccessTab a = tabs.OrderBy(o => o.StrTabName).First(f=>f.intTabID==56);

        }
        //Исользуем транзакциями
        static void Example_06()
        {
            Table<AccessTab> accessTabs = db.GetTable<AccessTab>();
            AccessTab aTab = accessTabs.FirstOrDefault(f => f.intTabID == 2);
            aTab.StrDescription = "Best Test 000";

            Table<AccessUser> accessUsers = db.GetTable<AccessUser>();
            AccessUser aUser = accessUsers.FirstOrDefault(f => f.intAccessID == 6822);
            aUser.dCreated = DateTime.Now;
            aUser.intTabID = 2;
            try
            {
                using (TransactionScope scope =
                    new TransactionScope())
                {
                    db.SubmitChanges();
                    scope.Complete();

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                db.Refresh(RefreshMode.OverwriteCurrentValues,accessTabs);
                Console.WriteLine("StrDesciption : {0}",aTab.StrDescription);

                db.Refresh(RefreshMode.OverwriteCurrentValues, accessUsers);
                Console.WriteLine("dCreated : {0}", aUser.dCreated);
            }
        }
    }
}
