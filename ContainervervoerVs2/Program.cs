

using ContainervervoerVs2;


namespace Containervervoer
{
    static class Program
    {
        static void Main()
        {
            {
                List<Container> containers = new List<Container>();

                containers.Add(new Container(Container.Type.Coolable, 30));

                containers.Add(new Container(Container.Type.Valuable, 30));
            }
        }
    }
}