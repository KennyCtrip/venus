using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;

using Venus.LightInject;
using Venus.Adapters.Lifetime;
using Venus.Extensions.Annotation;

namespace Venus
{
    /// <summary>
    /// An IoC container using LightInject as its underlying container.
    /// </summary>
    public class VenusContainer : IVenusContainer
    {
        private ServiceContainer container = new ServiceContainer();
        private static ITypeExtractor serviceTypeExtractor = new CachedTypeExtractor(new NamedTypeExtractor());

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="VenusContainer"/> class.
        /// </summary>
        public VenusContainer()
        {
            container.AssemblyScanner = new Venus.Extensions.Annotation.AssemblyScanner(serviceTypeExtractor);
            container.PropertyDependencySelector = new AnnotatedPropertyDependencySelector(new PropertySelector());
            container.FieldDependencySelector = new AnnotatedFieldDependencySelector(new FieldSelector());
            container.ConstructorDependencySelector = new AnnotatedConstructorDependencySelector();
            container.ConstructorSelector = new AnnotatedConstructorSelector(container.CanGetInstance);
            container.Initialize(sr => true, Initialize);
            container.RegisterAssembly("*.dll");
        }
        #endregion

        #region Define Type Methods
        /// <summary>
        /// Registers a concrete type as a service.
        /// </summary>
        /// <typeparam name="TService">The service type to register.</typeparam>
        public void Define<TService>()
        {
            container.Register<TService>(GetInnerLifetime(null));
        }

        /// <summary>
        /// Registers a concrete type as a service.
        /// </summary>
        /// <typeparam name="TService">The service type to register.</typeparam>
        /// <param name="lifetime">The lifetime of the registered service.</param>
        public void Define<TService>(ILifetime lifetime)
        {
            container.Register<TService>(GetInnerLifetime(lifetime));
        }

        /// <summary>
        /// Registers the <typeparamref name="TService"/> with the <typeparamref name="TImplementation"/>.
        /// </summary>
        /// <typeparam name="TService">The service type to register.</typeparam>
        /// <typeparam name="TImplementation">The implementing type.</typeparam>
        public void Define<TService, TImplementation>() where TImplementation : TService
        {
            container.Register<TService, TImplementation>(GetInnerLifetime(null));
        }

        /// <summary>
        /// Registers the <typeparamref name="TService"/> with the <typeparamref name="TImplementation"/>.
        /// </summary>
        /// <typeparam name="TService">The service type to register.</typeparam>
        /// <typeparam name="TImplementation">The implementing type.</typeparam>
        /// <param name="lifetime">The lifetime of the registered service.</param>
        public void Define<TService, TImplementation>(ILifetime lifetime) where TImplementation : TService
        {
            container.Register<TService, TImplementation>(GetInnerLifetime(lifetime));
        }

        /// <summary>
        /// Registers the <typeparamref name="TService"/> with the <typeparamref name="TImplementation"/>.
        /// </summary>
        /// <typeparam name="TService">The service type to register.</typeparam>
        /// <typeparam name="TImplementation">The implementing type.</typeparam>
        /// <param name="serviceName">The name of the service.</param>
        public void Define<TService, TImplementation>(string serviceName) where TImplementation : TService
        {
            container.Register<TService, TImplementation>(serviceName, GetInnerLifetime(null));
        }

        /// <summary>
        /// Registers the <typeparamref name="TService"/> with the <typeparamref name="TImplementation"/>.
        /// </summary>
        /// <typeparam name="TService">The service type to register.</typeparam>
        /// <typeparam name="TImplementation">The implementing type.</typeparam>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="lifetime">The lifetime of the registered service.</param>
        public void Define<TService, TImplementation>(string serviceName, ILifetime lifetime) where TImplementation : TService
        {
            container.Register<TService, TImplementation>(serviceName, GetInnerLifetime(lifetime));
        }

        /// <summary>
        /// Registers a concrete type as a service.
        /// </summary>
        /// <param name="serviceType">The concrete type to register.</param>
        public void Define(Type serviceType)
        {
            container.Register(serviceType, GetInnerLifetime(null));
        }

        /// <summary>
        /// Registers a concrete type as a service.
        /// </summary>
        /// <param name="serviceType">The concrete type to register.</param>
        /// <param name="lifetime">The lifetime of the registered service.</param>
        public void Define(Type serviceType, ILifetime lifetime)
        {
            container.Register(serviceType, GetInnerLifetime(lifetime));
        }

