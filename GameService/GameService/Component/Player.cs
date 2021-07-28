using System;
using ECS;

namespace Games.Component
{
    [Serializable]
    struct Player : IComponent
    {
        public string Id;
    }
}
