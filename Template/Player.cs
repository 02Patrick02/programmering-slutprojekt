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
        private Rectangle SkottRec;
        
        
        private Vector2 acceleraion = new Vector2(0, 1), SkottPos;

        private List<Vector2> SpelareSkottPos = new List<Vector2>();

        public Player (Texture2D tex, List<Vector2> SkottLista) : base(tex)
        {
            SpelareSkottPos = SkottLista;
        }

        public Player(Texture2D tex, Vector2 position, Point size) : base(tex)
        {
            Position = position;
            rectangle = new Rectangle(Position.ToPoint(), size);
        }

        public Rectangle Skottrec
        {
            get { return SkottRec; }
            set { SkottRec = value; }
        }

        public override void Update(GameTime gameTime)
        {
            SkottRec = new Rectangle((int)SkottPos.X, (int)SkottPos.Y, 150, 150);


            KeyboardState a = Keyboard.GetState();

            if(oldA == null)
            {
                oldA = a;
            }
            
            Velocity = new Vector2(0, Velocity.Y);

            if (oldGameTime == null)
                oldGameTime = gameTime;

            if (a.IsKeyDown(Keys.Z) && a.IsKeyUp(Keys.Z))
            {
                SpelareSkottPos.Add(Position + new Vector2(89, 0));
            }
            for (int i = 0; i < SpelareSkottPos.Count; i++)
            {
                SpelareSkottPos[i] = SpelareSkottPos[i] - new Vector2(0, 5);
            }

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