        /// <summary>
        /// Registers the <paramref name="serviceType"/> with the <paramref name="implementingType"/>.
        /// </summary>
        /// <param name="serviceType">The service type to register.</param>
        /// <param name="implementingType">The implementing type.</param>
        public void Define(Type serviceType, Type implementingType)
        {
            container.Register(serviceType, implementingType, GetInnerLifetime(null));
        }

        /// <summary>
        /// Registers the <paramref name="serviceType"/> with the <paramref name="implementingType"/>.
        /// </summary>
        /// <param name="serviceType">The service type to register.</param>
        /// <param name="implementingType">The implementing type.</param>
        /// <param name="lifetime">The lifetime of the registered service.</param>
        public void Define(Type serviceType, Type implementingType, ILifetime lifetime)
        {
            container.Register(serviceType, implementingType, GetInnerLifetime(lifetime));
        }

        /// <summary>
        /// Registers the <paramref name="serviceType"/> with the <paramref name="implementingType"/>.
        /// </summary>
        /// <param name="serviceType">The service type to register.</param>
        /// <param name="implementingType">The implementing type.</param>
        /// <param name="serviceName">The name of the service.</param>
        public void Define(Type serviceType, Type implementingType, string serviceName)
        {
            container.Register(serviceType, implementingType, serviceName, GetInnerLifetime(null));
        }

        /// <summary>
        /// Registers the <paramref name="serviceType"/> with the <paramref name="implementingType"/>.
        /// </summary>
        /// <param name="serviceType">The service type to register.</param>
        /// <param name="implementingType">The implementing type.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="lifetime">The lifetime of the registered service.</param>
        public void Define(Type serviceType, Type implementingType, string serviceName, ILifetime lifetime)
        {
            container.Register(serviceType, implementingType, serviceName, GetInnerLifetime(lifetime));
        }
        #endregion

        #region Define Instance Methods
        /// <summary>
        /// Registers the <typeparamref name="TService"/> with the given <paramref name="instance"/>. 
        /// </summary>
        /// <typeparam name="TService">The service type to register.</typeparam>
        /// <param name="instance">The instance returned when this service is requested.</param>
        public void DefineInstance<TService>(TService instance)
        {
            container.RegisterInstance<TService>(instance);
        }

        /// <summary>
        /// Registers the <typeparamref name="TService"/> with the given <paramref name="instance"/>. 
        /// </summary>
        /// <typeparam name="TService">The service type to register.</typeparam>
        /// <param name="instance">The instance returned when this service is requested.</param>
        /// <param name="serviceName">The name of the service.</param>
        public void DefineInstance<TService>(TService instance, string serviceName)
        {
            container.RegisterInstance<TService>(instance, serviceName);
        }

        /// <summary>
        /// Registers the <paramref name="serviceType"/> with the given <paramref name="instance"/>. 
        /// </summary>
        /// <param name="serviceType">The service type to register.</param>
        /// <param name="instance">The instance returned when this service is requested.</param>
        public void DefineInstance(Type serviceType, object instance)
        {
            container.RegisterInstance(serviceType, instance);
        }

        /// <summary>
        /// Registers the <paramref name="serviceType"/> with the given <paramref name="instance"/>. 
        /// </summary>
        /// <param name="serviceType">The service type to register.</param>
        /// <param name="instance">The instance returned when this service is requested.</param>
        /// <param name="serviceName">The name of the service.</param>
        public void DefineInstance(Type serviceType, object instance, string serviceName)
        {
            container.RegisterInstance(serviceType, instance, serviceName);
        }
        #endregion

        #region Lookup Methods
        /// <summary>
        /// Gets an instance of the given <typeparamref name="TService"/> type.
        /// </summary>
        /// <typeparam name="TService">The type of the requested service.</typeparam>
        /// <returns>The requested service instance.</returns>
        public TService Lookup<TService>()
        {
            return container.GetInstance<TService>();
        }

        /// <summary>
        /// Gets a named instance of the given <typeparamref name="TService"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the requested service.</typeparam>
        /// <param name="serviceName">The name of the requested service.</param>
        /// <returns>The requested service instance.</returns>    
        public TService Lookup<TService>(string serviceName)
        {
            return container.GetInstance<TService>(serviceName);
        }

