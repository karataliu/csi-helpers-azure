using System.Collections.Generic;
using System.Linq;

namespace Csi.Helpers.Azure
{
    struct ResourceIdBuilder
    {
        public interface ISubscription { IResourceGroup ResourceGroup(string name); }
        public interface IResourceGroup { }

        public static ISubscription Subscription(string name) => new SubscriptionImpl(name);

        struct SubscriptionImpl : ISubscription
        {
            public IEnumerable<string> AllSections
            {
                get
                {
                    yield return "subscriptions";
                    yield return name;
                }
            }
            private readonly string name;
            public SubscriptionImpl(string name) => this.name = name;
            public IResourceGroup ResourceGroup(string name) => new ResourceGroupImpl(this.AllSections, name);
        }

        struct ResourceGroupImpl : IResourceGroup
        {
            public IEnumerable<string> AllSections => prevSections.Concat(Sections);

            private IEnumerable<string> Sections
            {
                get
                {
                    yield return "resourceGroups";
                    yield return name;
                }
            }

            private readonly IEnumerable<string> prevSections;
            private readonly string name;

            public ResourceGroupImpl(IEnumerable<string> prevSections, string name)
            {
                this.prevSections = prevSections;
                this.name = name;
            }
        }
    }
}
