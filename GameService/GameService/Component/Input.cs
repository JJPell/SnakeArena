using System;
using System.Collections.Generic;
using System.Text;
using ECS;

namespace GameService.Component
{
    public struct Input : IComponent
    {
        bool Up;
        bool Down;
        bool Left;
        bool Right;
    }
}
