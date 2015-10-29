using System.Reflection;
using Autofac.Core;
using Microsoft.AspNet.SignalR;
using Xunit;

namespace Autofac.Integration.SignalR.Test
{
    public class RegistrationExtensionsFixture
    {
        [Fact]
        public void RegisterHubsFindHubInterfaces()
        {
            var builder = new ContainerBuilder();

            builder.RegisterHubs(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            Assert.True(container.IsRegistered<TestHub>());
        }

        [Fact]
        public void HubRegistrationsAreExternallyOwned()
        {
            var builder = new ContainerBuilder();
            builder.RegisterHubs(Assembly.GetExecutingAssembly());
            var container = builder.Build();

            var service = new TypedService(typeof(TestHub));
            IComponentRegistration registration;
            container.ComponentRegistry.TryGetRegistration(service, out registration);

            Assert.Equal(InstanceOwnership.ExternallyOwned, registration.Ownership);
        }

        [Fact]
        public void RegisterConnectionsFindConnectionInterfaces()
        {
            var builder = new ContainerBuilder();

            builder.RegisterPersistentConnections(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            Assert.True(container.IsRegistered<TestConnection>());
        }

        [Fact]
        public void ConnectionRegistrationsAreExternallyOwned()
        {
            var builder = new ContainerBuilder();
            builder.RegisterPersistentConnections(Assembly.GetExecutingAssembly());
            var container = builder.Build();

            var service = new TypedService(typeof(TestConnection));
            IComponentRegistration registration;
            container.ComponentRegistry.TryGetRegistration(service, out registration);

            Assert.Equal(InstanceOwnership.ExternallyOwned, registration.Ownership);
        }
    }

    public class TestHub : Hub
    {
    }

    public class TestConnection : PersistentConnection
    {
    }
}