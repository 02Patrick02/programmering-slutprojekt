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
        private Vector2 direction;
        public Enemy(Texture2D tex) : base(tex)
        {
            Rectangle = new Rectangle(Position.ToPoint(), Size);
        }

        public void OnCollide(BaseClass sprite)
        {
            if (sprite is Bullet) //enemy health minskar vid kollision med bullet
                Health--;
        }

        public void Update(GameTime gameTime, Vector2 playerPos)
        {
            direction = playerPos - Position;
            direction.Normalize();

            Velocity = direction * Speed; // Direction --> Velocity

            if (Velocity.X > 3) // X Velocity inte större än 3
                Velocity = new Vector2 (3, 0);

            else if (Velocity.X < 0 - 3)
                Velocity = new Vector2(-3, 0);

            if (Velocity.Y > 3) 
                Velocity = new Vector2(0, 3);

            else if (Velocity.Y < 0 - 3)
                Velocity = new Vector2(0, -3);

           
            Rectangle = new Rectangle(Position.ToPoint(), Size);


            if (Health <= 0) //om enemy kommer till 0 health så tas den bort
                IsRemoved = true;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }
    }
}
