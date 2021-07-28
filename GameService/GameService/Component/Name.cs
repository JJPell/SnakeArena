using System;
using ECS;

namespace Games.Component
{
    [Serializable]
    struct Name : IComponent
    {
        public string Value;
    }
}
