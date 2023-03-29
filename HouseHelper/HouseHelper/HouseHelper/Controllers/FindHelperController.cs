using HouseHelper.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HouseHelper.Controllers
{
    [Authorize(Roles = "FindHelper")]
    public class FindHelperController : Controller
    {
        private readonly HouseHelperContext _db = new HouseHelperContext();
        private readonly User userr = new User();
        private readonly IWebHostEnvironment webHostEnvironment;
        public FindHelperController(IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
        }
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
        public class ListCVModel
        {
            public string id { get; set; }
            public string name { get; set; }
            public int dob { get; set; }
            public int Exp { get; set; }
            public string location { get; set; }
            public string type { get; set; }
            public string img { get; set; }
            public string description { get; set; }
            public bool? gender { get; set; }
            public DateTime? datestart { get; set; }
            public string idcv { get; set; }    
        }
        public IActionResult Index()
        {
            var date = DateTime.Now.Year;
            var query = (from p in _db.Jobseekers
                         join q in _db.Cvs on p.JobSeekerCvid equals q.Cvid orderby p.JobSeekerid descending
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
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        [HttpGet("FindHelperProfile")]
        public IActionResult FindHelperProfile()
        {
            var findhelperid = User.FindFirstValue("userId");
            var query = _db.Users.Where(a => a.UserId == findhelperid).FirstOrDefault();
            var a = _db.Roles.Where(b => b.RoleId == query.RoleId).ToList();
            var infor = _db.Findhelpers.Where(a => a.FindHelperid == findhelperid).Select(
                findhelper => new Findhelper
                {
                    FindHelperid = findhelper.FindHelperid,
                    FindHelperName = findhelper.FindHelperName,
                    FindHelperPhone = findhelper.FindHelperPhone,
                    FindHelperImg = findhelper.FindHelperImg,
                    FindHelperAddress = findhelper.FindHelperAddress,
                    FindHelperLocation = findhelper.FindHelperLocation,
                    FindHelperEmail = findhelper.FindHelperEmail,
                    FindHelperDescription = findhelper.FindHelperDescription,
                }).ToList();
            ViewData["infor"] = infor;
            ViewData["Role"] = a;
            return View(/*model*/);
        }
        [HttpGet]
        public IActionResult EditFindHelperProfile(string id)
        {
            var s = _db.Findhelpers.Where(x => x.FindHelperid == id).FirstOrDefault();
            return View(s);
        }
        [HttpPost]
        public async Task<IActionResult> EditFindHelperProfile(Findhelper model)
        {

            var a = _db.Findhelpers.Where(x => x.FindHelperid == model.FindHelperid).FirstOrDefault();
            string uniqueFileName = UploadedFile(model);

            if (uniqueFileName != null)
            {
                Findhelper findhelper = new Findhelper
                {
                    FindHelperid = model.FindHelperid,
                    FindHelperName = model.FindHelperName,
                    FindHelperPhone = model.FindHelperPhone,
                    FindHelperImg = uniqueFileName,
                    FindHelperAddress = model.FindHelperAddress,
                    FindHelperLocation = model.FindHelperLocation,
                    FindHelperEmail = a.FindHelperEmail,
                    FindHelperDescription = model.FindHelperDescription
                };
                _db.Findhelpers.Remove(a);
                _db.Findhelpers.Add(findhelper);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Cập nhật thành công";
                return RedirectToAction(nameof(FindHelperProfile));
            }
            else if (uniqueFileName == null)
            {
                Findhelper findhelper = new Findhelper
                {
                    FindHelperid = model.FindHelperid,
                    FindHelperName = model.FindHelperName,
                    FindHelperPhone = model.FindHelperPhone,
                    FindHelperImg = a.FindHelperImg,
                    FindHelperAddress = model.FindHelperAddress,
                    FindHelperLocation = model.FindHelperLocation,
                    FindHelperEmail = a.FindHelperEmail,
                    FindHelperDescription = model.FindHelperDescription,
                };
                _db.Findhelpers.Remove(a);
                _db.Findhelpers.Add(findhelper);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Cập nhật thành công";
                return RedirectToAction(nameof(FindHelperProfile));
            }
            return PartialView();
        }
        private string UploadedFile(Findhelper model)
        {
            string uniqueFileName = null;

            if (model.ImgFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = model.ImgFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {

                    model.ImgFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        [HttpGet]
        public IActionResult PostJob(string id)
        {
            var jobsingle = _db.Findhelpers.Where(x => x.FindHelperid == id).FirstOrDefault();
            return View(jobsingle);
        }
        [HttpPost]
        public async Task<IActionResult> PostJob(Findhelper model, Job model1, string jobtitle, string jobname,
            string jobtype, DateTime jobstart, DateTime jobstartflex,
            string jobgender, string jobage, string jobexp, string jobeducation,
            string jobsalaryfrom, string jobsalaryto, string[] mainskill,
            string[] cookingskill, string[] otherskill, string jobdescription, string ImgFile)
        {
            var findhelperid = User.FindFirstValue("userId");
            string a = Guid.NewGuid().ToString();
            string b = Guid.NewGuid().ToString();
            string uniqueFileName1 = UploadedFile1(model1);
            var jobsid = "Job" + a;
            var findhelperinfo = _db.Findhelpers.Where(a => a.FindHelperid == findhelperid).FirstOrDefault();
            Findhelper findhelper = new Findhelper
            {
                FindHelperid = findhelperinfo.FindHelperid,
                FindHelperName = findhelperinfo.FindHelperName,
                FindHelperAddress = findhelperinfo.FindHelperAddress,
                FindHelperEmail = findhelperinfo.FindHelperEmail,
                FindHelperImg = findhelperinfo.FindHelperImg,
                FindHelperLocation = findhelperinfo.FindHelperLocation,
                FindHelperPhone = findhelperinfo.FindHelperPhone,
                FindHelperDescription = model.FindHelperDescription,
            };
            _db.Findhelpers.Remove(findhelperinfo);
            _db.Add(findhelper);
            if (uniqueFileName1 != null)
            {
                Job jobs = new Job()
                {
                    Jobid = "Job" + a,
                    JobAge = jobage,
                    JobName = jobname,
                    JobTitle = jobtitle,
                    JobDescription = jobdescription,
                    JobEducation = jobeducation,
                    JobDatePost = DateTime.Now,
                    JobStart = jobstart,
                    JobStartFlexibility = jobstartflex.ToString(),
                    JobType = jobtype,
                    JobGender = jobgender,
                    JobExp = jobexp,
                    JobImage = uniqueFileName1,
                    JobSalaryMin = jobsalaryfrom,
                    JobSalryMax = jobsalaryto,
                    FindHelperid = findhelperinfo.FindHelperid
                };
                _db.Add(jobs);
            }
            foreach (var item in mainskill)
            {
                string c = Guid.NewGuid().ToString();
                Jobseekermainskill newmainskill = new Jobseekermainskill
                {
                    JobSeekerMainSkillid = "mainskill" + c,
                    JobSeekerMainSkillname = item,
                    Jobid = jobsid,
                };
                _db.Add(newmainskill);
            }
            foreach (var item2 in cookingskill)
            {
                string d = Guid.NewGuid().ToString();
                Jobseekercookingskill newcookingskill = new Jobseekercookingskill
                {
                    JobSeekerCookingSkillid = "cookingskill" + d,
                    JobSeekerCookingSkillname = item2,
                    Jobid = jobsid,
                };
                _db.Add(newcookingskill);
            };
            foreach (var item3 in otherskill)
            {
                string e = Guid.NewGuid().ToString();
                Jobseekerotherskill newotherskill = new Jobseekerotherskill
                {
                    JobSeekerOtherSkillid = "otherskill" + e,
                    JobSeekerOtherSkillname = item3,
                    Jobid = jobsid,
                };
                _db.Add(newotherskill);
            };
            await _db.SaveChangesAsync();
            return RedirectToAction("JobSingle", new { id = jobsid });
        }
        private string UploadedFile1(Job model1)
        {
            string uniqueFileName1 = null;

            if (model1.ImgFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName1 = model1.ImgFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName1);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {

                    model1.ImgFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName1;
        }
        public IActionResult GetMyJob()
        {
            var findhelperid = User.FindFirstValue("userId");
            var listjobpost = _db.Jobs.Where(x => x.FindHelperid == findhelperid).ToList();
            var location = _db.Findhelpers.Where(x => x.FindHelperid == findhelperid).FirstOrDefault();
            ViewData["location"] = location.FindHelperLocation;
            ViewData["MyListJob"] = listjobpost;
            var TotalJobPost = listjobpost.Count;
            ViewData["TotalJobPost"] = TotalJobPost;
            return View();
        }
        [HttpGet]
        public IActionResult JobSingle(string id)
        {
            var findhelperid = User.FindFirstValue("userId");
            var JobDetails = _db.Jobs.Where(x => x.Jobid == id).FirstOrDefault();
            var Findhelper = _db.Findhelpers.Where(y => y.FindHelperid == findhelperid).FirstOrDefault();
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
            //ViewData["Location"] = Findhelper.FindHelperLocation;

            return View(JobDetails);
        }
        public IActionResult ViewCV(string id)
        {
            var findhelperid = User.FindFirstValue("userId");
            var findhelperinfo = _db.Findhelpers.Where(y => y.FindHelperid == findhelperid).FirstOrDefault();
            ViewData["Names"] = findhelperinfo.FindHelperName;
            ViewData["IMAGE"] = findhelperinfo.FindHelperImg;
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
        public async Task<IActionResult> DeleteJob(string id)
        {
            var findhelper = User.FindFirstValue("userId");
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
            return RedirectToAction("GetMyJob", "FindHelper");
        }
        [HttpGet]
        public IActionResult GetAllCV()
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
        [HttpGet]
        public IActionResult ChangePassFindHelper(string id)
        {
            var infor = _db.Findhelpers.Where(x => x.FindHelperid == id).FirstOrDefault();
            return View(infor);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassFindHelper(string password, Findhelper model)
        {
            var userid = User.FindFirstValue("userId");
            User? infor = _db.Users.Where(x => x.UserId == userid).FirstOrDefault();
            var hashPass = MD5Hash(password);
            User newfindhelper = new User
            {

                UserName = infor.UserName,
                Passwords = hashPass,
                Email = infor.Email,
                UserId = infor.UserId,
                RoleId = infor.RoleId,
            };
            _db.Remove(infor);
            _db.Add(newfindhelper);
            await _db.SaveChangesAsync();
            TempData["Regis"] = "Tạo thành công tài khoản";
            return RedirectToAction("Index", "FindHelper");
        }
    }
}

