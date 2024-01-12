using System;
using MySql.Data.MySqlClient;
using MVC.Models;
using System.Drawing;
namespace MVC.Models.Repository
{
    public class StudentRepository
    {
        string connstring = "server=localhost;port=3306;user=root;password=123456;database=studentdb";
        string connection = "server=localhost;port=3306;user=root;password=123456;database=date";
        public List<Student> getAllStudents()
        {
            List<Student> students=new List<Student>();

            MySqlConnection con = new MySqlConnection(connstring);
            
            con.Open();
            string query = "SELECT * FROM student";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader dr = cmd.ExecuteReader();
            bool check = dr.HasRows;
            while (dr.Read())
            {
                Student student = new Student();
                student.Id = Convert.ToInt32(dr["Id"]);
                student.Name = dr["Name"].ToString();
                student.RollNumber = dr["RollNumber"].ToString();
                student.DOB = dr["DOB"].ToString();
                student.Email = dr["Email"].ToString();
                student.Gender = dr["Gender"].ToString();
                student.City = dr["City"].ToString();
                student.Interest = dr["Interest"].ToString();
                student.Department = dr["Department"].ToString();
                student.DegreeTitle = dr["DegreeTitle"].ToString();
                student.Subject = dr["Subject"].ToString();
                student.StartDate = Convert.ToDateTime(dr["StartDate"]);
                student.EndDate = Convert.ToDateTime(dr["EndDate"]);
                students.Add(student);
                // Do something with the 'student' object here, like adding it to a list or using it further
            }
            return students;
        }
        public void EditStudent(Student updatedStudent)
        {
            MySqlConnection con = new MySqlConnection(connstring);

            con.Open();
            string query = "UPDATE student SET Name=@Name, RollNumber=@RollNumber, DOB=@DOB, Email=@Email, Gender=@Gender, City=@City, Interest=@Interest, Department=@Department, DegreeTitle=@DegreeTitle, Subject=@Subject, StartDate=@StartDate, EndDate=@EndDate WHERE Id=@Id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", updatedStudent.Id);
            cmd.Parameters.AddWithValue("@Name", updatedStudent.Name);
            cmd.Parameters.AddWithValue("@RollNumber", updatedStudent.RollNumber);
            cmd.Parameters.AddWithValue("@DOB", updatedStudent.DOB);
            cmd.Parameters.AddWithValue("@Email", updatedStudent.Email);
            cmd.Parameters.AddWithValue("@Gender", updatedStudent.Gender);
            cmd.Parameters.AddWithValue("@City", updatedStudent.City);
            cmd.Parameters.AddWithValue("@Interest", updatedStudent.Interest);
            cmd.Parameters.AddWithValue("@Department", updatedStudent.Department);
            cmd.Parameters.AddWithValue("@DegreeTitle", updatedStudent.DegreeTitle);
            cmd.Parameters.AddWithValue("@Subject", updatedStudent.Subject);
            cmd.Parameters.AddWithValue("@StartDate", updatedStudent.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", updatedStudent.EndDate);

            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void DeleteStudent(int studentId)
        {
            MySqlConnection con = new MySqlConnection(connstring);

            con.Open();
            string query = "DELETE FROM student WHERE Id=@Id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", studentId);

            cmd.ExecuteNonQuery();
            con.Close();
        }
        public bool InsertStudent(Student newStudent)
        {
            MySqlConnection con = new MySqlConnection(connstring);
            bool isInserted = false;

            try
            {
                con.Open();
                string query = "INSERT INTO student (Name, RollNumber, DOB, Email, Gender, City, Interest, Department, DegreeTitle, Subject, StartDate, EndDate) VALUES (@Name, @RollNumber, @DOB, @Email, @Gender, @City, @Interest, @Department, @DegreeTitle, @Subject, @StartDate, @EndDate)";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", newStudent.Name);
                cmd.Parameters.AddWithValue("@RollNumber", newStudent.RollNumber);
                cmd.Parameters.AddWithValue("@DOB", newStudent.DOB);
                cmd.Parameters.AddWithValue("@Email", newStudent.Email);
                cmd.Parameters.AddWithValue("@Gender", newStudent.Gender);
                cmd.Parameters.AddWithValue("@City", newStudent.City);
                cmd.Parameters.AddWithValue("@Interest", newStudent.Interest);
                cmd.Parameters.AddWithValue("@Department", newStudent.Department);
                cmd.Parameters.AddWithValue("@DegreeTitle", newStudent.DegreeTitle);
                cmd.Parameters.AddWithValue("@Subject", newStudent.Subject);
                cmd.Parameters.AddWithValue("@StartDate", newStudent.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", newStudent.EndDate);

                int rowsAffected = cmd.ExecuteNonQuery();
                isInserted = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            return isInserted;
        }

        public Student GetStudentById(int studentId)
        {
            MySqlConnection con = new MySqlConnection(connstring);
            Student student = new Student();
            con.Open();
            string query = "SELECT * FROM student WHERE Id = @Id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", studentId);

            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                
                student.Id = Convert.ToInt32(dr["Id"]);
                student.Name = dr["Name"].ToString();
                student.RollNumber = dr["RollNumber"].ToString();
                student.DOB = dr["DOB"].ToString();
                student.Email = dr["Email"].ToString();
                student.Gender = dr["Gender"].ToString();
                student.City = dr["City"].ToString();
                student.Interest = dr["Interest"].ToString();
                student.Department = dr["Department"].ToString();
                student.DegreeTitle = dr["DegreeTitle"].ToString();
                student.Subject = dr["Subject"].ToString();
                student.StartDate = Convert.ToDateTime(dr["StartDate"]);
                student.EndDate = Convert.ToDateTime(dr["EndDate"]);
            }
            con.Close();
            return student;
        }
        // Inside StudentRepository class

