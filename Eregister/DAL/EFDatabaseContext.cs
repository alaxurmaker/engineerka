//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using DBModels;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Data.Entity.ModelConfiguration.Conventions;
//using Eregister.Models.Features.Quiz;
//using Models;
//using Microsoft.AspNet.Identity.EntityFramework;
//using System.Threading.Tasks;
//using System.Security.Claims;
//using Microsoft.AspNet.Identity;

//namespace Eregister.Models
//{
//    public class ApplicationUser : IdentityUser
//    {
//        public DateTime BirthDate { get; internal set; }
//        public string Country { get; internal set; }
//        public DateTime EmailLinkDate { get; internal set; }
//        public string FirstName { get; internal set; }
//        public DateTime JoinDate { get; internal set; }
//        public DateTime LastLoginDate { get; internal set; }
//        public string LastName { get; internal set; }

//        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
//        {
//            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
//            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
//            // Add custom user claims here
//            return userIdentity;
//        }
//    }


//    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
//    {
//        public ApplicationDbContext()
//            : base("IdentityConnection", throwIfV1Schema: false)
//        {
//        }

//        public static ApplicationDbContext Create()
//        {
//            return new ApplicationDbContext();
//        }
//    }


//    public class EFDatabaseContext : DbContext //IdentityDbContext<ApplicationUser>
//    {
//        //public DatabaseContext()
//        //    : base("name=DatabaseContext")
//        //{
//        //Database.SetInitializer<DatabaseContext>(new CreateDatabaseIfNotExists<DatabaseContext>());
//        //}



//        public EFDatabaseContext() : base("EFDatabaseContext")
//        {
//        }

//        public static EFDatabaseContext Create()
//        {
//            return new EFDatabaseContext();
//        }

//        public DbSet<User> Users { get; set; }
//        public DbSet<Teacher> Teachers { get; set; }
//        public DbSet<Subject> Subjects { get; set; }
//        public DbSet<StudentParent> StudentParents { get; set; }
//        public DbSet<StudentHistory> StudentHistories { get; set; }
//        public DbSet<StudentGrade> StudentGrades { get; set; }
//        public DbSet<Student> Students { get; set; }
//        public DbSet<Season> Seasons { get; set; }
//        public DbSet<Room> Rooms { get; set; }
//        public DbSet<Parent> Parents { get; set; }
//        public DbSet<Group> Groups { get; set; }
//        public DbSet<GradeRating> GradeRatings { get; set; }
//        public DbSet<FinalGrade> FinalGrades { get; set; }
//        public DbSet<Educator> Educators { get; set; }
//        public DbSet<Address> Addresses { get; set; }
//        public DbSet<TeacherSubject> TeacherSubjects { get; set; }

//        public DbSet<Quiz> Quizzes { get; set; }

//        /// <summary>
//        /// ERegister Wall
//        /// </summary>
//        public DbSet<Post> Posts { get; set; }
//        public DbSet<Comment> Comments { get; set; }
//        public DbSet<Reply> Replies { get; set; }
//        public DbSet<Tag> Tags { get; set; }
//        public DbSet<PostTag> PostTags { get; set; }
//        public DbSet<PostCategory> PostCategories { get; set; }
//        public DbSet<Category> Categories { get; set; }
//        public DbSet<PostVideo> PostVideos { get; set; }
//        public DbSet<PostLike> PostLikes { get; set; }
//        public DbSet<CommentLike> CommentLikes { get; set; }
//        public DbSet<ReplyLike> ReplyLikes { get; set; }








//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
//            //  modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
//            //throw new UnintentionalCodeFirstException();
//        }


//    }

//    // UNCOMMENT
//    //public class ApplicationUser : IdentityUser
//    //{
//    //    public DateTime BirthDate { get; internal set; }
//    //    public string Country { get; internal set; }
//    //    public DateTime EmailLinkDate { get; internal set; }
//    //    public string FirstName { get; internal set; }
//    //    public DateTime JoinDate { get; internal set; }
//    //    public DateTime LastLoginDate { get; internal set; }
//    //    public string LastName { get; internal set; }

//    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
//    //    {
//    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
//    //        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
//    //        // Add custom user claims here
//    //        return userIdentity;
//    //    }
//    //}
//}


////public virtual DbSet<User> Users { get; set; }
////public virtual DbSet<Teacher> Teachers { get; set; }
////public virtual DbSet<Subject> Subjects { get; set; }
////public virtual DbSet<StudentParent> StudentParents { get; set; }
////public virtual DbSet<StudentHistory> StudentHistories { get; set; }
////public virtual DbSet<StudentGrade> StudentGrades { get; set; }
////public virtual DbSet<Student> Students { get; set; }
////public virtual DbSet<Season> Seasons { get; set; }
////public virtual DbSet<Room> Rooms { get; set; }
////public virtual DbSet<Parent> Parents { get; set; }
////public virtual DbSet<Group> Groups { get; set; }
////public virtual DbSet<GradeRating> GradeRatings { get; set; }
////public virtual DbSet<FinalGrade> FinalGrades { get; set; }
////public virtual DbSet<Educator> Educators { get; set; }
////public virtual DbSet<Address> Addresses { get; set; }
////public virtual DbSet<TeacherSubject> TeacherSubjects { get; set; }