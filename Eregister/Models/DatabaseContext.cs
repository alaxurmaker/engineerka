using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBModels;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Eregister.Models
{

        public class DatabaseContext : DbContext
        {
        //public DatabaseContext()
        //    : base("name=DatabaseContext")
        //{
        //Database.SetInitializer<DatabaseContext>(new CreateDatabaseIfNotExists<DatabaseContext>());
        //}

        public DatabaseContext() : base("DatabaseContext")
        {
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    throw new UnintentionalCodeFirstException();
        //}



        public virtual DbSet<User> Users { get; set; }
            public virtual DbSet<Teacher> Teachers { get; set; }
            public virtual DbSet<Subject> Subjects { get; set; }
            public virtual DbSet<StudentParent> StudentParents { get; set; }
            public virtual DbSet<StudentHistory> StudentHistories { get; set; }
            public virtual DbSet<StudentGrade> StudentGrades { get; set; }
            public virtual DbSet<Student> Students { get; set; }
            public virtual DbSet<Season> Seasons { get; set; }
            public virtual DbSet<Room> Rooms { get; set; }
            public virtual DbSet<Parent> Parents { get; set; }
            public virtual DbSet<Group> Groups { get; set; }
            public virtual DbSet<GradeRating> GradeRatings { get; set; }
            public virtual DbSet<FinalGrade> FinalGrades { get; set; }
            public virtual DbSet<Educator> Educators { get; set; }
            public virtual DbSet<Address> Addresses { get; set; }
            public virtual DbSet<TeacherSubject> TeacherSubjects { get; set; }
        }
    
}