﻿using Assignment_1.Data;
using Assignment_1.Migrations;
using Assignment_1.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Assignment_1.Controllers
{
    public class ClassAssignmentsController : Controller
    {
        private readonly Assignment_1Context _context;
        public ClassAssignmentsController(Assignment_1Context context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index(int id)
        {

            //if (id != null)
            //{
            //    ClassUserAssignments classUserAssignmentView = new ClassUserAssignments();
            var user = _context.User.Where(x => x.Id == id).First();
            //    classUserAssignmentView.User = user;
            //    var UserID = HttpContext.Session.GetInt32("UserID");
            ViewData["Student"] = user.UserType;
            //    var Course = from c in _context.Class select c;
            //    if (UserID != null)
            //    {
            //        Course = Course.Where(c => c.UserId == UserID);
            //        classUserAssignmentView.Class = Course.ToList();
            //        return View(classUserAssignmentView);
            //    }
            //}
            return View();
        }
        public IActionResult Create() 
        {
            var AssignmentID = HttpContext.Session.GetInt32("AssignmentID");
            return View();
        }

        public IActionResult Assignment(int ID)
        {
            AssignmentSubmissionViewModel ASVM = new AssignmentSubmissionViewModel();            

            //set assignment according to ID
            var assignments = from a in _context.ClassAssignments select a;
            ASVM.Assignment = assignments.Where(a => a.Id == ID).FirstOrDefault();

            //check database for existing entry
            int? UserID = HttpContext.Session.GetInt32("UserID");
            int ClassID = ASVM.Assignment.ClassId;

            //in case not already set, used in the submit()
            HttpContext.Session.SetInt32("AssignmentID",ID);
            HttpContext.Session.SetInt32("ClassID",ClassID);

            int AssignmentID = ID;
            var submission = from s in _context.AssignmentSubmissions select s;
            if (UserID != null && ASVM.Assignment != null)
            {
                //find this users assignment for a class if it exists
                ASVM.Submission = submission.Where(x => x.UserFK== UserID).Where(y => y.AssignmentFK == AssignmentID).Where(z => z.ClassFK == ClassID).FirstOrDefault();
                
            }
            return View(ASVM);
        }

        public IActionResult Submit()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Submit([Bind("Id,Data")] AssignmentSubmissions s)
        {
            //AssignmentSubmissions s = new AssignmentSubmissions();
            int? UserID = HttpContext.Session.GetInt32("UserID");
            int? AssignmentID = HttpContext.Session.GetInt32("AssignmentID");
            int? ClassID = HttpContext.Session.GetInt32("ClassID");
            s.UserFK = UserID == null ? 0 : UserID.Value;
            s.AssignmentFK = AssignmentID == null ? 0 : AssignmentID.Value;
            s.ClassFK = ClassID == null ? 0 : ClassID.Value;
            //s.Data= Data;
            s.SubmitDate= DateTime.Now;
            //s.SubmitTime= default(DateTime).Add(DateTime.Now.TimeOfDay);


            string directory = "wwwroot/Submissions";
            Directory.CreateDirectory(directory);
            IFormFile z = Request.Form.Files[0];
            string fileExtension = z.FileName.Substring(z.FileName.LastIndexOf('.'));
            string path = directory + "/" + UserID +"."+AssignmentID + fileExtension;
            using (Stream filestream = new FileStream(directory + "/" + UserID +"."+ AssignmentID + fileExtension, FileMode.Create, FileAccess.Write))
            {
                // Saves the file to where ever the filestream was pointed to.
                z.CopyTo(filestream);
                // Won't save properly without this
                filestream.Close();
            }

            s.Data = path;
        
            _context.AssignmentSubmissions.Add(s);
			await _context.SaveChangesAsync();

			return RedirectToAction("Assignment", new {ID = AssignmentID});//hardcode for testing
        }
    }
}
