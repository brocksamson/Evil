using System;
using Evil.TestHelpers;
using MbUnit.Framework;

namespace Evil.Tests.Agents
{
    [TestFixture]
    public class When_an_agent_is_discovered
    {
        [Test]
        [Future]
        public void Should_have_a_50_percent_chance_to_escape()//turn into row test, decreases based on attempts by same agent...
        {
            throw new NotImplementedException();
        }

        [Test]
        [Future]
        public void Should_have_a_10_percent_chance_of_death()//turn into row test, increases based on attempts by same agent...
        {
            throw new NotImplementedException();
        }

        [Test]
        [Future]
        public void Should_have_a_20_percent_chance_to_be_recognized() //turn into row test, increases based on attempts by same agent...
        {
            throw new NotImplementedException();
        }

        [Test]
        [Future]
        public void Should_have_a_20_percent_chance_of_capture()//turn into row test, increases based on attempts by same agent...
        {
            throw new NotImplementedException();
        }

        [Test]
        [Future]
        public void Should_be_a_20_percent_chance_of_police_involvement_regardless_of_enemy_lair_setting()
        {
            //this is the chance of the mission being noticed involuntarily by the police.
            //police involvement + recognition == warrant
            throw new NotImplementedException();
        }

    }
}
