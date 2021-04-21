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
    class Player : BaseClass
    {
        private GameTime oldGameTime;
        private KeyboardState oldA;
        
        private Vector2 acceleraion = new Vector2(0, 1);
        public Player(Texture2D tex, Vector2 position, Point size) : base(tex)
        {
            Position = position;
            rectangle = new Rectangle(Position.ToPoint(), size);
            
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState a = Keyboard.GetState();
            if(oldA == null)
            {
                oldA = a;
            }
            

            Velocity = new Vector2(0, Velocity.Y);

            if (oldGameTime == null)
                oldGameTime = gameTime;

            Move(a, oldA);

            double updateTime = gameTime.ElapsedGameTime.TotalMilliseconds - oldGameTime.ElapsedGameTime.TotalMilliseconds;
            float timeScalar = (float)updateTime / 20f;
            Velocity += acceleraion;
           


            rectangle = new Rectangle(Position.ToPoint(), rectangle.Size);

            oldGameTime = gameTime;
            oldA = a;
        }

        private void Move(KeyboardState a, KeyboardState oldA)
        {
            if (a.IsKeyDown(Keys.D))
                Velocity = new Vector2(10, Velocity.Y);
            if (a.IsKeyDown(Keys.A))
                Velocity = new Vector2(-10, Velocity.Y);

            if (a.IsKeyDown(Keys.Space) && !oldA.IsKeyDown(Keys.Space))
                Velocity = new Vector2(Velocity.X, -25);
        }
    }
}