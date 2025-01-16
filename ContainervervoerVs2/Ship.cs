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
                    throw new ArgumentException("Length must be greater than 0");
                }
                if (width <= 0)
                {
                    throw new ArgumentException("Width must be greater than 0");
                }
                Length = length;
                Width = width;
                for (int i = 0; i < width; i++) // Create rows
                {
                    _rows.Add(new Row(length));
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public int GetTotalWeight()
        {
            int totalWeight = 0;
            foreach (Row row in Rows)
            {
                totalWeight += row.GetTotalWeight();
            }

            return totalWeight;
        }

        public bool TryToAddContainer(Container container)
        {
            int leftWeight = GetLeftWeight();
            int rightWeight = GetRightWeight();
            int middleWeight = GetMiddleWeight();
            int minWeight = Math.Min(leftWeight, Math.Min(rightWeight, middleWeight));
            int middleIndex = Width / 2;

            // Try to add to middle row
            if ((Width % 2 != 0 && middleWeight == minWeight) || Width == 1)
            {
                if (_rows[middleIndex].TryToAddContainer(container))
                {
                    return true;
                }
            }

            // Choose row with lowest weight
            if (leftWeight <= rightWeight)
            {
                foreach (Row row in _rows.Take(middleIndex).OrderBy(row => row.GetTotalWeight()))
                {
                    if (row.TryToAddContainer(container))
                    {
                        return true;
                    }
                }
            }
            else
            {
                foreach (Row row in _rows.Skip(middleIndex + Width % 2).OrderBy(row => row.GetTotalWeight()))
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
            int totalWeight = GetTotalWeight();

            return 2 * totalWeight >= maxWeight;
        }

        public bool IsBalanced()
        {
            int totalWeight = GetTotalWeight();
            double difference = Math.Abs(GetLeftWeight() - GetRightWeight()) / (double)totalWeight * 100; // bereken verschil in percentage

            return difference <= MaxBalanceDifference;
        }

        public int GetLeftWeight()
        {
            int leftWeight = 0;
            for (int i = 0; i < Width / 2; i++)
            {
                leftWeight += _rows[i].GetTotalWeight();
            }

            return leftWeight;
        }

        public int GetRightWeight()
        {
            int rightWeight = 0;
            for (int i = Width / 2 + Width % 2; i < Width; i++)
            {
                rightWeight += _rows[i].GetTotalWeight();
            }

            return rightWeight;
        }

        public int GetMiddleWeight()
        {
            int middleWeight = 0;

            for (int i = Width / 2; i < Width / 2 + Width % 2; i++)
            {
                middleWeight += _rows[i].GetTotalWeight();
            }

            return middleWeight;
        }

        public void StartVisualizer()
        {
            string url = CreateVisualizerUrl();
            OpenVisualizerUrl(url);
        }

        private string CreateVisualizerUrl()
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

            return $"https://app6i872272.luna.fhict.nl/?length={Length}&width={Width}&stacks={stacks}&weights={weights}";
        }

        private void OpenVisualizerUrl(string url)
        {
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