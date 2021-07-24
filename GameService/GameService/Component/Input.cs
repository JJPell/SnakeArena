using ECS;

namespace Games.Component
{
    public struct Input : IComponent
    {
        public bool Up;
        public bool Down;
        public bool Left;
        public bool Right;
    }
}