        /// <summary>
        /// Gets an instance of the given <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">The type of the requested service.</param>
        /// <returns>The requested service instance.</returns>
        public object Lookup(Type serviceType)
        {
            return container.GetInstance(serviceType);
        }

        /// <summary>
        /// Gets a named instance of the given <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">The type of the requested service.</param>
        /// <param name="serviceName">The name of the requested service.</param>
        /// <returns>The requested service instance.</returns>
        public object Lookup(Type serviceType, string serviceName)
        {
            return container.GetInstance(serviceType, serviceName);
        }

        /// <summary>
        /// Tries to get an instance of the given <typeparamref name="TService"/> type.
        /// </summary>
        /// <typeparam name="TService">The type of the requested service.</typeparam>
        /// <returns>The requested service instance if available, otherwise default(T).</returns>
        public TService TryLookup<TService>()
        {
            return container.TryGetInstance<TService>();
        }

        /// <summary>
        /// Tries to get an instance of the given <typeparamref name="TService"/> type.
        /// </summary>
        /// <typeparam name="TService">The type of the requested service.</typeparam>
        /// <param name="serviceName">The name of the requested service.</param>
        /// <returns>The requested service instance if available, otherwise default(T).</returns>
        public TService TryLookup<TService>(string serviceName)
        {
            return container.TryGetInstance<TService>(serviceName);
        }

        /// <summary>
        /// Gets an instance of the given <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">The type of the requested service.</param>
        /// <returns>The requested service instance if available, otherwise null.</returns>
        public object TryLookup(Type serviceType)
        {
            return container.TryGetInstance(serviceType);
        }

        /// <summary>
        /// Gets a named instance of the given <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">The type of the requested service.</param>
        /// <param name="serviceName">The name of the requested service.</param>
        /// <returns>The requested service instance if available, otherwise null.</returns>
        public object TryLookup(Type serviceType, string serviceName)
        {
            return container.TryGetInstance(serviceType, serviceName);
        }

        /// <summary>
        /// Gets all instances of type <typeparamref name="TService"/>.
        /// </summary>
        /// <typeparam name="TService">The type of services to resolve.</typeparam>
        /// <returns>A list that contains all implementations of the <typeparamref name="TService"/> type.</returns>
        public IEnumerable<TService> LookupList<TService>()
        {
            return container.GetInstanceList<TService>();
        }

        /// <summary>
        /// Gets all instances of the given <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">The type of services to resolve.</param>
        /// <returns>A list that contains all implementations of the <paramref name="serviceType"/>.</returns>
        public IEnumerable<object> LookupList(Type serviceType)
        {
            return container.GetInstanceList(serviceType);
        }

        /// <summary>
        /// Gets all instances and corresponding service names of type <typeparamref name="TService"/>.
        /// </summary>
        /// <typeparam name="TService">The type of services to resolve.</typeparam>
        /// <returns>A dictionary that contains all implementations of the <typeparamref name="TService"/> type.</returns>
        public IDictionary<string, TService> LookupMap<TService>()
        {
            return container.GetInstanceMap<TService>();
        }

        /// <summary>
        /// Gets all instances and corresponding service names of type <typeparamref name="TService"/>.
        /// </summary>
        /// <param name="serviceType">The type of services to resolve.</param>
        /// <returns>A dictionary that contains all implementations of the <paramref name="serviceType"/>.</returns>
        public IDictionary<string, object> LookupMap(Type serviceType)
        {
            return container.GetInstanceMap(serviceType);
        }
        #endregion

        #region Private Methods
        private LightInject.ILifetime GetLifetime()
        {
            return new LightInject.PerContainerLifetime();
        }

        private LightInject.ILifetime GetInnerLifetime(ILifetime lifetime)
        {
            if (lifetime == null)
                return new LightInject.PerContainerLifetime();
            else
                return new AdapterLifetime(lifetime); 
        }

        private void Initialize(IServiceFactory serviceFactory, object instance)
        {
            var loggable = instance as ILoggable;
            if (loggable != null)
                loggable.SetLogger(serviceFactory.TryGetInstance<ILogger>());

            var containable = instance as IContainable;
            if (containable != null)
                containable.SetContainer(this);

            var initializable = instance as IInitializable;
            if (initializable != null)
                initializable.Initialize();
        }
        #endregion

        public void Dispose()
        {
            container.Dispose();
        }
    }
}
