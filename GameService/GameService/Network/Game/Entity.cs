using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Games;

namespace Games.Network.Game
{
    public class Entity
    {
        public int Id { get; private set; }
        public EntityType Type { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public Entity(int id, EntityType type, int x, int y)
        {
            Id = id;
            Type = type;
            X = x;
            Y = y;
        }
    }
}
