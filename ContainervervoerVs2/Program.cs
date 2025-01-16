using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;
using ContainervervoerVs2;

namespace Containervervoer
{
    static class Program
    {
        static void Main()
        {
            int length = 6;
            int width = 4;

            Ship ship = new Ship(length, width);

            List<ContainervervoerVs2.Container> containers = new List<ContainervervoerVs2.Container>();

            containers.AddRange(CreateContainers.CreateContainer(8, ContainervervoerVs2.Container.Type.Coolable, ContainervervoerVs2.Container.MaxWeight));  // coolable containers
            containers.AddRange(CreateContainers.CreateContainer(5, ContainervervoerVs2.Container.Type.CoolableValuable, ContainervervoerVs2.Container.MaxWeight));  // coolable & valuable containers
            containers.AddRange(CreateContainers.CreateContainer(40, ContainervervoerVs2.Container.Type.Normal, ContainervervoerVs2.Container.MaxWeight)); // normal containers
            containers.AddRange(CreateContainers.CreateContainer(30, ContainervervoerVs2.Container.Type.Valuable, ContainervervoerVs2.Container.MaxWeight)); // valuable containers

            foreach (ContainervervoerVs2.Container container in containers)
            {
                ship.TryToAddContainer(container);
            }

            try
            {
                if (!ship.IsProperlyLoaded())
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Gewicht is te laag.", ex);
            }
            try
            {
                if (!ship.IsBalanced())
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Het gewicht is niet eerlijk verdeel", ex);
            }

            Console.WriteLine("\nLaunching the visualizer...");
            ship.StartVisualizer();
        }
    }
    public class Container
    {
        public static readonly int EmptyWeight = 4;
        public static readonly int MaxWeight = 30;
        public int Weight { get; private set; }
        public Type ContainerType { get; private set; }

        public enum Type
        {
            Normal = 1,
            Valuable = 2,
            Coolable = 3,
            CoolableValuable = 4
        }

        public Container(int weight, Type containerType)
        {
            if (weight < EmptyWeight)
            {
                throw new Exception("Gewicht kan niet minder zijn dan 4 ton");
            }
            else if (weight > MaxWeight)
            {
                throw new Exception("Gewicht kan niet meer zijn dan 30 ton");
            }
            else
            {
                this.Weight = weight;
            }

            this.ContainerType = containerType;
        }

        public bool IsValuable => ContainerType == Type.Valuable || ContainerType == Type.CoolableValuable;
        public bool NeedsCooling => ContainerType == Type.Coolable || ContainerType == Type.CoolableValuable;
    }
}
