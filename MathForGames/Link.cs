using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;
namespace MathForGames
{
    class Link : Actor
    {
        private int _health = 2;
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

        public float Health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = 2;
            }
        }

        //This creates the base stats for Link
        public Link(float x, float y, char icon = ' ')
            : base(x, y, icon)
        {
            _sprite = new Sprite("Images/Link.png");
            _health = 2;
        }


        //An overloaded function for Link
        public Link(float x, float y, Color rayColor, char icon = ' ')
            : base (x, y, rayColor, icon)
        {
            _sprite = new Sprite("Images/Link.png");
            _health = 2;
        }


        //Makes it so that the player can not move if they ran out of health
        public void DisableControls()
        {
            _canMove = false;
        }


        //Checks to see if Link has collided with an enemy
        public virtual void OnCollision(Link link, Enemy enemy)
        {
            if (link._health <= 0 && CheckCollision(enemy) == true)
                link._health = link._health - 1;


            _canMove = false;
            Game._gameOver = true;
        }


        //If Link is close to an enemy and presses Space, they will deal damage to the 
        //enemy.
        public override void Attack(Link link, Enemy enemy)
        {
            base.Attack(link, enemy);
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
