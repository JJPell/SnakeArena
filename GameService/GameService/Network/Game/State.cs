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
                var name = world.GetComponentByType<Name>(entityId).Item2.Value;
                var position = world.GetComponentByType<Position>(entityId).Item2;
                var x = (int)position.X;
                var y = (int)position.Y;

                var player = new Player(entityId, x, y, name);
                this.entities.Add(player);
            }
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(entities);
        }
    }

}
