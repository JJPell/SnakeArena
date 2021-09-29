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
            var foodPositions = GetFoodPositions(world);

            if (foodPositions.Count() == 0) return;

            var playerPosition = world.GetComponentByType<Position>(entity.Value).Item2;

            foreach (var food in foodPositions)
            {
                var foodPosition = food.Value;

                if (WithinColisionArea(playerPosition, foodPosition))
                {
                    world.RemoveEntity(food.Key);
                    GrowBody(entity, world);
                    break;
                }
            }
        }

        private Dictionary<int, Position> GetFoodPositions(World world)
        {
            var foodPositions = new Dictionary<int, Position>();

            var entities = world.GetEntitiesWithComponent<Position>();

            foreach (var entity in entities)
            {
                (var exists, var entityType) = world.GetComponentByType<Component.Type>(entity.Value);

                if (exists && entityType.Value == EntityType.Food)
                {
                    var response = world.GetComponentByType<Position>(entity.Value);

                    foodPositions.Add(entity.Key, response.Item2);
                }
            }

            return foodPositions;
        }

        private bool WithinColisionArea(Position playerPosition, Position foodPosition)
        {
            if (playerPosition.X > (foodPosition.X - 1) && playerPosition.X < (foodPosition.X + 1))
            {
                return true;
            }
            if (playerPosition.Y > (foodPosition.Y - 1) && playerPosition.Y < (foodPosition.Y + 1))
            {
                return true;
            }

            return false;
        }

        private void GrowBody(KeyValuePair<int, IComponent[]> entity, World world)
        {
            var bodyLength = world.GetComponentByType<BodyLength>(entity.Value).Item2;
            bodyLength.Value++;
            world.ReplaceComponent(entity.Key, bodyLength);

            var lastBodyPart = GetLastBodyPart(entity.Value);
            var newBodyPart = lastBodyPart;
            newBodyPart.Index++;
            world.AddComponent(entity.Key, newBodyPart);
        }

        private BodyPart GetLastBodyPart(IComponent[] components)
        {
            var lastBodyPart = default(BodyPart);

            foreach (var component in components)
            {
                if (component.GetType() == typeof(BodyPart))
                {
                    var bodyPart = (BodyPart)component;
                    if (bodyPart.Index > lastBodyPart.Index)
                    {
                        lastBodyPart = bodyPart;
                    }
                }
            }

            return lastBodyPart;
        }
    }
}
