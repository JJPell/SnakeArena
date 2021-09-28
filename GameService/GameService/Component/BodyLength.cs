using System;
using ECS;

namespace Games.Component
{
    [Serializable]
    public struct BodyLength : IComponent
    {
        public int Value;
    }
}
