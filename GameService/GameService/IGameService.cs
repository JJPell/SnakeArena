using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Games;

namespace Games
{
    interface IGameService
    {
        public Game Create();
    }
}
