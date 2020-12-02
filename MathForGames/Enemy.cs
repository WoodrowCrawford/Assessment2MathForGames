using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Enemy : Actor
    {
        private float _health;
        private float _speed;
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
                _health = value;
            }
        }

        //This creates the base stats for enemy 1
        public Enemy(float x, float y, char icon = ' ')
            : base (x, y, icon)
        {
            _sprite = new Sprite("Images/Enemy1.png");
            _health = 2;
        }

        //This creates the base stats for enemy 2
        public Enemy(float x, float y, Color rayColor, char icon = ' ')
            : base (x, y, rayColor, icon)
        {
            _sprite = new Sprite("Images/Enemy2.png");
            _health = 2;
        }

       
        //This checks to see if the enemy has collided with Link
        public virtual void OnCollision(Enemy enemy, Scene scene, Link link, Actor actor)
        {
            if (enemy._health <= 0 && actor.CheckCollision(link) == true)
                enemy._health = enemy._health - 1;


            scene.RemoveActor(enemy);

        }

        //Movement pattern for the enemy only when alive


        public override void Start()
        {
            base.Start();
        }

        public override void Update(float deltaTime)
        {

            if (!_canMove)
                return;

           

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
