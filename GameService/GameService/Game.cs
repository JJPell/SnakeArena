using System;
using System.Collections.Generic;
using System.Linq;
using ECS;
using Games.Component;
using Games.Network.Game;
using Games.System;
using Player = Games.Component.Player;

namespace Games
{
    public class Game
    {
        public readonly Guid Id = new Guid();

        private int lastUpdated = DateTime.Now.Millisecond;

        private Dictionary<string, int> players = new Dictionary<string, int>();

        private const int playerLimit = 10;

        private readonly World world = new World();

        public Game()
        {
            var inputSystem = new InputSystem();
            var foodGeneratorSystem = new FoodGeneratorSystem();
            world.RegisterSystem(inputSystem);
            world.RegisterSystem(foodGeneratorSystem);
        }

        public int AddPlayer(string playerId, string name)
        {
            var components = this.CreatePlayerComponents(playerId, name);
            var entityId = world.CreateEntity(components);
            players.Add(playerId, entityId);
            Console.WriteLine("New player joined game " + Id.ToString() + " with playerID " + playerId);
            return entityId;
        }

        public State GetState()
        {
            var state = new State(this.world);
            return state;
        }

        public bool HasPlayer(string playerId)
        {
            return players.ContainsKey(playerId);
        }

        public bool IsJoinable()
        {
            return this.PlayerCount() < playerLimit;
        }

        public int PlayerCount()
        {
            var entities = world.GetEntitiesWithComponent<Player>();
            return entities.Count();
        }

        public void RemovePlayer(string playerId)
        {
            var entityId = players[playerId];
            world.RemoveEntity(entityId);
            players.Remove(playerId);
        }

        public void Update()
        {
            var time = DateTime.UtcNow.Millisecond;
            var delta = time - lastUpdated;
            world.Run(delta, time);
            lastUpdated = time;
        }

        public void UpdatePlayerInput(string playerId, Input input)
        {
            int entityId;
            bool exists = players.TryGetValue(playerId, out entityId);

            if (!exists)
            {
                throw new Exception("Player doesn't exist");
            }

            if (!world.IsEntity(entityId))
            {
                throw new Exception("Player Entity doesn't exist");
            }

            world.ReplaceComponent(entityId, input);
        }

        private IComponent[] CreatePlayerComponents(string playerId, string name)
        {
            var type = new Component.Type
            {
                Value = EntityType.Player,
            };

            var player = new Player
            {
                Id = playerId
            };

            var nameComponent = new Name
            {
                Value = name,
            };

            var input = new Input();
            var position = new Position();
            var bodyComponents = CreatePlayerBody();
            var components = new IComponent[] { type, player, nameComponent, input, position };

            return components.Concat(bodyComponents).ToArray();
        }

        private IComponent[] CreatePlayerBody()
        {
            var bodyLength = new BodyLength
            {
                Value = 4,
            };

            var bodyComponents = new IComponent[bodyLength.Value + 1];
            bodyComponents[0] = bodyLength;

            for (int i = 0; i < bodyLength.Value; i++)
            {
                var bodyPart = new BodyPart
                {
                    Index = i,
                    X = 0,
                    Y = 0 - i,
                };

                bodyComponents[i + 1] = bodyPart;
            }

            return bodyComponents;
        }
    }
}
