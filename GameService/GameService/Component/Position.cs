using System;
using ECS;

namespace Games.Component
{
    [Serializable]
    public struct Position : IComponent
    {
        public int X;
        public int Y;
    }
}
