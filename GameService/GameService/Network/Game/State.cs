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

        public State(World world)
        {
            var entities = world.Entities;

            foreach (var pair in entities)
            {
                var components = pair.Value;

                int id = 0;
                string name = "";
                int x = 0;
                int y = 0;

                foreach (var component in components)
                {
                    switch (component.GetType().Name)
                    {
                        case nameof(Games.Component.Name):
                            var nameComponent = (Games.Component.Name)component;
                            name = nameComponent.Value;
                            break;
                        case nameof(Games.Component.Position):
                            var positionComponent = (Games.Component.Position)component;
                            x = positionComponent.X;
                            y = positionComponent.Y;
                            break;
                        default:
                            break;
                    }
                }

                var player = new Player(id, x, y, name);
                this.entities.Add(player);
            }
        }

        string ToJson()
        {
            return JsonConvert.SerializeObject(entities);
        }
    }

}
