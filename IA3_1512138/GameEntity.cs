using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IA3_1512138
{
    class GameEntity
    {
        protected Sprite2D _model;

        public virtual void UnloadContent()
        {
            this._model.UnloadContent();
        }

        public virtual void Update(GameTime gameTime)
        {
            this._model.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this._model.Draw(gameTime, spriteBatch);
        }

        public virtual void Start()
        {
            this._model.Playing = true;
        }

        public virtual void Stop()
        {
            this._model.Playing = false;
        }


        public virtual void SetState(int state)
        {

        }

        public virtual int GetState()
        {
            return 0;
        }

        public virtual bool IsDead(GameEntity barrier)
        {
            return false;
        }

        public virtual Rectangle GetRec()
        {
            return new Rectangle();
        }

        public virtual bool ResetGame()
        {
            return true;
        }
    }
}
