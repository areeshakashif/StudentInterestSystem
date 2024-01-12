using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.Repository;
namespace MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        StudentRepository studentR;
        public StudentController(
            
            IWebHostEnvironment environment)
        {
       
            _environment = environment;
            studentR = new StudentRepository();
        }
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult StudentForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StudentForm(Student s)
        {
            Console.WriteLine("..............");
            if (!ModelState.IsValid)
            {
                // If ModelState is not valid, return to the form with validation errors
                return View(s);
            }

            bool a = studentR.InsertStudent(s);
            if (a)
            {
                // Student inserted successfully
                // Redirect to a success page or any other appropriate action
                return RedirectToAction("Index", "Student");
            }
            else
            {
                // Insertion failed, handle accordingly (maybe return to the form with an error message)
                ModelState.AddModelError(string.Empty, "Failed to insert student.");
                return View(s);
            }
        }
        public IActionResult Index()
        {
            ViewBag.StudentData= studentR.getAllStudents();
            return View();
        }
        public IActionResult StudentView(int Id)
        {
            Student s = studentR.GetStudentById(Id);
            ViewBag.info = s;
            return View();
        }
        public IActionResult Remove(int studentId)
        {
            Student s = studentR.GetStudentById(studentId);
            studentR.DeleteStudent(s.Id);
            return RedirectToAction("Index", s);
            //return View(s);
        }
        public IActionResult Edit(int Id)
        {
            Student s = studentR.GetStudentById(Id);
            ViewBag.info = s;
            Console.WriteLine(s.Name);
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Student updatedStudent)
        {
            if (ModelState.IsValid)
            {
                // If the model state is valid, proceed with updating the student
                studentR.EditStudent(updatedStudent);
                return RedirectToAction("Index", "Student"); // Redirect to the student list or another appropriate action
            }
            // If the model state is not valid, re-render the edit form with validation errors
            return View("Edit", updatedStudent);
        }

        public IActionResult StudentList()
        {
            // Your logic to retrieve and display student list
            return RedirectToAction("Index"); // Render the student list view
        }
        public IActionResult Delete(int Id)
        {
            studentR.DeleteStudent(Id);
            return RedirectToAction("Index");
        }
        public IActionResult StudentDashboard()
        {
            List<string> topInterests = studentR.GetTop5Interests();
            ViewBag.info = topInterests;
            List<string> bottomInterests = studentR.GetBottom5Interests();
            ViewBag.infor = bottomInterests;
            int distinctInterestsCount = studentR.GetDistinctInterestsCount();
            ViewBag.inform = distinctInterestsCount;
            var students = studentR.getAllStudents();

            // Dictionary to store student statuses
            var studentStatuses = studentR.GetStudentStatusCounts(students);
            ViewBag.i = studentStatuses;
            List<Dictionary<string, int>> departmentDistribution = studentR.GetDepartmentDistribution();
            ViewBag.b = departmentDistribution;
            List<Dictionary<string, int>> studentsCreatedDailyLast30Days = studentR.GetStudentsCreatedDailyLast30Days();
            ViewBag.c = studentsCreatedDailyLast30Days;
            List<Dictionary<string, int>> degreeDistribution = studentR.GetDegreeDistribution();
            ViewBag.b1 = degreeDistribution;
            List<Dictionary<string, int>> genderDistribution = studentR.GetGenderDistribution();
            ViewBag.b2 = genderDistribution;
            List<Dictionary<string, int>> provincialDistribution = studentR.GetProvincialDistribution();
            ViewBag.b3 = provincialDistribution;
            List<Dictionary<string, int>> last30DaysData = studentR.GetLast30DaysStudentRegistrations();
            ViewBag.b4 = last30DaysData;
            List<Dictionary<DateTime, int>> last24HoursData = studentR.GetLast24HoursStudentActions();
            ViewBag.e = last24HoursData;
            Dictionary<int, int> ageDistribution = studentR.GetAgeDistribution();
            ViewBag.e1 = ageDistribution;
            return View();
        }
    }
}