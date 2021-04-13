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
        public Vector2 _gravity;
        public Player(Texture2D tex, Vector2 position, Point size) : base(tex)
        {
            Position = position;
            rectangle = new Rectangle(Position.ToPoint(), size);
        }

        public override void Update()
        {
            KeyboardState a = Keyboard.GetState();

            Velocity = Vector2.Zero;

            Move(a);

            _gravity.Y += 9.82f * 1f / 60f; // Gravitation
            if (_gravity.Y > 20)
                _gravity.Y = 20;

            Velocity = new Vector2(Velocity.X, Velocity.Y + _gravity.Y);

            rectangle = new Rectangle(Position.ToPoint(), rectangle.Size);
        }

        private void Move(KeyboardState a)
        {
            if (a.IsKeyDown(Keys.D))
                Velocity = new Vector2(10, Velocity.Y);
            if (a.IsKeyDown(Keys.A))
                Velocity = new Vector2(-10, Velocity.Y);

            if (a.IsKeyDown(Keys.Space))
                Velocity = new Vector2(Velocity.X, -25);
        }
    }
}