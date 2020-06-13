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
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            var model = new Models.EmployeePaginationResult()
            {
                Page = page,
                PageSize = AppSetting.DefaultPageSize,
                RowCount = HumanResourceBLL.Employee_Count(searchValue),
                Data = HumanResourceBLL.Employee_List(page, AppSetting.DefaultPageSize, searchValue),
                searchValue = searchValue,
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
        public ActionResult Input(Employee data, HttpPostedFileBase files)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            if (string.IsNullOrEmpty(data.FirstName))
                ModelState.AddModelError("ErrorFirstName", "First name is required");
            if (string.IsNullOrEmpty(data.LastName))
                ModelState.AddModelError("ErrorLastName", "Last Name is required");
            if (string.IsNullOrEmpty(data.Title))
            {
                data.Title = "";
            }
            if (string.IsNullOrEmpty(data.Email))
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
            }
            string get = DateTime.Now.ToString("ddMMyyyhhmmss");
            string fileExtension = Path.GetExtension(files.FileName);
            string fileName = get + fileExtension;
            string path = Path.Combine(Server.MapPath("~/Images"), fileName);
            data.PhotoPath = fileName;
            files.SaveAs(path);
            if (!EncodeMD5.IsMD5(data.Password))
            {
                data.Password = EncodeMD5.GetMD5(data.Password);
            }
            if (data.EmployeeID == 0)
            {
                int employeeID = HumanResourceBLL.Employee_Add(data);
                return RedirectToAction("Index");
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