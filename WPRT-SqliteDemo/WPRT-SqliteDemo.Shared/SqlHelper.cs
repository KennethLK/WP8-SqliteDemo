using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPRT_SqliteDemo
{
    public class SqlHelper
    {
        SQLite.SQLiteConnection _conn = null;
        int _index = 1;
        int _index2 = 1;

        public bool OpenDb(string dbPath)
        {
            try
            {
                _conn = new SQLite.SQLiteConnection(dbPath);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void CloseDb()
        {
            _conn.Close();
        }

        public bool CreateTable()
        {
            try
            {
                int result = _conn.CreateTable<TestTable>();
                _conn.Execute("delete from TestTable");
                return result == 0;
            }
            catch
            {
                return false;
            }
        }

        public bool CreateTable2()
        {
            try
            {
                int result = _conn.CreateTable<TestTable2>();
                //清空表
                _conn.Execute("delete from TestTable2");
                return result == 0;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertOne()
        {
            try
            {
                int index = _index++;
                string name = "time is " + DateTime.Now.ToString("HH:mm:ss.fff");
                TestTable tt = new TestTable() { Id = index, Name = name };
                _conn.Insert(tt);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertOne2()
        {
            try
            {
                int index = _index2++;
                string name = "time is " + DateTime.Now.ToString("HH:mm:ss.fff");
                TestTable2 tt = new TestTable2() { Id = index, InsertDate = DateTime.Now };
                _conn.Insert(tt);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetCount()
        {
            try
            {
                var cmd = _conn.CreateCommand("select count(id) from TestTable");
                int cnt = cmd.ExecuteScalar<int>();
                return cnt;
            }
            catch
            {
                return -1;
            }
        }

        public bool DeleteOne()
        {
            try
            {
                int firstId = _conn.ExecuteScalar<int>("select Id from TestTable order by Id limit 1");
                _conn.Execute("delete from TestTable where Id=?", firstId);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteOne2()
        {
            try
            {
                int firstId = _conn.ExecuteScalar<int>("select Id from TestTable2 order by Id limit 1");
                _conn.Execute("delete from TestTable2 where Id=?", firstId);
            }
            catch
            {
                return false;
            }
            return true;
        }

        class TestTable
        {
            [PrimaryKey]
            public int Id { get; set; }

            public string Name { get; set; }
        }

        class TestTable2
        {
            [PrimaryKey]
            public int Id { get; set; }

            public DateTime InsertDate { get; set; }
        }

        class TestCrossTable
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public DateTime InsertDate { get; set; }
        }

        public int GetCrossTable()
        {
            try
            {
                var list = _conn.Query<TestCrossTable>("select t1.Id, t1.Name, t2.InsertDate from TestTable as t1 inner join TestTable2 as t2 on t1.Id = t2.Id");
                return list.Count;
            }
            catch
            {
                return -1;
            }
        }
    }
}
