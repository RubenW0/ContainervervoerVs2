namespace ContainervervoerVs2
{
    public class Container
    {
        public static readonly int EmptyWeight = 4;
        public static readonly int MaxWeight = 30;
        public int Weight { get; private set; }
        public Type ContainerType { get; private set; }

        public enum Type
        {
            Normal = 1,
            Valuable = 2,
            Coolable = 3,
            CoolableValuable = 4
        }

        public Container(int weight, Type containerType)
        {
            if (weight < EmptyWeight)
            {
                throw new Exception($"Gewicht kan niet minder zijn dan {EmptyWeight} ton");
            }
            else if (weight > MaxWeight)
            {
                throw new Exception($"Gewicht kan niet meer zijn dan {MaxWeight} ton");
            }
            else
            {
                this.Weight = weight;
            }

            this.ContainerType = containerType;
        }

        public bool IsValuable => ContainerType == Type.Valuable || ContainerType == Type.CoolableValuable;
        public bool NeedsCooling => ContainerType == Type.Coolable || ContainerType == Type.CoolableValuable;
    }
}