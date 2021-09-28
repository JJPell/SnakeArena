using ECS;
using Games.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.System
{
    class BodyLengthSystem : ECS.System
    {
        public override bool Match(IComponent[] entityComponents)
        {
            foreach (var component in entityComponents)
            {
                if (component.GetType() == typeof(BodyLength))
                {
                    return true;
                }
            }

            return false;
        }

        public override void Run(KeyValuePair<int, IComponent[]> entity, int delta, int time, World world)
        {
            throw new NotImplementedException();
        }
    }
}
