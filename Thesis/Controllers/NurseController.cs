using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thesis.Models;
using Thesis.CustomLibraries;
using System.Data.Entity;
using System.Security.Claims;
using System.Web.Security;

namespace Thesis.Controllers
{
    public class NurseController : Controller
    {
        // GET: Nurse
        public ActionResult Index()
        {

            using (var db = new MainDbContext())
             {
                 string firstname = User.Identity.Name;
                 var NurseModel = db.Nurse.FirstOrDefault(u => u.FirstName.Equals(firstname));

                 // Construct the viewmodel
                 Nurse model = new Nurse();
                 model.FirstName = NurseModel.FirstName;

                 return View(model);
            
             }
        }

        public ActionResult EditProfile()
        {
            using (var db = new MainDbContext())
            {

                string firstname = User.Identity.Name;
                var NurseModel = db.Nurse.FirstOrDefault(u => u.FirstName.Equals(firstname));

                var searchKeyAdd = db.Nurse.Select(u => u.Key_Address);
                var materializeAddKey = searchKeyAdd.ToList();
                var KeyAdd = materializeAddKey[0];
                var AddModel = db.Address.FirstOrDefault(u => u.Id.Equals(KeyAdd));

                var searchKeyCon = db.Nurse.Select(u => u.Key_Contact);
                var materializeConKey = searchKeyCon.ToList();
                var KeyCon = materializeConKey[0];
                var ConModel = db.Contact.FirstOrDefault(u => u.Id.Equals(KeyCon));

                var searchUserKey = db.Nurse.Select(u => u.Key_Users);
                var materializeUserKey = searchUserKey.ToList();
                var KeyUser = materializeUserKey[0];
                var UsersModel = db.Users.FirstOrDefault(u => u.Id.Equals(KeyUser));

                var password = CustomDecrypt.Decrypt(UsersModel.Password);

                UsersModel.Password = password;

                var viewmodel = new NurseModel
                {
                    Contact = ConModel,
                    Address = AddModel,
                    Nurse = NurseModel,
                    Users = UsersModel
                };

                return View(viewmodel);
            }
        }

        [HttpPost]
        public ActionResult EditProfile(NurseModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    string firstname = User.Identity.Name;

                    //Get Doctor
                    var NurseModel = db.Nurse.FirstOrDefault(u => u.FirstName.Equals(firstname));
                    NurseModel.HospName = model.Nurse.HospName;
                    NurseModel.EmployeeId = model.Nurse.EmployeeId;
                    NurseModel.LicenseNo = model.Nurse.LicenseNo;
                    NurseModel.FirstName = model.Nurse.FirstName;
                    NurseModel.MiddleName = model.Nurse.MiddleName;
                    NurseModel.LastName = model.Nurse.LastName;
                    NurseModel.Email = model.Nurse.Email;
                    NurseModel.DateBirth = model.Nurse.DateBirth;
                    NurseModel.Sex = model.Nurse.Sex;
                    NurseModel.SecQuestion = model.Nurse.SecQuestion;
                    NurseModel.SecAnswer = model.Nurse.SecAnswer;

                    db.Entry(NurseModel).State = EntityState.Modified;

                    // Get the Address
                    var searchKeyAdd = db.Nurse.Select(u => u.Key_Address);
                    var materializeAddKey = searchKeyAdd.ToList();
                    var KeyAdd = materializeAddKey[0];
                    var AddModel = db.Address.FirstOrDefault(u => u.Id.Equals(KeyAdd));

                    AddModel.City = model.Address.City;
                    AddModel.Province = model.Address.Province;
                    AddModel.Zipcode = model.Address.Zipcode;
                    AddModel.AddressType = model.Address.AddressType;

                    db.Entry(AddModel).State = EntityState.Modified;

                    // Get the Contact
                    var searchKeyCon = db.Nurse.Select(u => u.Key_Contact);
                    var materializeConKey = searchKeyCon.ToList();
                    var KeyCon = materializeConKey[0];
                    var ConModel = db.Contact.FirstOrDefault(u => u.Id.Equals(KeyCon));

                    ConModel.MobileNo = model.Contact.MobileNo;
                    ConModel.PhoneNo = model.Contact.PhoneNo;

                    db.Entry(ConModel).State = EntityState.Modified;

                    // Get the Users
                    var searchUserKey = db.Nurse.Select(u => u.Key_Users);
                    var materializeUserKey = searchUserKey.ToList();
                    var KeyUser = materializeUserKey[0];
                    var UsersModel = db.Users.FirstOrDefault(u => u.Id.Equals(KeyUser));

                    var encryptedPassword = CustomEnrypt.Encrypt(model.Users.Password);
                    UsersModel.Username = model.Users.Username;
                    UsersModel.Password = encryptedPassword;

                    db.Entry(UsersModel).State = EntityState.Modified;


                    db.SaveChanges();

                    var identity = new ClaimsIdentity(new[] {
                           new Claim(ClaimTypes.Name, NurseModel.FirstName),
                           new Claim(ClaimTypes.Role, "FDR")
                           }, "ApplicationCookie");

                    var ctx = Request.GetOwinContext();

                    var authManager = ctx.Authentication;

                    authManager.SignIn(identity);


                    return RedirectToAction("Index", "Nurse");
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