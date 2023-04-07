﻿// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

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
        /// <returns>Registration builder allowing the controller components to be customized.</returns>
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
        /// <returns>Registration builder allowing the persistent connections components to be customized.</returns>
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterPersistentConnections(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            return builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(PersistentConnection).IsAssignableFrom(t))
                .ExternallyOwned();
        }
    }
}
