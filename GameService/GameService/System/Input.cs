using ECS;
using System;
using System.Collections.Generic;

namespace Games.System
{
    class InputSystem : ECS.System
    {
        public override bool Match(IComponent[] entityComponents) {
            return entityComponents.GetType().Name == "Input";
        }

        public override void Run(KeyValuePair<Guid, IComponent[]> entity, int delta, int time) {
            foreach (var component in entity.Value)
            {
                if (component.GetType() == typeof(Component.Input)) {
                    var input = (Component.Input)component;
                    string[] messages = new string[] {
                        "Up: " + input.Up,
                        "Down: " + input.Down,
                        "Left: " + input.Left,
                        "Right: " + input.Right,
                    };

                    Console.WriteLine(String.Join(", ", messages));
                }
            }
        }
    }
}
