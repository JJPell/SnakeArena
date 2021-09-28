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
            var components = GetComponents(entity.Value);
            var input = components.Item1;
            var position = components.Item2;
            var bodyParts = components.Item3;

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

            var normalSpeed = 1;
            var diagonalSpeed = normalSpeed / 2;
            var speed = inputCount == 1 ? normalSpeed : diagonalSpeed;

            var deltaX = 0;
            var deltaY = 0;

            if (input.Up)
            {
                deltaY += speed;
            }

            if (input.Down)
            {
                deltaY -= speed;
            }

            if (input.Left)
            {
                deltaX -= speed;
            }

            if (input.Right)
            {
                deltaX += speed;
            }

            var newbodyParts = MoveBodyParts(bodyParts, deltaX, deltaY);
            position.X += deltaX;
            position.Y += deltaY;

            world.ReplaceComponent(entity.Key, position);
            world.ReplaceComponentsOfSameType<BodyPart>(entity.Key, newbodyParts);
        }

        private int CountInputs(Input input)
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

        private (Input, Position, BodyPart[]) GetComponents(IComponent[] components)
        {
            Input input = new Input();
            Position position = new Position();
            var bodyParts = new List<BodyPart>();

            foreach (var component in components)
            {
                if (component.GetType() == typeof(Input))
                {
                    input = (Input)component;
                }
                else if (component.GetType() == typeof(Position))
                {
                    position = (Position)component;
                }
                else if (component.GetType() == typeof(BodyPart))
                {
                    bodyParts.Add((BodyPart)component);
                }
            }

            bodyParts.Sort((bodyPartA, bodyPartB) => bodyPartA.Index - bodyPartB.Index);

            return (input, position, bodyParts.ToArray());
        }

        private IComponent[] MoveBodyParts(BodyPart[] bodyParts, int deltaX, int deltaY)
        {
            var parts = bodyParts;

            for (int i = 0; i < bodyParts.Length; i++)
            {
                if (i == 0)
                {
                    parts[i].X += deltaX;
                    parts[i].Y += deltaY;
                }
                else
                {
                    parts[i].X = bodyParts[i - 1].X;
                    parts[i].Y = bodyParts[i - 1].Y;
                }
            }

            // Recast as IComponent[]
            IComponent[] components = new IComponent[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                components[i] = parts[i];
            }

            return components;
        }
    }
}
