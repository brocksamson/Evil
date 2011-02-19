using System;
using System.Collections.Generic;
using System.Reflection;
using Evil.Infrastructure.Structuremap;
using MbUnit.Framework;
using StructureMap.Graph;

namespace Evil.Infrastructure.Tests
{
    /// <summary>
    /// Initializes the IOC Container for all the infrastructure tests.  
    /// SetupFixture either applies to assembly or namespace + children namespaces, can't remember which...
    /// </summary>
    [AssemblyFixture]
    public class Initialize
    {
        [SetUp]
        public void InitalizeIoc()
        {
            new TestBootstrapper().Bootstrap();
        }
    }

    public class TestBootstrapper : Bootstrapper
    {
        protected override void GetAssemblies(IAssemblyScanner assembly)
        {
            assembly.AssemblyContainingType<Bootstrapper>();
            assembly.TheCallingAssembly();
        }
    }
}