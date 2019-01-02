using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IA3_1512138
{
    class Character : GameControl
    {
        private int timeLastFrame = 0;
        private int timePerFrame = 50;
        private int state = 0;
        private int count = 0;

        public Character(Game game, Sprite2D model) : base(game, model)
        {
            model.Left = 100;
            model.Top = this._game.GraphicsDevice.PresentationParameters.BackBufferHeight - 220;
            model.Width = 150;
            model.Height = 150;
        }

        public override void Update(GameTime gameTime)
        {
            timeLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (this.state == 2)
            {
                if (count <= 5) this._model.Top += (count - 5) * 5;
                else this._model.Top += (count - 5) * 5;
            }

            if (timeLastFrame > timePerFrame)
            {
                // Idle
                if (this.state == 0) this._model.ImageIndex = (this._model.ImageIndex + 1) % 10;
                
                if (this._model.Playing)
                {
                    // Run
                    if (this.state == 1) this._model.ImageIndex = (this._model.ImageIndex + 1) % 8;

                    // Jump
                    if (this.state == 2)
                    {
                        this._model.ImageIndex = (this._model.ImageIndex + 1) % 10;
                        
                        if (++count > 10)
                        {
                            this.SetState(1);
                            count = 0;
                        }
                    }

                    // Dead
                    if (this.state == 3)
                    {
                        this._model.ImageIndex = (this._model.ImageIndex + 1) % 10;

                        if (this._model.ImageIndex == 9) this._model.Playing = false;
                    }

                    // Slide
                    if (this.state == 4)
                    {
                        this._model.ImageIndex = (this._model.ImageIndex + 1) % 5;
                    }
                }
                    
                timeLastFrame = 0;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (this.state)
            {
                case 0: spriteBatch.Draw(this._model.Images[this._model.ImageIndex], new Rectangle(this._model.Left, this._model.Top, this._model.Width, this._model.Height), Color.White); break;
                case 1: spriteBatch.Draw(this._model.Images[this._model.ImageIndex + 10], new Rectangle(this._model.Left, this._model.Top, this._model.Width, this._model.Height), Color.White); break;
                case 2: spriteBatch.Draw(this._model.Images[this._model.ImageIndex + 18], new Rectangle(this._model.Left, this._model.Top, this._model.Width, this._model.Height), Color.White); break;
                case 3: spriteBatch.Draw(this._model.Images[this._model.ImageIndex + 28], new Rectangle(this._model.Left, this._model.Top, this._model.Width, this._model.Height), Color.White); break;
                case 4: spriteBatch.Draw(this._model.Images[this._model.ImageIndex + 38], new Rectangle(this._model.Left, this._model.Top, this._model.Width, this._model.Height), Color.White); break;
            }
        }

        public override void SetState(int state)
        {
            if (this.count == 0)
            {
                switch (state)
                {
                    case 0: timePerFrame = 100; break;
                    case 1: timePerFrame = 50; break;
                    case 2: timePerFrame = 40; timeLastFrame = 0; break;
                    case 3: timePerFrame = 60; break;
                    case 4: timePerFrame = 50; break;
                }

                this._model.Top = this._game.GraphicsDevice.PresentationParameters.BackBufferHeight - 220;
                this._model.ImageIndex = 0;

                if (state >= 0 && state <= 4) this.state = state;
                else this.state = 0;
            }
        }

        public override int GetState()
        {
            return this.state;
        }

        public override bool IsDead(GameEntity barrier)
        {
            Rectangle rectangle = barrier.GetRec();
            int left = this._model.Left;
            int top = this._model.Top;
            int width = this._model.Width;
            int height = this._model.Height;

            if (this.state == 4)
            {
                if (barrier.GetState() != 2)
                {
                    height /= 2;
                    top += height;
                }
            }

            if ((left < rectangle.X + rectangle.Width / 2 && left + width > rectangle.X + rectangle.Width / 2) &&
                (top < rectangle.Y + rectangle.Height / 2 && top + height > rectangle.Y + rectangle.Height / 2))
            {
                this.state = 3;
                this._model.ImageIndex = 0;
                return true;
            }

            return false;
        }

        public override bool ResetGame()
        {
            this._model.Left = 100;
            this._model.Top = this._game.GraphicsDevice.PresentationParameters.BackBufferHeight - 220;
            count = 0;
            this.state = 0;
            this._model.ImageIndex = 0;

            return base.ResetGame();
        }
    }
}
