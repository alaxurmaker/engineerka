using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using Eregister.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eregister.Controllers
{
    public class TimetableController : BaseController
    {
        // GET: Timetable
        public ActionResult IndexForStudent()
        {
            var sched = new DHXScheduler(this);
            sched.Config.isReadonly = true;
            sched.Config.first_hour = 8;
            sched.Config.last_hour = 16;
            sched.Skin = DHXScheduler.Skins.Terrace;
            sched.LoadData = true;
            sched.EnableDataprocessor = true;
            sched.InitialDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            return View(sched);
        }

        public ActionResult IndexForTeacher()
        {
            var sched = new DHXScheduler(this);
            sched.Config.isReadonly = false;
            sched.Config.first_hour = 8;
            sched.Config.last_hour = 16;
            sched.Skin = DHXScheduler.Skins.Terrace;
            sched.LoadData = true;
            sched.EnableDataprocessor = true;
            sched.InitialDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            return View(sched);
        }

        public ContentResult Data()
        {
            var db = new ApplicationDbContext();
            var group = TempData["TeacherGroup"];
            int groupId=0;
            if (group != null)
            {              
                groupId = Convert.ToInt32(group.ToString());
            }
            else
            {
                var groupEdit = TempData["TeacherGroupEdit"];
                groupId = Convert.ToInt32(groupEdit.ToString());
            }

            //Przedłużanie żywotności TempData
            TempData.Remove("TeacherGroupEdit");
            TempData.Add("TeacherGroupEdit", groupId);
            TempData.Remove("TeacherGroup");
            TempData.Add("TeacherGroup", groupId);

            return (new SchedulerAjaxData(
                 db.Events
                   .Select(e => new { e.id, e.text, e.start_date, e.end_date, e.group_id })
                     .Where(x => x.group_id == groupId))
                );
        }

        public ContentResult Save(FormCollection actionValues)
        {            
            var action = new DataAction(actionValues);
            var changedEvent = DHXEventsHelper.Bind<Event>(actionValues);
            var entities = new ApplicationDbContext();
            try
            {
                var group = TempData["TeacherGroupEdit"];
                if (Int32.TryParse(group.ToString(), out int groupId))
                {
                    switch (action.Type)
                    {
                        case DataActionTypes.Insert:
                            var newEvent = new Event();
                            newEvent = changedEvent;
                            newEvent.group_id = groupId;
                            entities.Events.Add(newEvent);
                            break;
                        case DataActionTypes.Delete:
                            changedEvent = entities.Events.FirstOrDefault(ev => ev.id == action.SourceId);
                            entities.Events.Remove(changedEvent);
                            break;
                        default:// "update"
                            var target = entities.Events.Single(e => e.id == changedEvent.id);
                            DHXEventsHelper.Update(target, changedEvent, new List<string> { "id" });
                            break;
                    }
                    entities.SaveChanges();
                    action.TargetId = changedEvent.id;
                }

            }
            catch (Exception ex)
            {
                action.Type = DataActionTypes.Error;
            }
            
            return (new AjaxSaveResponse(action));
        }

        public ActionResult Timetables()
        {
            TempData.Remove("TeacherGroup");
            TempData.Remove("TeacherGroupEdit");

            var db = new ApplicationDbContext();
            return View(db.Groups.ToList());

        }

        public ActionResult ChosenGroup(int? id)
        {
            if (User.IsInRole("Teacher"))
            {
                TempData.Add("TeacherGroupEdit", id);
                return RedirectToAction("IndexForTeacher");
            }
            else
            {
                TempData.Add("TeacherGroup", id);
                return RedirectToAction("IndexForStudent");
            }
        }
    }
}