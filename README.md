# Autofac.SignalR

ASP.NET classic SignalR integration for [Autofac](https://autofac.org).

[![Build status](https://ci.appveyor.com/api/projects/status/b90fy9gig8jxcq2g?svg=true)](https://ci.appveyor.com/project/Autofac/autofac-signalr)

Please file issues and pull requests for this package [in this repository](https://github.com/autofac/Autofac.SignalR/issues) rather than in the Autofac core repo.

If you're working with ASP.NET Core, you want [Autofac.Extensions.DependencyInjection](https://www.nuget.org/packages/Autofac.Extensions.DependencyInjection), not this package.

- [Documentation](https://autofac.readthedocs.io/en/latest/integration/signalr.html)
- [NuGet](https://www.nuget.org/packages/Autofac.SignalR2/)
- [Contributing](https://autofac.readthedocs.io/en/latest/contributors.html)
- [Open in Visual Studio Code](https://open.vscode.dev/autofac/Autofac.SignalR)

## Quick Start

To get Autofac integrated with SignalR you need to reference the SignalR integration NuGet package, register your hubs, and set the dependency resolver.

```c#
protected void Application_Start()
{
  var builder = new ContainerBuilder();

  // Register your SignalR hubs.
  builder.RegisterHubs(Assembly.GetExecutingAssembly());

  // Set the dependency resolver to be Autofac.
  var container = builder.Build();
  GlobalHost.DependencyResolver = new AutofacDependencyResolver(container);
}
```

Check out the [Autofac SignalR integration documentation](https://autofac.readthedocs.io/en/latest/integration/signalr.html) for more information.

## Get Help

**Need help with Autofac?** We have [a documentation site](https://autofac.readthedocs.io/) as well as [API documentation](https://autofac.org/apidoc/). We're ready to answer your questions on [Stack Overflow](https://stackoverflow.com/questions/tagged/autofac) or check out the [discussion forum](https://groups.google.com/forum/#forum/autofac).
