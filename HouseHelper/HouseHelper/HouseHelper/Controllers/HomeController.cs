using HouseHelper.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using static HouseHelper.Controllers.FindHelperController;
using static HouseHelper.Controllers.JobSeekerController;

namespace HouseHelper.Controllers
{
    public class HomeController : Controller
    {
        private readonly HouseHelperContext _db = new HouseHelperContext();
        private readonly ILogger<HomeController> _logger;
        
        public string MD5Hash(string pass)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(new UTF8Encoding().GetBytes(pass));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                stringBuilder.Append(result[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return PartialView();
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(string fullname, string username, string phone, string password, string email, int roless, string returnUrl)
        {

            ViewData["ReturnUrl"] = returnUrl;
            var CheckUser = _db.Users.Where(a => a.UserName.ToLower() == username).FirstOrDefault();
            var CheckEmail = _db.Users.Where(a => a.Email.ToLower() == email.ToLower()).FirstOrDefault();
            var CheckRoless = _db.Roles.Where(a => a.RoleId == roless).FirstOrDefault();
            string a = Guid.NewGuid().ToString();
            var hashPass = MD5Hash(password);
            if (CheckUser == null && CheckEmail == null)
            {
                if (roless == 2)
                {
                    User newUser = new User
                    {
                        UserName = username,
                        Passwords = hashPass,
                        Email = email,
                        UserId = "Job" + a,
                        RoleId = roless
                    };
                    Jobseeker jobseeker = new Jobseeker
                    {
                        JobSeekerid = "Job" + a,
                        JobSeekername = fullname,
                        JobSeekerEmail = email,
                        JobSeekerphone = phone,
                    };
                    _db.Add(newUser);
                    _db.Add(jobseeker);

                }
                else if (roless == 3)
                {
                    User newUser = new User
                    {
                        UserName = username,
                        Passwords = hashPass,
                        Email = email,
                        UserId = "Find" + a,
                        RoleId = roless
                    };
                    Findhelper findhelper = new Findhelper
                    {
                        FindHelperid = "Find" + a,
                        FindHelperName = username,
                        FindHelperEmail = email,
                        FindHelperPhone = phone,
                    };
                    _db.Add(newUser);
                    _db.Add(findhelper);

                }
                await _db.SaveChangesAsync();
                TempData["Regis"] = "Tạo thành công tài khoản";
                return RedirectToAction("login");
            }
            TempData["RegisterFail"] = "Tài khoản đã tồn tại";
            return PartialView();
        }
        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }
        [HttpGet("login")]
        public IActionResult Login()
        {
            return PartialView();
        }
        [HttpPost("login")]
        public async Task<IActionResult> CheckLogin(string username, string password)
        {
            var hashPass = MD5Hash(password);
            var loginWithUserName = _db.Users.Where(p => p.UserName == username).Select(
                p => new User
                {
                    UserName = p.UserName,
                    Passwords = p.Passwords,
                    RoleId = p.RoleId,
                    UserId = p.UserId
                }
                ).ToList();
            var loginWithEmail = _db.Users.Where(p => p.Email == username).Select(
                a => new User
                {
                    Email = a.Email,
                    Passwords = a.Passwords,
                    RoleId = a.RoleId,
                    UserId = a.UserId
                }
                ).ToList();
            foreach (var item in loginWithUserName)
            {
                if (String.Compare(item.UserName.ToLower(), username.ToLower()) == 0)
                {
                    if (item.Passwords == MD5Hash(password))
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim("username", username));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                        claims.Add(new Claim(ClaimTypes.Name, username));
                        var User = _db.Users.Include(item => item.Jobseeker).Where(a => a.UserId == item.UserId).FirstOrDefault();
                        claims.Add(new Claim("userid", User.UserId));
                        claims.Add(new Claim("userImg", User?.Jobseeker?.JobSeekerimg ?? ""));
                        claims.Add(new Claim("JobseekerName", User?.Jobseeker?.JobSeekername ?? ""));
                        claims.Add(new Claim("CV", User?.Jobseeker?.JobSeekerCvid ?? ""));
                        var Img = _db.Users.Include(item => item.Findhelper).Where(a => a.UserId == item.UserId).FirstOrDefault();
                        claims.Add(new Claim("userid", User.UserId));
                        claims.Add(new Claim("FindhelperImg", Img?.Findhelper?.FindHelperImg ?? ""));
                        claims.Add(new Claim("FindhelperName", Img?.Findhelper?.FindHelperName ?? ""));
                        var Roles = _db.Roles.Where(roles => roles.RoleId == item.RoleId).FirstOrDefault();
                        claims.Add(new Claim(ClaimTypes.Role, Roles.RoleName));
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        if (Roles.RoleName == "Admins")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (Roles.RoleName == "JobSeeker")
                        {
                            return RedirectToAction("Index", "JobSeeker");
                        }
                        else if (Roles.RoleName == "FindHelper")
                        {
                            return RedirectToAction("Index", "FindHelper");
                        }

                    }
                    TempData["Error"] = "Tài Khoản hoặc Mật khẩu sai";
                }
            }

