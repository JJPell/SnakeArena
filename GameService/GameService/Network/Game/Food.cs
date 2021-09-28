using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Network.Game
{
    public class Food : Entity
    {
        public Food(int id, int x, int y) : base(id, EntityType.Food, x, y)
        {
        }
    }
}
