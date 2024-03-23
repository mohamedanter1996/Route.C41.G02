﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.DAL.Models
{
   public enum Gender
    {
        [EnumMember(Value = "Male")]
        Male = 1,
        [EnumMember(Value = "Female")]
        Female = 2
    }

  public  enum EmpType 
    {
        [EnumMember(Value = "FullTime")]
        FullTime =1,
        [EnumMember(Value = "PartTime")]
        PartTime =2
    }


    public class Employee:ModelBase
    {

        [Required]
        [MaxLength(50, ErrorMessage = "Max Length Of Name is 50 chars")]
        [MinLength(5, ErrorMessage = "Min Length Of Name is 5 chars")]
        public string Name { get; set; }

        [Range(22, 30)]
        public int Age { get; set; }

        [RegularExpression(@"^[@-91{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
, ErrorMessage = "Address must be like 123-Street-City-Country")]

        public string Address { get; set; }

        [DataType(DataType.Currency)]

        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]

        public string Email { get; set; }

        [Display(Name = "Phone Name")]
        [Phone]
        public string PhoneName { get; set; }

        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmpType EmployeeType { get; set; }    
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

    }
}
