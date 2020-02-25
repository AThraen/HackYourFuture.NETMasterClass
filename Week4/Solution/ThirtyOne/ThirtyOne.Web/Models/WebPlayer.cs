using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirtyOne.Shared.Models;

namespace ThirtyOne.Web.Models
{
    public class WebPlayer : Player
    {
        public WebPlayer(string name) : base(name)
        {

        }

        public override void Turn(Game game)
        {
            //Do nothing
        }
    }
}
