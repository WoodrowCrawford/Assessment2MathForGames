using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;
namespace MathForGames
{
    class Link : Actor
    {
        private float _speed = 1;
        private Sprite _sprite;
        private bool _canMove = true;

        public float Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        public Link(float x, float y, char icon = ' ')
            : base(x, y, icon)
        {
            _sprite = new Sprite("Images/Link.png");
        }

        public Link(float x, float y, Color rayColor, char icon = ' ')
            : base (x, y, rayColor, icon)
        {
            _sprite = new Sprite("Images/Link.png");
        }

        public void DisableControls()
        {
            _canMove = false;
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update(float deltaTime)
        {

            if (!_canMove)
                return;

            int xDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_W))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_S));
            
            velocity = velocity.Normalized * Speed;
            base.Update(deltaTime);
        }

        public override void Draw()
        {
            _sprite.Draw(_globalTransform);
            base.Draw();
        }

    }
}
