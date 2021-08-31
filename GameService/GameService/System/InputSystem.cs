using ECS;
using Games.Component;
using System;
using System.Collections.Generic;

namespace Games.System
{
    class InputSystem : ECS.System
    {
        public override bool Match(IComponent[] entityComponents)
        {
            foreach (var component in entityComponents)
            {
                if (component.GetType() == typeof(Input))
                {
                    return true;
                }
            }

            return false;
        }

        public override void Run(KeyValuePair<int, IComponent[]> entity, int delta, int time, World world)
        {
            var components = entity.Value;
            var input = world.GetComponentByType<Component.Input>(components).Item2;
            var position = world.GetComponentByType<Component.Position>(components).Item2;

            var inputCount = CountInputs(input);

            if (inputCount == 0)
            {
                return;
            }

            string[] messages = new string[] {
                "Up: " + input.Up,
                "Down: " + input.Down,
                "Left: " + input.Left,
                "Right: " + input.Right,
            };

            Console.WriteLine(String.Join(", ", messages));
            Console.WriteLine(delta);

            var normalSpeed = 0.6;
            var diagonalSpeed = normalSpeed / 2;
            var speed = inputCount == 1 ? normalSpeed : diagonalSpeed;

            if (input.Up)
            {
                position.Y += speed;
            }

            if (input.Down)
            {
                position.Y -= speed;
            }

            if (input.Left)
            {
                position.X -= speed;
            }

            if (input.Right)
            {
                position.X += speed;
            }

            world.ReplaceComponent(entity.Key, position);
        }

        private int CountInputs(Component.Input input)
        {

            var inputsEnabled = 0;

            if (input.Up)
            {
                inputsEnabled++;
            }
            if (input.Down)
            {
                inputsEnabled++;
            }
            if (input.Left)
            {
                inputsEnabled++;
            }
            if (input.Right)
            {
                inputsEnabled++;
            }

            return inputsEnabled;
        }
    }
}
