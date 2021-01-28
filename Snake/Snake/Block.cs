using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Snake
{
    public class Block
    {
        public bool SafeBlock { get; set; }

        public bool HasFruit { get; set; }

        public bool HotChili { get; set; }

        public Image Img { get; set; }

        public Block(bool safeBlock, bool hasFruit, bool hotChili, Image img)
        {
            SafeBlock = safeBlock;
            HasFruit = hasFruit;
            HotChili = hotChili;
            Img = img;
        }
    }
}
