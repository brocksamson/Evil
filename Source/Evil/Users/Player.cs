using System.Collections.Generic;
using System.Collections.ObjectModel;
using Evil.Agents;
using Evil.Common;
using Evil.Lairs;

namespace Evil.Users
{
    public class Player : Entity
    {
        public virtual string Name { get; set; }
        public virtual Account Account { get; set; }
        public virtual IEnumerable<Agent> Agents { get; set; }
        public virtual Lair MainLair { get; set; }

        public Player()
        {
            Agents = new Collection<Agent>();
        }
    }
}