using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeSubmissionSimple.Shared;
using Microsoft.EntityFrameworkCore;

namespace CodeSubmissionSimple.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }


        public DbSet<Submission> Submissions { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Candidate> Candidates { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            {
                modelBuilder.Entity<Submission>()
                    .HasOne(a => a.Candidate)
                    .WithOne(b => b.Submission)
                    .HasForeignKey<Candidate>(b => b.SubmissionId);
            }

            modelBuilder.Entity<Submission>().HasData(new Submission
            {
                SubmissionId = 1,
                isCompleted = false
            }); ;

            //Tests
            modelBuilder.Entity<Candidate>().HasData(new Candidate
            {
                CandidateId = 1,
                Name = "Promise",
                Surname = "Email",
                Email = "chux05@hotmail.com",
                SubmissionId = 1
            });

            //Questions - deprecated 
            modelBuilder.Entity<Question>().HasData(new Question
            {
                QuestionId = 1,
                Description = "Given a string x, reverse and return it",
                Langauge = "Python",
                CodeStub = "def reverse_string():",
            });

            modelBuilder.Entity<Question>().HasData(new Question
            {
                QuestionId = 2,
                Description = "Given a string S consisting only '0's and '1's,  find the last index of the '1' present in it.",
                Langauge = "C#",
                CodeStub = @"
            public class Solution{
                public int LastIndexOfOne(string S) {
                
                    return 0;  
                }
            }",
            });

            modelBuilder.Entity<Question>().HasData(new Question
            {
                QuestionId = 3,
                Description = "Given a database 'Users', write a query to display all the users",
                Langauge = "SQL",
                CodeStub = "",
            });

            modelBuilder.Entity<Question>().HasData(new Question
            {
                QuestionId = 4,
                Description = @"Given a piece of html, change the text to be red using the 'red-card class
<div id=""firstDiv"" class=""red-card"">",
                Langauge = "CSS",
                CodeStub = "",
            });

            modelBuilder.Entity<Question>().HasData(new Question
            {
                QuestionId = 5,
                Description  = @"Given a piece of html, change the text to be pink using Javascript
<div id=""firstDiv"">",
                Langauge = "JavaScript",
                CodeStub = "",
            });

            modelBuilder.Entity<Question>().HasData(new Question
            {
                QuestionId = 6,
                Description = "Given a farenheit variable, convert it to Celcius ",
                Langauge = "JavaScript",
                CodeStub = @"function toCelsius(fahrenheit) {
}",
            });

            modelBuilder.Entity<Question>().HasData(new Question
            {
                QuestionId = 7,
                Description = "Merge two sorted arrays and return it as a single array.",
                Langauge = "C#",
                CodeStub = @"
            public class Solution{
                public int[] MergeArrays(int[] nums1, int[]nums2) {
          
                }
            }",
            });

            modelBuilder.Entity<TestStatus>().HasData(new TestStatus
            {
                TestStatusId = 1,
                QuestionId = 1,
                SubmissionId =1
            });


        }
    }
}
