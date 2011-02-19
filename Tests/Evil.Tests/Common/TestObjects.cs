using System;
using Evil.Common;

namespace Evil.Tests.Common
{
    public class ObjectWithNoDomainSignature : Entity
    {
        public string Foo { get; set; }
        public int Bar { get; set; }
    }


    public class ObjectWithOneDomainSignature : Entity
    {
        [DomainSignature]
        public string Foo { get; set; }

        public int Bar { get; set; }
    }

    public class ObjectWithMultipleDomainSignature : Entity
    {
        [DomainSignature]
        public string Foo { get; set; }

        [DomainSignature]
        public int Bar { get; set; }
    }


    public class ObjectWithComplexDomainSignature : Entity
    {
        [DomainSignature]
        public ObjectWithOneDomainSignature Complex { get; set; }
    }

    public class ObjectWithInheritedDomainSignature : ObjectWithOneDomainSignature
    {
        [DomainSignature]
        public DateTime Hammer { get; set; }
    }

    public class ObjectWithOneDomainSignature2 : ObjectWithOneDomainSignature
    {
    }
}