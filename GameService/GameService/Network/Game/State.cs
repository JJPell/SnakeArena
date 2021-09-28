using System;
using System.Collections.Generic;
using ECS;
using Games;
using Games.Component;
using Newtonsoft.Json;

namespace Games.Network.Game
{
    public class State
    {
        private List<Entity> entities = new List<Entity>();

        private World world;

        public State(World world)
        {
            this.world = world;
            var entities = world.Entities;

            foreach (var pair in entities)
            {
                var entityId = pair.Key;
                var entityTypeComponent = world.GetComponentByType<Component.Type>(pair.Value).Item2;

                switch (entityTypeComponent.Value)
                {
                    case EntityType.Player:
                        CreatePlayer(entityId);
                        break;
                    case EntityType.Food:
                        CreateFood(entityId);
                        break;
                    case EntityType.FoodGenerator:
                        break;
                    default:
                        break;
                }
            }
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(entities);
        }

        private void CreatePlayer(int entityId)
        {
            var name = world.GetComponentByType<Name>(entityId).Item2.Value;
            var position = world.GetComponentByType<Position>(entityId).Item2;
            var x = (int)Math.Round(position.X);
            var y = (int)Math.Round(position.Y);

            var player = new Player(entityId, x, y, name);
            this.entities.Add(player);
        }

        private void CreateFood(int entityId)
        {
            var position = world.GetComponentByType<Position>(entityId).Item2;
            var x = (int)Math.Round(position.X);
            var y = (int)Math.Round(position.Y);

            var food = new Food(entityId, x, y);
            this.entities.Add(food);
        }
    }

}
