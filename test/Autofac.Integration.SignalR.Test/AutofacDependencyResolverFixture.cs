// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;

namespace Autofac.Integration.SignalR.Test;

public class AutofacDependencyResolverFixture
{
    [Fact]
    public void CurrentPropertyExposesTheCorrectResolver()
    {
        var container = new ContainerBuilder().Build();
        var resolver = new AutofacDependencyResolver(container);

        GlobalHost.DependencyResolver = resolver;

        Assert.Equal(GlobalHost.DependencyResolver, AutofacDependencyResolver.Current);
    }

    [Fact]
    public void NullLifetimeScopeThrowsException()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => new AutofacDependencyResolver(null));
        Assert.Equal("lifetimeScope", exception.ParamName);
    }

    [Fact]
    public void ProvidedLifetimeScopeExposed()
    {
        var container = new ContainerBuilder().Build();
        var dependencyResolver = new AutofacDependencyResolver(container);

        Assert.Equal(container, dependencyResolver.LifetimeScope);
    }

    [Fact]
    public void GetServiceReturnsNullForUnregisteredService()
    {
        var container = new ContainerBuilder().Build();
        var resolver = new AutofacDependencyResolver(container);

        var service = resolver.GetService(typeof(object));

        Assert.Null(service);
    }

    [Fact]
    public void GetServiceReturnsRegisteredService()
    {
        var builder = new ContainerBuilder();
        builder.Register(c => new object());
        var container = builder.Build();
        var resolver = new AutofacDependencyResolver(container);

        var service = resolver.GetService(typeof(object));

        Assert.NotNull(service);
    }

    [Fact]
    public void GetServicesReturnsNullForUnregisteredService()
    {
        var container = new ContainerBuilder().Build();
        var resolver = new AutofacDependencyResolver(container);

        var services = resolver.GetServices(typeof(object));

        Assert.Null(services);
    }

    [Fact]
    public void GetServicesReturnsRegisteredService()
    {
        var builder = new ContainerBuilder();
        builder.Register(c => new object());
        var container = builder.Build();
        var resolver = new AutofacDependencyResolver(container);

        var services = resolver.GetServices(typeof(object));

        Assert.Single(services);
    }

    [Fact]
    public void CanResolveDefaultServices()
    {
        var container = new ContainerBuilder().Build();
        var resolver = new AutofacDependencyResolver(container);

        var service = resolver.GetService(typeof(IMessageBus));

        Assert.IsAssignableFrom<IMessageBus>(service);
    }

    [Fact]
    public void CanOverrideDefaultServices()
    {
        var builder = new ContainerBuilder();
        var messageBus = new Mock<IMessageBus>().Object;
        builder.RegisterInstance(messageBus);
        var resolver = new AutofacDependencyResolver(builder.Build());

        var service = resolver.GetService(typeof(IMessageBus));

        Assert.Same(messageBus, service);
    }
}
