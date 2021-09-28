using ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.Component;

namespace Games.System
{
    class FoodGeneratorSystem : ECS.System
    {
        public override bool Match(IComponent[] entityComponents)
        {
            foreach (var component in entityComponents)
            {
                if (component.GetType() == typeof(Component.Type))
                {
                    return true;
                }
            }

            return false;
        }

        public override void Run(KeyValuePair<int, IComponent[]> entity, int delta, int time, World world)
        {
            if (IsFoodEntity(world)) return;

            var worldSize = 18;
            var random = new Random();

            var x = random.Next(0, worldSize);
            var y = random.Next(0, worldSize);

            var position = new Position
            {
                X = x,
                Y = y,
            };

            var type = new Component.Type
            {
                Value = EntityType.Food,
            };

            var components = new IComponent[]
            {
                position,
                type,
            };

            world.CreateEntity(components);
        }

        private bool IsFoodEntity(World world)
        {
            var matched = world.GetEntitiesWithComponent<Component.Type>();

            foreach (var entity in matched)
            {
                var component = world.GetComponentByType<Component.Type>(entity.Value);

                if (component.Item2.Value == EntityType.Food)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
