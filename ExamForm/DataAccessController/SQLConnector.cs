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

    // TODO - Implement async?
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
                dP.Add("@Email", user.EmailAddress);
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

        // TODO Add get user by userId, finish this method
        /// <summary>
        /// Get user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUser(int userId)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                User CurrentUser;
                var p = new DynamicParameters();
                p.Add("@UserId", userId);

                CurrentUser = connection.Query<User>(
                    "dbo.sp_User_GetUser_By_Id",
                    p,
                    commandType: CommandType.StoredProcedure).First();

                return CurrentUser;
            }
        }

        /// <summary>
        /// Get User by username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User GetUser(string username, string password)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                var p = new DynamicParameters();
                p.Add("@UsernameIn", username);
                p.Add("@PasswordIn", password);

                return connection.QueryFirst<User>(
                    "dbo.sp_User_GetUser_By_Login",
                    p,
                    commandType: CommandType.StoredProcedure);
            }
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
        public IList<Semester> GetAllSemesters()
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                return connection.Query<Semester>("dbo.sp_Course_GetSemesters", null, commandType: CommandType.StoredProcedure).ToArray();
            }
        }

        /// <summary>
        /// Gets semester by Id
        /// </summary>
        /// <param name="semesterId"></param>
        /// <returns></returns>
        public Semester GetSemester(int semesterId)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                var p = new DynamicParameters();
                p.Add("@SemesterId", semesterId);
                return connection.QueryFirst<Semester>("dbo.sp_Course_GetSemesters", p, commandType: CommandType.StoredProcedure);
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
        public IList<Course> GetAllCourses()
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                return connection.Query<Course>("dbo.sp_Course_GetCourses", commandType: CommandType.StoredProcedure).ToArray();
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

        public async Task<bool> AddChoice(int questionId, string choiceText, bool IsCorrect)
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

        public IList<Exam> GetExamByCourseId(int courseId)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString()))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@CourseId", courseId);
                return connection.Query<Exam>("dbo.sp_Exam_GetExam_By_CourseId", p, commandType: CommandType.StoredProcedure).ToList();
            }
        }


    }
}
