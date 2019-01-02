using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace IA3_1512138
{
    class GameControl : GameEntity
    {
        protected Game _game;

        public GameControl(Game game, Sprite2D model)
        {
            this._game = game;
            this._model = model;
        }
    }
}
