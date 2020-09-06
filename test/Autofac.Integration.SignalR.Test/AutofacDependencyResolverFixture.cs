// This software is part of the Autofac IoC container
// Copyright © 2013 Autofac Contributors
// https://autofac.org
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;
using Moq;
using Xunit;

namespace Autofac.Integration.SignalR.Test
{
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
}