using ContainervervoerVs2;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ContainervervoerVs2
{
    public class Ship
    {
        public int Length { get; set; }
        public int Width { get; set; }
        private List<Row> _rows = new List<Row>();
        public ReadOnlyCollection<Row> Rows => _rows.AsReadOnly();
        private readonly int MaxBalanceDifference = 20;

        public Ship(int length, int width)
        {
            try
            {
                if (length <= 0)
                {
                    throw new ArgumentException("Length moet groter dan 0 zijn");
                }
                if (width <= 0)
                {
                    throw new ArgumentException("Width moet groter dan 0 zijn");
                }
                Length = length;
                Width = width;
                for (int i = 0; i < width; i++) //rij aanmaken
                {
                    _rows.Add(new Row(length));
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message); throw; // Hergooi de uitzondering na logging, zodat de aanroeper het ook weet
            }
        }

        public int CalculateTotalWeight()
        {
            int totalWeight = 0;
            foreach (Row row in Rows)
            {
                totalWeight += row.CalculateTotalWeight();
            }

            return totalWeight;
        }

        public bool TryToAddContainer(Container container)
        {
            int leftWeight = CalculateLeftWeight();
            int rightWeight = CalculateRightWeight();
            int middleWeight = CalculateMiddleWeight();
            int minWeight = Math.Min(leftWeight, Math.Min(rightWeight, middleWeight));
            int middleIndex = Width / 2;

            // Probeer de container toe te voegen aan de middelste rij als het een oneven breedte is en de middelste rij het minst zwaar is
            if ((Width % 2 != 0 && middleWeight == minWeight) || Width == 1)
            {
                if (_rows[middleIndex].TryToAddContainer(container))
                {
                    return true;
                }
            }

            // Voeg de container toe aan de rij met het minste gewicht
            if (leftWeight <= rightWeight)
            {
                foreach (Row row in _rows.Take(middleIndex).OrderBy(row => row.CalculateTotalWeight()))
                {
                    if (row.TryToAddContainer(container))
                    {
                        return true;
                    }
                }
            }
            else
            {
                foreach (Row row in _rows.Skip(middleIndex + Width % 2).OrderBy(row => row.CalculateTotalWeight()))
                {
                    if (row.TryToAddContainer(container))
                    {
                        return true;

                    }
                }
            }

            return false;
        }

        public bool IsProperlyLoaded()
        {
            int maxWeight = Length * Width * (Stack.StackCapacity + Container.MaxWeight);
            int totalWeight = CalculateTotalWeight();

            if (2 * totalWeight >= maxWeight)
            {
                return true;
            }

            return false;
        }

        public bool IsBalanced()
        {
            int totalWeight = CalculateTotalWeight();
            double difference = Math.Abs(CalculateLeftWeight() - CalculateRightWeight()) / (double)totalWeight * 100; // berekent het % verschil van links & rechts

            if (difference > MaxBalanceDifference)
            {
                return false;
            }

            return true;
        }

        public int CalculateLeftWeight()
        {
            int leftWeight = 0;
            for (int i = 0; i < Width / 2; i++)
            {
                leftWeight += _rows[i].CalculateTotalWeight();
            }

            return leftWeight;
        }

        public int CalculateRightWeight()
        {
            int rightWeight = 0;
            for (int i = Width / 2 + Width % 2; i < Width; i++)
            {
                rightWeight += _rows[i].CalculateTotalWeight();
            }

            return rightWeight;
        }

        public int CalculateMiddleWeight()
        {
            int middleWeight = 0;

            for (int i = Width / 2; i < Width / 2 + Width % 2; i++)
            {
                middleWeight += _rows[i].CalculateTotalWeight();
            }

            return middleWeight;
        }


        public void StartVisualizer()
        {
            string stacks = string.Empty;
            string weights = string.Empty;

            for (int i = 0; i < _rows.Count; i++)
            {
                Row row = _rows[i];

                if (i > 0)
                {
                    stacks += "/";
                    weights += "/";
                }

                for (int j = 0; j < row.Stacks.Count; j++)
                {
                    Stack stack = row.Stacks[j];

                    if (j > 0)
                    {
                        stacks += ",";
                        weights += ",";
                    }

                    for (int k = 0; k < stack.Containers.Count; k++)
                    {
                        Container container = stack.Containers[k];

                        if (k > 0)
                        {
                            stacks += "-";
                            weights += "-";
                        }

                        stacks += GetContainerType(container);
                        weights += container.Weight;
                    }
                }
            }

            string url = $"https://app6i872272.luna.fhict.nl/?length={Length}&width={Width}&stacks={stacks}&weights={weights}";

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private int GetContainerType(Container container)
        {
            if (container.IsValuable)
            {
                if (container.NeedsCooling)
                {
                    return 4;
                }
                return 2;
            }

            if (container.NeedsCooling)
            {
                return 3;
            }

            return 1;
        }
    }
}