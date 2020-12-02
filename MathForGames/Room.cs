﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Room : Actor
    {
        private Sprite _sprite;

        public Room(float x, float y)
            : base(x, y)
        {
            _sprite = new Sprite("Images/Map.png");
        }
    }
}
