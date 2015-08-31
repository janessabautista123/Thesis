using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thesis.Models;
using Thesis.CustomLibraries;
using System.Data.Entity;
using System.Security.Claims;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;

namespace Thesis.Controllers
{
    public class DoctorController : Controller
    {
        
        // GET: Doctor
        public ActionResult Index()
        {
            
            using (var db = new MainDbContext())
            {
               // /*using (SqlCommand cmd = new SqlCommand())
               // {
               //     cmd.CommandType = CommandType.StoredProcedure;
               //     cmd.CommandText = "DOCUMENT_SELECT";
               //     cmd.Parameters.Add("FirstName");
               // }
                string firstname = User.Identity.Name;
                var DocModel = db.Doctor.FirstOrDefault(u => u.FirstName.Equals(firstname));


                // Construct the viewmodel
                Doctor model = new Doctor();
                model.FirstName = DocModel.FirstName;

                return View(model);

            }
        
        }


        public ActionResult EditProfile()
        {
            using (var db = new MainDbContext())
            {
                string firstname = User.Identity.Name;
                var DocModel = db.Doctor.FirstOrDefault(u => u.FirstName.Equals(firstname));

                var searchKeyAdd = db.Doctor.Select(u => u.Key_Address);
                var materializeAddKey = searchKeyAdd.ToList();
                var KeyAdd = materializeAddKey[0];
                var AddModel = db.Address.FirstOrDefault(u => u.Id.Equals(KeyAdd));

                var searchKeyCon = db.Doctor.Select(u => u.Key_Contact);
                var materializeConKey = searchKeyCon.ToList();
                var KeyCon = materializeConKey[0];
                var ConModel = db.Contact.FirstOrDefault(u => u.Id.Equals(KeyCon));

                var searchUserKey = db.Doctor.Select(u => u.Key_Users);
                var materializeUserKey = searchUserKey.ToList();
                var KeyUser = materializeUserKey[0];
                var UsersModel = db.Users.FirstOrDefault(u => u.Id.Equals(KeyUser));

                var password = CustomDecrypt.Decrypt(UsersModel.Password);

                UsersModel.Password = password;

                var viewmodel = new DoctorModel {
                                    Contact = ConModel,
                                    Address = AddModel,
                                    Doctor = DocModel,

                                    Users = UsersModel
                                    };

                return View(viewmodel);
            }
    
            
        }


        [HttpPost]
        public ActionResult EditProfile(DoctorModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {

                    string firstname = User.Identity.Name;

                    //Get Doctor
                    var DocModel = db.Doctor.FirstOrDefault(u => u.FirstName.Equals(firstname));
                    DocModel.HospName = model.Doctor.HospName;
                    DocModel.EmployeeId = model.Doctor.EmployeeId;
                    DocModel.LicenseNo = model.Doctor.LicenseNo;
                    DocModel.Specialization = model.Doctor.Specialization;
                    DocModel.FirstName = model.Doctor.FirstName;
                    DocModel.MiddleName = model.Doctor.MiddleName;
                    DocModel.LastName = model.Doctor.LastName;
                    DocModel.Email = model.Doctor.Email;
                    DocModel.DateBirth = model.Doctor.DateBirth;
                    DocModel.Sex = model.Doctor.Sex;
                    DocModel.SecQuestion = model.Doctor.SecQuestion;
                    DocModel.SecAnswer = model.Doctor.SecAnswer;

                    db.Entry(DocModel).State = EntityState.Modified;

                    // Get the Address
                    var searchKeyAdd = db.Doctor.Select(u => u.Key_Address);
                    var materializeAddKey = searchKeyAdd.ToList();
                    var KeyAdd = materializeAddKey[0];
                    var AddModel = db.Address.FirstOrDefault(u => u.Id.Equals(KeyAdd));

                    AddModel.City = model.Address.City;
                    AddModel.Province = model.Address.Province;
                    AddModel.Zipcode = model.Address.Zipcode;
                    AddModel.AddressType = model.Address.AddressType;

                    db.Entry(AddModel).State = EntityState.Modified;

                    // Get the Contact
                    var searchKeyCon = db.Doctor.Select(u => u.Key_Contact);
                    var materializeConKey = searchKeyCon.ToList();
                    var KeyCon = materializeConKey[0];
                    var ConModel = db.Contact.FirstOrDefault(u => u.Id.Equals(KeyCon));

                    ConModel.MobileNo = model.Contact.MobileNo;
                    ConModel.PhoneNo = model.Contact.PhoneNo;

                    db.Entry(ConModel).State = EntityState.Modified;

                    // Get the Users
                    var searchUserKey = db.Doctor.Select(u => u.Key_Users);
                    var materializeUserKey = searchUserKey.ToList();
                    var KeyUser = materializeUserKey[0];
                    var UsersModel = db.Users.FirstOrDefault(u => u.Id.Equals(KeyUser));

                    var encryptedPassword = CustomEnrypt.Encrypt(model.Users.Password);
                    UsersModel.Username = model.Users.Username;
                    UsersModel.Password = encryptedPassword;
                     
                    db.Entry(UsersModel).State = EntityState.Modified;

                   
                    db.SaveChanges();

                    var identity = new ClaimsIdentity(new[] {
                           new Claim(ClaimTypes.Name, DocModel.FirstName),
                           new Claim(ClaimTypes.Role, "doctor")
                           }, "ApplicationCookie");

                    var ctx = Request.GetOwinContext();

                    var authManager = ctx.Authentication;

                    authManager.SignIn(identity);

                    return RedirectToAction("Index", "Doctor");
                }
                
            }

            return View(model);
    
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