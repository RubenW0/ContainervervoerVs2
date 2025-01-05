using ContainervervoerVs2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainervervoerVs2
{
    public class Ship
    {
        public ContainerStack[,] Layout { get; set; }
        public int Y { get; set; } = 4;
        public int X { get; set; } = 3;
        public int MaxWeight { get; set; } = 300; //in ton




    }
}
