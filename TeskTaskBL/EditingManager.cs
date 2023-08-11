using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditingBL
{
    public class EditingManager
    {
        List<Image> images = new List<Image>();
        List<ImageEditor> editors = new List<ImageEditor>();

        public bool Completed {  get; private set; }

        public EditingManager(List<Image> images, List<ImageEditor> editors)
        {
            this.images = images;
            this.editors = editors;
        }

        //Calculates total time when the last image will be edited by whole crew
        public int CalculateTotalTime()
        {
            double totalImagesPerMinute = 0;
            foreach (ImageEditor editor in editors) 
            {
                totalImagesPerMinute += 1.0 / editor.TimePerImage;
            }

            double totalTime = images.Count / totalImagesPerMinute;
            return (int)totalTime;
        }

        public bool isAllImagesCompleted()
        {
            if (images.All(image => image.isCompleted)) return true;
            else return false;
        }

        public async Task StartEditingAsync(CancellationToken? token = null)
        {
            await Task.Run(() => StartEditing(token));
        }

        //Simulates working
        //The last image will be edited by one person
        public void StartEditing(CancellationToken? token = null)
        {
            int time = 0;
            var array = images.ToArray();
            int pointer = 0;
            int totalCount = 0;

            while (true)
            {
                time++;
                if (token is not null && token.Value.IsCancellationRequested) return;

                foreach (ImageEditor editor in editors)
                {
                    if (time % (editor.TimePerImage * 60) == 0 && totalCount < images.Count)
                    {
                        array[pointer++].MakeCompleted();
                        editor.ImageCount++;
                        totalCount++;
                    }
                }

                if (isAllImagesCompleted())
                {
                    Completed = true;
                    Console.WriteLine();
                    foreach (ImageEditor editor in editors)
                    {
                        Console.WriteLine($"{editor.Name} edited images: {editor.ImageCount}");
                    }
                    var totalTime = CalculateTotalTime();
                    Console.WriteLine($"Total editing time: {totalTime} min ({totalTime / 60} h {totalTime % 60} min)");
                    break;
                }
            }
        }
    }
}
