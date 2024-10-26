using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Turbovets.TaskPlanner.DataAccess.Abstractions;
using Turbovets.TaskPlanner.Domain.Models;

namespace Turbovets.TaskPlanner.DataAccess
{
    public class FileWorkItemsRepository : IWorkItemRepository
    {
        private const string fileName = "work-items.json";
        private List<WorkItem> _workItemsList;
        private readonly Dictionary<Guid, WorkItem> _workItemsDictionary;

        public FileWorkItemsRepository()
        {
            _workItemsList = ReadFromFile();
            _workItemsDictionary = _workItemsList.ToDictionary(item => item.Id, item => item);
        }

        private List<WorkItem> ReadFromFile()
        {
            if (!(File.Exists(fileName)))
            {
                return new List<WorkItem>();
            }

            var json = File.ReadAllText(fileName);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<WorkItem>();
            }

            return JsonConvert.DeserializeObject<List<WorkItem>>(json) ?? new List<WorkItem>();


        }

        public Guid Add(WorkItem workItem)
        {
            WorkItem workItemNew = workItem.Clone();
            _workItemsDictionary.Add(workItemNew.Id, workItemNew);
            SaveChanges();
            return workItemNew.Id;
        }

        public WorkItem Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public WorkItem[] GetAll()
        {
            return _workItemsDictionary.Values.ToArray();
        }

        public bool Remove(Guid id)
        {
            if(_workItemsDictionary.Remove(id))
            {
                SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SaveChanges()
        {
            var workItemsArray = _workItemsDictionary.Values.ToArray();
            var json = JsonConvert.SerializeObject(workItemsArray);
            File.WriteAllText(fileName, json);
        }

        public bool Update(WorkItem workItem)
        {
            throw new NotImplementedException();
        }
    }
}