            foreach (var item in loginWithEmail)
            {
                if (String.Compare(item.Email.ToLower(), username.ToLower()) == 0)
                {
                    if (item.Passwords == MD5Hash(password))
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim("username", username));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                        claims.Add(new Claim(ClaimTypes.Name, username));
                        var UserId = _db.Users.Where(a => a.UserId == item.UserId).FirstOrDefault();
                        claims.Add(new Claim("userid", UserId.UserId));

                        var Roles = _db.Roles.Where(roles => roles.RoleId == item.RoleId).FirstOrDefault();
                        claims.Add(new Claim(ClaimTypes.Role, Roles.RoleName));
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);

                        if (Roles.RoleName == "Admins")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (Roles.RoleName == "JobSeeker")
                        {
                            return RedirectToAction("Index", "JobSeeker");
                        }
                        else if (Roles.RoleName == "FindHelper")
                        {
                            return RedirectToAction("Index", "FindHelper");
                        }
                    }

                    TempData["Error"] = "Tài Khoản hoặc Mật khẩu sai";
                }
            }
            TempData["Error"] = "Tài Khoản hoặc Mật khẩu sai";
            return PartialView("login");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult getAllJob()
        {
            var query = (from p in _db.Findhelpers
                         join q in _db.Jobs on p.FindHelperid equals q.FindHelperid
                         select new ListJobModel
                         {
                             id = q.Jobid,
                             name = p.FindHelperName,
                             title = q.JobTitle,
                             datestart = q.JobStart,
                             gender = q.JobGender,
                             description = q.JobDescription,
                             location = p.FindHelperLocation,
                             type = q.JobType,
                             img = q.JobImage,
                             datepost = q.JobDatePost

                         }).ToList();
            var totaljob = query.Count;
            ViewData["totaljob"] = totaljob;
            ViewBag.data1 = query;
            return View();
        }
        [HttpGet]
        public IActionResult getAllCV()
        {
            var date = DateTime.Now.Year;
            var query = (from p in _db.Jobseekers
                         join q in _db.Cvs on p.JobSeekerCvid equals q.Cvid
                         select new ListCVModel
                         {
                             id = p.JobSeekerid,
                             name = p.JobSeekername,
                             datestart = q.Workactualstart,
                             dob = date - p.JobSeekerdob.Value.Year,
                             Exp = date - q.Workstartdate.Value.Year,
                             location = p.JobSeekerLocation,
                             type = q.Worktype,
                             img = p.JobSeekerimg,
                             description = p.JobSeekerDescription,
                             gender = p.JobSeekergender,

                         }).ToList();
            var totalcv = query.Count;
            ViewData["TotalCV"] = totalcv;
            ViewBag.data = query;
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}