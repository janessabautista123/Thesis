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
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateDoctor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDoctor(DoctorModel model)
        {
            if(ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    var queryUser = db.Users.FirstOrDefault(u => u.Username == model.Users.Username);
                    if (queryUser == null)
                    {
                            var encryptedPassword = CustomEnrypt.Encrypt(model.Users.Password);
                            
                            var doctor = db.Doctor.Create();
                            var address = db.Address.Create();
                            var contact = db.Contact.Create();
                            var user = db.Users.Create();
                            var role = db.Role.Create();
                            doctor.FirstName = model.Doctor.FirstName;
                            doctor.MiddleName = model.Doctor.MiddleName;
                            doctor.LastName = model.Doctor.LastName;
                            doctor.Email = model.Doctor.Email;
                            doctor.DateBirth = model.Doctor.DateBirth;
                            doctor.Sex = model.Doctor.Sex;
                            doctor.SecQuestion = model.Doctor.SecQuestion;
                            doctor.SecAnswer = model.Doctor.SecAnswer;
                            doctor.EmployeeId = model.Doctor.EmployeeId;
                            doctor.LicenseNo = model.Doctor.LicenseNo;
                            doctor.HospName = model.Doctor.HospName;
                            doctor.Specialization = model.Doctor.Specialization;
                            address.AddressType = model.Address.AddressType;
                            address.City = model.Address.City;
                            address.Province = model.Address.Province;
                            address.Zipcode = model.Address.Zipcode;
                            user.Username = model.Users.Username;
                            user.Password = encryptedPassword;
                            user.Password = encryptedPassword;
                            role.RoleType = "doctor";
                            contact.MobileNo = model.Contact.MobileNo;
                            contact.PhoneNo = model.Contact.PhoneNo;

                            db.Address.Add(address);
                            db.Contact.Add(contact);
                            db.Role.Add(role);
                            db.SaveChanges();

                            user.Key_Role = role.Id;
                            db.Users.Add(user);
                            db.SaveChanges();

                            doctor.Key_Users = user.Id;
                            doctor.Key_Address = address.Id;
                            doctor.Key_Contact = contact.Id;
                            db.Doctor.Add(doctor);
                            db.SaveChanges();

                            return RedirectToAction("CreateDoctor", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("CreateDoctor", "Admin");
                        }
                    }
                }
            else
            {
                 ModelState.AddModelError("", "One or more fields have been");
            }
            return View();
        }

        public ActionResult CreateNurse()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateNurse(NurseModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    var queryUser = db.Users.FirstOrDefault(u => u.Username == model.Users.Username);
                    if (queryUser == null)
                    {
                        var encryptedPassword = CustomEnrypt.Encrypt(model.Users.Password);

                        var nurse = db.Nurse.Create();
                        var address = db.Address.Create();
                        var contact = db.Contact.Create();
                        var user = db.Users.Create();
                        var role = db.Role.Create();
                        nurse.FirstName = model.Nurse.FirstName;
                        nurse.MiddleName = model.Nurse.MiddleName;
                        nurse.LastName = model.Nurse.LastName;
                        nurse.Email = model.Nurse.Email;
                        nurse.EmployeeId = model.Nurse.EmployeeId;
                        nurse.LicenseNo = model.Nurse.LicenseNo;
                        nurse.HospName = model.Nurse.HospName;
                        nurse.DateBirth = model.Nurse.DateBirth;
                        nurse.Sex = model.Nurse.Sex;
                        address.AddressType = model.Address.AddressType;
                        address.City = model.Address.City;
                        address.Province = model.Address.Province;
                        address.Zipcode = model.Address.Zipcode;
                        user.Username = model.Users.Username;
                        user.Password = encryptedPassword;
                        user.Password = encryptedPassword;
                        role.RoleType = "nurse";
                        nurse.SecQuestion = model.Nurse.SecQuestion;
                        nurse.SecAnswer = model.Nurse.SecAnswer;
                        contact.MobileNo = model.Contact.MobileNo;
                        contact.PhoneNo = model.Contact.PhoneNo;

                        db.Address.Add(address);
                        db.Contact.Add(contact);
                        db.Role.Add(role);
                        db.SaveChanges();

                        user.Key_Role = role.Id;
                        db.Users.Add(user);
                        db.SaveChanges();

                        nurse.Key_Users = user.Id;
                        nurse.Key_Address = address.Id;
                        nurse.Key_Contact = contact.Id;
                        db.Nurse.Add(nurse);
                        db.SaveChanges();

                        return RedirectToAction("CreateNurse", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("CreateNurse", "Admin");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "One or more fields is incorrect");
            }
            return View();
        }

        public ActionResult CreateFDR()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateFDR(FDRModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    var queryUser = db.Users.FirstOrDefault(u => u.Username == model.Users.Username);
                    if (queryUser == null)
                    {
                        
                            var encryptedPassword = CustomEnrypt.Encrypt(model.Users.Password);

                            var fdr = db.FDR.Create();
                            var address = db.Address.Create();
                            var contact = db.Contact.Create();
                            var user = db.Users.Create();
                            var role = db.Role.Create();
                            fdr.FirstName = model.FDR.FirstName;
                            fdr.MiddleName = model.FDR.MiddleName;
                            fdr.LastName = model.FDR.LastName;
                            fdr.Email = model.FDR.Email;
                            fdr.EmployeeId = model.FDR.EmployeeId;
                            fdr.LicenseNo = model.FDR.LicenseNo;
                            fdr.HospName = model.FDR.HospName;
                            fdr.Specialization = model.FDR.Specialization;
                            fdr.DateBirth = model.FDR.DateBirth;
                            fdr.Sex = model.FDR.Sex;
                            address.AddressType = model.Address.AddressType;
                            address.City = model.Address.City;
                            address.Province = model.Address.Province;
                            address.Zipcode = model.Address.Zipcode;
                            user.Username = model.Users.Username;
                            user.Password = encryptedPassword;
                            user.Password = encryptedPassword;
                            role.RoleType = "FDR";
                            fdr.SecQuestion = model.FDR.SecQuestion;
                            fdr.SecAnswer = model.FDR.SecAnswer;
                            contact.MobileNo = model.Contact.MobileNo;
                            contact.PhoneNo = model.Contact.PhoneNo;

                            db.Address.Add(address);
                            db.Contact.Add(contact);
                            db.Role.Add(role);
                            db.SaveChanges();

                            user.Key_Role = role.Id;
                            db.Users.Add(user);
                            db.SaveChanges();

                            fdr.Key_Users = user.Id;
                            fdr.Key_Address = address.Id;
                            fdr.Key_Contact = contact.Id;
                            db.FDR.Add(fdr);
                            db.SaveChanges();
          
                            return RedirectToAction("CreateFDR", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("CreateFDR", "Admin");
                        }
                    }
            }
            else
            {
                ModelState.AddModelError("", "One or more fields have been");
            }
            return View();
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }
    }
}