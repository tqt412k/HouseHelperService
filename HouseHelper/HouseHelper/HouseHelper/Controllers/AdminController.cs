using HouseHelper.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static HouseHelper.Controllers.FindHelperController;
using static HouseHelper.Controllers.JobSeekerController;

namespace HouseHelper.Controllers
{
    [Authorize(Roles = "Admins")]
    public class AdminController : Controller
    {
        private readonly HouseHelperContext _db = new HouseHelperContext();
        private readonly User userr = new User();
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
        
        public IActionResult Index()
        {
            var userid = User.FindFirstValue("userId");
            Admin? admin = _db.Admins.Where(x=>x.Adid == userid).FirstOrDefault();
           var query = (from p in _db.Findhelpers
                        join q in _db.Jobs on p.FindHelperid equals q.FindHelperid orderby q.JobDatePost descending 
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

                        }).Take(8).ToList();
            ViewBag.data = query;
            var query1 = (from p in _db.Jobseekers
                         join q in _db.Cvs on p.JobSeekerCvid equals q.Cvid
                         select new ListCVModel
                         {
                             id = p.JobSeekerid,
                             name = p.JobSeekername,
                             datestart = q.Workactualstart,
                             location = p.JobSeekerLocation,
                             type = q.Worktype,
                             img = p.JobSeekerimg,
                             description = p.JobSeekerDescription,
                             gender = p.JobSeekergender,

                         }).Take(8).ToList();
            ViewBag.data1 = query1;
            ViewData["query1"] = query1.Count();
            var UserCount = _db.Users.Count();
            var JobSeekCount = _db.Jobseekers.Count();
            var FindHelperCount = _db.Findhelpers.Count();
            var CvCount = _db.Cvs.Count();
            var JobCount = _db.Jobs.Count();
            ViewData["Img"] = admin.Adimg;
            ViewData["UserCount"] = UserCount;
            ViewData["JobSeekCount"] = JobSeekCount;
            ViewData["FindHelperCount"] = FindHelperCount;
            ViewData["CvCount"] = CvCount;
            ViewData["JobCount"] = JobCount;
            return View();

        }
        [HttpGet]
        public IActionResult changePassAdmin(string id)
        {
            var infor = _db.Admins.Where(x=>x.Adid == id).FirstOrDefault();
            return View(infor);
        }
        [HttpPost]
        public async Task<IActionResult> changePassAdmin(string password,Admin model)
        {
            var userid = User.FindFirstValue("userId");
            User? infor = _db.Users.Where(x => x.UserId == userid).FirstOrDefault();
            var hashPass = MD5Hash(password);
            User newadmin = new User
            {

                UserName = infor.UserName,
                Passwords = hashPass,
                Email = infor.Email,
                UserId = infor.UserId,
                RoleId = infor.RoleId,
            };
            _db.Remove(infor);
            _db.Add(newadmin);
            await _db.SaveChangesAsync();
            TempData["Regis"] = "Tạo thành công tài khoản";
            return RedirectToAction("Index", "Admin");
        }
        public IActionResult getAllJob()
        {
            var query = (from p in _db.Findhelpers
                         join q in _db.Jobs on p.FindHelperid equals q.FindHelperid
                         orderby q.JobDatePost descending
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
            ViewBag.data = query;
            ViewData["Count"] = query.Count();
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
                             idcv = p.JobSeekerCvid,


                         }).ToList();
            ViewBag.data = query;
            ViewData["countquery"] = query.Count();
            return View();  
        }
        public class ListFindHelper
        {
            public string id { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public string email { get; set; }
            public string username { get; set; }
            public string phone { get; set; }
            public string location { get; set; }
        }
        public class ListJobSeeker
        {
            public string id { get; set; }
            public string name { get; set; }
            public int dob { get; set; }
            public string email { get; set; }
            public string username { get; set; }
            public string phone { get; set; }
            public string location { get; set; }
        }
        public IActionResult getAllJobSeeker()
        {
            var date = DateTime.Now.Year;
            var query = (from p in _db.Jobseekers
                         join q in _db.Users on p.JobSeekerid equals q.UserId
                         select new ListJobSeeker
                         {
                             id = p.JobSeekerid,
                             name = p.JobSeekername,
                             dob = date - p.JobSeekerdob.Value.Year,
                             username = q.UserName,
                             email = q.Email,
                             phone = p.JobSeekerphone,
                             location = p.JobSeekerLocation
                         }).ToList();
            ViewBag.data = query;
            ViewData["countquery"] = query.Count();
            return View();
        }
        public IActionResult getAllFindHelper()
        {
            var date = DateTime.Now.Year;
            var query = (from p in _db.Findhelpers
                         join q in _db.Users on p.FindHelperid equals q.UserId
                         select new ListFindHelper
                         {
                             id = p.FindHelperid,
                             name = p.FindHelperName,
                             username = q.UserName,
                             email = q.Email,
                             phone = p.FindHelperPhone,
                             location = p.FindHelperLocation,
                             address = p.FindHelperAddress,
                         }).ToList();
            ViewBag.data = query;
            ViewData["countquery"] = query.Count();
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        public IActionResult detailCV(string id)
        {
            var userid = _db.Jobseekers.Where(x => x.JobSeekerid == id).FirstOrDefault();
            var infoCV = _db.Cvs.Where(y => y.Cvid == userid.JobSeekerCvid).FirstOrDefault();
            var mainskill = _db.Jobseekermainskills.Where(z => z.JobSeekerid == id).ToList();
            var cookskill = _db.Jobseekercookingskills.Where(c => c.JobSeekerid == id).ToList();
            var otherskill = _db.Jobseekerotherskills.Where(e => e.JobSeekerid == id).ToList();
            var hocvan = _db.Jobseekerlanguages.Where(e => e.JobSeekerid == id).ToList();
            var query = _db.Users.Where(a => a.UserId == id).FirstOrDefault();
            var a = _db.Roles.Where(b => b.RoleId == query.RoleId).ToList();
            var b = DateTime.Now.Year;
            var age = b - userid.JobSeekerdob.Value.Year;
            var exp = b - infoCV.Workstartdate.Value.Year;
            var dateactual = infoCV.Workactualstart.Value.Day;
            var monthactual = infoCV.Workactualstart.Value.Month;
            var yearactual = infoCV.Workactualstart.Value.Year;
            ViewData["User"] = userid;
            ViewData["Type"] = infoCV.Worktype;
            ViewData["infoCV"] = infoCV;
            ViewData["mainskill"] = mainskill;
            ViewData["cookskill"] = cookskill;
            ViewData["otherskill"] = otherskill;
            ViewData["hocvan"] = hocvan;
            ViewData["Role"] = a;
            ViewData["Age"] = age;
            ViewData["Exp"] = exp;
            ViewData["dateactual"] = dateactual + "/" + monthactual + "/" + yearactual;
            return View(userid);
        }
        [HttpPost]
        public async Task<IActionResult> deleteCV(string id)
        {
            var Userinfo = _db.Jobseekers.Where(x => x.JobSeekerCvid == id).FirstOrDefault();
            if(Userinfo != null)
            {
                var mainskill2 = _db.Jobseekermainskills.Where(y => y.JobSeekerid == Userinfo.JobSeekerid).ToArray();
                foreach (var item in mainskill2)
                {
                    _db.Remove(item);
                }
                var otherskill = _db.Jobseekerotherskills.Where(y => y.JobSeekerid == Userinfo.JobSeekerid).ToArray();
                foreach (var item1 in otherskill)
                {
                    _db.Remove(item1);
                }
                var cookingskill = _db.Jobseekercookingskills.Where(y => y.JobSeekerid == Userinfo.JobSeekerid).ToArray();
                foreach (var item2 in cookingskill)
                {
                    _db.Remove(item2);
                }
                var type = _db.Jobseekerlanguages.Where(y => y.JobSeekerid == Userinfo.JobSeekerid).ToArray();
                foreach (var item3 in type)
                {
                    _db.Remove(item3);
                }
                Jobseeker jobseeker = new Jobseeker
                {
                    JobSeekerid = Userinfo.JobSeekerid,
                    JobSeekername = Userinfo.JobSeekername,
                    JobSeekerdob = Userinfo.JobSeekerdob,
                    JobSeekerphone = Userinfo.JobSeekerphone,
                    JobSeekergender = Userinfo.JobSeekergender,
                    JobSeekerEmail = Userinfo.JobSeekerEmail,
                    JobSeekerLocation = Userinfo.JobSeekerLocation,
                    JobSeekeraddress = Userinfo.JobSeekeraddress,
                    JobSeekerimg = Userinfo.JobSeekerimg,
                    JobSeekerDescription = Userinfo.JobSeekerDescription,
                    JobSeekerCvid = null,

                };

                _db.Remove(Userinfo);
                _db.Add(jobseeker);
                await _db.SaveChangesAsync();
                Cv? CVinfo = _db.Cvs.Where(x => x.Cvid == id).FirstOrDefault();

                _db.Remove(CVinfo);
                await _db.SaveChangesAsync();


                return RedirectToAction("getAllCV", "Admin");
            }

            return Ok();
        }
        public IActionResult detailJob(string id)
        {
            var JobDetails = _db.Jobs.Where(x => x.Jobid == id).FirstOrDefault();
            var Findhelper = _db.Findhelpers.Where(y => y.FindHelperid == JobDetails.FindHelperid).FirstOrDefault();
            var mainskill = _db.Jobseekermainskills.Where(z => z.Jobid == id).ToList();
            var cookskill = _db.Jobseekercookingskills.Where(c => c.Jobid == id).ToList();
            var otherskill = _db.Jobseekerotherskills.Where(e => e.Jobid == id).ToList();
            ViewData["Findhelper"] = Findhelper;
            ViewData["mainskill"] = mainskill;
            ViewData["cookskill"] = cookskill;
            ViewData["otherskill"] = otherskill;
            ViewData["Address"] = Findhelper.FindHelperAddress;
            ViewData["Description"] = Findhelper.FindHelperDescription;
            ViewData["img"] = Findhelper.FindHelperImg;
            ViewData["Location"] = Findhelper.FindHelperLocation;
            ViewData["email"] = Findhelper.FindHelperEmail;
            ViewData["name"] = Findhelper.FindHelperName;
            ViewData["phone"] = Findhelper.FindHelperPhone;
            return View(JobDetails);
        }
        public async Task<IActionResult> deleteJob(string id)
        {
            
            var mainskill2 = _db.Jobseekermainskills.Where(y => y.Jobid == id).ToArray();
            foreach (var item in mainskill2)
            {
                _db.Remove(item);
            }
            var otherskill = _db.Jobseekerotherskills.Where(y => y.Jobid == id).ToArray();
            foreach (var item1 in otherskill)
            {
                _db.Remove(item1);
            }
            var cookingskill = _db.Jobseekercookingskills.Where(y => y.Jobid == id).ToArray();
            foreach (var item2 in cookingskill)
            {
                _db.Remove(item2);
            }
            Job? jobinfo = _db.Jobs.Where(x => x.Jobid == id).FirstOrDefault();
            _db.Remove(jobinfo);
            await _db.SaveChangesAsync();
            return RedirectToAction("getAllJob", "Admin");
        }
        [HttpPost]
        public async Task<IActionResult> deleteFindHelper(string id)
        {
            Findhelper? findhelper = _db.Findhelpers.Where(x => x.FindHelperid == id).FirstOrDefault();
            _db.Remove(findhelper);
            Job? job = _db.Jobs.Where(x => x.FindHelperid == id).FirstOrDefault();
            if(job != null)
            {
                var mainskill2 = _db.Jobseekermainskills.Where(y => y.Jobid == job.Jobid).ToArray();
                foreach (var item in mainskill2)
                {
                    _db.Remove(item);
                }
                var otherskill = _db.Jobseekerotherskills.Where(y => y.Jobid == job.Jobid).ToArray();
                foreach (var item1 in otherskill)
                {
                    _db.Remove(item1);
                }
                var cookingskill = _db.Jobseekercookingskills.Where(y => y.Jobid == job.Jobid).ToArray();
                foreach (var item2 in cookingskill)
                {
                    _db.Remove(item2);
                }
                _db.Remove(job);
            }
            var userss = _db.Users.Where(x => x.UserId == id).FirstOrDefault();
            _db.Remove(userss);
        await _db.SaveChangesAsync();
            return RedirectToAction("getAllFindHelper", "Admin");
        }
        public async Task<IActionResult> deleteJobSeeker(string id)
        {
            Jobseeker? jobseekers = _db.Jobseekers.Where(x => x.JobSeekerid == id).FirstOrDefault();
            if (jobseekers != null)
            {
                var mainskill2 = _db.Jobseekermainskills.Where(y => y.JobSeekerid == jobseekers.JobSeekerid).ToArray();
                foreach (var item in mainskill2)
                {
                    _db.Remove(item);
                }
                var otherskill = _db.Jobseekerotherskills.Where(y => y.Jobid == jobseekers.JobSeekerid).ToArray();
                foreach (var item1 in otherskill)
                {
                    _db.Remove(item1);
                }
                var cookingskill = _db.Jobseekercookingskills.Where(y => y.Jobid == jobseekers.JobSeekerid).ToArray();
                foreach (var item2 in cookingskill)
                {
                    _db.Remove(item2);
                }
                var language = _db.Jobseekerlanguages.Where(y => y.JobSeekerid == jobseekers.JobSeekerid).ToArray();
                foreach (var item3 in language)
                {
                    _db.Remove(item3);
                }
                _db.Remove(jobseekers);
                Cv? cvs = _db.Cvs.Where(x => x.Cvid == jobseekers.JobSeekerCvid).FirstOrDefault();
                if(cvs != null)
                {
                    _db.Remove(cvs);
                }
                var userss = _db.Users.Where(x => x.UserId == id).FirstOrDefault();
                _db.Remove(userss);
                await _db.SaveChangesAsync();
                return RedirectToAction("getAllJobSeeker", "Admin");
            }

            return BadRequest();
           
        }

    }
}
