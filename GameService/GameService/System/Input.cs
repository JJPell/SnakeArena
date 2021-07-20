using ECS;
using System;
using System.Collections.Generic;

namespace GameService.System
{
    class InputSystem : ECS.System
    {
        public override bool Match(IComponent[] entityComponents) {
            throw new NotImplementedException();
        }

        public override void Run(KeyValuePair<Guid, IComponent[]> entity, int delta, int time) {
            throw new NotImplementedException();
        }
    }
}
