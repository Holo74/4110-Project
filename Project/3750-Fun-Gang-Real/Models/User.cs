﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Drawing;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;

namespace Assignment_1.Models
{
    public class User
    {
        public int Id { get; set; }

        //[RegularExpression(@"^[a-zA-Z\.]*+@+[a-zA-Z\.]*$")]
        [Required]
        public string Email { get; set; }

        // Requirements.  A single lower cases letter.  1 upper case letter.  1 decimal.  At least 1 special character.  Min length of 8 characters
        [Validators.PasswordValidation]
        //[StringLength(80, MinimumLength = 6)]
        [Required]
        public string Password { get; set; }
        [Compare(otherProperty: "Password"), Display(Name = "Confirm Password"), NotMapped]
        public string ConfirmPassword { get; set; }

        // [Required]
        public string FirstName { get; set; }
        //[Required]
        public string LastName { get; set; }

        [DateValidation(ErrorMessage = "User Age must be at least 16")]

        //[Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        //  [Required]
        public string UserType { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? PhoneNumber { get; set; }

        public string? ReferenceOne { get; set; }
        public string? ReferenceTwo { get; set; }
        public string? ReferenceThree { get; set; }
        //[DataType(DataType.Currency)]
        public decimal? Balance { get; set; }
        public string? Image { get; set; }
    }
}