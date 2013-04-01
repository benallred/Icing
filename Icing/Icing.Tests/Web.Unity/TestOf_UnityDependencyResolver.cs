using System;
using System.Collections.Generic;
using System.Linq;
using Icing.TestTools.MSTest;
using Icing.Web.Unity;

using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Icing.Tests.Web.Unity
{
	[TestClass]
	public class TestOf_UnityDependencyResolver
	{
		[TestMethod]
		public void Constructor()
		{
			ExceptionAssertEx.Throws<ArgumentNullException>(() => new UnityDependencyResolver(null));

			UnityContainer expectedUnityContainer = new UnityContainer();
			UnityDependencyResolver unityDependencyResolver = new UnityDependencyResolver(expectedUnityContainer);

			PrivateObject privateUnityDependencyResolver = new PrivateObject(unityDependencyResolver);
			object actualUnityContainer = privateUnityDependencyResolver.GetField("UnityContainer");

			Assert.IsNotNull(actualUnityContainer);
			Assert.AreEqual(expectedUnityContainer, actualUnityContainer);
		}

		[TestMethod]
		public void GetService()
		{
			UnityDependencyResolver unityDependencyResolver = CreateUnityDependencyResolver();
			Assert.IsNotNull(unityDependencyResolver.GetService(typeof(ISomething)));
			Assert.IsNotNull(unityDependencyResolver.GetService(typeof(IElse)));
			Assert.IsNull(unityDependencyResolver.GetService(typeof(IAnother)));
		}

		[TestMethod]
		public void GetServices()
		{
			UnityDependencyResolver unityDependencyResolver = CreateUnityDependencyResolver();

			Assert.IsNotNull(unityDependencyResolver.GetServices(typeof(ISomething)));
			Assert.IsTrue(unityDependencyResolver.GetServices(typeof(ISomething)).Count() == 1);
			
			Assert.IsNotNull(unityDependencyResolver.GetServices(typeof(IElse)));
			Assert.IsFalse(unityDependencyResolver.GetServices(typeof(IElse)).Any());
			
			Assert.IsNotNull(unityDependencyResolver.GetServices(typeof(IAnother)));
			Assert.IsFalse(unityDependencyResolver.GetServices(typeof(IAnother)).Any());

			// Test catch
			Mock<IUnityContainer> mockOf_IUnityContainer = new Mock<IUnityContainer>();
			mockOf_IUnityContainer.Setup(unityContainer => unityContainer.ResolveAll(It.IsAny<Type>())).Throws<Exception>();
			IEnumerable<object> services = new UnityDependencyResolver(mockOf_IUnityContainer.Object).GetServices(typeof(ISomething));
			Assert.IsNotNull(services);
			Assert.IsFalse(services.Any());
		}

		[TestMethod]
		public void BeginScope()
		{
			UnityDependencyResolver unityDependencyResolver = CreateUnityDependencyResolver();
			Assert.AreEqual(unityDependencyResolver, unityDependencyResolver.BeginScope());
		}

		[TestMethod]
		public void Dispose()
		{
			UnityDependencyResolver unityDependencyResolver = CreateUnityDependencyResolver();
			unityDependencyResolver.Dispose(); // Should be a no-op. Make sure it doesn't throw an exception.
		}

		#region Helper Methods

		private static UnityDependencyResolver CreateUnityDependencyResolver()
		{
			return new UnityDependencyResolver(new UnityContainer()
																	.RegisterType<ISomething, Something>()
																	.RegisterType<ISomething, Something>("Something 2")
																	.RegisterType<IElse, Else>());
		}

		#endregion

		#region Helper Classes

		private interface ISomething
		{
		}

		private class Something : ISomething
		{
		}

		private interface IElse
		{
		}

		private class Else : IElse
		{
		}

		private interface IAnother
		{
		}

		#endregion
	}
}