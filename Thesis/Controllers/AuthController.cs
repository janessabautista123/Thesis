using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thesis.Models;
using System.Security.Claims;
using Thesis.CustomLibraries;

namespace Thesis.Controllers
{

    [AllowAnonymous]
    public class AuthController : Controller
    {
        int time = 0;
        // GET: Auth
        [HttpGet]
        public ActionResult Login()
        {
                return View();
            
        }

        [HttpPost]
        public ActionResult Login(LoginModel2 model)
        {
            if(!ModelState.IsValid)
            //if (model.CaptchaCodeText !=  Convert.ToString(Session["Captcha"])) //check if input == to captcha shown
            {
                if (Session["time"] == null)
                {

                    model.CapImage = "data:image/png;base64," + Convert.ToBase64String(new Utility().VerificationTextGenerator());
                    model.CapImageText = Convert.ToString(Session["Captcha"]);

                    return View("Sample", model);
                }
                else
                {
                    time = (int)Session["time"];
                    time++;
                    Session["time"] = time;
                    Response.Write("Wrong times: " + time.ToString());
                    if (time >= 3)
                    {
                        model.CapImage = "data:image/png;base64," + Convert.ToBase64String(new Utility().VerificationTextGenerator());
                        model.CapImageText = Convert.ToString(Session["Captcha"]);

                        return View("Sample", model);//Returns the view with the input values so that the user doesn't have to retype again

                    }
                } //Returns the view with the input values so that the user doesn't have to retype again
                
                /*if (Session["time"] == null)
                        {
                            Session["time"] = time + 1;
                            Response.Write("Wrong times: 1");
                        }
                        else
                {
                    time = (int)Session["time"];
                    time++;
                    Session["time"] = time;
                    Response.Write("Wrong times: " + time.ToString());
                    if (time >= 3)
                    {
                        model.CapImage = "data:image/png;base64," + Convert.ToBase64String(new Utility().VerificationTextGenerator());
                        model.CapImageText = Convert.ToString(Session["Captcha"]);

                        return View("Sample", model);//Returns the view with the input values so that the user doesn't have to retype again

                    }
                }*/
                return View();
            }
            else
            {
                //MainDbContext db = new MainDbContext();
                using (var db = new MainDbContext())
                {
                    var usernameCheck = db.Users.FirstOrDefault(u => u.Username == model.Users.Username);
                    var getPassword = db.Users.Where(u => u.Username == model.Users.Username).Select(u => u.Password);
                    var materializePassword = getPassword.ToList();
                    if (materializePassword.Count != 0)
                    {
                        var password = materializePassword[0];
                        var decryptedPassword = CustomDecrypt.Decrypt(password);
                        if (model.Users.Username != null && model.Users.Password == decryptedPassword)
                        {
                            var searchrole = db.Role.Where(u => u.Id == usernameCheck.Key_Role).Select(u => u.RoleType);
                            var materializeRole = searchrole.ToList();
                            var role = materializeRole[0];
                            var ctx = Request.GetOwinContext();
                            var authManager = ctx.Authentication;
                            if (role == "doctor")
                            {
                                var checkname = db.Doctor.FirstOrDefault(u => u.Key_Users == usernameCheck.Id);
                                var getname = db.Doctor.Where(u => u.Id == checkname.Id).Select(u => u.FirstName);
                                var materializeName = getname.ToList();
                                var name = materializeName[0];

                                var identity = new ClaimsIdentity(new[] {
                           new Claim(ClaimTypes.Name, name),
                           new Claim(ClaimTypes.Role, role)
                           }, "ApplicationCookie");

                                authManager.SignIn(identity);
                                return RedirectToAction("Index", "Doctor");
                            }
                            else if (role == "FDR")
                            {
                                var checkname = db.FDR.FirstOrDefault(u => u.Key_Users == usernameCheck.Id);
                                //var getname = db.PersonInfo.Where(u => u.Id == checkname.Key_PersonInfo).Select(u => u.FirstName);
                                var getname = db.FDR.Select(u => u.FirstName);
                                var materializeName = getname.ToList();
                                var name = materializeName[0];
                                var identity = new ClaimsIdentity(new[] {
                           new Claim(ClaimTypes.Name, name),
                           new Claim(ClaimTypes.Role, role)
                           }, "ApplicationCookie");


                                authManager.SignIn(identity);
                                return RedirectToAction("Index", "FDR");
                            }
                            else if (role == "nurse")
                            {
                                /*var checkname = db.Nurse.FirstOrDefault(u => u.Key_Users == usernameCheck.Id);
                                 var getname = db.PersonInfo.Where(u => u.Id == checkname.Key_PersonInfo).Select(u => u.FirstName);
                                 var materializeName = getname.ToList();
                                 var name = materializeName[0];*/
                                var identity = new ClaimsIdentity(new[] {
                           //new Claim(ClaimTypes.Name, name),
                           new Claim(ClaimTypes.Role, role)
                           }, "ApplicationCookie");


                                authManager.SignIn(identity);
                                return RedirectToAction("Index", "Nurse");
                            }
                            else if (role == "administrator")
                            {
                                /*var checkname = db.Admin.FirstOrDefault(u => u.Key_Users == usernameCheck.Id);
                                 var getname = db.PersonInfo.Where(u => u.Id == checkname.Key_PersonInfo).Select(u => u.FirstName);
                                 var materializeName = getname.ToList();
                                 var name = materializeName[0];*/
                                var identity = new ClaimsIdentity(new[] {
                           //new Claim(ClaimTypes.Name, name),
                           new Claim(ClaimTypes.Role, role)
                           }, "ApplicationCookie");


                                authManager.SignIn(identity);
                                return RedirectToAction("Index", "Admin");
                            }

                        }
                        if (Session["time"] == null)
                        {
                            Session["time"] = time + 1;
                            Response.Write("Wrong times: 1");
                        }
                        else
                        {
                            time = (int)Session["time"];
                            time++;
                            Session["time"] = time;
                            Response.Write("Wrong times: " + time.ToString());
                            if (time >= 3)
                            {
                                model.CapImage = "data:image/png;base64," + Convert.ToBase64String(new Utility().VerificationTextGenerator());
                                model.CapImageText = Convert.ToString(Session["Captcha"]);
                        
                                return View("Sample", model);//Returns the view with the input values so that the user doesn't have to retype again

                            }
                        }
                    }
                    else
                    {
                        
                        if (Session["time"] == null)
                        {
                            Session["time"] = time + 1;
                            Response.Write("Wrong times: 1");
                        }
                        else
                        {
                            time = (int)Session["time"];
                            time++;
                            Session["time"] = time;
                            Response.Write("Wrong times: " + time.ToString());
                            if (time >= 3)
                            {
                                model.CapImage = "data:image/png;base64," + Convert.ToBase64String(new Utility().VerificationTextGenerator());
                                model.CapImageText = Convert.ToString(Session["Captcha"]);
                        
                                return View("Sample", model);//Returns the view with the input values so that the user doesn't have to retype again

                            }
                        }
                        //model.CapImage = "data:image/png;base64," + Convert.ToBase64String(new Utility().VerificationTextGenerator());
                        //model.CapImageText = Convert.ToString(Session["Captcha"]);
                        ModelState.AddModelError("", "Invalid username or password"); //Should always be declared on the end of an action method
                    }

                    return View(model);
                }
            }
        }
                

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            Session.Clear();

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }
        
        public ActionResult Registration()
        {
            return View();
        }

        

        [HttpPost]
        public ActionResult Registration(LoginModel model)
        {
            if (    ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    var queryUser = db.Users.FirstOrDefault(u => u.Username == model.Users.Username);
                    if (queryUser == null)
                    {
                        var encryptedPassword = CustomEnrypt.Encrypt(model.Users.Password);
                        var user = db.Users.Create();
                        var role = db.Role.Create();
                        user.Username = model.Users.Username;
                        user.Password = encryptedPassword;
                        role.RoleType = "administrator";
                        db.Role.Add(role);
                        db.SaveChanges();

                        user.Key_Role = role.Id;
                        db.Users.Add(user);
                        db.SaveChanges();

                        return RedirectToAction("Login", "Auth");
                    }
                    else
                    {
                        return RedirectToAction("Registration", "Auth");
                    }

                }
            }
            else
            {
                ModelState.AddModelError("", "One or more fields have been");
            }
            return View();
        }
    }
}