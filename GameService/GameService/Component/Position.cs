using System;
using ECS;

namespace Games.Component
{
    [Serializable]
    struct Position : IComponent
    {
        public int X;
        public int Y;
    }
}
