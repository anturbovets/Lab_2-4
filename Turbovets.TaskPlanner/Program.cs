using Turbovets.TaskPlanner.ConsoleRunner.Services;
using Turbovets.TaskPlanner.DataAccess;
using Turbovets.TaskPlanner.DataAccess.Abstractions;
using Turbovets.TaskPlanner.Domain.Logic;
using Turbovets.TaskPlanner.Domain.Models;

internal static class Program
{
    public static void Main(string[] args)
    {
        FileWorkItemsRepository fileWorkItemsRepository = new FileWorkItemsRepository();
        SimpleTaskPlanner simpleTaskPlanner = new SimpleTaskPlanner(fileWorkItemsRepository);

        bool keepRunning = true;

        while (keepRunning) 
        {
            Console.WriteLine("Menu:\n" +
                            "1. Create Task\n" +
                            "2. Tasks\n" +
                            "3. Mark as complited\n" +
                            "4. Remove Task\n" +
                            "5. Exit\n");

            var input = Int32.Parse(Console.ReadLine());
            
            switch (input)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Enter Task info: ");
                    CreateWorkItemService createWorkItemService = new CreateWorkItemService();
                    var workItem = createWorkItemService.GetWorkItem();
                    fileWorkItemsRepository.Add(workItem);
                    break;

                case 2:
                    Console.Clear();
                    var workItemArray = simpleTaskPlanner.CreatePlan();
                    int f = 1;
                    foreach (var item in workItemArray)
                    {
                        Console.WriteLine($"{f}. {item.ToString()}");
                        f++;
                    }

                    if (workItemArray.Length > 0)
                    {
                        Console.WriteLine();
                    }

                    break;

                case 3:
                    while (true)
                    {
                        Console.Clear();
                        int flag = 1;
                        var sortedArray = simpleTaskPlanner.CreatePlan();

                        foreach (var item in sortedArray)
                        {
                            Console.WriteLine($"{flag}. {item.ToString()}");
                            flag++;
                        }

                        Console.Write("\nWhich Task mark as complited?: ");
                        var complited = int.Parse(Console.ReadLine());

                        if (complited > 0 && complited <= sortedArray.Length)
                        {
                            sortedArray[complited - 1].IsComplited = true;
                            fileWorkItemsRepository.SaveChanges();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Choose another option");
                        }
                    }
                    break;

                case 4:
                    Console.Clear();
                    int num = 1;
                    var sortedArray2 = simpleTaskPlanner.CreatePlan();

                    foreach (var item in sortedArray2)
                    {
                        Console.WriteLine($"{num}. {item.ToString()}");
                        num++;
                    }

                    Console.Write("\nWhich Task you want to remove?: ");
                    var number = int.Parse(Console.ReadLine());

                    if (number > 0 && number <= sortedArray2.Length)
                    {
                        Guid guid = sortedArray2[number - 1].Id;
                        fileWorkItemsRepository.Remove(guid);
                    }
                    Console.Clear();
                    break;

                case 5:
                    keepRunning = false;
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;

            }
        }
        
            

    }
}
