using System;
using ECS;

namespace Games.Component
{
    [Serializable]
    public struct Input : IComponent
    {
        public bool Up;
        public bool Down;
        public bool Left;
        public bool Right;
    }
}
