using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectECommerce.DataAccess.Data;
using ProjectECommerce.DataAccess.Repoistory.IRepository;
using ProjectECommerce.Models;
using ProjectECommerce.Utility;

namespace ProjectECommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin + "," + SD.Role_Employee)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public UserController(IUnitOfWork unitOfWork,ApplicationDbContext context)
        {
            _context= context;
            _unitOfWork= unitOfWork;
                
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]

        public IActionResult GetAll()
        {
            var userList = _context.AppliactionUsers.ToList(); //AspNetUser
            var roles = _context.Roles.ToList(); //AspNetRoles
            var userRoles = _context.UserRoles.ToList(); //AspNetUserRoles

            foreach (var user in userList)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(r => r.Id == roleId).Name;
                if (user.CompanyId != null)
                {
                    user.Company = new Company()
                    {
                        Name = _unitOfWork.Company.Get(Convert.ToInt32(user.CompanyId)).Name
                    };
                   
                }
                if (user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""

                    };
                }
            }
            //Remove Admin Role User
            var adminUser = userList.FirstOrDefault(u => u.Role == SD.Role_Admin);
            userList.Remove(adminUser);
            return Json(new { data = userList });
        }
        [HttpPost]

        public IActionResult LockUnlock([FromBody]string id)
        {
            bool isLocked = false;
            var userInDb = _context.AppliactionUsers.FirstOrDefault(au => au.Id == id);
            if(userInDb == null)
                return Json(new {success=false,
                message="Something went wrong while lock and unlock user"});
            if(userInDb != null && userInDb.LockoutEnd>DateTime.Now)
            {
                userInDb.LockoutEnd = DateTime.Now;
                isLocked = false;
            }
            else
            {
                userInDb.LockoutEnd = DateTime.Today.AddYears(100);
                isLocked = true;
            }
            _context.SaveChanges();
            return Json(new {success=true,message=isLocked==true?
                "User successfully locked":"User successfully unlock"});
        }

        #endregion
    }
}
