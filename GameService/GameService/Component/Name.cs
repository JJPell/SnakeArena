using System;
using ECS;

namespace Games.Component
{
    [Serializable]
    public struct Name : IComponent
    {
        public string Value;
    }
}
