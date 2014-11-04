﻿using System.Collections.Generic;
using System.Linq;

namespace System.ComponentModel.Server
{
    /// <summary>
    /// When implemented, represents a factory of all message-handlers that are used to handle the messages
    /// for the <see cref="MessageProcessor"/>.
    /// </summary>
    public abstract class MessageHandlerFactory
    {
        private readonly List<MessageHandlerClass> _messageHandlers;        

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageHandlerFactory" /> class.
        /// </summary>
        protected MessageHandlerFactory()
        {                        
            _messageHandlers = new List<MessageHandlerClass>();
        }

        /// <summary>
        /// Gets or sets the set of assemblies that represent the Service Layer.
        /// </summary>
        public AssemblySet ServiceLayer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the set of assemblies that represent the Application Layer.
        /// </summary>
        public AssemblySet ApplicationLayer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the set of assemblies that represent the Domain Layer.
        /// </summary>
        public AssemblySet DomainLayer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the set of assemblies that represent the Data Access Layer.
        /// </summary>
        public AssemblySet DataAccessLayer
        {
            get;
            set;
        }

        #region [====== MessageHandlers ======]

        /// <summary>
        /// Registers all message-handler types that are found in the Service Layer, Application Layer or Data Access Layer.
        /// </summary>        
        public void RegisterMessageHandlers()
        {
            RegisterMessageHandlers(null);
        }

        /// <summary>
        /// Registers all message-handler types that are found in the Service Layer, Application Layer or Data Access Layer.
        /// </summary>
        /// <param name="predicate">A predicate that filters the requires message-handlers to register (optional).</param>        
        public void RegisterMessageHandlers(Func<Type, bool> predicate)
        {
            RegisterMessageHandlers(predicate, ServiceLayer, ApplicationLayer, DataAccessLayer);
        }

        private void RegisterMessageHandlers(Func<Type, bool> predicate, params AssemblySet[] assemblies)
        {            
            foreach (var type in AssemblySet.Join(assemblies).GetTypes())
            {
                MessageHandlerClass handler;

                if (MessageHandlerClass.TryRegisterIn(this, type, predicate, out handler))
                {
                    _messageHandlers.Add(handler);
                }
            }           
        }

        /// <inheritdoc />
        internal IEnumerable<IMessageHandler<TMessage>> CreateMessageHandlersFor<TMessage>(TMessage message) where TMessage : class
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            return from handlerClass in _messageHandlers
                let handlers = handlerClass.CreateInstancesInEveryRoleFor(message)
                from handler in handlers
                select handler;
        }        

        /// <summary>
        /// Create an instance of the requested message handler type.
        /// </summary>
        /// <param name="type">Type to create.</param>
        /// <returns>An instance of the requested message handler type.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type"/> is <c>null</c>.
        /// </exception>                
        protected internal abstract object CreateMessageHandler(Type type);

        #endregion

        #region [====== Dependencies ======]

        /// <summary>
        /// Registers a set of types that are found in the ServiceLayer and Data Access Layer as dependencies.
        /// </summary>
        /// <param name="concreteTypePredicate">A predicate that identifies which types should be registered as dependencies.</param>
        /// <remarks>
        /// By default, all types will be registered with a <see cref="InstanceLifetime.PerUnitOfWork" /> lifetime.
        /// </remarks>
        public void RegisterDependencies(Func<Type, bool> concreteTypePredicate)
        {
            RegisterDependencies(concreteTypePredicate, InstanceLifetime.PerUnitOfWork);
        }

        /// <summary>
        /// Registers a set of types that are found in the ServiceLayer and Data Access Layer as dependencies.
        /// </summary>
        /// <param name="concreteTypePredicate">A predicate that identifies which types should be registered as dependencies.</param>
        /// <param name="defaultLifetime">
        /// Specifies the lifetime of the types that are registered that are not annotated with the <see cref="InstanceLifetimeAttribute" />.
        /// </param>
        public void RegisterDependencies(Func<Type, bool> concreteTypePredicate, InstanceLifetime defaultLifetime)
        {
            DependencyClass.RegisterDependencies(this, concreteTypePredicate, defaultLifetime);            
        }

