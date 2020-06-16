using LiteCommerce.BusiniessLayer;
using LiteCommerce.DomainModels;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [AuthorizeRedirect(Roles = WebUserRoles.ADMINISTRATOR)]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index(int page = 1, string searchValue = "",string country="")
        {
            var model = new Models.EmployeePaginationResult()
            {
                Page = page,
                PageSize = AppSetting.DefaultPageSize,
                RowCount = HumanResourceBLL.Employee_Count(searchValue,country),
                Data = HumanResourceBLL.Employee_List(page, AppSetting.DefaultPageSize, searchValue,country),
                searchValue = searchValue,
                country = country,
            };
            return View(model);
        }

        /// <summary>
        /// Add or edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add Employee";
                Employee newEmployee = new Employee();
                newEmployee.EmployeeID = 0;
                return View(newEmployee);
            }
            else
            {
                Employee editEmployee = HumanResourceBLL.GetEmployee(Convert.ToInt32(id));
                if (editEmployee == null)
                    return RedirectToAction("Index");
                ViewBag.Title = "Edit Employee";
                return View(editEmployee);
            }
        }

        [HttpPost]
        public ActionResult Input(Employee data, HttpPostedFileBase files = null, string Email = "")
        {
            if (data.Country == "null")
            {
                ModelState.AddModelError("errorAddr", "Vui lòng chọn quốc gia");
            }
            if (data.Notes == null)
            {
                data.Notes = "";
            }
            if (data.Address == null)
            {
                data.Address = "";
            }
            if (data.HomePhone == null)
            {
                data.HomePhone = "";
            }
            if (data.City == null)
            {
                data.City = "";
            }
            if (data.PhotoPath == null)
            {
                data.PhotoPath = "";
            }
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            if (string.IsNullOrEmpty(data.FirstName))
                ModelState.AddModelError("ErrorFirstName", "First name is required");
            if (string.IsNullOrEmpty(data.LastName))
                ModelState.AddModelError("ErrorLastName", "Last Name is required");
            if (string.IsNullOrEmpty(data.Password))
                ModelState.AddModelError("ErrorPassword", "Password is required");
            /*if (string.IsNullOrEmpty(data.Email))
            {
                data.Email = "";
            }
            if (string.IsNullOrEmpty(data.Address))
            {
                data.Address = "";
            }
            if (string.IsNullOrEmpty(data.Notes))
            {
                data.Notes = "";
            }
            if (string.IsNullOrEmpty(data.HomePhone))
            {
                data.HomePhone = "";
            }*/
            if (files != null)
            {
                string get = DateTime.Now.ToString("ddMMyyyhhmmss");
                string fileExtension = Path.GetExtension(files.FileName);
                string fileName = get + fileExtension;
                string path = Path.Combine(Server.MapPath("~/Images"), fileName);
                data.PhotoPath = fileName;
                files.SaveAs(path);
            }
            if (!EncodeMD5.IsMD5(data.Password))
            {
                data.Password = EncodeMD5.GetMD5(data.Password);
            }
            if (data.EmployeeID == 0)
            {
                bool result = AccountBLL.GetEmail(Email);
                if (result)
                {
                    ModelState.AddModelError("errorEmailDuplicate", "Email đã tồn tại. Vui lòng nhập email khác!");
                    return View();
                }
                else
                {
                    
                    int employeeID = HumanResourceBLL.Employee_Add(data);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                bool updateResult = HumanResourceBLL.Employee_Update(data);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string method = "", int[] employeeIDs = null)
        {
            if (employeeIDs != null)
            {
                HumanResourceBLL.Employee_Delete(employeeIDs);
            }
            return RedirectToAction("Index");
        }
    }
}