using QuickExamLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace QuickExamLibrary.DataAccessController
{
    /// <summary>
    /// Used for querying the database
    /// </summary>
    /// 

    public class SQLConnector : IDatabaseConnection
    {
        // User
        /// <summary>
        /// Queries the database to see if the user exists
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> AuthenticateUserAsync(string username, string password)
        {
            int UserExists = -1;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                var dP = new DynamicParameters();
                dP.Add("@Username", username);
                dP.Add("@Password", password);

                dP.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                await connection.ExecuteAsync("dbo.sp_User_Login", dP, commandType: CommandType.StoredProcedure);

                //Returns -1 if user does not exist and 1 if they do
                UserExists = dP.Get<int>("@ReturnValue");
                if (UserExists == 1)
                {
                    return true;
                }
                else return false;
            }
        }

        /// <summary>
        /// Adds a new user to the database if the first and last name do not already exist
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> CreateNewUserAsync(User user)
        {          
            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                int IsCreated = -1;

                var dP = new DynamicParameters();
                dP.Add("@Firstname", user.FirstName);
                dP.Add("@Lastname", user.LastName);
                dP.Add("@Email", user.Email);
                dP.Add("@RoleId", user.RoleType);
                dP.Add("@Username", user.Username);
                dP.Add("@Password", user.Password);

                //Returns value 0 if inserted or -1 if user already exists
                dP.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                await connection.ExecuteAsync("dbo.sp_User_NewUser", dP, commandType: CommandType.StoredProcedure);

                IsCreated = dP.Get<int>("@ReturnValue");

                if (IsCreated == 0)
                {
                    return true;
                }
                else return false;
            }

        }

        /// <summary>
        /// Get User by username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> GetUserAsync(string username, string password)
        {
            using (var connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                await connection.OpenAsync();
                var p = new DynamicParameters();
                p.Add("@UsernameIn", username);
                p.Add("@PasswordIn", password);

                return await connection.QueryFirstAsync<User>(
                    "dbo.sp_User_GetUser_By_Login",
                    p,
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Returns a list of all the teachers in the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllTeachersAsync()
        {
            using (var connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<User>("dbo.sp_User_GetAllTeachers", commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Returns 1 if the email address exists in the database
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public async Task<bool> EmailAddressExists(string emailAddress)
        {
            using (var connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                await connection.OpenAsync();
                var p = new DynamicParameters();
                p.Add("@EmailAddress", emailAddress);

                int returnValue = await connection.QueryFirstAsync<int>("dbo.sp_User_EmailExists", p, commandType: CommandType.StoredProcedure);

                if (returnValue == 1)
                {
                    return true;
                }
                else return false;
            }
        }

        /// <summary>
        /// Change the users password using their userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> ChangePassword(int userId, string existingPassword, string newPassword)
        {
            int IsCreated = -1;
            using (var connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                await connection.OpenAsync();
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@ExistingPassword", existingPassword);
                p.Add("@NewPassword", newPassword);
                p.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                int returnValue = await connection.ExecuteAsync("dbo.sp_User_ChangePassword", p, commandType: CommandType.StoredProcedure);

                IsCreated = p.Get<int>("@ReturnValue");

                if (IsCreated == 0)
                {
                    return true;
                }
                else return false;
            }
        }

        public async Task<bool> ChangePassword(string emailAddress, string newPassword)
        {
            int IsCreated = -1;
            if (await EmailAddressExists(emailAddress))
            {
                using (var connection = new SqlConnection(GlobalConfig.ConnectionString()))
                {
                    await connection.OpenAsync();
                    var p = new DynamicParameters();
                    p.Add("@EmailAddress", emailAddress);
                    p.Add("@NewPassword", newPassword);

                    p.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    int returnValue = await connection.ExecuteAsync("dbo.sp_User_ForgotPassword", p, commandType: CommandType.StoredProcedure);

                    IsCreated = p.Get<int>("@ReturnValue");

                    if (IsCreated == 0)
                    {
                        return true;
                    }
                    else return false;
                }
            }
            else return false;           
        }

        // Courses
        /// <summary>
        /// Create a new semester
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<bool> NewSemesterAsync(string start, string end)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                int IsCreated = -1;

                var dP = new DynamicParameters();
                dP.Add("@StartDate", start);
                dP.Add("@EndDate", end);
                dP.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                await connection.ExecuteAsync("dbo.sp_Course_NewSemester", dP, commandType: CommandType.StoredProcedure);

                IsCreated = dP.Get<int>("@ReturnValue");

                if (IsCreated == 0)
                {
                    return true;
                }
                else return false;
            }
        }

        /// <summary>
        /// Get all semesters
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Semester>> GetAllSemestersAsync()
        {
            IEnumerable<Semester> semesters;
            using (var connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                await connection.OpenAsync();
                semesters = await connection.QueryAsync<Semester>("dbo.sp_Course_GetSemesters", null, commandType: CommandType.StoredProcedure);
                return semesters.ToList();
            }
        }

        /// <summary>
        /// Gets semester by Id
        /// </summary>
        /// <param name="semesterId"></param>
        /// <returns></returns>
        public async Task<Semester> GetSemesterAsync(int semesterId)
        {
            using (var connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                await connection.OpenAsync();
                var p = new DynamicParameters();
                p.Add("@SemesterId", semesterId);
                return await connection.QueryFirstAsync<Semester>("dbo.sp_Course_GetSemester_By_Id", p, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Inserts a new course
        /// </summary>
        /// <param name="courseTitle"></param>
        /// <param name="teacherId"></param>
        /// <param name="semesterId"></param>
        /// <returns></returns>
        public async Task<bool> NewCourseAsync(string courseTitle, int teacherId, int semesterId)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                int IsCreated = -1;

                var dP = new DynamicParameters();
                dP.Add("@CourseTitle", courseTitle);
                dP.Add("@TeacherId", teacherId);
                dP.Add("@SemesterId", semesterId);
                dP.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                await connection.ExecuteAsync("dbo.sp_Course_NewCourse", dP, commandType: CommandType.StoredProcedure);

                IsCreated = dP.Get<int>("@ReturnValue");

                if (IsCreated == 0)
                {
                    return true;
                }
                else return false;
            }
        }

        /// <summary>
        /// Gets all courses
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Course>> GetAllCoursesAsync()
        {
            IEnumerable<Course> courses;
            using (var connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                await connection.OpenAsync();
                courses = await connection.QueryAsync<Course>("dbo.sp_Course_GetCourses", commandType: CommandType.StoredProcedure);
                return courses.ToList();
            }
        }

        /// <summary>
        /// Enrolls a specifed student into the specifed course
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> EnrollStudentAsync(int courseId, int userId)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                var dP = new DynamicParameters();
                dP.Add("@CourseId", courseId);
                dP.Add("@UserId", userId);
                dP.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                await connection.ExecuteAsync("dbo.sp_Course_Enroll", dP, commandType: CommandType.StoredProcedure);

                int IsCreated = dP.Get<int>("@ReturnValue");

                if (IsCreated == 0)
                {
                    return true;
                }
                else return false;
            }
        }

        /// <summary>
        /// Gets users enrolled courses using student ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Course>> GetCoursesByUserId(int userId)
        {
            IEnumerable<Course> courses;
            using (var connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                await connection.OpenAsync();
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                courses = await connection.QueryAsync<Course>("dbo.sp_Course_Enrolled_By_UserId", p, commandType: CommandType.StoredProcedure);
                return courses.ToList();
            }
        }

        //Exam
        /// <summary>
        /// Inserts a new exam
        /// </summary>
        /// <param name="examTitle"></param>
        /// <param name="courseId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<bool> NewExamAsync(string examTitle, int courseId, DateTime? startTime, DateTime? endTime)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                int IsCreated = -1;

                var dP = new DynamicParameters();
                dP.Add("@Title", examTitle);
                dP.Add("@CourseId", courseId);
                dP.Add("@StartTime", startTime);
                dP.Add("@EndTime", endTime);
                dP.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                await connection.ExecuteAsync("dbo.sp_Exam_NewExam", dP, commandType: CommandType.StoredProcedure);

                IsCreated = dP.Get<int>("@ReturnValue");

                if (IsCreated == 0)
                {
                    return true;
                }
                else return false;
            }
        }

        /// <summary>
        /// Adds a new question to the database and returns the question id
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="questionText"></param>
        /// <returns></returns>
        public async Task<int> AddQuestionAsync(int examId, string questionText)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                var dP = new DynamicParameters();
                dP.Add("@ExamId", examId);
                dP.Add("@QuestionText", questionText);

                dP.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                await connection.ExecuteAsync("dbo.sp_Exam_NewQuestion", dP, commandType: CommandType.StoredProcedure);

                return dP.Get<int>("@ReturnValue");
            }
        }

        /// <summary>
        /// Adds a choice to a question
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="choiceText"></param>
        /// <param name="IsCorrect"></param>
        /// <returns></returns>
        public async Task<bool> AddChoiceAsync(int questionId, string choiceText, bool IsCorrect)
        {
            int boolToBit;
            if (IsCorrect == true)
            {
                boolToBit = 1;
            }
            else boolToBit = 0;

            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                var dP = new DynamicParameters();
                dP.Add("@QuestionId", questionId);
                dP.Add("@ChoiceText", choiceText);
                dP.Add("@IsCorrect", boolToBit);

                dP.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                await connection.ExecuteAsync("dbo.sp_Exam_NewChoice", dP, commandType: CommandType.StoredProcedure);


                int IsCreated = dP.Get<int>("@ReturnValue");

                if (IsCreated == 0)
                {
                    return true;
                }
                else return false;
            }
        }

        /// <summary>
        /// Returns a list of exams in a specified course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<IList<Exam>> GetExamByCourseIdAsync(int courseId)
        {
            IEnumerable<Exam> exams;
            using (var connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                await connection.OpenAsync();
                DynamicParameters p = new DynamicParameters();
                p.Add("@CourseId", courseId);
                exams = await connection.QueryAsync<Exam>("dbo.sp_Exam_GetExam_By_CourseId", p, commandType: CommandType.StoredProcedure);
                return exams.ToList();
            }
        }

        /// <summary>
        /// Returns all the exams that have not been completed by the current student
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Exam>> GetIncompleteExamsAsync(int userId)
        {
            IEnumerable<Exam> incompExams;
            using (var connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                await connection.OpenAsync();
                DynamicParameters p = new DynamicParameters();
                p.Add("@UserId", userId);   
                
                incompExams = await connection.QueryAsync<Exam>("dbo.sp_Exam_GetIncompleteExams", p, commandType: CommandType.StoredProcedure);

                return incompExams;
            }
        }

        /// <summary>
        /// Fetches questions and their choices for a given examId
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        public async Task<IList<Question>> GetQuestionsAsync(int examId)
        {
            IEnumerable<Question> questions;
            IEnumerable<Choice> choices;
            using (var connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                await connection.OpenAsync();
                DynamicParameters p = new DynamicParameters();
                p.Add("@ExamId", examId);

                questions = await connection.QueryAsync<Question>("dbo.sp_Exam_GetExamQuestions_ByExamId", p, commandType: CommandType.StoredProcedure);
                choices = await connection.QueryAsync<Choice>("dbo.sp_Exam_GetExamChoices_ByExamId", p, commandType: CommandType.StoredProcedure);

                foreach (Question q in questions)
                {                 
                    foreach (Choice c in choices)
                    {
                        if(c.QuestionId == q.QuestionId)
                        {
                            q.AddChoice(c);
                        }                      
                    }                  
                }

                return questions.ToList();
            }
        }

        /// <summary>
        /// Creates a new entry into the student exam table and returns the new studentExamId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="examId"></param>
        /// <returns></returns>
        public async Task<int> FinishExamAsync(int userId, int examId)
        {
            using (var connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                await connection.OpenAsync();
                DynamicParameters p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@ExamId", examId);
                p.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                await connection.ExecuteAsync("dbo.sp_Exam_FinishExam", p, commandType: CommandType.StoredProcedure);

                return p.Get<int>("@ReturnValue");
            }
        }

        /// <summary>
        /// Inputs the students question choice into the AnsweredQuestions table and returns 0 if successful
        /// </summary>
        /// <param name="choiceId"></param>
        /// <param name="questionId"></param>
        /// <param name="studentExamId"></param>
        /// <returns></returns>
        public async Task<bool> AnswerQuestionAsync(int choiceId, int questionId, int studentExamId)
        {
            int IsCreated = -2;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@ChoiceId", choiceId);
                p.Add("@QuestionId", questionId);
                p.Add("@StudentExamId", studentExamId);
                p.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                await connection.ExecuteAsync("dbo.sp_Exam_AnswerQuestion", p, commandType: CommandType.StoredProcedure);

                IsCreated = p.Get<int>("@ReturnValue");

                if (IsCreated == 0)
                {
                    return true;
                }
                else return false;
            }
        }

    }
}
