﻿// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using Autofac.Core;
using Microsoft.AspNet.SignalR;

namespace Autofac.Integration.SignalR.Test;

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
        container.ComponentRegistry.TryGetRegistration(service, out var registration);

        Assert.NotNull(registration);
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
        container.ComponentRegistry.TryGetRegistration(service, out IComponentRegistration registration);

        Assert.Equal(InstanceOwnership.ExternallyOwned, registration.Ownership);
    }

    private class TestHub : Hub
    {
    }

    private class TestConnection : PersistentConnection
    {
    }
}
