using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;

namespace WebApplication1.Controllers

{
    [Route("/Admin/Student")]
    public class StudentController : Controller
    {
       
       private List<Student> _students = new List<Student>();
       public StudentController() 
       {
            //Tạo danh sách sinh viên với 4 dữ liệu mẫu
            _students = new List<Student>()
            { 
                new Student() { Id = 101, Name = "Hải Nam", Branch = Branch.IT,
                Gender = Gender.Male, IsRegular=true,
                Address = "A1-2018", Email = "nam@g.com" },

                new Student() { Id = 102, Name = "Minh Tú", Branch = Branch.BE,
                Gender = Gender.Female, IsRegular=true,
                Address = "A1-2019", Email = "tu@g.com" },

                new Student() { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE,
                Gender = Gender.Male, IsRegular=false,
                Address = "A1-2020", Email = "phong@g.com" },

                new Student() { Id = 104, Name = "Xuân Mai", Branch = Branch.EE,
                Gender = Gender.Female, IsRegular = false,
                Address = "A1-2021", Email = "mai@g.com" }

            };
       }
        [HttpGet("List")]
        [HttpGet("/")]
        public IActionResult Index()
        {
            //Trả về View Index.cshtml cùng Model là danh sách sv listStudents
            return View(_students);
        }

        [HttpGet("Add")]
        public IActionResult Create()
        {
            //lấy danh sách các giá trị Gender để hiển thị radio button trên form
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            //lấy danh sách các giá trị Branch để hiển thị select-option trên form
            //Để hiển thị select-option trên View cần dùng List<SelectListItem>
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
            };
            return View();
        }
        [HttpPost("Add")]
        public IActionResult Create(Student s)
        {
            s.Id = _students.Last<Student>().Id + 1;
            _students.Add(s);
            return View("Index", _students);
        }

        public IActionResult Create(Student s, IFormFile Image)
        {
            //giải thích: Câu 9: Nếu thông tin file trong input ở trong file create mà khác rỗng tức đã lấy
            //file thì sẽ lấy tên file ở dòng lệnh 76 và 77

            if (Image != null)
            {
                var fileName = Image.FileName;
                fileName = Path.GetFileName(fileName);

                // giải thích: Câu 9: gán tên file vào cột Anh đã tạo ở bên file Index
                s.Anh = fileName;


            }
            //giải thích: Câu 9: Nếu thông tin file trong input ở trong file create mà rỗng tức chưa truyền link file vào thì sẽ hiển thị ra 
            // "Chưa chọn file ảnh đại diện" ở cột Anh đã tạo ở bên file Index
            else
            {
                s.Anh = "Chưa chọn file ảnh đại diện!";
            }

            s.Id = _students.Last<Student>().Id + 1;
            _students.Add(s);
            return View("Index", _students);
        }
    }
}

