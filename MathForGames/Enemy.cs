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
        private Vector2 _moveTo1;
        private Vector2 _moveTo2;
        private Vector2 _moveTo3;

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

        public Vector2 MoveTo1
        {
            get
            {
                return _moveTo1;
            }
            set
            {
                _moveTo1 = value;
            }

        }

        public Vector2 MoveTo2
        {
            get
            {
                return _moveTo2;
            }
            set
            {
                _moveTo2 = value;
            }
        }

        public Vector2 MoveTo3
        {
            get
            {
                return _moveTo3;
            }
            set
            {
                _moveTo3 = value;
            }
        }

        //This creates the base stats for enemy 1
        public Enemy(float x, float y, Vector2 MoveTo1, Vector2 MoveTo2, Vector2 MoveTo3, char icon = ' ')
            : base (x, y, icon)
        {
            _sprite = new Sprite("Images/Enemy1.png");
            _health = 2;
            MoveTo1 = _moveTo1;
            MoveTo2 = _moveTo2;
            MoveTo3 = _moveTo3;
        }

        //This creates the base stats for enemy 2
        public Enemy(float x, float y, Vector2 MoveTo1, Vector2 MoveTo2, Vector2 MoveTo3, Color rayColor, char icon = ' ')
            : base (x, y, rayColor, icon)
        {
            _sprite = new Sprite("Images/Enemy2.png");
            _health = 2;
            MoveTo1 = _moveTo1;
            MoveTo2 = _moveTo2;
            MoveTo3 = _moveTo3;
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
