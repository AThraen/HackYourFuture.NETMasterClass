using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirtyOne.Shared.Models;

namespace ThirtyOne.Web.Models
{
    public class GameViewModel
    {
        public Game CurrentGame { get; set; }

        public WebPlayer CurrentPlayer{ get; set; }
    }
}
