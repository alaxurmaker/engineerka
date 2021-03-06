﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
//using DBModels;
using Models;
//using Models.RegisterModels;
using Eregister.Models.Features.Quiz;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eregister.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationUser : IdentityUser
    {
        public DateTime BirthDate { get; internal set; }
        public string Country { get; internal set; }
        public DateTime EmailLinkDate { get; internal set; }
        public DateTime JoinDate { get; internal set; }
        public DateTime LastLoginDate { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string PersonSex { get; internal set; }
        public string TokenValue { get; internal set; }
        public string TokenValueAdv { get; internal set; }
        public bool TokenIsValid { get; internal set; }
        public bool TokenAdvIsValid { get; internal set; }
        public string CustomSkin { get; internal set; }
        public string NameSurname { get; internal set; }

        //        //public int ImageId { get; set; }
        //        //public string ImageTitle { get; set; }
        public byte[] ImageByte { get; set; }
        //        //public string ImagePath { get; set; }


        public virtual Parent Parent { get; set; }
         public virtual Teacher Teacher { get; set; }
         public virtual Student Student { get; set; }
        

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
       
    }

    public class Teacher
    {
        [Key]
        public string UserId { get; set; }

        public string Title { get; set; }
        public string Pesel { get; set; }

        [ForeignKey("Address")]
        public int? AddressID { get; set; }

      //  [ForeignKey("Group")]
        public int? GroupID { get; set; }
        //[ForeignKey("Alert")]
        //public int? AlertID { get; set; }   
        //public virtual Alert Alert { get; set; }

        public virtual Address Address { get; set; }

        //[Required]
        [Required, ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        //public virtual Group Group { get; set; }
        // public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; }
    }

    public class Student
    {
        [Key]
        public string UserId { get; set; }

        public string Pesel { get; set; }
        public DateTime? JoinDate { get; set; }
        public string GroupName { get; set; }

        [ForeignKey("Address")]
        public int? AddressID { get; set; }
        //public int EducatorID { get; set; }

        [ForeignKey("Group")]
        public int? GroupID { get; set; }
        public virtual Address Address { get; set; }

      //  [ForeignKey("StudentSubject")]
     //   public int StudentSubjectID { get; set; }
        //public virtual ICollection<Subject> Subjects { get; set; }

        // [Required]
        [Required, ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        // public virtual Educator Educator { get; set; }


        //[ForeignKey("Alert")]
        //public int? AlertID { get; set; }
        //public virtual Alert Alert { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; }
        public virtual ICollection<StudentParent> StudentParents { get; set; }
        public virtual ICollection<StudentGrade> StudentGrades { get; set; }
        public virtual ICollection<StudentHistory> StudentHistories { get; set; }
        // public virtual ICollection<Group> Groups { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
    }
    public class Parent
    {
        [Key]
        public string UserId { get; set; }

        public string Phone { get; set; }

        [ForeignKey("Address")]
        public int? AddressID { get; set; }
      
        [Required, ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        
        public virtual Address Address { get; set; }
        public virtual ICollection<StudentParent> StudentParents { get; set; }
    }

    public class Alert
    {
        [Key]
        public int AlertID { get; set; }
    //    public int Count { get; set; }
        public bool IsOn { get; set; }

        public DateTime? AddDate { get; set; }

        [ForeignKey("Student")]
        public string StudentUserId { get; set; }

        [ForeignKey("Teacher")]
        public string TeacherUserId { get; set; }

        [ForeignKey("AlertContent")]
        public int AlertContentID { get; set; }

        public virtual AlertContent AlertContent { get; set; }

        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
    //public class MyUser
    //{
    //    [Key, ForeignKey("ApplicationUser")]
    //    public string UserId { get; set; }

    //    //kody do uwierzytelniania roli
    //    public string TokenValue { get; set; }
    //    public bool TokenIsValid { get; set; }
    //    public string RoleAuthorize { get; set; }

    //    public virtual ApplicationUser ApplicationUser { get; set; }  //IDENTITY
    //    public virtual Teacher Teacher { get; set; }
    //    public virtual Student Student { get; set; }
    //    public virtual Parent Parent { get; set; }
    //    //public virtual ICollection<Parent> Parents { get; set; }
    //    //  public virtual ICollection<Teacher> Teachers { get; set; }
    //    // public virtual ICollection<Student> Students { get; set; }

    //}

    public enum Sex
    {
        M,
        K
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
          //  : base("EFDatabaseContext", throwIfV1Schema: false)
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new CreateDatabaseIfNotExists<ApplicationDbContext>());
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
   // }


/*    public class EFDatabaseContext : DbContext*/ //IdentityDbContext<ApplicationUser>
    //{
        //public DatabaseContext()
        //    : base("name=DatabaseContext")
        //{
        //Database.SetInitializer<DatabaseContext>(new CreateDatabaseIfNotExists<DatabaseContext>());
        //}

        //public DbSet<MyUser> MyUserss { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentParent> StudentParents { get; set; }
        public DbSet<StudentHistory> StudentHistories { get; set; }
        public DbSet<StudentGrade> StudentGrades { get; set; }
        public DbSet<Student> Students { get; set; }
       // public DbSet<Season> Seasons { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Group> Groups { get; set; }
       // public DbSet<GradeRating> GradeRatings { get; set; }
        public DbSet<FinalGrade> FinalGrades { get; set; }
        //public DbSet<Educator> Educators { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<AlertContent> AlertContents { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Event> Events { get; set; }
        /// <summary>
        /// ERegister Wall
        /// </summary>
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostVideo> PostVideos { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
        public DbSet<ReplyLike> ReplyLikes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //moje laczenie student,parent,teacher id z appuser
            
                //modelBuilder.Entity<ApplicationUser>()
                //    .HasRequired(c => c.Student)
                //    .WithRequired(d => d.ApplicationUser);


                base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }


        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //    //  modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //    //throw new UnintentionalCodeFirstException();
        //}


    }
}



