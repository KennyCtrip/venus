using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Venus.Tests
{
    interface IUserRepository
    {
        string Name { get; }
    }

    class SqlUserRepository : IUserRepository
    {
        public string Name
        {
            get { return "sql"; }
        }
    }

    class OracleUserRepository : IUserRepository
    {
        public string Name
        {
            get { return "oracle"; }
        }
    }

    interface ILogger
    {
        string Name { get; }
    }

    class FileLogger : ILogger
    {
        public string Name
        {
            get { return "file"; }
        }
    }

    class MailLogger : ILogger
    {
        public string Name
        {
            get { return "mail"; }
        }
    }

    interface IUserService
    {
        IUserRepository UserRepository { get; }
        ILogger Logger { get; }
    }

    class UserServiceWithConstructorDependency : IUserService
    {
        public UserServiceWithConstructorDependency(IUserRepository repository)
        {
            throw new InvalidOperationException("This constructor should not be called by container.");
        }

        public UserServiceWithConstructorDependency(IUserRepository repository, ILogger logger)
        {
            UserRepository = repository;
            Logger = logger;
        }

        public IUserRepository UserRepository
        {
            get;
            private set;
        }

        public ILogger Logger
        {
            get;
            private set;
        }
    }

    class UserServiceWithConstructorAndPropertyDependencies : IUserService
    {
        [InjectConstructor]
        public UserServiceWithConstructorAndPropertyDependencies(IUserRepository repository)
        {
            UserRepository = repository;
        }

        public UserServiceWithConstructorAndPropertyDependencies(IUserRepository repository, ILogger logger)
        {
            throw new InvalidOperationException("This constructor should not be called by container.");
        }

        public IUserRepository UserRepository
        {
            get;
            private set;
        }

        [Inject]
        public ILogger Logger
        {
            get;
            private set;
        }
    }

    class UserServiceWithNamedDependencies : IUserService
    {
        [InjectConstructor]
        public UserServiceWithNamedDependencies([Inject("oracle")]IUserRepository repository)
        {
            UserRepository = repository;
        }

        public UserServiceWithNamedDependencies(IUserRepository repository, ILogger logger)
        {
            throw new InvalidOperationException("This constructor should not be called by container.");
        }

        public IUserRepository UserRepository
        {
            get;
            private set;
        }

        [Inject("mail")]
        public ILogger Logger
        {
            get;
            private set;
        }
    }

    class Loggable : ILoggable
    {
        private Venus.ILogger logger;

        public void SetLogger(Venus.ILogger logger)
        {
            this.logger = logger;
        }

        public Venus.ILogger Logger
        {
            get { return logger; }
        }
    }

    class Containable : IContainable
    {
        private IVenusContainer container;

        public void SetContainer(IVenusContainer container)
        {
            this.container = container;
        }

        public IVenusContainer Container
        {
            get { return container; }
        }
    }

    class Initializable : IInitializable
    {
        private bool initialized = false;

        public void Initialize()
        {
            initialized = true;
        }

        public bool Initialized
        {
            get { return initialized; }
        }
    }

    class Disposable : IDisposable
    {
        private bool disposabled = false;

        public void Dispose()
        {
            disposabled = true;
        }

        public bool Disposabled
        {
            get { return disposabled; }
        }
    }

    class FullInitialization : ILoggable,IContainable, IInitializable, IDisposable
    {
        private Venus.ILogger logger;
        private IVenusContainer container;
        private bool initialized = false;
        private bool disposabled = false;

        internal Venus.ILogger Logger
        {
            get { return logger; }
        }

        internal IVenusContainer Container
        {
            get { return container; }
        }

        internal bool Initialized
        {
            get { return initialized; }
        }

        internal bool Disposabled
        {
            get { return disposabled; }
        }

        public void SetLogger(Venus.ILogger logger)
        {
            Console.WriteLine("set logger");
            this.logger = logger;
        }

        public void SetContainer(IVenusContainer container)
        {
            Console.WriteLine("set container");
            this.container = container;
        }

        public void Initialize()
        {
            Console.WriteLine("initialize");
            initialized = true;
        }

        public void Dispose()
        {
            Console.WriteLine("dispose");
            disposabled = true;
        }
    }

    class CommonLogger : Venus.ILogger
    {
        #region members
        public bool IsInfoEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public bool IsIDebugEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public bool IsWarnEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public bool IsErrorEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public bool IsFatalEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public void Fatal(string message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message, Exception exception)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}