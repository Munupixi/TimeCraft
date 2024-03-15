﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCraft
{
    internal interface IEntity
    {
        void DeleteById(int id);
        void Add();
        void Update();
    }
}