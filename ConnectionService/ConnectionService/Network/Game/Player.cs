using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Games;

namespace ConnectionService.Network.Game
{
    public class Player : Entity
    {
        public string Name { get; private set; }

        public Player(int id, int x, int y, string name) : base(id, EntityType.Player, x, y)
        {
            Name = name;
        }
    }
}
