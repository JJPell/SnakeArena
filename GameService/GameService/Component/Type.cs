using System;
using ECS;

namespace Games.Component
{
    [Serializable]
    struct Type : IComponent
    {
        public EntityType Value;
    }
}
