using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    class Wall : BaseClass
    {

        public Wall(Texture2D tex, Rectangle rectangle) : base(tex)
        {
            Rectangle = rectangle;
        }

    }
}
