using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Games;

namespace ConnectionService.Network.Game
{
    public class Entity
    {
        public int Id { get; private set; }
        public EntityType EntityType { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public Entity(int id, EntityType type, int x, int y)
        {
            Id = id;
            EntityType = type;
            X = x;
            Y = y;
        }
    }
}
