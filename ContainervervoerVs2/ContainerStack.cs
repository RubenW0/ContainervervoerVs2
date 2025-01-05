using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainervervoerVs2
{
    public class ContainerStack
    {
        private const int MaxWeightOnTop = 120; //in ton
        private List<Container> containers;

        public ContainerStack()
        {
            containers = new List<Container>();
        }

        public int TotalWeight()
        {
            int totalWeight = 0;
            foreach (var container in containers)
            {
                totalWeight += container.ContainerWeight;
            }
            return totalWeight;
        }


        public bool CanAddContainer(Container newContainer)
        {
            if (containers.Count > 0)
            {
                if (containers[containers.Count - 1].ContainerType == Container.Type.Valuable ||
                    containers[containers.Count - 1].ContainerType == Container.Type.CoolableValuable)
                {
                    return false;
                }
            }

            int totalWeightAbove = 0;
            foreach (var container in containers)
            {
                totalWeightAbove += container.ContainerWeight;
            }

            return (totalWeightAbove + newContainer.ContainerWeight) <= MaxWeightOnTop;
        }

        public bool AddContainer(Container newContainer)
        {
            if (CanAddContainer(newContainer))
            {
                containers.Add(newContainer);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
