using System;
using ECS;

namespace Games.Component
{
    [Serializable]
    public struct Position : IComponent
    {
        public double X;
        public double Y;
    }
}
