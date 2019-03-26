using System;
using System.Linq;
using System.Reflection;

namespace EntityCore.Anotations
{
    public class TableName : Attribute
    {
        string name;
        public TableName(string name)
        {
            this.name = name;
        }
    }

    public class RelectionEnumerator
    {
    }

    public class Context
    {
        private string connectionString;
        public Context(string server, int port, string username, string password, string database)
        {
            connectionString = $"server=localhost;port=3306;database =question-database;user=appUser;password=D3ggyo123!";


            var typesWithAttribute = from a in AppDomain.CurrentDomain.GetAssemblies().AsParallel()
                                     from t in a.GetTypes()
                                     let attributes = t.GetCustomAttribute(typeof(TableName), true)
                                     where attributes != null && attributes.le
        }
    }
}
