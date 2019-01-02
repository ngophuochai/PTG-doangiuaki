using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IA3_1512138
{
    class Sprite2D
    {
        int _left;
        int _top;
        int _width;
        int _height;
        List<Texture2D> _images = new List<Texture2D>();
        int imageIndex = 0;
        bool _playing = false;

        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public int Top { get => _top; set => _top = value; }
        public int Left { get => _left; set => _left = value; }
        public bool Playing { get => _playing; set => _playing = value; }
        public int ImageIndex { get => imageIndex; set => imageIndex = value; }
        public List<Texture2D> Images { get => _images; set => _images = value; }

        public Sprite2D(int left, int top, int width, int height)
        {
            this._left = left;
            this._top = top;
            this._width = width;
            this._height = height;
        }

        public Sprite2D(Sprite2D sprite2D)
        {
            this._left = sprite2D.Left;
            this._top = sprite2D.Top;
            this._width = sprite2D.Width;
            this._height = sprite2D.Height;
            this._images = sprite2D.Images;
        }

        public void LoadContent(Game game, string[] imagesPath)
        {
            for (int i = 0; i < imagesPath.Length; i++)
            {
                var tex = game.Content.Load<Texture2D>(imagesPath[i]);
                this._images.Add(tex);
            }
        }

        public void UnloadContent()
        {
            this._images.Clear();
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_images[imageIndex], new Rectangle(_left, _top, _width, _height), Color.White);
        }
    }
}
