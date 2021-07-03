using System;
using System.Collections.Generic;
using System.Text;
using SnakeArena.Game.Core;
using ECS;


namespace GameService.Game.Component
{
    struct Position : IComponent
    {
        public int X;
        public int Y;
    }
}
