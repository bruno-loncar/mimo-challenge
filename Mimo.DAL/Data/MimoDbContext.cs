using Microsoft.EntityFrameworkCore;
using Mimo.Model.Achievements;
using Mimo.Model.Courses;
using Mimo.Model.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mimo.DAL.Data
{
    public class MimoDbContext : DbContext
    {
        public MimoDbContext(DbContextOptions<MimoDbContext> options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Lesson> Lesson { get; set; }
        public DbSet<Achievement> Achievements { get; set; }

        public DbSet<UserLesson> UserLessons { get; set; }
        public DbSet<UserChapter> UserChapters { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many to many - UserLesson (Lessions completed by user)
            modelBuilder.Entity<UserLesson>()
                .HasOne(userLesson => userLesson.User)
                .WithMany(user => user.UserLessons)
                .HasForeignKey(userLesson => userLesson.UserId);
            modelBuilder.Entity<UserLesson>()
                .HasOne(userLesson => userLesson.Lesson)
                .WithMany(lession => lession.UserLessons)
                .HasForeignKey(userLesson => userLesson.LessonId);

            // Many to many - UserChapter (Chapters completed by user)
            modelBuilder.Entity<UserChapter>()
                .HasOne(userChapter => userChapter.User)
                .WithMany(user => user.UserChapters)
                .HasForeignKey(userChapter => userChapter.UserId);
            modelBuilder.Entity<UserChapter>()
                .HasOne(userChapter => userChapter.Chapter)
                .WithMany(chapter => chapter.UserChapters)
                .HasForeignKey(userChapter => userChapter.ChapterId);

            // Many to many - UserCourse (Courses completed by user)
            modelBuilder.Entity<UserCourse>()
                .HasOne(userCourse => userCourse.User)
                .WithMany(user => user.UserCourses)
                .HasForeignKey(userCourse => userCourse.UserId);
            modelBuilder.Entity<UserCourse>()
                .HasOne(userCourse => userCourse.Course)
                .WithMany(course => course.UserCourses)
                .HasForeignKey(userCourse => userCourse.CourseId);

            // Many to many - UserAchievement (Achievements completed by user)
            modelBuilder.Entity<UserAchievement>()
                .HasOne(userAchievement => userAchievement.User)
                .WithMany(user => user.UserAchievements)
                .HasForeignKey(userAchievement => userAchievement.UserId);
            modelBuilder.Entity<UserAchievement>()
                .HasOne(userAchievement => userAchievement.Achievement)
                .WithMany(achievement => achievement.UserAchievements)
                .HasForeignKey(userAchievement => userAchievement.AchievementId);
        }

    }
}
