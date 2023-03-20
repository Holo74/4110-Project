﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Assignment_1.Models
{
    public class Class
    {
        public Class(int userId, string department, int courseNumber, string courseName, int numOfCredits, string location, string daysOfWeek, DateTime startTime, DateTime endTime)
        {
            UserId = userId;
            Department = department;
            CourseNumber = courseNumber;
            CourseName = courseName;
            NumOfCredits = numOfCredits;
            Location = location;
            DaysOfWeek = daysOfWeek;
            StartTime = startTime;
            EndTime = endTime;
        }
    
        // NOT COMPLETELY ACCURATE YET **************************************************************
        //make sure to alter submit() in ClassesController if any of this changes
        [Required]
        public int ClassId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public int CourseNumber { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public int NumOfCredits { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string DaysOfWeek { get; set; }//MO:TU:WE:TH:FR
        [Required]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
    }
}