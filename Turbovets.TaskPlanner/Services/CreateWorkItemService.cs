using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Turbovets.TaskPlanner.Domain.Models;
using Turbovets.TaskPlanner.Domain.Models.Enums;

namespace Turbovets.TaskPlanner.ConsoleRunner.Services
{
    public class CreateWorkItemService
    {
        private WorkItem _workItem;

        public CreateWorkItemService()
        {
            _workItem = new WorkItem();
            Title();
            Description();
            DueDate();
            Priority();
            Complexity();
            CreationDate();
            IsComplited();
            Console.Clear();
        }


        public WorkItem GetWorkItem()
        {
            return _workItem;
        }


        private void Title()
        {
            Console.Write("Title: ");
            _workItem.Title = Console.ReadLine();
        }

        private void Description()
        {
            Console.Write("Description: ");
            _workItem.Description = Console.ReadLine();
        }

        private void DueDate()
        {
            while (true)
            {
                Console.Write("Due date (dd.MM.yyyy): ");
                var input = Console.ReadLine();

                if (DateTime.TryParseExact(input, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime result)) {
                    _workItem.DueDate = result;
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong format");
                }
            }
        }

        private void Priority()
        {
            Console.WriteLine("Priority: ");
            var flag = 1;
            foreach (Priority p in Enum.GetValues(typeof(Priority)))
            {
                Console.WriteLine($"{flag}. {p}");
                flag++;
            }

            Console.Write("\nChoose Priority: ");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int result) && Enum.IsDefined(typeof(Priority), result))
            {
                _workItem.Priority = (Priority) result - 1;
            }
            else
            {
                Console.WriteLine("Wrong Priority!\n");
            }
        }

        private void Complexity()
        {
            Console.WriteLine("\nComplexity: ");
            var flag = 1;
            foreach (Complexity p in Enum.GetValues(typeof(Complexity)))
            {
                Console.WriteLine($"{flag}. {p}");
                flag++;
            }

            Console.Write("\nChoose Complexity: ");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int result) && Enum.IsDefined(typeof(Complexity), result))
            {
                _workItem.Complexity = (Complexity) result - 1;
            }
            else
            {
                Console.WriteLine("Wrong Complexity!\n");
            }
        }

        private void CreationDate()
        {
            _workItem.CreationDate = DateTime.Now;
        }

        private void IsComplited()
        {
            _workItem.IsComplited = false;
        }
    }
}
