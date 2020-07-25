using IRCgirl.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRCgirl.API
{
	public class ServiceLocator
	{
		private Dictionary<Type, object> services;

		public ServiceLocator()
		{
			services = new Dictionary<Type, object>();
		}

		/// <summary>
		/// Registers a service of type <typeparamref name="T"/>.
		/// Returns true if the service was registered.
		/// </summary>
		/// <typeparam name="T">The type of the service to register.</typeparam>
		/// <returns></returns>
		public bool RegisterSingleton<T>() where T : IService, new()
		{
			if (services.ContainsKey(typeof(T)))
				return false;

			services.Add(typeof(T), new T());
			return true;
		}

		/// <summary>
		/// Registers an existing service object. Returns true if the service was registered.
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <typeparam name="T">The type of the service to register.</typeparam>
		/// <param name="service">The service object to register.</param>
		public bool RegisterSingleton<T>(T service) where T : class, IService
		{
			if (services.ContainsKey(typeof(T)))
				return false;

			if (service == null)
				throw new ArgumentNullException(nameof(service));

			services.Add(typeof(T), service);

			return true;
		}

		/// <summary>
		/// Returns a service of type <typeparamref name="T"/> if it can be located.
		/// </summary>
		/// <typeparam name="T">The type of the service to register.</typeparam>
		/// <exception cref="ServiceNotFoundException"></exception>
		/// <returns></returns>
		public T Locate<T>() where T : class, IService
		{
			if (!services.ContainsKey(typeof(T)))
				throw new ServiceNotFoundException(typeof(T));

			return services[typeof(T)] as T;
		}
	}
}