        public List<string> GetTop5Interests()
        {
            List<string> topInterests = new List<string>();

            using (MySqlConnection con = new MySqlConnection(connstring))
            {
                con.Open();
                string query = "SELECT Interest, COUNT(*) AS InterestCount FROM student GROUP BY Interest ORDER BY InterestCount DESC LIMIT 5";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string interest = dr["Interest"].ToString();
                    topInterests.Add(interest);
                }
                con.Close();
            }
            return topInterests;
        }

        public List<string> GetBottom5Interests()
        {
            List<string> bottomInterests = new List<string>();
            
            using (MySqlConnection con = new MySqlConnection(connstring))
            {
                con.Open();
                string query = "SELECT Interest, COUNT(*) AS InterestCount FROM student GROUP BY Interest ORDER BY InterestCount ASC LIMIT 5";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string interest = dr["Interest"].ToString();
                    bottomInterests.Add(interest);
                }
                con.Close();
            }
            return bottomInterests;
        }
        public int GetDistinctInterestsCount()
        {
            MySqlConnection con = new MySqlConnection(connstring);
            int distinctInterestCount = 0;

            try
            {
                con.Open();
                string query = "SELECT COUNT(DISTINCT Interest) AS DistinctInterestCount FROM student";
                MySqlCommand cmd = new MySqlCommand(query, con);

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    distinctInterestCount = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                throw;
            }
            finally
            {
                con.Close();
            }
            return distinctInterestCount;
        }
        public List<Dictionary<string, int>> GetDepartmentDistribution()
        {
            List<Dictionary<string, int>> departmentCounts = new List<Dictionary<string, int>>();

            MySqlConnection con = new MySqlConnection(connstring);
            con.Open();
            string query = "SELECT Department, COUNT(*) AS Count FROM student GROUP BY Department";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Dictionary<string, int> departmentCount = new Dictionary<string, int>();
                string department = dr["Department"].ToString();
                int count = Convert.ToInt32(dr["Count"]);
                departmentCount[department] = count;
                departmentCounts.Add(departmentCount);
            }

            con.Close();
            return departmentCounts;
        }

        public List<Dictionary<string, int>> GetDegreeDistribution()
        {
            List<Dictionary<string, int>> degreeCountList = new List<Dictionary<string, int>>();

            MySqlConnection con = new MySqlConnection(connstring);
            con.Open();
            string query = "SELECT DegreeTitle, COUNT(*) AS Count FROM student GROUP BY DegreeTitle";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Dictionary<string, int> degreeCount = new Dictionary<string, int>();
                string degree = dr["DegreeTitle"].ToString();
                int count = Convert.ToInt32(dr["Count"]);
                degreeCount.Add(degree, count);
                degreeCountList.Add(degreeCount);
            }

            con.Close();
            return degreeCountList;
        }

        public List<Dictionary<string, int>> GetGenderDistribution()
        {
            List<Dictionary<string, int>> genderCountList = new List<Dictionary<string, int>>();

            MySqlConnection con = new MySqlConnection(connstring);
            con.Open();
            string query = "SELECT Gender, COUNT(*) AS Count FROM student GROUP BY Gender";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Dictionary<string, int> genderCount = new Dictionary<string, int>();
                string gender = dr["Gender"].ToString();
                int count = Convert.ToInt32(dr["Count"]);
                genderCount.Add(gender, count);
                genderCountList.Add(genderCount);
            }

            con.Close();
            return genderCountList;
        }
        public List<Dictionary<string, int>> GetLast30DaysStudentRegistrations()
{
    List<Dictionary<string, int>> registrationData = new List<Dictionary<string, int>>();

    MySqlConnection con = new MySqlConnection(connstring);
    con.Open();

    string query = "SELECT DATE_FORMAT(StartDate, '%Y-%m-%d') AS FormattedDate, COUNT(*) AS count FROM student WHERE StartDate >= DATE_SUB(CURDATE(), INTERVAL 20 DAY) GROUP BY FormattedDate";
    MySqlCommand cmd = new MySqlCommand(query, con);
    MySqlDataReader dr = cmd.ExecuteReader();

    while (dr.Read())
    {
        Dictionary<string, int> data = new Dictionary<string, int>();
        string date = dr["FormattedDate"].ToString();
        int count = Convert.ToInt32(dr["count"]);
        data[date] = count;
        registrationData.Add(data);
    }

    con.Close();
    return registrationData;
}


        public List<Dictionary<DateTime, int>> GetLast24HoursStudentActions()
        {
            List<Dictionary<DateTime, int>> last24HoursData = new List<Dictionary<DateTime, int>>();

            // Simulating data for the last 24 hours for each student
            List<Student> students = getAllStudents(); // Fetch all students (you should have a method like this)

            foreach (var student in students)
            {
                Dictionary<DateTime, int> studentActions = new Dictionary<DateTime, int>();

                // Generate actions for the student for the last 24 hours
                DateTime currentDateTime = DateTime.Now;
                Random random = new Random(student.Id); // Using student's ID for consistency in random data

                for (int i = 0; i < 96; i++)
                {
                    DateTime actionDate = currentDateTime.AddMinutes(-i * 15);
                    int actionCount = random.Next(1, 10); // Simulated action count

                    studentActions.Add(actionDate, actionCount);
                }

                last24HoursData.Add(studentActions);
            }

            return last24HoursData;
        }

        public Dictionary<int, int> GetAgeDistribution()
        {
            Dictionary<int, int> ageDistribution = new Dictionary<int, int>();

            MySqlConnection con = new MySqlConnection(connstring);
            con.Open();
            string query = "SELECT DOB FROM student";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                DateTime dob = Convert.ToDateTime(dr["DOB"]);
                int age = CalculateAge(dob);

                if (ageDistribution.ContainsKey(age))
                    ageDistribution[age]++;
                else
                    ageDistribution.Add(age, 1);
            }

            con.Close();
            return ageDistribution;
        }

        private int CalculateAge(DateTime dob)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dob.Year;

            if (dob > today.AddYears(-age))
                age--;

            return age;
        }
        public List<Dictionary<string, int>> GetStudentsCreatedDailyLast30Days()
        {
            List<Dictionary<string, int>> studentsCreatedDailyList = new List<Dictionary<string, int>>();

            MySqlConnection con = new MySqlConnection(connstring);
            con.Open();

            DateTime startDate = DateTime.Today.AddDays(-30);
            DateTime endDate = DateTime.Today;

            string query = "SELECT DATE_FORMAT(StartDate, '%Y-%m-%d') AS FormattedDate, COUNT(*) AS count FROM student WHERE StartDate BETWEEN @StartDate AND @EndDate GROUP BY FormattedDate";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));

            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Dictionary<string, int> data = new Dictionary<string, int>();
                string formattedDate = dr["FormattedDate"].ToString();
                int count = Convert.ToInt32(dr["count"]);
                data[formattedDate] = count;
                studentsCreatedDailyList.Add(data);
            }

            con.Close();
            return studentsCreatedDailyList;
        }



        public List<Dictionary<string, int>> GetProvincialDistribution()
        {
            List<Dictionary<string, int>> provincialDistributionList = new List<Dictionary<string, int>>();

            MySqlConnection con = new MySqlConnection(connstring);
            con.Open();

            string query = "SELECT City, COUNT(*) AS TotalStudents FROM student GROUP BY City";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Dictionary<string, int> provincialDistribution = new Dictionary<string, int>();
                string province = dr["City"].ToString();
                int totalStudents = Convert.ToInt32(dr["TotalStudents"]);
                provincialDistribution.Add(province, totalStudents);
                provincialDistributionList.Add(provincialDistribution);
            }

            con.Close();
            return provincialDistributionList;
        }

        public string GetStudentStatus(DateTime startDate, DateTime endDate)
        {
            var now = DateTime.Now;
            if (endDate < now)
            {
                return "Graduated";
            }
            else if (startDate <= now && endDate >= now)
            {
                return "Studying";
            }
            else if (startDate > now)
            {
                return "Recently Enrolled";
            }
            else
            {
                return "About to Graduate";
            }
        }
        public Dictionary<string, int> GetStudentStatusCounts(List<Student> students)
        {
            var studentStatuses = new Dictionary<string, int>();

            foreach (var student in students)
            {
                string status = GetStudentStatus(student.StartDate, student.EndDate);

                if (studentStatuses.ContainsKey(status))
                {
                    studentStatuses[status]++;
                }
                else
                {
                    studentStatuses[status] = 1;
                }
            }

            return studentStatuses;
        }

    }
}
