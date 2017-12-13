using System;
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
    //public class ApplicationUser : IdentityUser
    //{
    //    public DateTime BirthDate { get; internal set; }
    //    public string Country { get; internal set; }
    //    public DateTime EmailLinkDate { get; internal set; }
    //    public string FirstName { get; internal set; }
    //    public DateTime JoinDate { get; internal set; }
    //    public DateTime LastLoginDate { get; internal set; }
    //    public string LastName { get; internal set; }

    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }
    //}
    //}



    //

    public class ApplicationUser : IdentityUser
    {
        public DateTime BirthDate { get; internal set; }
        public string Country { get; internal set; }
        public DateTime EmailLinkDate { get; internal set; }
        public string FirstName { get; internal set; }
        public DateTime JoinDate { get; internal set; }
        public DateTime LastLoginDate { get; internal set; }
        public string LastName { get; internal set; }
        public string PersonSex { get; internal set; }



        public virtual Student Student { get; set; }
        // public virtual ICollection<Parent> Parents { get; set; }
        //  public virtual ICollection<Teacher> Teachers { get; set; }
        //          public virtual ICollection<Student> Students { get; set; }
        

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
       
}




public class Student
    {
        //public int StudentID { get; set; }

        [Key, ForeignKey("User")]
        public string UserId { get; set; }
        public string FirstName { get; set; }

        // other fields...

        public virtual ApplicationUser User { get; set; }

        //public string StudentId { get; set; }
        //public string UserId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public int? Pesel { get; set; }
       // public DateTime? BirthDate { get; set; }
      //  public DateTime? JoinDate { get; set; }
        public string Classroom { get; set; }
       // public Sex? Sex { get; set; }

        public int? AddressID { get; set; }
        //public int UserID { get; set; }
        public int? EducatorID { get; set; }

        public virtual Address Address { get; set; }
        //   public virtual User User { get; set; }
       // public virtual ApplicationUser User { get; set; }

        public virtual Educator Educator { get; set; }
        public virtual ICollection<StudentParent> StudentParents { get; set; }
        public virtual ICollection<StudentGrade> StudentGrades { get; set; }
        public virtual ICollection<StudentHistory> StudentHistories { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

    }

    public enum Sex
    {
        //   [Display(Name = "Mężczyzna")]
        M,
        //  [Display(Name = "Kobieta")]
        K
    }







    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
          //  : base("EFDatabaseContext", throwIfV1Schema: false)
            : base("DefaultConnection", throwIfV1Schema: false)
        {
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



        //public EFDatabaseContext() : base("EFDatabaseContext")
        //{
        //}

        //public static EFDatabaseContext Create()
        //{
        //    return new EFDatabaseContext();
        //}

       // public DbSet<User> Userss { get; set; }
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

    // UNCOMMENT
    //public class ApplicationUser : IdentityUser
    //{
    //    public DateTime BirthDate { get; internal set; }
    //    public string Country { get; internal set; }
    //    public DateTime EmailLinkDate { get; internal set; }
    //    public string FirstName { get; internal set; }
    //    public DateTime JoinDate { get; internal set; }
    //    public DateTime LastLoginDate { get; internal set; }
    //    public string LastName { get; internal set; }

    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }
    //}
}



