﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameService;

namespace ConnectionService
{
    interface IGameService
    {
        public Game Create();
    }
}
