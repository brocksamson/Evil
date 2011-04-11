using System;
using System.Collections.Generic;
using System.Text;
using Evil.Agents;
using Evil.Common;
using Evil.Lairs;
using Evil.Tests.Extensions;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace Evil.Tests.Agents
{
    [TestFixture]
    public class When_infiltrating_a_bar
    {
        [Test]
        public void Should_raise_infiltration_started_event()
        {
            var agent = new Agent
                            {
                                Name = "test"
                            };
            var enemyBar = new Lair
                               {

                               };
            //agent.InfiltrationStarted
            agent.SetId(1);
            agent.Infiltrate(enemyBar);
        }
    }

    public static class EntityExtensions
    {
        public static void SetId(this Entity entity, int id)
        {
            entity.SetProperty(m => m.Id, id);
        }
    }
}
