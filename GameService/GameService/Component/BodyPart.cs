using System;
using ECS;

namespace Games.Component
{
    [Serializable]
    public struct BodyPart : IComponent
    {
        public int Index;
        public double X;
        public double Y;
    }
}
