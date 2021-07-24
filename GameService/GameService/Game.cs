﻿using System;
using System.Collections.Generic;
using System.Linq;
using ECS;
using Games.Component;

namespace Games
{
    public class Game
    {
        public readonly Guid Id = new Guid();

        private Dictionary<string, Guid> players = new Dictionary<string, Guid>();

        private World world;

        private const int playerLimit = 10;

        private int lastUpdated = DateTime.Now.Millisecond;

        public Guid AddPlayer(string playerId, string name)
        {
            var components = this.CreatePlayerComponents(playerId, name);
            return world.CreateEntity(components);
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
            var time = DateTime.Now.Millisecond;
            var delta = time - lastUpdated;
            world.Run(delta, time);
            lastUpdated = time;
        }

        public void UpdatePlayerInput(string playerId, Input input)
        {
            Guid entityId;
            bool exists = players.TryGetValue(playerId, out entityId);

            if (!exists) {
                throw new Exception("Player doesn't exist");
            }

            if (!world.IsEntity(entityId)) {
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

            return new IComponent[] { type, player, nameComponent, input, position };
        }
    }
}
