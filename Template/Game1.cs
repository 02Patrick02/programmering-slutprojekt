using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Template
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D ground, wall, underground, playerTex, enemyTex, bulletTex;

        private List<BaseClass> sprites;
        private List<Player> player;
        private BinaryReader br;
        private BinaryWriter bw;
        private SpriteFont font;

        private const int BLOCK_SIZE = 80;
        private int enemySpawnRate = 60;
        int Score = 0;
         



        private Random rnd = new Random();
            
        public enum GameState
        {
            MainColor,
            ColorSelect,
        }
        GameState CurrentState = GameState.MainColor;

       



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
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ground = Content.Load<Texture2D>("Ground");
            wall = Content.Load<Texture2D>("Wall");
            playerTex = Content.Load<Texture2D>("PlayerTex");
            enemyTex = Content.Load<Texture2D>("Enemy");
            underground = Content.Load<Texture2D>("Underground");
            bulletTex = Content.Load<Texture2D>("Bullet");
            font = Content.Load<SpriteFont>("Menu");



            sprites = new List<BaseClass>()
            {
               new Player(playerTex, bulletTex, new Vector2(800, 350), new Point(100, 100)),
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
            player = sprites.OfType<Player>().ToList();


            //filhantering
            bw = new BinaryWriter(
                  new FileStream("Score.txt",
                  FileMode.OpenOrCreate,
                  FileAccess.Write));
            bw.Write(Score);
            bw.Close();


            br = new BinaryReader(
                 new FileStream("Score.txt",
                 FileMode.OpenOrCreate,
                 FileAccess.Read));

            Score = br.ReadInt32();
            br.Close();
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

                    if ((spriteA is Enemy && spriteB is Wall) ||
                        (spriteA is Wall && spriteB is Enemy))
                        continue;

                    if (spriteA is Bullet && spriteB is Bullet)
                        continue;


                    if (spriteA.Intersects(spriteB))
                    {
                        ((ICollidable)spriteA).OnCollide(spriteB);
                    
                        if (spriteA is Enemy && spriteB is Bullet )
                        {
                            Score++;

                            bw = new BinaryWriter(
                                 new FileStream("Score.txt",
                                 FileMode.OpenOrCreate,
                                 FileAccess.Write));
                            bw.Write(Score);
                            bw.Close();
                        }
                    }
                }
                spriteA.Position += spriteA.Velocity;
            }


            var enemies = sprites.Where(sprite => sprite is Enemy) as BaseClass[] ?? sprites.Where(c => c is Enemy).ToArray();
            
            foreach(Enemy enemy in enemies)
            {
                enemy.Update(gameTime, player[0].Position);
            }

            if(rnd.Next(enemySpawnRate) == 0) //spawnar enemy random
            {
                sprites.Add(new Enemy(enemyTex)
                {
                    Position = new Vector2(rnd.Next(1920), -100),
                    Size = new Point(100, 100),
                    Health = 1,
                    Speed = 2
                });
            }



            switch (CurrentState) //byter färg när man trycker på W och S
            {
                case GameState.MainColor:
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                        CurrentState = GameState.ColorSelect;
                    break;

                case GameState.ColorSelect:
                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                        CurrentState = GameState.MainColor;
                    break;
            }

            if (player[0].IsRemoved) //om spelaren tas bort  vid kollision stängs spelet av
                Exit();

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

            switch (CurrentState) //byter färg 
            {
                case GameState.MainColor:
                    GraphicsDevice.Clear(Color.White);
                    break;

                case GameState.ColorSelect:
                    GraphicsDevice.Clear(Color.Black);
                    break;
            }

            spriteBatch.DrawString(font, "Score: " + Score.ToString(), new Vector2(50, 50), Color.White);

            

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
