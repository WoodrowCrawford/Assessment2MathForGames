using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Actor
    {
        private float _speed = 3;
        protected char _icon = ' ';
        private Vector2 _velocity = new Vector2();
        protected Matrix3 _globalTransform = new Matrix3();
        protected Matrix3 _localTransform = new Matrix3();
        protected Matrix3 _translation = new Matrix3();
        protected Matrix3 _rotation = new Matrix3();
        protected Matrix3 _scale = new Matrix3();
        protected Actor _parent;
        protected Color _raycolor;
        protected ConsoleColor _color;
        protected Actor[] _children = new Actor[0];
        protected float _rotationAngle;
        protected float _collisionRadius;
       

        public bool Started { get; private set; }

        public Vector2 Forward
        {
            get
            {
                return new Vector2(_globalTransform.m11, _globalTransform.m21);
            }
            set
            {
                Vector2 lookPosition = LocalPosition + value.Normalized;
                LookAt(lookPosition);
            }
        }

        public Vector2 WorldPosition
        {
            get
            {
                return new Vector2(_globalTransform.m13, _globalTransform.m23);
            }
        }

        public Vector2 LocalPosition
        {
            get
            {
                return new Vector2(_localTransform.m13, _localTransform.m23);
            }
            set
            {
                _translation.m13 = value.X;
                _translation.m23 = value.Y;
            }
        }

        public Vector2 velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = value;
            } 

        }

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

        public Actor(float x, float y, char icon = ' ')
        {
            _icon = icon;
            
            _localTransform = new Matrix3();
            LocalPosition = new Vector2(x, y);
            _velocity = new Vector2();

        }

        public Actor(float x, float y, Color raycolor, char icon = ' ')
            :this(x,y,icon)
        {
            _localTransform = new Matrix3();
            _raycolor = raycolor;
        }

        public void AddChild(Actor child)
        {
            Actor[] tempArray = new Actor[_children.Length + 1];
            for (int i = 0; i < _children.Length; i++)
            {
                tempArray[i] = _children[i];
            }

            tempArray[_children.Length] = child;
            _children = tempArray;
            child._parent = this;
        }

        public bool RemoveChild(Actor child)
        {
            bool childRemoved = false;

            if (child == null)
                return false;

            Actor[] tempArray = new Actor[_children.Length - 1];

            int j = 0;
            for (int i = 0; i <_children.Length; i++)
            {
                if (child != _children[i])
                {
                    tempArray[j] = _children[i];
                    j++;
                }
                else
                {
                    childRemoved = true;
                }
            }

            _children = tempArray;
            child._parent = null;
            return childRemoved;
        }

        public void SetTranslation(Vector2 position)
        {
            _translation = Matrix3.CreateTranslation(position);
        }

        public void SetRotation(float radians)
        {
            _rotation = Matrix3.CreateRotation(radians);
        }

        public void Rotate(float radians)
        {
            _rotation *= Matrix3.CreateRotation(radians);
        }

        public void SetScale(float x, float y)
        {
            _scale = Matrix3.CreateScale(new Vector2(x, y));
        }

        public void LookAt(Vector2 position)
        {
            Vector2 direction = (position - LocalPosition).Normalized;

            float dotProd = Vector2.DotProduct(Forward, direction);
            if (Math.Abs(dotProd) > 1)
                return;
            float angle = (float)Math.Acos(dotProd);


            Vector2 perp = new Vector2(direction.Y, -direction.X);

            
            float perpDot = Vector2.DotProduct(perp, Forward);

            if (perpDot != 0)
                angle *= -perpDot / Math.Abs(perpDot);

            Rotate(angle);
        }


        public bool CheckCollision(Actor other)
        {
            float distance = (other.WorldPosition - WorldPosition).Magnitude;
            return distance <= other._collisionRadius + _collisionRadius;
        }

        public virtual void OnCollision(Actor other)
        {

        }

        private void UpdateTransforms()
        {
            _localTransform = _translation * _rotation * _scale;

            if (_parent != null)
                _globalTransform = _parent._globalTransform * _localTransform;
            else
                _globalTransform = Game.GetCurrentScene().World * _localTransform;
        }

        private void UpdateFacing()
        {
            if (_velocity.Magnitude <= 0)
                return;
            Forward = velocity.Normalized;
        }


        public virtual void Start()
        {
            Started = true;
        }

        public virtual void Update(float deltaTime)
        {
            UpdateTransforms();
            UpdateFacing();

            if (velocity.Magnitude > Speed)
                velocity = velocity.Normalized * Speed;

            LocalPosition += _velocity * deltaTime;
        }

        public virtual void Draw()
        {
            Raylib.DrawLine(
                (int)(WorldPosition.X * 32),
                (int)(WorldPosition.Y * 32),
                (int)((WorldPosition.X + Forward.X) * 32),
                (int)((WorldPosition.Y + Forward.Y) * 32),
                Color.ORANGE
                );

            Console.ForegroundColor = _color;

            if (WorldPosition.X >= 0 && WorldPosition.X < Console.WindowWidth
              && WorldPosition.Y >= 0 && WorldPosition.Y < Console.WindowHeight)
            {
                Console.SetCursorPosition((int)WorldPosition.X, (int)WorldPosition.Y);
                Console.Write(_icon);
            }


            Console.ForegroundColor = Game.DefaultColor;
        }

        public virtual void End()
        {
            Started = false;
        }

    }
}
