For Ninject.

1) Install Ninject MVC5 package.
	If your project is not MVC 5 and is upgraded, Install-Package -Id  Microsoft.AspNet.WebHelpers

2) Support WebAPI: Install WebApiContrib.ioC.Ninject Package.

3) Support WebAPI: Add GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel); in CreateKernel() method 
in App_start/Ninject.Web.Common.cs.

4)	Add the below in RegisterServices(IKernel kernel) in App_start/Ninject.Web.Common.cs		
			kernel.Bind<ICountingKsRepository>().To<CountingKsRepository>();
            kernel.Bind<CountingKsContext>().To<CountingKsContext>();
