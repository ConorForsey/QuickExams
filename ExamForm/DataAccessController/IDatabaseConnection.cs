using QuickExamLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickExamLibrary.DataAccessController
{
    /// <summary>
    /// Interface for easily adding new connection queries
    /// </summary>
    public interface IDatabaseConnection
    {
        // User
        Task<bool> CreateNewUserAsync(User user);
        Task<bool> AuthenticateUserAsync(string username, string password);
        User GetUser(int userId);                               // TODO - Delete?
        User GetUser(string username, string password);

        // Course
        // TODO - Add edit course
        Task<bool> NewSemesterAsync(string start, string end);
        IList<Semester> GetAllSemesters();
        Semester GetSemester(int semesterId);
        Task<bool> NewCourseAsync(string courseTitle, int teacherId, int semesterId);
        IList<Course> GetAllCourses();

        // Exam
        Task<bool> NewExamAsync(string examTitle, int courseId, DateTime? startTime, DateTime? endTime);
        Task<int> AddQuestionAsync(int examId, string questionText);
        Task<bool> AddChoice(int questionId, string choiceText, bool IsCorrect);
        IList<Exam> GetExamByCourseId(int courseId);


    }
}
