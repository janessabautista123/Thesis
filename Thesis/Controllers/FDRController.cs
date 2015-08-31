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
    public class FDRController : Controller
    {
        // GET: FDR
        public ActionResult Index()
        {
            
            using (var db = new MainDbContext())
            {

                string firstname = User.Identity.Name;
                var FdrModel = db.FDR.FirstOrDefault(u => u.FirstName.Equals(firstname));

                // Construct the viewmodel
                FDR model = new FDR();
                model.FirstName = FdrModel.FirstName;

                return View(model);

            }

        }

        public ActionResult CreatePatient()
        {
                /*string firstname = User.Identity.Name;
                var profilemodel = db.PersonInfo.FirstOrDefault(u => u.FirstName.Equals(firstname));

                var searchkeycont = db.PersonInfo.Where(u => u.FirstName == firstname).Select(u => u.Key_Contact);
                var materializeKey = searchkeycont.ToList();
                var Keycont = materializeKey[0];
                var contactmodel = db.Contact.FirstOrDefault(u => u.Id.Equals(Keycont));

                var searchkeyadd = db.PersonInfo.Where(u => u.FirstName == firstname).Select(u => u.Key_Address);
                var materializeKeyAdd = searchkeyadd.ToList();
                var Keyadd = materializeKeyAdd[0];
                var addmodel = db.Address.FirstOrDefault(u => u.Id.Equals(Keyadd));

                var searchdoc = db.PersonInfo.Where(u => u.FirstName == firstname).Select(u => u.Id);
                var materializedoc = searchdoc.ToList();
                var Keydoc = materializedoc[0];
                var docmodel = db.Doctor.FirstOrDefault(u => u.Key_PersonInfo.Equals(Keydoc));

                var searchuser = db.Doctor.Where(u => u.Key_PersonInfo == Keydoc).Select(u => u.Key_Users);
                var materializeuser = searchuser.ToList();
                var Keyuser = materializeuser[0];
                var usermodel = db.Users.FirstOrDefault(u => u.Id.Equals(Keyuser));*/
                
               //  var searchrole = db.Role.Where(u => u.RoleType == "doctor").Select(u => u.Id);
                
               // var materializeKey = searchrole.ToList();

               // var searchuser = db.Users.Where(u => u.Key_Role == u.Id).Select(u => u.Id);
                
                /*
                for (int i = 0; i < materializeKey.Count ; i++)
                {
                    var Keyrole = materializeKey[i];

                    var searchuser = db.Users.Where(u => u.Key_Role == Keyrole).Select(u => u.Id);


                    var materializeUserId = searchrole.ToList();

                    for (int j = 0; j < materializeUserId.Count; j++) 
                    {
                        var Keyuser = materializeUserId[j];
                        var searchdoctor = db.Doctor.Where(u => u.Key_Users == Keyuser).Select(u => u.Key_PersonInfo);

                        var materializeDoctorId = searchdoctor.ToList();

                        for (int k = 0; k < materializeDoctorId.Count; k++)
                        {
                            var Keypersoninfo = materializeDoctorId[k];
                            var searchpersoninfo = db.PersonInfo.Where(u => u.Id == Keypersoninfo).Select(u => u.Id);

                            var materializePersoninfoId = searchpersoninfo.ToList();

                            for (int l = 0; l < materializePersoninfoId.Count; l++)
                            {
                                var Namepersoninfo = materializePersoninfoId[l];
                                var profilemodel = db.PersonInfo.FirstOrDefault(u => u.Id.Equals(Namepersoninfo));
                                var viewmodel = new PatientModel
                                {
                                    PersonInfo = profilemodel
                                };
                            }
                        }

                    }


                }*/

                MainDbContext db = new MainDbContext();

                    ViewBag.FirstName = new SelectList(db.Doctor, "Id", "FirstName");

                    return View();

                /*var viewmodel = new PatientModel
                {
                    PersonInfo = profilemodel,
                    Contact = contactmodel,
                    Address = addmodel,
                    Doctor = docmodel,
                    Users = usermodel,
                    Role = rolemodel
                }; */


            
        }

        [HttpPost]
        public ActionResult CreatePatient(PatientModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    var queryUser = db.Users.FirstOrDefault(u => u.Username == model.Users.Username);
                    if (queryUser == null)
                    {
                        var encryptedPassword = CustomEnrypt.Encrypt(model.Users.Password);

                        var patient = db.Patient.Create();
                        var address = db.Address.Create();
                        var contact = db.Contact.Create();
                        var user = db.Users.Create();
                        var role = db.Role.Create();
                        patient.FirstName = model.Patient.FirstName;
                        patient.MiddleName = model.Patient.MiddleName;
                        patient.LastName = model.Patient.LastName;
                        patient.Email = model.Patient.Email;
                        patient.DateBirth = model.Patient.DateBirth;
                        patient.Sex = model.Patient.Sex;
                        patient.MaritalStatus = model.Patient.MaritalStatus;
                        patient.HoursWorked = model.Patient.HoursWorked;
                        patient.CompanyName = model.Patient.CompanyName;
                        patient.Occupation = model.Patient.Occupation;
                        address.AddressType = model.Address.AddressType;
                        address.City = model.Address.City;
                        address.Province = model.Address.Province;
                        address.Zipcode = model.Address.Zipcode;
                        user.Username = model.Users.Username;
                        user.Password = encryptedPassword;
                        user.Password = encryptedPassword;
                        contact.MobileNo = model.Contact.MobileNo;
                        contact.PhoneNo = model.Contact.PhoneNo;

                        db.Address.Add(address);
                        db.Contact.Add(contact);
                        db.Role.Add(role);
                        db.SaveChanges();

                        user.Key_Role = role.Id;
                        db.Users.Add(user);
                        db.SaveChanges();

                        patient.Key_Address = address.Id;
                        patient.Key_Contact = contact.Id;
                        db.Patient.Add(patient);
                        db.SaveChanges();

                        return RedirectToAction("CreatePatient", "FDR");
                    }
                    else
                    {
                        return RedirectToAction("CreatePatient", "FDR");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "One or more fields have been");
            }
            return View();
        }


        public ActionResult EditProfile()
        { 
            using (var db = new MainDbContext())
            {

                string firstname = User.Identity.Name;
                var FdrModel = db.FDR.FirstOrDefault(u => u.FirstName.Equals(firstname));

                var searchKeyAdd = db.FDR.Select(u => u.Key_Address);
                var materializeAddKey = searchKeyAdd.ToList();
                var KeyAdd = materializeAddKey[0];
                var AddModel = db.Address.FirstOrDefault(u => u.Id.Equals(KeyAdd));

                var searchKeyCon = db.FDR.Select(u => u.Key_Contact);
                var materializeConKey = searchKeyCon.ToList();
                var KeyCon = materializeConKey[0];
                var ConModel = db.Contact.FirstOrDefault(u => u.Id.Equals(KeyCon));

                var searchUserKey = db.FDR.Select(u => u.Key_Users);
                var materializeUserKey = searchUserKey.ToList();
                var KeyUser = materializeUserKey[0];
                var UsersModel = db.Users.FirstOrDefault(u => u.Id.Equals(KeyUser));

                var password = CustomDecrypt.Decrypt(UsersModel.Password);

                UsersModel.Password = password;

                var viewmodel = new FDRModel
                {
                    Contact = ConModel,
                    Address = AddModel,
                    FDR = FdrModel,
                    Users = UsersModel
                };

                return View(viewmodel);
            }

        }

        [HttpPost]
        public ActionResult EditProfile(FDRModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    string firstname = User.Identity.Name;

                    //Get Doctor
                    var FdrModel = db.FDR.FirstOrDefault(u => u.FirstName.Equals(firstname));
                    FdrModel.HospName = model.FDR.HospName;
                    FdrModel.EmployeeId = model.FDR.EmployeeId;
                    FdrModel.LicenseNo = model.FDR.LicenseNo;
                    FdrModel.Specialization = model.FDR.Specialization;
                    FdrModel.FirstName = model.FDR.FirstName;
                    FdrModel.MiddleName = model.FDR.MiddleName;
                    FdrModel.LastName = model.FDR.LastName;
                    FdrModel.Email = model.FDR.Email;
                    FdrModel.DateBirth = model.FDR.DateBirth;
                    FdrModel.Sex = model.FDR.Sex;
                    FdrModel.SecQuestion = model.FDR.SecQuestion;
                    FdrModel.SecAnswer = model.FDR.SecAnswer;

                    db.Entry(FdrModel).State = EntityState.Modified;

                    // Get the Address
                    var searchKeyAdd = db.FDR.Select(u => u.Key_Address);
                    var materializeAddKey = searchKeyAdd.ToList();
                    var KeyAdd = materializeAddKey[0];
                    var AddModel = db.Address.FirstOrDefault(u => u.Id.Equals(KeyAdd));

                    AddModel.City = model.Address.City;
                    AddModel.Province = model.Address.Province;
                    AddModel.Zipcode = model.Address.Zipcode;
                    AddModel.AddressType = model.Address.AddressType;

                    db.Entry(AddModel).State = EntityState.Modified;

                    // Get the Contact
                    var searchKeyCon = db.FDR.Select(u => u.Key_Contact);
                    var materializeConKey = searchKeyCon.ToList();
                    var KeyCon = materializeConKey[0];
                    var ConModel = db.Contact.FirstOrDefault(u => u.Id.Equals(KeyCon));

                    ConModel.MobileNo = model.Contact.MobileNo;
                    ConModel.PhoneNo = model.Contact.PhoneNo;

                    db.Entry(ConModel).State = EntityState.Modified;

                    // Get the Users
                    var searchUserKey = db.FDR.Select(u => u.Key_Users);
                    var materializeUserKey = searchUserKey.ToList();
                    var KeyUser = materializeUserKey[0];
                    var UsersModel = db.Users.FirstOrDefault(u => u.Id.Equals(KeyUser));

                    var encryptedPassword = CustomEnrypt.Encrypt(model.Users.Password);
                    UsersModel.Username = model.Users.Username;
                    UsersModel.Password = encryptedPassword;

                    db.Entry(UsersModel).State = EntityState.Modified;


                    db.SaveChanges();

                    var identity = new ClaimsIdentity(new[] {
                           new Claim(ClaimTypes.Name, FdrModel.FirstName),
                           new Claim(ClaimTypes.Role, "FDR")
                           }, "ApplicationCookie");

                    var ctx = Request.GetOwinContext();

                    var authManager = ctx.Authentication;

                    authManager.SignIn(identity);


                    return RedirectToAction("Index", "FDR");
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