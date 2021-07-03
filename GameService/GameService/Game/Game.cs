using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS;
using GameService.Game.Component;

namespace GameService.Game
{
    class Game
    {
        private World world;

        public Guid AddPlayer(string name)
        {
            var components = this.CreatePlayerComponents(name);
            return world.CreateEntity(components);
        }

        public void UpdatePlayerInput(Guid id, Input input)
        {
            var player = world.GetComponents(id);
            player.
        }
        
        private IComponent[] CreatePlayerComponents(string name)
        {
            var type = new Component.Type
            {
                Value = EntityType.Player,
            };

            var nameComponent = new Name
            {
                Value = name,
            };

            var input = new Input();
            var position = new Position();

            return new IComponent[] { type, nameComponent, input, position };
        }
    }
}
