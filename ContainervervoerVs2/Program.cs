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

            List<Container> containers = new List<Container>();

            containers.AddRange(CreateContainers.CreateContainer(8, false, true, Container.MaxWeight));  // koeling containers
            containers.AddRange(CreateContainers.CreateContainer(5, true, true, Container.MaxWeight));  // koeling & waardevolle containers
            containers.AddRange(CreateContainers.CreateContainer(80, false, false, Container.MaxWeight)); // normale containers
            containers.AddRange(CreateContainers.CreateContainer(30, true, false, Container.MaxWeight)); // waardevolle containers

            foreach (Container container in containers)
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
}
