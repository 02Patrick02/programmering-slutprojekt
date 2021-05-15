using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    class Enemy : BaseClass, ICollidable
    {
        public Enemy(Texture2D tex, Vector2 position, Point size, int health) : base(tex)
        {
            Position = position;
            rectangle = new Rectangle(position.ToPoint(), size);
            Health = health;
           
        }

        public void OnCollide(BaseClass sprite)
        {
            if (sprite is Bullet)
                Health--;
        }

        public override void Update(GameTime gametime)
        {
            if (Health < 0)
                IsRemoved = true;
        }
    }
}
