using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

using Microsoft.Practices.Unity;

using IMvcDependencyResolver = System.Web.Mvc.IDependencyResolver;
using IWebApiDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace Icing.Web.Unity
{
	/// <summary>Provides methods that simplify service location and dependency resolution with a Unity container backing.</summary>
	public class UnityDependencyResolver : IMvcDependencyResolver, IWebApiDependencyResolver
	{
		private readonly IUnityContainer UnityContainer;

		/// <summary>
		/// Initializes a new instance of the <see cref="UnityDependencyResolver"/> class.
		/// </summary>
		/// <param name="unityContainer">The unity container.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="unityContainer"/> is null.</exception>
		public UnityDependencyResolver(IUnityContainer unityContainer)
		{
			if (unityContainer == null)
			{
				throw new ArgumentNullException("unityContainer");
			}

			UnityContainer = unityContainer;
		}

		/// <summary>
		/// Resolves singly registered services that support arbitrary object creation.
		/// </summary>
		/// <param name="serviceType">The type of the requested service or object.</param>
		/// <returns>The requested service or object.</returns>
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

		/// <summary>
		/// Resolves multiply registered services.
		/// </summary>
		/// <param name="serviceType">The type of the requested services.</param>
		/// <returns>The requested services.</returns>
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

		/// <summary>
		/// Starts a resolution scope. 
		/// </summary>
		/// <returns>The dependency scope.</returns>
		public IDependencyScope BeginScope()
		{
			return this;
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			// When BeginScope returns 'this', the Dispose method must be a no-op.
			// See http://www.asp.net/web-api/overview/extensibility/using-the-web-api-dependency-resolver
		}
	}
}