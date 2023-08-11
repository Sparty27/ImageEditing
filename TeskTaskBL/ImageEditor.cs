using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditingBL
{
    public class ImageEditor
    {
        private int _count;
        public string Name { get; private set; }
        public int TimePerImage { get; private set; }
        public int ImageCount 
        {   get => _count;
            //Setter is more safe
            set {
                if (value > _count) _count = value;
            } 
        }

        public ImageEditor(string name, int timePerImage) 
        {
            Name = name;
            TimePerImage = timePerImage;
        }
    }
}
