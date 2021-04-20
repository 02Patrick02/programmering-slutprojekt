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
    class BaseClass
    {
        protected Texture2D texture;
        protected Rectangle rectangle;



        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public int Health { get; set; }


        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }


        public virtual void Update()
        {


        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, Rectangle, Color.White);
        }

        public BaseClass(Texture2D tex)
        {
            texture = tex;
        }
        public bool Intersects(BaseClass sprite)
        {
            if ((Velocity.X > 0 && IsTouchingLeft(sprite)) ||
                (Velocity.X < 0 & IsTouchingRight(sprite)))
            {
                Velocity = new Vector2(0, Velocity.Y);
                return true;
            }

            if ((Velocity.Y > 0 && IsTouchingTop(sprite)) ||
                (Velocity.Y < 0 & IsTouchingBottom(sprite)))
            {
                Velocity = new Vector2(Velocity.X, 0);
                return true;
            }

            if (Rectangle.Intersects(sprite.Rectangle))
                return true;

            return false;
        }
        protected bool IsTouchingLeft(BaseClass sprite)
        {
            return Rectangle.Right + Velocity.X > sprite.Rectangle.Left &&
              Rectangle.Left < sprite.Rectangle.Left &&
              Rectangle.Bottom > sprite.Rectangle.Top &&
              Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(BaseClass sprite)
        {
            return Rectangle.Left + Velocity.X < sprite.Rectangle.Right &&
              Rectangle.Right > sprite.Rectangle.Right &&
              Rectangle.Bottom > sprite.Rectangle.Top &&
              Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(BaseClass sprite)
        {
            if (sprite is Player)
                ((Player)sprite).gravity = Vector2.Zero;

            return Rectangle.Bottom + Velocity.Y > sprite.Rectangle.Top &&
              Rectangle.Top < sprite.Rectangle.Top &&
              Rectangle.Right > sprite.Rectangle.Left &&
              Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(BaseClass sprite)
        {

            return Rectangle.Top + Velocity.Y < sprite.Rectangle.Bottom &&
              Rectangle.Bottom > sprite.Rectangle.Bottom &&
              Rectangle.Right > sprite.Rectangle.Left &&
              Rectangle.Left < sprite.Rectangle.Right;
        }
    }
}
