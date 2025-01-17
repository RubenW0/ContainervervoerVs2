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
            containers.AddRange(CreateContainers.CreateContainer(5, ContainervervoerVs2.Container.Type.Normal, ContainervervoerVs2.Container.MaxWeight)); // normal containers
            containers.AddRange(CreateContainers.CreateContainer(30, ContainervervoerVs2.Container.Type.Valuable, ContainervervoerVs2.Container.MaxWeight)); // valuable containers

            foreach (ContainervervoerVs2.Container container in containers)
            {
                ship.TryToAddContainer(container);
            }

            if (!ship.IsProperlyLoaded())
            {
                Console.WriteLine("Warning: Gewicht is te laag.");
            }

            if (!ship.IsBalanced())
            {
                Console.WriteLine("Warning: Het gewicht is niet eerlijk verdeel.");
            }

            Console.WriteLine("\nLaunching the visualizer...");
            ship.StartVisualizer();
        }
    }
}