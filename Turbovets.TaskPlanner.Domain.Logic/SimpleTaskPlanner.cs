using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turbovets.TaskPlanner.DataAccess.Abstractions;
using Turbovets.TaskPlanner.Domain.Models;

namespace Turbovets.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        private IWorkItemRepository _workItemRepository;
        public SimpleTaskPlanner(IWorkItemRepository workItemRepository)
        {
            _workItemRepository = workItemRepository;
        }

        public WorkItem[] CreatePlan()
        {
            var result = _workItemRepository.GetAll().ToList();
            result.Sort(CompareWorkItems);
            return result.Where(item => !item.IsComplited).ToArray();
        }

        public int CompareWorkItems(WorkItem workItem1, WorkItem workItem2)
        {
            var priority = workItem2.Priority.CompareTo(workItem1.Priority);
            if (priority != 0)
            {
                return priority;
            }

            var dueDate = workItem1.DueDate.CompareTo(workItem2.DueDate);
            if (dueDate != 0)
            {
                return dueDate;
            }

            return string.Compare(workItem1.Title, workItem2.Title, StringComparison.OrdinalIgnoreCase);
        }
    }
}
