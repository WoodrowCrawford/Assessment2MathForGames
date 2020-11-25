using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
   public class Vector2
    {
        private float _x;
        private float _y;

        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public float Magnitude
        {
            get
            {
                return (float)Math.Sqrt(X * X + Y * Y);
            }
        }
    }
}
