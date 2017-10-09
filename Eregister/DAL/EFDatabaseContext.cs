using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBModels;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using Eregister.Models.Features.Quiz;

namespace Eregister.Models
{

    public class EFDatabaseContext : DbContext
    {
        //public DatabaseContext()
        //    : base("name=DatabaseContext")
        //{
        //Database.SetInitializer<DatabaseContext>(new CreateDatabaseIfNotExists<DatabaseContext>());
        //}

        public EFDatabaseContext() : base("EFDatabaseContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentParent> StudentParents { get; set; }
        public DbSet<StudentHistory> StudentHistories { get; set; }
        public DbSet<StudentGrade> StudentGrades { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GradeRating> GradeRatings { get; set; }
        public DbSet<FinalGrade> FinalGrades { get; set; }
        public DbSet<Educator> Educators { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }

        public DbSet<Quiz> Quizzes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //  modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //throw new UnintentionalCodeFirstException();
        }

        //public virtual DbSet<User> Users { get; set; }
        //public virtual DbSet<Teacher> Teachers { get; set; }
        //public virtual DbSet<Subject> Subjects { get; set; }
        //public virtual DbSet<StudentParent> StudentParents { get; set; }
        //public virtual DbSet<StudentHistory> StudentHistories { get; set; }
        //public virtual DbSet<StudentGrade> StudentGrades { get; set; }
        //public virtual DbSet<Student> Students { get; set; }
        //public virtual DbSet<Season> Seasons { get; set; }
        //public virtual DbSet<Room> Rooms { get; set; }
        //public virtual DbSet<Parent> Parents { get; set; }
        //public virtual DbSet<Group> Groups { get; set; }
        //public virtual DbSet<GradeRating> GradeRatings { get; set; }
        //public virtual DbSet<FinalGrade> FinalGrades { get; set; }
        //public virtual DbSet<Educator> Educators { get; set; }
        //public virtual DbSet<Address> Addresses { get; set; }
        //public virtual DbSet<TeacherSubject> TeacherSubjects { get; set; }
    }

}