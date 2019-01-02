using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IA3_1512138
{
    class Island : GameControl
    {
        List<Sprite2D> scrollings = new List<Sprite2D>();

        public Island(Game game, Sprite2D model) : base(game, model)
        {
            scrollings.Add(new Sprite2D(model));
            scrollings.Add(new Sprite2D(model));

            for (int i = 0; i < scrollings.Count; i++)
            {
                scrollings[i].Left = 0 + i * this._game.GraphicsDevice.PresentationParameters.BackBufferWidth;
                scrollings[i].Top = 0;
                scrollings[i].Width = this._game.GraphicsDevice.PresentationParameters.BackBufferWidth;
                scrollings[i].Height = this._game.GraphicsDevice.PresentationParameters.BackBufferHeight;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (this._model.Playing)
            {
                for (int i = 0; i < scrollings.Count; i++)
                {
                    scrollings[i].Left -= 10;

                    if (scrollings[i].Left + scrollings[i].Width <= 0) scrollings[i].Left = scrollings[i].Width;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            scrollings[1].Draw(gameTime, spriteBatch);
            for (int i = 0; i < scrollings.Count; i++)
            {
                scrollings[i].Draw(gameTime, spriteBatch);
            }
        }

        public override bool ResetGame()
        {
            this._model.Playing = false;

            for (int i = 0; i < scrollings.Count; i++)
            {
                scrollings[i].Left = 0 + i * this._game.GraphicsDevice.PresentationParameters.BackBufferWidth;
                scrollings[i].Top = 0;
                scrollings[i].Width = this._game.GraphicsDevice.PresentationParameters.BackBufferWidth;
                scrollings[i].Height = this._game.GraphicsDevice.PresentationParameters.BackBufferHeight;
            }

            return base.ResetGame();
        }
    }
}
