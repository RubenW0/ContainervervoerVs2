﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainervervoerVs2
{
    public class Container
    {
        public enum Type
        {
            Normal = 1,
            Valuable = 2,
            Coolable = 3,
            CoolableValuable = 4
        }

        public Type ContainerType { get; set; }
        public int ContainerWeight { get; set; } 



        public Container(Type containerType, int containerWeight)
        {
            ContainerType = containerType;
            ContainerWeight = containerWeight;
        }



    }
}
