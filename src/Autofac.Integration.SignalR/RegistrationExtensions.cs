// This software is part of the Autofac IoC container
// Copyright © 2013 Autofac Contributors
// http://autofac.org
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
using System.Reflection;
using Autofac.Builder;
using Autofac.Features.Scanning;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Autofac.Integration.SignalR
{
    /// <summary>
    /// Extends <see cref="ContainerBuilder"/> with methods to support ASP.NET SignalR.
    /// </summary>
    public static class RegistrationExtensions
    {
        /// <summary>
        /// Register types that implement <see cref="IHub"/> in the provided assemblies.
        /// </summary>
        /// <param name="builder">The container builder.</param>
        /// <param name="assemblies">Assemblies to scan for controllers.</param>
        /// <returns>Registration builder allowing the controller components to be customised.</returns>
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
            RegisterHubs(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            return builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(IHub).IsAssignableFrom(t))
                .ExternallyOwned();
        }

        /// <summary>
        /// Register types that implement <see cref="PersistentConnection"/> in the provided assemblies.
        /// </summary>
        /// <param name="builder">The container builder.</param>
        /// <param name="assemblies">Assemblies to scan for persistent connections.</param>
        /// <returns>Registration builder allowing the persistent connections components to be customised.</returns>
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterPersistentConnections(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            return builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(PersistentConnection).IsAssignableFrom(t))
                .ExternallyOwned();
        }
    }
}
