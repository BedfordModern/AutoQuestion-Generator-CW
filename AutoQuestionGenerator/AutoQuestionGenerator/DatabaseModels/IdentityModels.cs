using AutoQuestionGenerator.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace AutoQuestionGenerator.DatabaseModels
{
    public static class DatabaseConnector
    {
        private const string MYSQL_DATE_FORMAT = "yyyy/mm/dd";

        private const string MYSQL_TIME_FORMAT = "hh:MM:ss";
        private const string MYSQL_DATETIME_FORMAT = MYSQL_DATE_FORMAT + " " + MYSQL_TIME_FORMAT;

        internal static string GetAdminGroups()
        {
            throw new NotImplementedException();
        }

        private static string SQL_Push = "";

        private static List<MySqlConnection> Conncetions = new List<MySqlConnection>();
        private static MySqlConnection Connect()
        {
            foreach (var item in Conncetions)
            {
                if (item.State == System.Data.ConnectionState.Closed || item.State == System.Data.ConnectionState.Broken)
                {
                    item.Open();
                    return item;
                }
                else if (item.State == System.Data.ConnectionState.Open || item.State == System.Data.ConnectionState.Connecting)
                {
                    return item;
                }
            }
            MySqlConnection con = new MySqlConnection("server=localhost;port=3306;database=question-database;user=appUser;password=D3ggyo123!;");
            Conncetions.Add(con);
            con.Open();
            return con;
        }

        internal static Groups[] GetTeacherGroups(int organisationid, int userid)
        {
            string sql = "SELECT group_table.groupid, group_table.group_name, group_table.createdby from group_table" +
                "inner join group_table on group_table.createdby = users.userid" +
               $"WHERE group_table.createdby = {userid} OR (group_table.accesstype = 0 AND users.organisationid = {organisationid});";
            var rdr = GetReader(sql);

            List<Groups> ret = new List<Groups>();
            while (rdr.Read())
            {
                ret.Add(new Groups()
                {
                    GroupID = rdr.GetInt32("group_table.groupid"),
                    Group_Name = rdr.GetString("group_table.group_name"),
                    CreatedBy = rdr.GetInt32("group_table.createdby"),
                });
            }
            rdr.Close();
            return ret.ToArray();
        }

        internal static int AddQuestionSet(QuestionSets qSet)
        {
            string sql = $"INSERT INTO questionsets (WorkSetID, UserID, Date_Asked)" +
                $" VALUES ({qSet.WorkSetID}, {qSet.UserID}, '{qSet.Date_Asked.ToString(MYSQL_DATETIME_FORMAT)}');";

            return ExecuteSql(sql);
        }

        internal static Organisations GetOrganisation(int OrganisationID)
        {
            string sql = $"SELECT * FROM organisations WHERE OrganisationID={OrganisationID};";

            var rdr = GetReader(sql);

            Organisations ret = null;
            if (rdr.Read())
            {
                ret = new Organisations()
                {
                    OrganisationID = rdr.GetInt32("organisationID"),
                    Organisation_Username = rdr.GetString("Organisation_Username"),
                    Organisation_Name = rdr.GetString("Organisation_Name"),
                    Organisation_Password = rdr.GetString("Organisation_Password"),
                    Subscription_Renew_Date = rdr.GetDateTime("Subscription_Renew_Date"),
                    Subscription_Type = rdr.GetInt32("Subscription_Type")
                };
                
            }
            rdr.Close();

            return ret;
        }

        internal static int AddQuestion(Questions qust)
        {
            string sql = $"INSERT INTO questions (QuestionSetID, Question_Type, Seed, Difficulty, AnswerCorrect)" +
                $" VALUES ({qust.QuestionSetID}, {qust.Question_Type}, {qust.Seed}, {qust.Difficulty}, {qust.AnswerCorrect});";

            return ExecuteSql(sql);
        }

        internal static T[] GetWhere<T>(string where)
        {
            string sql = $"SELECT * FROM {typeof(T).GetAttributeValue((TableAttribute tbl) => tbl.Name)} WHERE {where};";

            var rdr = GetReader(sql);

            var properties = typeof(T).GetProperties();

            List<T> retVal = new List<T>();
            while (rdr.Read())
            {
                var item = (T)Activator.CreateInstance(typeof(T));

                foreach (var property in properties)
                {
                    property.SetValue(item, rdr[property.Name]);
                }
            }
            rdr.Close();
            return retVal.ToArray();
        }

        internal static bool OwnsWorkset(int uid, int worksetID)
        {
            string sql = $"SELECT WorksetID FROM worksets WHERE WorksetID={worksetID} AND SetBy={uid};";
            var rdr = GetReader(sql);

            var ret = rdr.Read();
            rdr.Close();
            return ret;
        }

        internal static bool HasAccessTo(int uid, int worksetID)
        {
            string sql = $"SELECT groupusers.GroupUserID FROM groupusers" +
                $"inner join groups on groupusers.GroupID = groups.GroupID" +
                $"inner join worksets on groups.GroupID = worsets.GroupID" +
                $"WHERE worksets.WorksetID={worksetID} AND groupusers.UserID={uid};";
            var rdr = GetReader(sql);

            var ret = rdr.Read();
            rdr.Close();
            return ret;
        }

        internal static Worksets GetWorkset(int worksetID)
        {
            string sql = $"SELECT * FROM worksets WHERE WorksetID={worksetID};";

            var rdr = GetReader(sql);
            Worksets ret = null;
            if (rdr.Read())
            {
                ret = new Worksets()
                {
                    WorksetID = rdr.GetInt32("WorksetID"),
                    WorksetName = rdr.GetString("WorksetName"),
                    Date_Due = rdr.GetDateTime("Date_Due"),
                    Date_Set = rdr.GetDateTime("Date_Set"),
                    ExamStyle = rdr.GetBoolean("ExamStyle"),
                    RandomOrdering = rdr.GetBoolean("RandomOrdering"),
                    SetBy = rdr.GetInt32("SetBy"),
                    SetType = rdr.GetInt32("SetType"),
                    GroupID = rdr.GetInt32("GroupID"),
                    Time_Allowed = rdr.GetInt32("Time_Allowed")
                };
            }
            rdr.Close();
            return ret;
        }

        internal static void DeleteWorkset(int setID)
        {
            string sql = $"DELETE FROM worksets WHERE WorksetID={setID};";

            ExecuteSql(sql);
        }

        internal static void DeleteWork(int setID)
        {
            string sql = $"DELETE FROM work WHERE WorkSetID={setID};";

            ExecuteSql(sql);
        }

        internal static void DeleteQuestionSets(int setID)
        {
            string sql = $"DELETE FROM questionsets WHERE WorkSetID={setID};";

            ExecuteSql(sql);
        }

        internal static void DeleteQuestions(int questionSetID)
        {
            string sql = $"DELETE FROM questions WHERE QuestionSetID = {questionSetID};";

            ExecuteSql(sql);
        }

        internal static int GetGroup(string groupname, int organisationID)
        {
            string sql = $"SELECT group_table.GroupID from group_table " +
                $"INNER JOIN users on users.UserID = group_table.UserID " +
                $"WHERE group_table.Group_Name = '{groupname}' AND users.OrganisationID={organisationID}";

            var ret = -1;
            var rdr = GetReader(sql);
            if (rdr.Read())
                ret = (int)rdr[0];
            rdr.Close();
            return ret;
        }

        internal static int AddUser(Users usr)
        {
            string sql = $"INSERT INTO users (Username, Password, First_Name, Last_Name, OrganisationID, Last_Logged_In)" +
                $" VALUES ('{usr.Username}', '{usr.Password}', '{usr.First_Name}', '{usr.Last_Name}', {usr.OrganisationID}, '{usr.Last_Logged_In.ToString(MYSQL_DATETIME_FORMAT)}');";

            return ExecuteSql(sql);
        }

        internal static bool UserInRole(int uid, string roleName)
        {
            string sql = "SELECT userroles.UserRoleID FROM userroles " +
                "inner join roles on userrole.RoleID == roles.RoleID " +
                $"WHERE roles.Role_Name='{roleName}' AND userroles.UserID={uid};";
            var rdr = GetReader(sql);

            var ret = rdr.Read();
            rdr.Close();
            return ret;
        }

        internal static int AddUserRole(UserRoles userRoles)
        {
            string sql = $"INSERT INTO userroles(UserID,RoleID) VALUES ({userRoles.UserID},{userRoles.RoleID});";

            return ExecuteSql(sql);
        }

        internal static int GetRoleID(string role)
        {
            string sql = $"SELECT RoleID from roles WHERE Role_Name='{role}';";
            var rdr = GetReader(sql);

            int roleid = -1;
            if (rdr.Read())
            {
                roleid = (int)rdr[0];
            }
            rdr.Close();
            return roleid;
        }

        internal static Users[] GetUsers(int OrganisationID)
        {
            var con = Connect();
            string sql = $"SELECT * FROM Users WHERE OrganisationID={OrganisationID};";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataReader rdr = cmd.ExecuteReader();

            List<Users> ret = new List<Users>();
            while (rdr.Read())
            {
                ret.Add(new Users()
                {
                    UserID = rdr.GetInt32("userid"),
                    Username = rdr.GetString("username"),
                    First_Name = rdr.GetString("first_name"),
                    Last_Name = rdr.GetString("last_name"),
                    OrganisationID = OrganisationID,
                    Last_Logged_In = rdr.GetDateTime("last_logged_in"),
                    Password = rdr.GetString("password")
                });
            }
            rdr.Close();
            return ret.ToArray();
        }

        internal static int AddWorkset(Worksets sets)
        {
            string sql = $"INSERT INTO worksets (WorksetName, GroupID, SetType, ExamStyle, RandomOrdering, Time_Allowed, SetBy, Date_Due. Date_Set)" +
                $" VALUES ('{sets.WorksetName}', {sets.GroupID}, {sets.SetType}, {sets.ExamStyle}, {sets.RandomOrdering}, {sets.Time_Allowed}, {sets.SetBy}, '{sets.Date_Due.ToString(MYSQL_DATE_FORMAT)}', '{sets.Date_Set.ToString(MYSQL_DATE_FORMAT)}');";

            return ExecuteSql(sql);
        }

        internal static T[] Get<T>()
        {
            string sql = $"SELECT * FROM {typeof(T).GetAttributeValue((TableAttribute tbl) => tbl.Name)};";

            var rdr = GetReader(sql);

            var properties = typeof(T).GetProperties();

            List<T> retVal = new List<T>();
            while (rdr.Read())
            {
                var item = (T)Activator.CreateInstance(typeof(T));
                
                foreach (var property in properties)
                {
                    property.SetValue(item, rdr[property.Name]);
                }
            }
            rdr.Close();
            return retVal.ToArray();
        }

        internal static void Update<T>(T model)
        {
            string sql = "UPDATE " + model.GetType().GetAttributeValue((TableAttribute tbl) => tbl.Name) + " SET {0} WHERE {1};",
                setdata = "",
                wheredata = "";
            
            var properties = model.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string equals = "";
                if (property.PropertyType == typeof(int) || property.PropertyType == typeof(decimal))
                {
                    equals = property.Name + " = " + property.GetValue(model).ToString();
                }
                else if (property.PropertyType == typeof(DateTime))
                {

                }
                else
                {
                    equals = property.Name + " = '" + property.GetValue(model).ToString() + "'";
                }

                if (Attribute.IsDefined(property.PropertyType, typeof(KeyAttribute)))
                {
                    if (!string.IsNullOrWhiteSpace(wheredata))
                        wheredata += " AND ";

                    wheredata += equals;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(setdata))
                        setdata += ", ";
                    setdata += equals;
                }
            }

            sql = string.Format(sql, setdata, wheredata);

            SQL_Push += sql;
        }

        internal static Catagory[] GetCatagories()
        {
            var sql = "SELECT * FROM Catagories";
            var rdr = GetReader(sql);

            List<Catagory> ret = new List<Catagory>();
            while (rdr.Read())
            {
                ret.Add(new Catagory()
                {
                    CatagoryID = rdr.GetInt32("CatagoryID"),
                    CatagoryName = rdr.GetString("CatagoryName"),
                    CatagoryType = rdr.GetInt32("CatagoryType")
                });
            }
            rdr.Close();
            return ret.ToArray();
        }

        internal static Users GetUser(int userID)
        {
            string sql = $"SELECT * FROM Users WHERE UserID={userID};";
            var rdr = GetReader(sql);

            Users ret = null;
            if (rdr.Read())
            {
                ret = new Users()
                {
                    UserID = rdr.GetInt32("userid"),
                    Username = rdr.GetString("username"),
                    First_Name = rdr.GetString("first_name"),
                    Last_Name = rdr.GetString("last_name"),
                    OrganisationID = rdr.GetInt32("organisationid"),
                    Last_Logged_In = rdr.GetDateTime("last_logged_in"),
                    Password = rdr.GetString("password")
                };
            }
            rdr.Close();
            return ret;
        }

        private static int LastInsertID(MySqlConnection con)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", con);
            var rdr = cmd.ExecuteReader();
            int ret = -1;
            if (rdr.Read())
            {
                ret = (int)rdr[0];
            }
            rdr.Close();
            return ret;
        }

        internal static int AddWork(Work work)
        {
            string sql = $"INSERT INTO work (QuestionType, Difficulty, Seed, WorkSetID)" +
                   $" VALUES ({work.QuestionType}, {work.Difficulty}, {work.Seed}, {work.WorkSetID});";

            return ExecuteSql(sql);
        }

        internal static void PushChanges()
        {
            if (string.IsNullOrEmpty(SQL_Push))
                return;
            string[] items = SQL_Push.Split(';');
            var con = Connect();
            MySqlCommand cmd = new MySqlCommand();
            var trans = con.BeginTransaction();
            cmd.Transaction = trans;
            cmd.Connection = con;


            foreach (var item in items)
            {
                cmd.CommandText = item;
                cmd.ExecuteNonQuery();
            }

            trans.Commit();

            SQL_Push = "";
            trans.Dispose();
        }

        internal static int AddGroup(Groups newGroup)
        {
            string sql = $"INSERT INTO group_table (Group_Name, AccessType, CreatedBy) VALUES ('{newGroup.Group_Name}',{newGroup.AccessType},{newGroup.CreatedBy}) ";

            return ExecuteSql(sql);
        }

        private static int ExecuteSql(string sql)
        {
            var con = Connect();
            MySqlCommand cmd = new MySqlCommand(sql, con);

            cmd.ExecuteNonQuery();
            return LastInsertID(con);
        }

        internal static int AddQesutionType(QuestionTypes questiontype)
        {
            string sql = $"INSERT INTO questiontypes(Type_Name, Class, Catagory) VALUES ('{questiontype.Type_Name}','{questiontype.Class}',{questiontype.Catagory});";

            return ExecuteSql(sql);
        }

        internal static int AddCatagory(Catagory catagory)
        {
            string sql = $"INSERT INTO catagories(CatagoryName,CatagoryType) VALUES ('{catagory.CatagoryName}',{catagory.CatagoryType});";

            return ExecuteSql(sql);
        }

        internal static int AddGroupUser(GroupUsers usrAdd)
        {
            var sql = $"INSERT INTO groupusers (GroupID, UserID) VALUES ({usrAdd.GroupID}, {usrAdd.UserID});";

            return ExecuteSql(sql);
        }

        internal static CatagoryType[] GetCatagoryTypes()
        {
            var sql = "SELECT * FROM CatagoryTpes;";
            var rdr = GetReader(sql);

            var ret = new List<CatagoryType>();

            while (rdr.Read())
            {
                ret.Add(new CatagoryType()
                {
                    CatTypeID = rdr.GetInt32("CatTypeID"),
                    CatTypeName = rdr.GetString("CatTypeName")
                });
            }

            return ret.ToArray();
        }

        internal static Users GetUser(string username, int organisationID)
        {
            string sql = $"SELECT * FROM Users WHERE username='{username}' AND organisationid={organisationID};";
            var rdr = GetReader(sql);
            Users ret = null;
            if (rdr.Read())
            {
                ret = new Users()
                {
                    UserID = rdr.GetInt32("userid"),
                    Username = rdr.GetString("username"),
                    First_Name = rdr.GetString("first_name"),
                    Last_Name = rdr.GetString("last_name"),
                    OrganisationID = rdr.GetInt32("organisationid"),
                    Last_Logged_In = rdr.GetDateTime("last_logged_in"),
                    Password = rdr.GetString("password")
                };
            }
            rdr.Close();
            return ret;
        }

        private static MySqlDataReader GetReader(string SQL)
        {
            var con = Connect();
            MySqlCommand cmd = new MySqlCommand(SQL, con);
            MySqlDataReader rdr = cmd.ExecuteReader();
            return rdr;
        }
    }

    class ItemFactory<T> where T : new()
    {
        public T GetNewItem()
        {
            return new T();
        }
    }

    /*
    public class IdentityModels : DbContext
    {
        public IdentityModels()
        {

        }

        public IdentityModels(DbContextOptions<IdentityModels> model) : base(model)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=question-database;user=appUser;password=D3ggyo123!;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(bool))
                    {
                        property.SetValueConverter(new BoolToIntConverter());
                    }
                }
            }
        }

        public DbSet<Subscriptions> subscriptions { get; set; }
        public DbSet<Organisations> organisations { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<Roles> roles { get; set; }
        public DbSet<UserRoles> userroles { get; set; }
        public DbSet<AccessTypes> accessTypes { get; set; }
        public DbSet<Groups> groups { get; set; }
        public DbSet<GroupUsers> groupUsers { get; set; }
        public DbSet<QuestionTypes> questionTypes { get; set; }
        public DbSet<QuestionSets> questionSets { get; set; }
        public DbSet<Questions> questions { get; set; }
        public DbSet<Worksets> worksets { get; set; }
        public DbSet<Work> work { get; set; }
        public DbSet<SetTypes> worktype { get; set; }
        public DbSet<Catagory> catagories { get; set; }
        public DbSet<CatagoryType> CatagoryTypes { get; set; }
    }*/

    [Table("catagorytypes")]
    public class CatagoryType
    {
        [Key]
        public int CatTypeID { get; set; }
        public string CatTypeName { get; set; }
    }

    [Table("catagories")]
    public class Catagory
    {
        [Key]
        public int CatagoryID { get; set; }
        public string CatagoryName { get; set; }
        public int CatagoryType { get; set; }
    }

    [Table("subscriptions")]
    public class Subscriptions
    {
        [Key]
        public int SubscriptionID { get; set; }
        public int Subsription_Length { get; set; }
        public string Subscription_Name { get; set; }
        public string Subscription_Description { get; set; }
    }

    [Table("organisations")]
    public class Organisations
    {
        [Key]
        public int OrganisationID { get; set; }
        public string Organisation_Username { get; set; }
        public string Organisation_Password { get; set; }
        public string Organisation_Name { get; set; }
        public int Subscription_Type { get; set; }
        public DateTime Subscription_Renew_Date { get; set; }
    }

    [Table("users")]
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int OrganisationID { get; set; }
        public DateTime Last_Logged_In { get; set; }
    }

    [Table("roles")]
    public class Roles
    {
        [Key]
        public int RoleID { get; set; }
        public string Role_Name { get; set; }
        public string Role_Description { get; set; }
    }

    [Table("userroles")]
    public class UserRoles
    {
        [Key]
        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
    }

    [Table("accesstypes")]
    public class AccessTypes
    {
        [Key]
        public int AccessTypeID { get; set; }
        public string AccessType_Name { get; set; }
        public string AccessType_Description { get; set; }
    }

    [Table("group_table")]
    public class Groups
    {
        [Key]
        public int GroupID { get; set; }
        public string Group_Name { get; set; }
        public int CreatedBy { get; set; }
        public int AccessType { get; set; }
    }

    [Table("groupusers")]
    public class GroupUsers
    {
        [Key]
        public int GroupUserID { get; set; }
        public int UserID { get; set; }
        public int GroupID { get; set; }
    }

    [Table("questiontypes")]
    public class QuestionTypes
    {
        [Key]
        public int TypeID { get; set; }
        public string Type_Name { get; set; }
        public string Class { get; set; }
        public int Catagory { get; set; }
    }

    [Table("questionsets")]
    public class QuestionSets
    {
        [Key]
        public int QuestionSetID { get; set; }
        public int UserID { get; set; }
        public int WorkSetID { get; set; }
        public DateTime Date_Asked { get; set; }
    }

    [Table("questions")]
    public class Questions
    {
        [Key]
        public int QuestionID { get; set; }
        public int Seed { get; set; }
        public int Difficulty { get; set; }
        public int Question_Type { get; set; }
        public int QuestionSetID { get; set; }
        public int AnswerCorrect { get; set; }
    }

    [Table("worksets")]
    public class Worksets
    {
        [Key]
        public int WorksetID { get; set; }
        public string WorksetName { get; set; }
        public int? GroupID { get; set; }
        public int SetBy { get; set; }
        public int SetType { get; set; }
        public int Time_Allowed { get; set; }
        public bool ExamStyle { get; set; }
        public bool RandomOrdering { get; set; }
        public DateTime Date_Set { get; set; }
        public DateTime Date_Due { get; set; }
    }

    [Table("work")]
    public class Work
    {
        [Key]
        public int WorkID { get; set; }
        public int WorkSetID { get; set; }
        public int Seed { get; set; }
        public int QuestionType { get; set; }
        public int? Difficulty { get; set; }
    }

    [Table("settypes")]
    public class SetTypes
    {
        [Key]
        public int SetType_ID { get; set; }
        public string SetType_Name { get; set; }
    }
}
