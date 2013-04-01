using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

using Microsoft.Practices.Unity;

using IMvcDependencyResolver = System.Web.Mvc.IDependencyResolver;
using IWebApiDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace Icing.Web.Unity
{
	public class UnityDependencyResolver : IMvcDependencyResolver, IWebApiDependencyResolver
	{
		private readonly IUnityContainer UnityContainer;

		public UnityDependencyResolver(IUnityContainer unityContainer)
		{
			if (unityContainer == null)
			{
				throw new ArgumentNullException("unityContainer");
			}

			UnityContainer = unityContainer;
		}

		public object GetService(Type serviceType)
		{
			try
			{
				return UnityContainer.Resolve(serviceType);
			}
			catch
			{
				return null;
			}
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			try
			{
				return UnityContainer.ResolveAll(serviceType);
			}
			catch
			{
				return new List<object>();
			}
		}

		public IDependencyScope BeginScope()
		{
			return this;
		}

		public void Dispose()
		{
			// When BeginScope returns 'this', the Dispose method must be a no-op.
			// See http://www.asp.net/web-api/overview/extensibility/using-the-web-api-dependency-resolver
		}
	}
}