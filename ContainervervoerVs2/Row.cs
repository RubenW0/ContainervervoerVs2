using System.Collections.ObjectModel;

namespace ContainervervoerVs2
{
    public class Row
    {
        private List<Stack> _stacks = new List<Stack>();
        public ReadOnlyCollection<Stack> Stacks => _stacks.AsReadOnly();

        public Row(int length)
        {
            for (int i = 0; i < length; i++)
            {
                bool isCooled = i == 0; // Check if the first stack needs cooling
                _stacks.Add(new Stack(isCooled));
            }
        }

        public int GetTotalWeight()
        {
            int totalWeight = 0;
            foreach (Stack stack in Stacks)
            {
                totalWeight += stack.GetTotalWeight();
            }

            return totalWeight;
        }

        public bool TryToAddContainer(Container container)
        {
            var sortedStacks = Stacks
                .Select((stack, index) => (stack, index))
                .OrderBy(tuple => tuple.stack.GetTotalWeight())
                .ToList();

            foreach (var (stack, index) in sortedStacks)
            {
                if (stack.TryToAddContainer(container))
                {
                    if (!container.IsValuable)
                    {
                        if (AreFrontAndBackStacksAccessible(index))
                        {
                            return true;
                        }
                        stack.TryToRemoveContainer(container);
                    }
                    else
                    {
                        if (IsStackAccessible(index) && AreFrontAndBackStacksAccessible(index))
                        {
                            return true;
                        }
                        stack.TryToRemoveContainer(container);
                    }
                }
            }

            return false;
        }

        public bool IsStackAccessible(int index)
        {
            if (index == 0 || index == Stacks.Count - 1)
            {
                return true;
            }

            int currentHeight = Stacks[index].Containers.Count;

            if (currentHeight == 0) 
            {
                return true;
            }

            if (!Stacks[index].HasValuable)
            {
                return true;
            }

            int nextHeight = Stacks[index + 1].Containers.Count;
            int previousHeight = Stacks[index - 1].Containers.Count;

            return currentHeight > nextHeight || currentHeight > previousHeight;
        }

        public bool AreFrontAndBackStacksAccessible(int index)
        {
            bool isPreviousAccessible = IsPreviousStackAccessible(index);
            bool isNextAccessible = IsNextStackAccessible(index);

            return isPreviousAccessible && isNextAccessible;
        }

        private bool IsPreviousStackAccessible(int index)
        {
            if (index - 1 >= 0)
            {
                return IsStackAccessible(index - 1);
            }
            return true;
        }

        private bool IsNextStackAccessible(int index)
        {
            if (index + 1 < Stacks.Count)
            {
                return IsStackAccessible(index + 1);
            }
            return true;
        }
    }
}