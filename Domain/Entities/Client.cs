﻿using Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client : BaseUser
    {
        public IList<Enterprise> ? Enterprises { get; set; }
    }
}
