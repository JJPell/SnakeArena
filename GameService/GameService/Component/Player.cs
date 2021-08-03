using System;
using ECS;

namespace Games.Component
{
    [Serializable]
    public struct Player : IComponent
    {
        public string Id;
    }
}
