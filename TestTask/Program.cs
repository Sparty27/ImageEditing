using System;
using ImageEditingBL;

namespace ImageEditing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int amountImages = Parse("Enter the amount of images: ");

            List<ImageEditor> editors = new List<ImageEditor>();
            List<Image> images = new List<Image>();

            for (int i = 1; i <= amountImages; i++)
            {
                images.Add(new Image(i, $"Image number {i}"));
            }

            int numberEmployees = Parse("Enter the number of employees: ");
            Console.WriteLine();
            for (int i = 1; i <= numberEmployees; i++)
            {
                int time = Parse($"Enter editing time (minutes) per 1 image for Person {i}: ");
                editors.Add(new ImageEditor($"Person {i}", time));
            }

            EditingManager editingManager = new EditingManager(images, editors);

            //declaring cancellation token to cancel async method during the work
            CancellationTokenSource cts = new();
            CancellationToken token = cts.Token;

            Console.WriteLine("\nCommands:\nS - start work\nC - stop work\n");
            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();
                if (key.Key == ConsoleKey.C)
                {
                    cts.Cancel();
                    if (editingManager.Completed) Console.WriteLine("The work has not been canceled");
                    else Console.WriteLine("The work has been canceled");
                }
                else if (key.Key == ConsoleKey.S)
                {
                    editingManager.StartEditingAsync(token);
                }
            }
        }

        private static int Parse(string message)
        {
            while (true)
            {
                Console.Write(message);

                var result = Console.ReadLine();

                if (int.TryParse(result, out int value)) return value;
                else Console.WriteLine("Invalid value!");
            }
        }
    }
}