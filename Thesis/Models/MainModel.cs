using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thesis.Models
{
    public class MainModel
    {
    }

    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string MaritalStatus { get; set; }
        public string HoursWorked { get; set; }
        public string Occupation { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string SecQuestion { get; set; }
        public string SecAnswer { get; set; }
        public string DateBirth { get; set; }
        public string Email { get; set; }
        public int Key_Address { get; set; }
        public int Key_Contact { get; set; }
        public string CompanyName { get; set; }
        public int Key_CurrDoctor { get; set; }
        public int Key_PrevDoctor { get; set; }


    }

    public class Nurse
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string HospName { get; set; }
        public string LicenseNo { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string SecQuestion { get; set; }
        public string SecAnswer { get; set; }
        public string DateBirth { get; set; }
        public string Email { get; set; }
        public int Key_Address { get; set; }
        public int Key_Contact { get; set; }
        public int Key_Users { get; set; }
    }

    public class FDR
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string HospName { get; set; }
        public string LicenseNo { get; set; }
        public string Specialization { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string SecQuestion { get; set; }
        public string SecAnswer { get; set; }
        public string DateBirth { get; set; }
        public string Email { get; set; }
        public int Key_Address { get; set; }
        public int Key_Contact { get; set; }
        public int Key_Users { get; set; }
    }

    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string HospName { get; set; }
        public string LicenseNo { get; set; }
        public string Specialization { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string SecQuestion { get; set; }
        public string SecAnswer { get; set; }
        public string DateBirth { get; set; }
        public string Email { get; set; }
        public int Key_Address { get; set; }
        public int Key_Contact { get; set; }
        public int Key_Users { get; set; }
    }

    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string AddressType { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Zipcode { get; set; }
    }

    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
    }

    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int Key_Role { get; set; }

    }

    public class Role
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string RoleType { get; set; }
    }
}