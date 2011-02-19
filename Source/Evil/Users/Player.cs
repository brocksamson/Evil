using System.Collections.Generic;
using System.Collections.ObjectModel;
using Evil.Agents;
using Evil.Bases;
using Evil.Common;

namespace Evil.Users
{
    public class Player : Entity
    {
        public virtual string Name { get; set; }
        public virtual Account Account { get; set; }
        public virtual IEnumerable<Agent> Agents { get; set; }
        public virtual Base MainBase { get; set; }

        public Player()
        {
            Agents = new Collection<Agent>();
        }
    }
}