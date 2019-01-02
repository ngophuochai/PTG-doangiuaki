using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IA3_1512138
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private int timeLastCount = 0;
        private int timePerCount = 140;
        private List<GameEntity> components = new List<GameEntity>();
        private bool playing = false;
        private SpriteFont font, fontGameover;
        private int score = 0;
        private int highScore = 0;
        private bool gameover = false;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("Score/Font");
            fontGameover = Content.Load<SpriteFont>("Score/Gameover");

            components.Add(CreateIsland());
            components.Add(CreateBarrier());
            components.Add(CreateCharacter());
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            for (int i = 0; i < components.Count; i++) components[i].UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var keyboardState = Keyboard.GetState();
            var keys = keyboardState.GetPressedKeys();

            if (playing && keys.Length == 0 && components[2].GetState() != 1) components[2].SetState(1);

            if (!playing && keys.Length > 0 && keys[0].ToString().Equals("Right") && components[2].GetState() != 3)
            {
                for (int i = 0; i < components.Count; i++) components[i].Start();

                components[2].SetState(1);
                playing = true;
            }

            if (playing && keys.Length > 0 && components[2].GetState() != 4 && keys[0].ToString().Equals("Down")) components[2].SetState(4);

            if (playing && keys.Length > 0 && components[2].GetState() != 2 && (keys[0].ToString().Equals("Space") || keys[0].ToString().Equals("Up"))) components[2].SetState(2);

            if (keys.Length > 0 && (keys[0].ToString().Equals("Left")))
            {
                for (int i = 0; i < components.Count; i++) components[i].ResetGame();

                score = 0;
                gameover = false;
                playing = false;
            }

            if (playing && components[2].IsDead(components[1]))
            {
                for (int i = 0; i < components.Count; i++) if (i != 2) components[i].Stop();

                if (score >= highScore) highScore = score;
                gameover = true;
                playing = false;
            }

            for (int i = components.Count - 1; i >= 0; i--) components[i].Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            for (int i = 0; i < components.Count; i++) components[i].Draw(gameTime, spriteBatch);
            
            timeLastCount += gameTime.ElapsedGameTime.Milliseconds;

            if (timeLastCount > timePerCount)
            {
                if (playing) score++;
                timeLastCount = 0;
            }

            spriteBatch.DrawString(font, "Score: " + score, new Vector2(10, 50), Color.Black);
            
            spriteBatch.DrawString(font, "Highest Score: " + highScore, new Vector2(10, 10), Color.Black);
            
            if (gameover) spriteBatch.DrawString(fontGameover, "GAME OVER", new Vector2(400, 300), Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private GameEntity CreateIsland()
        {
            var sprite2D = new Sprite2D(0, 0, 0, 0);

            sprite2D.LoadContent(this, new string[] { "Background/CountryField" });
            return new Island(this, sprite2D);
        }

        private GameEntity CreateCharacter()
        {
            var sprite2D = new Sprite2D(0, 0, 0, 0);
            string[] path = new string[43];

            for (int i = 0; i < 43; i++)
            {
                if (i < 10) path[i] = string.Format("Character/AdventureGirl/Idle ({0})", i + 1);

                if (i >= 10 && i < 18) path[i] = string.Format("Character/AdventureGirl/Run ({0})", i + 1 - 10);

                if (i >= 18 && i < 28) path[i] = string.Format("Character/AdventureGirl/Jump ({0})", i + 1 - 18);

                if (i >= 28 && i < 38) path[i] = string.Format("Character/AdventureGirl/Dead ({0})", i + 1 - 28);

                if (i >= 38) path[i] = string.Format("Character/AdventureGirl/Slide ({0})", i + 1 - 38);
            }

            sprite2D.LoadContent(this, path);
            return new Character(this, sprite2D);
        }

        private GameEntity CreateBarrier()
        {
            var sprite2D = new Sprite2D(0, 0, 0, 0);
            string[] path = new string[20];

            path[0] = "Barrier/Ground/Bush";
            path[1] = "Barrier/Ground/Cherry";
            path[2] = "Barrier/Ground/Tree";

            for (int i = 3; i < 20; i++)
            {
                if (i < 11) path[i] = string.Format("Barrier/Fly/Bird/Move ({0})", i + 1 - 3);

                if (i >= 11) path[i] = string.Format("Barrier/Fly/Duck/Move ({0})", i + 1 - 11);
            }

            sprite2D.LoadContent(this, path);
            return new Barrier(this, sprite2D);
        }
    }
}
