using ContainervervoerVs2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainervervoerVs2
{
    public class Ship
    {
        public ContainerStack[,] Layout { get; set; }
        public int Y { get; set; } = 4; //rijen
        public int X { get; set; } = 3; //stapels per rij
        public int MaxWeight { get; set; } = 300; //in ton


        public Ship()
        {
            Layout = new ContainerStack[Y, X];
            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    Layout[y, x] = new ContainerStack();
                }
            }
        }

        public int TotalWeight()
        {
            int totalWeight = 0;
            foreach (var stack in Layout)
            {
                totalWeight += stack.TotalWeight();
            }
            return totalWeight;
        }

        public bool PlaceContainer(Container container)
        {
            if (TotalWeight() + container.ContainerWeight > MaxWeight)
            {
                Console.WriteLine("Schip overschrijdt maximaal gewicht. Container kan niet worden toegevoegd.");
                return false;
            }

            if (container.ContainerType == Container.Type.Valuable || container.ContainerType == Container.Type.CoolableValuable)
            {
                if (TryPlaceInFrontOrBack(container))
                {
                    return true;
                }

                if (TryPlaceInAccessibleRow(container))
                {
                    return true;
                }
            }
            else
            {
                if (TryPlaceInAnyRow(container))
                {
                    return true;
                }
            }

            Console.WriteLine("Geen geldige plek gevonden voor container.");
            return false;
        }

        private bool TryPlaceInFrontOrBack(Container container)
        {
            for (int x = 0; x < X; x++)
            {
                if (Layout[0, x].AddContainer(container))
                {
                    return true;
                }
            }

            for (int x = 0; x < X; x++)
            {
                if (Layout[Y - 1, x].AddContainer(container))
                {
                    return true;
                }
            }

            return false;
        }

        private bool TryPlaceInAccessibleRow(Container container)
        {
            for (int y = 1; y < Y - 1; y++) 
            {
                bool isAccessible = true;
                for (int x = 0; x < X; x++)
                {
                    if (Layout[y - 1, x].TotalWeight() > 0)
                    {
                        isAccessible = false;
                        break;
                    }
                }

                if (isAccessible)
                {
                    for (int x = 0; x < X; x++)
                    {
                        if (Layout[y, x].AddContainer(container))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool TryPlaceInAnyRow(Container container)
        {
            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    if (Layout[y, x].AddContainer(container))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void StartVisualizer()
        {
            string stack = "";
            string weight = "";
            for (int z = 0; z < Y; z++)
            {
                if (z > 0)
                {
                    stack += '/';
                    weight += '/';
                }

                for (int x = 0; x < X; x++)
                {
                    if (x > 0)
                    {
                        stack += ",";
                        weight += ",";
                    }

                    if (Layout[z, x].Containers.Count > 0)
                    {
                        for (int y = 0; y < Layout[z, x].Containers.Count; y++)
                        {
                            Container container = Layout[z, x].Containers[y];
                            stack += Convert.ToString((int)container.ContainerType);
                            weight += Convert.ToString(container.ContainerWeight);
                            if (y < (Layout[z, x].Containers.Count - 1))
                            {
                                weight += "-";
                            }
                        }
                    }
                }
            }

            string url = "https://i872272.luna.fhict.nl/ContainerVisualizer/index.html?length=" + Y + "&width=" + X + "&stacks=" + stack + "&weights=" + weight;

            Process.Start(new ProcessStartInfo()
            {
                FileName = url,
                UseShellExecute = true
            });
        }
    }
}