        /// <summary>
        /// Registers a set of types that are found in the ServiceLayer and Data Access Layer as dependencies that are
        /// implementations of certain abstract types (interfaces or abstract classes) that are found in the Application Layer
        /// or Domain Layer.
        /// </summary>
        /// <param name="concreteTypePredicate">A predicate that identifies which concrete types should be registered as dependencies.</param>
        /// <param name="abstractTypePredicate">A predicate that identifies which abstract types should be registered as dependencies.</param>
        /// <exception cref="ArgumentException">
        /// One or more types specified by <paramref name="abstractTypePredicate"/> match with multiple concrete implementations.
        /// </exception>
        /// <remarks>
        /// By default, all types will be registered with a <see cref="InstanceLifetime.PerUnitOfWork" /> lifetime.
        /// </remarks>
        public void RegisterDependencies(Func<Type, bool> concreteTypePredicate, Func<Type, bool> abstractTypePredicate)
        {
            RegisterDependencies(concreteTypePredicate, abstractTypePredicate, InstanceLifetime.PerUnitOfWork);
        }

        /// <summary>
        /// Registers a set of types that are found in the ServiceLayer and Data Access Layer as dependencies that are
        /// implementations of certain abstract types (interfaces or abstract classes) that are found in the Application Layer
        /// or Domain Layer.
        /// </summary>
        /// <param name="concreteTypePredicate">A predicate that identifies which concrete types should be registered as dependencies.</param>
        /// <param name="abstractTypePredicate">A predicate that identifies which abstract types should be registered as dependencies.</param>
        /// <param name="defaultLifetime">
        /// Specifies the lifetime of the types that are registered that are not annotated with the <see cref="InstanceLifetimeAttribute" />.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="defaultLifetime"/> is not a valid <see cref="InstanceLifetime" />.
        /// </exception>
        public void RegisterDependencies(Func<Type, bool> concreteTypePredicate, Func<Type, bool> abstractTypePredicate, InstanceLifetime defaultLifetime)
        {
            DependencyClass.RegisterDependencies(this, concreteTypePredicate, abstractTypePredicate, defaultLifetime);            
        }

        #endregion

        #region [====== Type Registration ======]

        /// <summary>
        /// Registers a certain type of which a new instance is created each time it is resolved.
        /// </summary>
        /// <param name="concreteType">A concrete type.</param>        
        protected internal abstract void RegisterWithPerResolveLifetime(Type concreteType);

        /// <summary>
        /// Registers a certain type for a specified <paramref name="abstractType"/> of which a new instance is
        /// created each time it is resolved, for a certain abstract type.
        /// </summary>
        /// <param name="concreteType">A concrete type that implements <paramref name="abstractType"/>.</param>
        /// <param name="abstractType">An abstract type.</param>
        protected internal abstract void RegisterWithPerResolveLifetime(Type concreteType, Type abstractType);

        /// <summary>
        /// Registers a certain type to resolve with a lifetime that spans a single Unit of Work.
        /// </summary>
        /// <param name="concreteType">A concrete type.</param>        
        protected internal abstract void RegisterWithPerUnitOfWorkLifetime(Type concreteType);

        /// <summary>
        /// Registers a certain type to resolve for a specified <paramref name="abstractType"/>
        /// with a lifetime that spans a single Unit Test Scenario.
        /// </summary>
        /// <param name="concreteType">A concrete type that implements <paramref name="abstractType"/>.</param>
        /// <param name="abstractType">An abstract type.</param>       
        protected internal abstract void RegisterWithPerScenarioLifetime(Type concreteType, Type abstractType);

        /// <summary>
        /// Registers a certain type to resolve with a lifetime that spans a single Unit Test Scenario.
        /// </summary>
        /// <param name="concreteType">A concrete type.</param>        
        protected internal abstract void RegisterWithPerScenarioLifetime(Type concreteType);

        /// <summary>
        /// Registers a certain type to resolve for a specified <paramref name="abstractType"/>
        /// with a lifetime that spans a single Unit of Work.
        /// </summary>
        /// <param name="concreteType">A concrete type that implements <paramref name="abstractType"/>.</param>
        /// <param name="abstractType">An abstract type.</param>       
        protected internal abstract void RegisterWithPerUnitOfWorkLifetime(Type concreteType, Type abstractType);

        /// <summary>
        /// Registers a certain type with a lifetime that reflects that of a singleton.
        /// </summary>
        /// <param name="concreteType">A concrete type.</param>               
        protected internal abstract void RegisterSingleton(Type concreteType);

        /// <summary>
        /// Registers a certain type with a lifetime that reflects that of a singleton for a certain <paramref name="abstractType"/>.
        /// </summary>
        /// <param name="concreteType">A concrete type that implements <paramref name="abstractType"/>.</param>
        /// <param name="abstractType">An abstract type.</param>  
        protected internal abstract void RegisterSingleton(Type concreteType, Type abstractType);

        #endregion
    }
}
