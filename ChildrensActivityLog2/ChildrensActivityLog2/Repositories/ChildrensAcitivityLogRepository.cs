using ChildrensActivityLog2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ChildrensActivityLog2.Repositories
{
    public class ChildrensAcitivityLogRepository
    {
        private ChildrensActivityLogContext _context;
        public ChildrensAcitivityLogRepository(ChildrensActivityLogContext context)
        {
            _context = context;
        }

        public void Add(Child child)
        {
            _context.Children.Add(child);
            _context.SaveChanges();
        }

        public void Add(PlayEvent playEvent)
        {
            _context.PlayEvents.Add(playEvent);
            _context.SaveChanges();
        }



        public void AddSleepingPeriodByChildId(int childId, SleepingPeriod sleepingPeriod)
        {
            var child = GetChildById(childId, false, false);
            child.SleepingPeriods.Add(sleepingPeriod);
            //_context.SaveChanges();
        }

        public bool ChildExists(int childId)
        {
            return _context.Children.Any(c => c.Id == childId);
        }

        public void ClearAll()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM SleepingPeriods");
            _context.Database.ExecuteSqlCommand("DELETE FROM ChildrensPlayEvents");
            _context.Database.ExecuteSqlCommand("DELETE FROM PlayEvents");
            _context.Database.ExecuteSqlCommand("DELETE FROM Children");
            _context.SaveChanges();
        }

        public int CountChildren()
        {
            return _context.Children.Count();
        }

        public int CountPlayEvents()
        {
            return _context.PlayEvents.Count();
        }

        public void DeleteChild(Child child)
        {
            _context.Children.Remove(child);
        }

        public IEnumerable<Child> GetAllChildren()
        {
            return _context.Children.OrderBy(c => c.FirstName).ToList();
        }

        public IEnumerable<PlayEvent> GetAllPlayEvents()
        {
            return _context.PlayEvents.ToList();
        }

        public IEnumerable<SleepingPeriod> GetAllsleepingPeriodsByChildId()
        {
            return _context.SleepingPeriods.ToList();
        }

        public Child GetChildById(int id)
        {
            return _context.Children.FirstOrDefault(c => c.Id == id);
        }

        public Child GetChildById(int id, bool includePlayEvents, bool includeSleepingPeriods)
        {
            if (includeSleepingPeriods && includePlayEvents)
            {
                return _context.Children.Include(c => c.SleepingPeriods).Include(c => c.SleepingPeriods)
                        .Where(c => c.Id == id).FirstOrDefault();
            }
            if (includePlayEvents)
            {
                return _context.Children.Include(c => c.ChildrensPlayEvents)
                        .Where(c => c.Id == id).FirstOrDefault();
            }
            if (includeSleepingPeriods)
            {
                return _context.Children.Include(c => c.SleepingPeriods)
                        .Where(c => c.Id == id).FirstOrDefault();
            }
            return _context.Children.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<PlayEvent> GetPlayEventsByChildId(int id)
        {
            return _context.PlayEvents.Include(p => p.ChildrensPlayEvents.Where(c => c.ChildId == id)).ToList();
            //.Where(p => p.ChildrensPlayEvents.ChildId == id);
        }

        public void Remove(Child child)
        {
            _context.Children.Remove(child);
            _context.SaveChanges();
        }

        public void Remove(PlayEvent playEvent)
        {
            _context.PlayEvents.Remove(playEvent);
            _context.SaveChanges();
        }

        public void Remove(SleepingPeriod sleepingPeriod)
        {
            _context.SleepingPeriods.Remove(sleepingPeriod);
            _context.SaveChanges();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Update(PlayEvent playEvent)
        {
            _context.PlayEvents.Update(playEvent);
            _context.SaveChanges();
        }

        public void Update(SleepingPeriod sleepingPeriod)
        {
            _context.SleepingPeriods.Update(sleepingPeriod);
            _context.SaveChanges();
        }
    }
}

