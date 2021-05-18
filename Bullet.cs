using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Template
{
    class Bullet : BaseClass, ICollidable
    {
        private int direction;
        public Bullet(Texture2D tex, int direction) : base(tex)
        {
            this.direction = direction;
        }

        public override void Update(GameTime gameTime)
        {
            if (direction == 1)
                Velocity = new Vector2(-Speed, 0);
            if (direction == 2)
                Velocity = new Vector2(Speed, 0);
            if (direction == 3)
                Velocity = new Vector2(0, -Speed);

            rectangle = new Rectangle(Position.ToPoint(), rectangle.Size);
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);
        }

        public void OnCollide(BaseClass sprite)
        {
            if (sprite is Wall)
                IsRemoved = true;
            if (sprite is Enemy)
                IsRemoved = true;
        }
    }
}
