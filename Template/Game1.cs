using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Template
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D defaultTex, ground, wall, underground, playerTex, enemyTex, bulletTex;
        private List<BaseClass> sprites;
        const int BLOCK_SIZE = 80;



        static int[,] Map = new int[,]
        {
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 ,2},
            {2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,2},
            {2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,2},
            {2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,2},
            {2, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1 ,2},
            {2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,2},
            {2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,2},
            {2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,1},
            {2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,1},
            {2, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1 ,2},
            {2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,2},
            {2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,2},
            {2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,2},
            {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 ,3}
        };


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
          
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.ApplyChanges();

            base.Initialize();
        }


        protected override void LoadContent()
        {
            defaultTex = new Texture2D(GraphicsDevice, 1, 1);
            defaultTex.SetData(new Color[1] { Color.White });

            spriteBatch = new SpriteBatch(GraphicsDevice);
            ground = Content.Load<Texture2D>("Ground");
            wall = Content.Load<Texture2D>("Wall");
            playerTex = Content.Load<Texture2D>("PlayerTex");
            enemyTex = Content.Load<Texture2D>("enemy");
            underground = Content.Load<Texture2D>("Underground");
            bulletTex = Content.Load<Texture2D>("Bullet");
        


            sprites = new List<BaseClass>()
            {
               new Player(playerTex, bulletTex, new Vector2(200, 200), new Point(100, 100)),
               new Enemy(enemyTex, new Vector2(200, 200), new Point(100, 100)),
            };

            for (int i = 0; i < Map.GetLength(1); i++) // Tile X
            {
                for (int j = 0; j < Map.GetLength(0); j++) // Tile Y
                {
                    if (Map[j, i] == 0)
                    {
                        sprites.Add(new Wall(ground, new Rectangle(BLOCK_SIZE * i, BLOCK_SIZE * j, BLOCK_SIZE, BLOCK_SIZE)));
                    }

                    else if (Map[j, i] == 2)
                    {
                        sprites.Add(new Wall(wall, new Rectangle(BLOCK_SIZE * i, BLOCK_SIZE * j, BLOCK_SIZE, BLOCK_SIZE)));
                    }
                    else if (Map[j, i] == 3)
                    {
                        sprites.Add(new Wall(underground, new Rectangle(BLOCK_SIZE * i, BLOCK_SIZE * j, BLOCK_SIZE, BLOCK_SIZE)));
                    }
                }
            }
        }


        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState a = Keyboard.GetState();
            if (a.IsKeyDown(Keys.Escape))
                Exit();

            AddChildren();

            foreach (var sprite in sprites)
            {
                sprite.Update(gameTime);
            }

            var collidableSprites = sprites.Where(sprite => sprite is ICollidable) as BaseClass[] ?? sprites.Where(c => c is ICollidable).ToArray();

            foreach (var spriteA in collidableSprites)
            {
                foreach (var spriteB in sprites)
                {
                    if (spriteA == spriteB)
                        continue;

                    if ((spriteA is Player && spriteB is Bullet) ||
                        (spriteA is Bullet && spriteB is Player))
                        continue;

                    if (spriteA is Bullet && spriteB is Bullet)
                        continue;

                    if (spriteA.Intersects(spriteB))
                    {
                        ((ICollidable)spriteA).OnCollide(spriteB);
                    }
                }
                spriteA.Position += spriteA.Velocity;

            }

            RemoveSprites();
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            foreach (var sprite in sprites)
            {
                sprite.Draw(spriteBatch);
            }

            //foreach (var sprite in sprites)
            //{
            //    spriteBatch.Draw(defaultTex, sprite.Rectangle, Color.Blue);
            //}

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void AddChildren()
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                var sprite = sprites[i];
                for (int j = 0; j < sprite.Children.Count; j++)
                {
                    sprites.Add(sprite.Children[j]);
                }
                    

                sprite.Children = new List<BaseClass>();
            }
        } 
        
        private void RemoveSprites()
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                if (!sprites[i].IsRemoved) continue;

                sprites.RemoveAt(i);
                i--;
            }
        }
    }
}
