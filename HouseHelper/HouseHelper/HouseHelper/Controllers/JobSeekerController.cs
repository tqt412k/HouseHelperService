using HouseHelper.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Net.WebSockets;
using static HouseHelper.Controllers.FindHelperController;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HouseHelper.Controllers
{
    [Authorize(Roles = "JobSeeker")]
    public class JobSeekerController : Controller
    {
        private readonly HouseHelperContext _db = new HouseHelperContext();
        private readonly User userr = new User();
        private readonly IWebHostEnvironment webHostEnvironment;
        public JobSeekerController(IWebHostEnvironment hostEnvironment)
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
        public IActionResult Index()
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
            ViewBag.data4 = query;
            return View();
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        
        
        [HttpGet("Profile")]
        public IActionResult Profile()
        {
            var jobseekerid = User.FindFirstValue("userId");
            var query = _db.Users.Where(a => a.UserId == jobseekerid).FirstOrDefault();
            var a = _db.Roles.Where(b => b.RoleId == query.RoleId).ToList();
            //var infor = _db.Jobseekers.Where(a => a.JobSeekerid == jobseekerid).FirstOrDefault();
            //Jobseeker model = new Jobseeker();
            var infor = _db.Jobseekers.Where(a => a.JobSeekerid == jobseekerid).Select(
                jobseeker => new Jobseeker
                {
                    JobSeekerid = jobseeker.JobSeekerid,
                    JobSeekername = jobseeker.JobSeekername,
                    JobSeekerdob = jobseeker.JobSeekerdob,
                    JobSeekerphone = jobseeker.JobSeekerphone,
                    JobSeekergender = jobseeker.JobSeekergender,
                    JobSeekerEmail = jobseeker.JobSeekerEmail,
                    JobSeekerimg = jobseeker.JobSeekerimg,
                    JobSeekerLocation = jobseeker.JobSeekerLocation,
                    JobSeekeraddress = jobseeker.JobSeekeraddress
                }).ToList();
            ViewData["infor"] = infor;
            ViewData["Role"] = a;
            return View(/*model*/);
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var s = _db.Jobseekers.Where(x => x.JobSeekerid == id).FirstOrDefault();
            return View(s);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Jobseeker model) 
        {
            
        var a = _db.Jobseekers.Where(x => x.JobSeekerid == model.JobSeekerid).FirstOrDefault();
        string uniqueFileName = UploadedFile(model);

                if (uniqueFileName != null)
                {
                    Jobseeker jobseeker = new Jobseeker
                    {
                        JobSeekerid = model.JobSeekerid,
                        JobSeekername = model.JobSeekername,
                        JobSeekerdob = model.JobSeekerdob,
                        JobSeekerphone = model.JobSeekerphone,
                        JobSeekergender = model.JobSeekergender,
                        JobSeekerEmail = a.JobSeekerEmail,
                        JobSeekerLocation = model.JobSeekerLocation,
                        JobSeekeraddress = model.JobSeekeraddress,
                        JobSeekerSalaryFrom = a.JobSeekerSalaryFrom,
                        JobSeekerSalaryTo = a.JobSeekerSalaryTo,
                        JobSeekerCvid = a.JobSeekerCvid,
                        JobSeekerimg = uniqueFileName,
                    };
                    _db.Jobseekers.Remove(a);
                    _db.Jobseekers.Add(jobseeker);
                    await _db.SaveChangesAsync();
                TempData["Success"] = "Cập nhật thành công";
                    return RedirectToAction(nameof(Profile));
    }
                else if (uniqueFileName == null)
                {
                    Jobseeker jobseeker = new Jobseeker
                    {
                        JobSeekerid = model.JobSeekerid,
                        JobSeekername = model.JobSeekername,
                        JobSeekerdob = model.JobSeekerdob,
                        JobSeekerphone = model.JobSeekerphone,
                        JobSeekergender = model.JobSeekergender,
                        JobSeekerEmail = a.JobSeekerEmail,
                        JobSeekerLocation = model.JobSeekerLocation,
                        JobSeekeraddress = model.JobSeekeraddress,
                        JobSeekerSalaryFrom = a.JobSeekerSalaryFrom,
                        JobSeekerSalaryTo = a.JobSeekerSalaryTo,
                        JobSeekerCvid = a.JobSeekerCvid,
                        JobSeekerimg = a.JobSeekerimg,
                    };
                    _db.Jobseekers.Remove(a);
                    _db.Jobseekers.Add(jobseeker);
                    await _db.SaveChangesAsync();
                TempData["Success"] = "Cập nhật thành công";
                return RedirectToAction(nameof(Profile));
            }
            return PartialView();
        }
        private string UploadedFile(Jobseeker model)
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
        public IActionResult GetImage()
        {
            var jobseekerid = User.FindFirstValue("userId");
            //var imgs = 
            //string path = "/images/" + imgs;
            //TempData["image"] = path;
            //return View(path);
            string photo = _db.Jobseekers.Where(p => p.JobSeekerid == jobseekerid).Select(img => img.JobSeekerimg).FirstOrDefault();
            var path = "images/" + photo;
            return View(path);
        }
        public async Task<IActionResult> CreateCV()
        {
            string jobseekerid = string.Empty;

            jobseekerid = User.FindFirstValue("userId");

            try
            {
                Jobseeker? jobseeker = await _db.Jobseekers.Where(x => x.JobSeekerid == jobseekerid).FirstOrDefaultAsync();
                return View(jobseeker);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Ok();
            
            
        }
        public async Task<IActionResult> PostCV(string hocvan,string salarymax,string salarymin, string worktype,string jobseekerdescription,string[] mainskill,string[] cookingskill, string[] otherskill,DateTime daysactual,DateTime daystart, Jobseeker model)
        {
            var jobseekerid = User.FindFirstValue("userId");
            string a = Guid.NewGuid().ToString();
            string b = Guid.NewGuid().ToString();
            Cv newCV = new Cv
            {
                Cvid = "Cv" + a,
                Worktype = worktype,
                Workactualstart = daysactual,
                Workstartdate = daystart,
            };
            _db.Add(newCV);
            var info = _db.Jobseekers.Where(a => a.JobSeekerid == jobseekerid).FirstOrDefault();
            Jobseeker jobseeker = new Jobseeker
            {
                JobSeekerid = info.JobSeekerid,
                JobSeekername = info.JobSeekername,
                JobSeekerdob = info.JobSeekerdob,
                JobSeekerphone = info.JobSeekerphone,
                JobSeekergender = info.JobSeekergender,
                JobSeekerEmail = info.JobSeekerEmail,
                JobSeekerLocation = info.JobSeekerLocation,
                JobSeekeraddress = info.JobSeekeraddress,
                JobSeekerimg = info.JobSeekerimg,
                JobSeekerDescription = jobseekerdescription,
                JobSeekerSalaryFrom = salarymin,
                JobSeekerSalaryTo = salarymax,
                JobSeekerCvid = "Cv" + a,
            };
            _db.Jobseekers.Remove(info);
            _db.Add(jobseeker);
            foreach (var item in mainskill)
            {
                string c = Guid.NewGuid().ToString();
                Jobseekermainskill newmainskill = new Jobseekermainskill
                {
                    JobSeekerMainSkillid = "mainskill" + c,
                    JobSeekerMainSkillname = item,
                    JobSeekerid = jobseekerid,
                };
                _db.Add(newmainskill);
            }
            foreach(var item2 in cookingskill)
            {
                string d = Guid.NewGuid().ToString();
                Jobseekercookingskill newcookingskill = new Jobseekercookingskill
                {
                    JobSeekerCookingSkillid = "cookingskill" + d,
                    JobSeekerCookingSkillname = item2,
                    JobSeekerid = jobseekerid,
                };
                 _db.Add(newcookingskill);
            };
                Jobseekerlanguage newlanguage = new Jobseekerlanguage
            {
                JobSeekerLanguageid = "languageskill" + b,
                JobSeekerLanguagename = hocvan,
                JobSeekerid = jobseekerid,
            };
            foreach (var item3 in otherskill)
            {
                string e = Guid.NewGuid().ToString();
                Jobseekerotherskill newotherskill = new Jobseekerotherskill
                {
                    JobSeekerOtherSkillid = "otherskill" + e,
                    JobSeekerOtherSkillname = item3,
                    JobSeekerid = jobseekerid,
                };
                _db.Add(newotherskill);
            };
            _db.Add(newlanguage);
            await _db.SaveChangesAsync();
            return RedirectToAction("ViewCV","JobSeeker", new {id = jobseekerid });
        }
        public IActionResult ViewCV(string id)
        {
            var jobseekerid = User.FindFirstValue("userId");
            var userid = _db.Jobseekers.Where(x => x.JobSeekerid == id).FirstOrDefault();
            var infoCV = _db.Cvs.Where(y => y.Cvid == userid.JobSeekerCvid).FirstOrDefault();
            var mainskill = _db.Jobseekermainskills.Where(z=>z.JobSeekerid == jobseekerid).ToList();
            var cookskill = _db.Jobseekercookingskills.Where(c => c.JobSeekerid == jobseekerid).ToList();
            var otherskill = _db.Jobseekerotherskills.Where(e => e.JobSeekerid == jobseekerid).ToList();
            var hocvan = _db.Jobseekerlanguages.Where(e => e.JobSeekerid == jobseekerid).ToList();
            var query = _db.Users.Where(a => a.UserId == jobseekerid).FirstOrDefault();
            var a = _db.Roles.Where(b => b.RoleId == query.RoleId).ToList();
            var b = DateTime.Now.Year;
            var age = b - userid.JobSeekerdob.Value.Year;
            var exp = b - infoCV.Workstartdate.Value.Year;
            var dateactual = infoCV.Workactualstart.Value.Day;
            var monthactual = infoCV.Workactualstart.Value.Month;
            var yearactual =  infoCV.Workactualstart.Value.Year;
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
        public class ListJobModel
        {
            public string id { get; set; }
            public string name { get; set; }
            public string title { get; set; }
            public DateTime? datestart { get; set; }
            public DateTime? datepost { get; set; }
            public string description { get; set; }
            public string location { get; set; }
            public string type { get; set; }
            public string gender { get; set; }
            public string img { get; set; }
        }
        [HttpGet]
        public IActionResult GetAllJob()
        { 
            var query = (from p in _db.Findhelpers
                         join q in _db.Jobs on p.FindHelperid equals q.FindHelperid
                         select new ListJobModel
                         {
                             id = q.Jobid,
                             name = p.FindHelperName,
                             title = q.JobTitle,
                             datestart =  q.JobStart,
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
        public IActionResult ViewJob(string id)
        {
            var jobseekerid = User.FindFirstValue("userId");
            var users = _db.Jobseekers.Where(x => x.JobSeekerid == jobseekerid).FirstOrDefault();
            ViewData["Names"] = users.JobSeekername;
            ViewData["IMAGE"] = users.JobSeekerimg;
            var jobsingle = _db.Jobs.Where(x => x.Jobid == id).FirstOrDefault();
            var findhelperinfo = _db.Findhelpers.Where(y => y.FindHelperid == jobsingle.FindHelperid).FirstOrDefault();
            var mainskill = _db.Jobseekermainskills.Where(z => z.Jobid == id).ToList();
            var cookskill = _db.Jobseekercookingskills.Where(c => c.Jobid == id).ToList();
            var otherskill = _db.Jobseekerotherskills.Where(e => e.Jobid == id).ToList();
            ViewData["Findhelper"] = findhelperinfo;
            ViewData["mainskill"] = mainskill;
            ViewData["cookskill"] = cookskill;
            ViewData["otherskill"] = otherskill;
            ViewData["Address"] = findhelperinfo.FindHelperAddress;
            ViewData["Description"] = findhelperinfo.FindHelperDescription;
            ViewData["img"] = findhelperinfo.FindHelperImg;
            ViewData["Location"] = findhelperinfo.FindHelperLocation;
            ViewData["email"] = findhelperinfo.FindHelperEmail;
            ViewData["name"] = findhelperinfo.FindHelperName;
            ViewData["phone"] = findhelperinfo.FindHelperPhone;

            return View(jobsingle);
        }
        public class ModelEdit
        {
            public Jobseeker JobseekerModel { get; set; }
            public Cv CvModel { get; set; }
            public Jobseekermainskill MainskillModel { get; set; }
            public Jobseekercookingskill CookingModel { get; set; }
            public Jobseekerotherskill OtherskillModel { get; set; }
            public Jobseekerlanguage LanguageModel { get; set; }

        }
        [HttpGet]
        public ActionResult ChangePassWord(string id) {
            var user = _db.Jobseekers.Where(x=>x.JobSeekerid == id).FirstOrDefault();
            var username = _db.Users.Where(y => y.UserId == id).FirstOrDefault();
            ViewData["username"] = username.UserName;
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string id,string password)
        {
            var user = _db.Users.Where(x => x.UserId == id).FirstOrDefault();
            var hashPass = MD5Hash(password);
                User newUser = new User
                {
                    UserName = user.UserName,
                    Passwords = hashPass,
                    Email = user.Email,
                    UserId = user.UserId,
                    RoleId = user.RoleId,
                    
                };
                _db.Remove(user);
                _db.Add(newUser);
                await _db.SaveChangesAsync();   
                TempData["Regis"] = "Thay đổi mật khẩu thành công";
                return RedirectToAction("Index","JobSeeker");
        }
        [HttpGet]
        public async Task<IActionResult> Search(string WordSearch, string LocationSearch, string TypeSearch)
        {
            if (WordSearch == null || LocationSearch == null || TypeSearch == null)
            {
                if (WordSearch == null && LocationSearch != null && TypeSearch != null)
                {
                    var query1 = (from p in _db.Findhelpers
                                  join q in _db.Jobs on p.FindHelperid equals q.FindHelperid
                                  where p.FindHelperLocation.Contains(LocationSearch) && q.JobType.Contains(TypeSearch)
                                  select new ListJobModel
                                  {
                                      id = q.Jobid,
                                      name = p.FindHelperName,
                                      title = q.JobTitle,
                                      datestart = q.JobStart,
                                      description = q.JobDescription,
                                      location = p.FindHelperLocation,
                                      type = q.JobType,
                                      img = q.JobImage,
                                      datepost = q.JobDatePost
                                  }).ToList();
                    var totaljob = query1.Count;
                    ViewData["totaljob"] = totaljob;
                    ViewBag.data = query1;
                    ViewData["getpostdetail"] = WordSearch;
                    ViewData["LocationSearch"] = LocationSearch;
                    ViewData["TypeSearch"] = TypeSearch;
                    return View();
                }
                else if(WordSearch == null && LocationSearch == null && TypeSearch != null)
                {
                    var query1 = (from p in _db.Findhelpers
                                  join q in _db.Jobs on p.FindHelperid equals q.FindHelperid
                                  where q.JobType.Contains(TypeSearch)
                                  select new ListJobModel
                                  {
                                      id = q.Jobid,
                                      name = p.FindHelperName,
                                      title = q.JobTitle,
                                      datestart = q.JobStart,
                                      description = q.JobDescription,
                                      location = p.FindHelperLocation,
                                      type = q.JobType,
                                      img = q.JobImage,
                                      datepost = q.JobDatePost
                                  }).ToList();
                    var totaljob = query1.Count;
                    ViewData["totaljob"] = totaljob;
                    ViewBag.data = query1;
                    ViewData["getpostdetail"] = WordSearch;
                    ViewData["LocationSearch"] = LocationSearch;
                    ViewData["TypeSearch"] = TypeSearch;
                    return View();
                }
                else if(WordSearch == null && LocationSearch != null && TypeSearch == null)
                {
                    var query1 = (from p in _db.Findhelpers
                                  join q in _db.Jobs on p.FindHelperid equals q.FindHelperid
                                  where p.FindHelperLocation.Contains(LocationSearch)
                                  select new ListJobModel
                                  {
                                      id = q.Jobid,
                                      name = p.FindHelperName,
                                      title = q.JobTitle,
                                      datestart = q.JobStart,
                                      description = q.JobDescription,
                                      location = p.FindHelperLocation,
                                      type = q.JobType,
                                      img = q.JobImage,
                                      datepost = q.JobDatePost
                                  }).ToList();
                    var totaljob = query1.Count;
                    ViewData["totaljob"] = totaljob;
                    ViewBag.data = query1;
                    ViewData["getpostdetail"] = WordSearch;
                    ViewData["LocationSearch"] = LocationSearch;
                    ViewData["TypeSearch"] = TypeSearch;
                    return View();
                }
                else if(WordSearch != null && LocationSearch == null && TypeSearch == null)
                {
                    var query1 = (from p in _db.Findhelpers
                                 join q in _db.Jobs on p.FindHelperid equals q.FindHelperid
                                 where q.JobName.Contains(WordSearch)
                                 select new ListJobModel
                                 {
                                     id = q.Jobid,
                                     name = p.FindHelperName,
                                     title = q.JobTitle,
                                     datestart = q.JobStart,
                                     description = q.JobDescription,
                                     location = p.FindHelperLocation,
                                     type = q.JobType,
                                     img = q.JobImage,
                                     datepost = q.JobDatePost
                                 }).ToList();
                    var totaljob = query1.Count;
                    ViewData["totaljob"] = totaljob;
                    ViewBag.data = query1;
                    ViewData["getpostdetail"] = WordSearch;
                    ViewData["LocationSearch"] = LocationSearch;
                    ViewData["TypeSearch"] = TypeSearch;
                    return View();
                }
                else if(WordSearch != null && LocationSearch == null && TypeSearch != null)
                {
                    var query1 = (from p in _db.Findhelpers
                                 join q in _db.Jobs on p.FindHelperid equals q.FindHelperid
                                 where q.JobName.Contains(WordSearch) && q.JobType.Contains(TypeSearch)
                                 //|| q.JobType.Contains(TypeSearch)
                                 select new ListJobModel
                                 {
                                     id = q.Jobid,
                                     name = p.FindHelperName,
                                     title = q.JobTitle,
                                     datestart = q.JobStart,
                                     description = q.JobDescription,
                                     location = p.FindHelperLocation,
                                     type = q.JobType,
                                     img = q.JobImage,
                                     datepost = q.JobDatePost
                                 }).ToList();
                    var totaljob = query1.Count;
                    ViewData["totaljob"] = totaljob;
                    ViewBag.data = query1;
                    ViewData["getpostdetail"] = WordSearch;
                    ViewData["LocationSearch"] = LocationSearch;
                    ViewData["TypeSearch"] = TypeSearch;
                    return View();
                }
                else if(WordSearch != null && LocationSearch != null && TypeSearch == null)
                {
                    var query1 = (from p in _db.Findhelpers
                                  join q in _db.Jobs on p.FindHelperid equals q.FindHelperid
                                  where q.JobName.Contains(WordSearch) && p.FindHelperLocation.Contains(LocationSearch)
                                  //|| q.JobType.Contains(TypeSearch)
                                  select new ListJobModel
                                  {
                                      id = q.Jobid,
                                      name = p.FindHelperName,
                                      title = q.JobTitle,
                                      datestart = q.JobStart,
                                      description = q.JobDescription,
                                      location = p.FindHelperLocation,
                                      type = q.JobType,
                                      img = q.JobImage,
                                      datepost = q.JobDatePost
                                  }).ToList();
                    var totaljob = query1.Count;
                    ViewData["totaljob"] = totaljob;
                    ViewBag.data = query1;
                    ViewData["getpostdetail"] = WordSearch;
                    ViewData["LocationSearch"] = LocationSearch;
                    ViewData["TypeSearch"] = TypeSearch;
                    return View();
                }
                else if(WordSearch == null && LocationSearch == null && TypeSearch == null)
                {
                    var query1 = (from p in _db.Findhelpers
                                  join q in _db.Jobs on p.FindHelperid equals q.FindHelperid
                                  select new ListJobModel
                                  {
                                      id = q.Jobid,
                                      name = p.FindHelperName,
                                      title = q.JobTitle,
                                      datestart = q.JobStart,
                                      description = q.JobDescription,
                                      location = p.FindHelperLocation,
                                      type = q.JobType,
                                      img = q.JobImage,
                                      datepost = q.JobDatePost
                                  }).ToList();
                    var totaljob = query1.Count;
                    ViewData["totaljob"] = totaljob;
                    ViewBag.data = query1;
                    ViewData["getpostdetail"] = WordSearch;
                    ViewData["LocationSearch"] = LocationSearch;
                    ViewData["TypeSearch"] = TypeSearch;
                    return View();
                }
            }
            else if(WordSearch != null && LocationSearch != null && TypeSearch != null)
            {
                var query1 = (from p in _db.Findhelpers
                             join q in _db.Jobs on p.FindHelperid equals q.FindHelperid
                             where q.JobName.Contains(WordSearch) && p.FindHelperLocation.Contains(LocationSearch) && q.JobType.Contains(TypeSearch)
                              select new ListJobModel
                             {
                                 id = q.Jobid,
                                 name = p.FindHelperName,
                                 title = q.JobTitle,
                                 datestart = q.JobStart,
                                 description = q.JobDescription,
                                 location = p.FindHelperLocation,
                                 type = q.JobType,
                                 img = q.JobImage,
                                 datepost = q.JobDatePost
                             }).ToList();
                var totaljob = query1.Count;
                ViewData["totaljob"] = totaljob;
                ViewBag.data = query1;
                ViewData["getpostdetail"] = WordSearch;
                ViewData["LocationSearch"] = LocationSearch;
                ViewData["TypeSearch"] = TypeSearch;
                return View();
            }
            
            return Ok();

        }
        [HttpPost]
        public async Task<IActionResult> DeleteCv(string id)
        {
            var jobseekerid = User.FindFirstValue("userId");
            var mainskill2 = _db.Jobseekermainskills.Where(y=>y.JobSeekerid == jobseekerid).ToArray();
            foreach (var item in mainskill2)
            {
                _db.Remove(item);
            }
            var otherskill = _db.Jobseekerotherskills.Where(y => y.JobSeekerid == jobseekerid).ToArray();
            foreach (var item1 in otherskill)
            {
                _db.Remove(item1);
            }
            var cookingskill = _db.Jobseekercookingskills.Where(y => y.JobSeekerid == jobseekerid).ToArray();
            foreach (var item2 in cookingskill)
            {
                _db.Remove(item2);
            }
            var type = _db.Jobseekerlanguages.Where(y => y.JobSeekerid == jobseekerid).ToArray();
            foreach (var item3 in type)
            {
                _db.Remove(item3);
            }
            var Userinfo = _db.Jobseekers.Where(x => x.JobSeekerCvid == id).FirstOrDefault();
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


            return RedirectToAction("Index", "JobSeeker");
        }
    }
}

