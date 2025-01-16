using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainervervoerVs2
{
    public class CreateContainers
    {
        private static readonly Random rand = new Random();

        public static List<Container> CreateContainer(int count, Container.Type containerType, int weight = 0)
        {
            List<Container> containers = new List<Container>();

            for (int i = 0; i < count; i++)
            {
                int randomWeight = 0;
                if (weight < Container.EmptyWeight || weight > Container.MaxWeight)
                {
                    randomWeight = rand.Next(Container.EmptyWeight, Container.MaxWeight);
                }
                else
                {
                    randomWeight = weight;
                }
                containers.Add(new Container(randomWeight, containerType));
            }

            return containers.OrderByDescending(container => container.Weight).ToList();
        }
    }
}