using System;
using System.Collections.Generic;
using ECS;
using Games;
using Games.Component;
using Newtonsoft.Json;

namespace ConnectionService.Network.Game
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

                var id = entityId.ToString();
                var name = world.GetComponentByType<Player>(entityId).Item2.Name;
                var x = world.GetComponentByType<Position>(entityId).Item2.X;
                var y = world.GetComponentByType<Position>(entityId).Item2.Y;

                var player = new Player(id, x, y, name);
                this.entities.Add(player);
            }
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(entities);
        }
    }

}
