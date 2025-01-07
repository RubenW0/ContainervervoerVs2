

using ContainervervoerVs2;


namespace Containervervoer
{
    static class Program
    {
        static void Main()
        {
            {
                Ship ship = new Ship();

                List<Container> containers = new List<Container>();

                containers.Add(new Container(Container.Type.Coolable, 30));
                containers.Add(new Container(Container.Type.Coolable, 30));
                containers.Add(new Container(Container.Type.Coolable, 30));
                containers.Add(new Container(Container.Type.Coolable, 30));
                containers.Add(new Container(Container.Type.Valuable, 30));
                containers.Add(new Container(Container.Type.Valuable, 30));
                containers.Add(new Container(Container.Type.Valuable, 30));
                containers.Add(new Container(Container.Type.Valuable, 30));

                Console.WriteLine("Attempting to place containers on the ship...");

                foreach (var container in containers)
                {
                    bool placed = ship.PlaceContainer(container);
                    Console.WriteLine($"Container ({container.ContainerType}, {container.ContainerWeight} tons): " +
                                      (placed ? "Placed successfully." : "Placement failed."));
                }

                Console.WriteLine($"\nTotal weight on the ship: {ship.TotalWeight()} tons");

                Console.WriteLine("\nLaunching the visualizer...");
                ship.StartVisualizer();
            }
        }
    }
}