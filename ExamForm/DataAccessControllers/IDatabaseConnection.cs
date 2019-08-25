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
        Task<User> GetUserAsync(string username, string password);
        Task<IEnumerable<User>> GetAllTeachersAsync();
        Task<bool> EmailAddressExists(string emailAddress);
        Task<bool> ChangePassword(int userId, string existingPassword, string newPassword);
        Task<bool> ChangePassword(string emailAddress, string newPassword);

        // Course
        // TODO - Add edit course
        Task<bool> NewSemesterAsync(string start, string end);
        Task<IList<Semester>> GetAllSemestersAsync();
        Task<Semester> GetSemesterAsync(int semesterId);
        Task<bool> NewCourseAsync(string courseTitle, int teacherId, int semesterId);
        Task<IList<Course>> GetAllCoursesAsync();
        Task<bool> EnrollStudentAsync(int courseId, int userId);
        Task<IEnumerable<Course>> GetCoursesByUserId(int userId);

        // Exam
        Task<bool> NewExamAsync(string examTitle, int courseId, DateTime? startTime, DateTime? endTime);
        Task<int> AddQuestionAsync(int examId, string questionText);
        Task<bool> AddChoiceAsync(int questionId, string choiceText, bool IsCorrect);
        Task<IList<Exam>> GetExamByCourseIdAsync(int courseId);
        Task<IEnumerable<Exam>> GetIncompleteExamsAsync(int userId);
        Task<IList<Question>> GetQuestionsAsync(int examId);
        Task<int> FinishExamAsync(int userId, int examId);
        Task<bool> AnswerQuestionAsync(int choiceId, int questionId, int studentExamId);

    }
}
