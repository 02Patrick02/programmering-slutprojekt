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
    class Player : BaseClass, ICollidable
    {
        private GameTime oldGameTime;
        private KeyboardState oldA, a;
        private Texture2D bulletTex;

        private int jumpCooldown, shootCooldown;

        private Vector2 acceleraion = new Vector2(0, 1);


        public Player(Texture2D tex, Texture2D bulletTex, Vector2 position, Point size) : base(tex)
        {
            Position = position;
            rectangle = new Rectangle(Position.ToPoint(), size);
            this.bulletTex = bulletTex;
        }

        private void Shoot()
        {
            if (a.IsKeyDown(Keys.Left) && shootCooldown == 0)
            {
                Children.Add(new Bullet(bulletTex, 1)
                {
                    Parent = this,
                    Position = this.Position,
                    Rectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, 50, 50),
                    Speed = 5
                });
                shootCooldown = 30;
                return;
            }
                

            if (a.IsKeyDown(Keys.Right) && shootCooldown == 0)
            {
                Children.Add(new Bullet(bulletTex, 2)
                {
                    Parent = this,
                    Position = this.Position,
                    Rectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, 50, 50),
                    Speed = 5
                });
                shootCooldown = 30;
                return;
            }
                

            if (a.IsKeyDown(Keys.Up) && shootCooldown == 0)
            {
                Children.Add(new Bullet(bulletTex, 3)
                {
                    Parent = this,
                    Position = this.Position,
                    Rectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, 50, 50),
                    Speed = 5
                });
                shootCooldown = 30;
                return;
            }
                
        }
        

        public override void Update(GameTime gameTime)
        {
           a = Keyboard.GetState();

            if(oldA == null)
            {
                oldA = a;
            }
            
            Velocity = new Vector2(0, Velocity.Y);

           
            Shoot();
            if (shootCooldown > 0)
                shootCooldown--;

            if (oldGameTime == null)
                oldGameTime = gameTime;

            Move(a, oldA);

            Velocity += acceleraion;
           
            rectangle = new Rectangle(Position.ToPoint(), rectangle.Size);

            if (jumpCooldown > 0)
                jumpCooldown--;


            oldGameTime = gameTime;
            oldA = a;
        }

        private void Move(KeyboardState a, KeyboardState oldA)
        {
            if (a.IsKeyDown(Keys.D))
                Velocity = new Vector2(10, Velocity.Y);

            if (a.IsKeyDown(Keys.A))
                Velocity = new Vector2(-10, Velocity.Y);

            if (jumpCooldown != 0) return;

            if (a.IsKeyDown(Keys.Space) && !oldA.IsKeyDown(Keys.Space))
            {
                Velocity = new Vector2(Velocity.X, -25);
                jumpCooldown = 45;
            }
        }

        public void OnCollide(BaseClass sprite)
        {
            if (sprite is Enemy)
                IsRemoved = true;
        }
    }
}