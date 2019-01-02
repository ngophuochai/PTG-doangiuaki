using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IA3_1512138
{
    class Barrier : GameControl
    {
        private int timeLastFrame = 0;
        private int timePerFrame = 60;
        private Random rand = new Random();
        private int state = 0;

        public Barrier(Game game, Sprite2D model) : base(game, model)
        {
            //this._model.Left = 500;
            //this._model.Top = this._game.GraphicsDevice.PresentationParameters.BackBufferHeight - 220;
            //this._model.Width = 90;
            //this._model.Height = 70;
        }

        public override void Update(GameTime gameTime)
        {
            if (this._model.Playing)
            {
                // Bush
                if (this.state == 0)
                {
                    this._model.ImageIndex = 0;
                }

                // Cherry
                if (this.state == 1)
                {
                    this._model.ImageIndex = 0;
                }

                // Tree
                if (this.state == 2)
                {
                    this._model.ImageIndex = 0;
                }

                // Bird
                if (this.state == 3)
                {
                    timeLastFrame += gameTime.ElapsedGameTime.Milliseconds;

                    if (timeLastFrame > timePerFrame)
                    {
                        this._model.ImageIndex = (this._model.ImageIndex + 1) % 8;
                        timeLastFrame = 0;
                    }
                }

                // Duck
                if (this.state == 4)
                {
                    timeLastFrame += gameTime.ElapsedGameTime.Milliseconds;

                    if (timeLastFrame > timePerFrame)
                    {
                        this._model.ImageIndex = (this._model.ImageIndex + 1) % 9;
                        timeLastFrame = 0;
                    }
                }

                this._model.Left -= 10;

                if (this._model.Left + this._model.Width <= 0)
                {
                    SetState(rand.Next(5));
                    this._model.Left = this._game.GraphicsDevice.PresentationParameters.BackBufferWidth;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (this.state)
            {
                case 0: spriteBatch.Draw(this._model.Images[this._model.ImageIndex], new Rectangle(this._model.Left, this._model.Top, this._model.Width, this._model.Height), Color.White); break;
                case 1: spriteBatch.Draw(this._model.Images[this._model.ImageIndex + 1], new Rectangle(this._model.Left, this._model.Top, this._model.Width, this._model.Height), Color.White); break;
                case 2: spriteBatch.Draw(this._model.Images[this._model.ImageIndex + 2], new Rectangle(this._model.Left, this._model.Top, this._model.Width, this._model.Height), Color.White); break;
                case 3: spriteBatch.Draw(this._model.Images[this._model.ImageIndex + 3], new Rectangle(this._model.Left, this._model.Top, this._model.Width, this._model.Height), Color.White); break;
                case 4: spriteBatch.Draw(this._model.Images[this._model.ImageIndex + 11], new Rectangle(this._model.Left, this._model.Top, this._model.Width, this._model.Height), Color.White); break;
            }
        }

        public override Rectangle GetRec()
        {
            return new Rectangle(this._model.Left, this._model.Top, this._model.Width, this._model.Height);
        }

        public override void SetState(int state)
        {
            if (state >= 0 && state <= 4) this.state = state;
            else this.state = 0;
            this._model.ImageIndex = 0;
            this._model.Left = this._game.GraphicsDevice.PresentationParameters.BackBufferWidth;

            if (this.state == 0)
            {
                this._model.Top = this._game.GraphicsDevice.PresentationParameters.BackBufferHeight - 170;
                this._model.Width = 150;
                this._model.Height = 100;
            }

            if (this.state == 1)
            {
                this._model.Top = this._game.GraphicsDevice.PresentationParameters.BackBufferHeight - 170;
                this._model.Width = 100;
                this._model.Height = 100;
            }

            if (this.state == 2)
            {
                this._model.Top = this._game.GraphicsDevice.PresentationParameters.BackBufferHeight - 220;
                this._model.Width = 84;
                this._model.Height = 150;
            }

            if (this.state == 3)
            {
                this._model.Top = this._game.GraphicsDevice.PresentationParameters.BackBufferHeight - 220 - rand.Next(100);
                this._model.Width = 70;
                this._model.Height = 50;
            }

            if (this.state == 4)
            {
                this._model.Top = this._game.GraphicsDevice.PresentationParameters.BackBufferHeight - 220 - rand.Next(100);
                this._model.Width = 90;
                this._model.Height = 70;
            }
        }

        public override int GetState()
        {
            return this.state;
        }

        public override bool ResetGame()
        {
            this.SetState(0);
            this._model.Playing = false;

            return base.ResetGame();
        }
    }
}
