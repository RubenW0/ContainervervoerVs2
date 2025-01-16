using System.Collections.ObjectModel;

namespace ContainervervoerVs2;

public class Stack
{
    private List<Container> _containers = new List<Container>();
    public ReadOnlyCollection<Container> Containers => _containers.AsReadOnly();
    public bool IsCooled { get; private set; }
    public bool HasValuable => _containers.Any(container => container.IsValuable);
    public static readonly int StackCapacity = 120;

    public Stack(bool isCooled)
    {
        IsCooled = isCooled;
    }

    public bool CanHoldWeight(Container container)
    {
        int currentWeight = 0;
        foreach (var existingContainer in _containers)
        {
            currentWeight += existingContainer.Weight;
        }
        return currentWeight + container.Weight <= StackCapacity;
    }

    public int GetTotalWeight()
    {
        int totalWeight = 0;
        foreach (var container in _containers)
        {
            totalWeight += container.Weight;
        }
        return totalWeight;
    }

    public bool TryToAddContainer(Container container)
    {
        if (container.NeedsCooling && !IsCooled)
        {
            return false;
        }

        if (HasValuable)
        {
            return false;
        }

        if (CanHoldWeight(container))
        {
            _containers.Add(container);
            return true;
        }

        return false;
    }

    public bool TryToRemoveContainer(Container container)
    {
        return _containers.Remove(container);
    }
}
