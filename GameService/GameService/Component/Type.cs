using System;
using ECS;

namespace Games.Component
{
    [Serializable]
    public struct Type : IComponent
    {
        public EntityType Value;
    }
}
