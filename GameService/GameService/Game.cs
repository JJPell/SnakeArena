using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS;
using GameService.Component;

namespace GameService
{
    public class Game
    {
        private World world;

        public Guid Id = new Guid();

        private const int playerLimit = 10;

        public Guid AddPlayer(string name)
        {
            var components = this.CreatePlayerComponents(name);
            return world.CreateEntity(components);
        }

        public int PlayerCount()
        {
            world.
        }

        public void UpdatePlayerInput(Guid id, Input input)
        {
            if (!world.IsEntity(id)) {
                throw new Exception("Player Entity doesn't exist");
            }

            world.ReplaceComponent(id, input);
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
