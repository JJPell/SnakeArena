using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS;

namespace GameService.Game.Component
{
    struct Type : IComponent
    {
        public EntityType Value;
    }
}
