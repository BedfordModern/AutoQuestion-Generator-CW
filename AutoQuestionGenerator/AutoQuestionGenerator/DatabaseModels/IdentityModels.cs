using AutoQuestionGenerator.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.DatabaseModels
{
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
