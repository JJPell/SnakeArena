using System;
using System.Collections.Generic;
using System.Text;
using SnakeArena.Game.Core;
using ECS;

namespace GameService.Game.Component
{
    struct Input : IComponent
    {
        bool Up;
        bool Down;
        bool Left;
        bool Right;
    }
}
