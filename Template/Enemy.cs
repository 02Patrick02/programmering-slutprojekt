using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    class Enemy : BaseClass
    {
        public Enemy(Texture2D tex, Vector2 position, Point size) : base(tex)
        {
            Position = position;
            rectangle = new Rectangle(Position.ToPoint(), size);
        }

        public override void Update(GameTime gametime)
        {

        }
    }
}
