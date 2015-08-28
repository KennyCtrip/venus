using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Venus.Tests.AutoRegistration
{
    interface IUserRepository
    {
        string Name { get; }
    }

    [Named]
    class SqlUserRepositoryAutoRegistered : IUserRepository
    {
        public string Name
        {
            get { return "sql"; }
        }
    }

    [Named(Name = "oracle")]
    class OracleUserRepositoryAutoRegistered : IUserRepository
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

    [Named]
    class FileLoggerAutoRegistered : ILogger
    {
        public string Name
        {
            get { return "file"; }
        }
    }

    [Named(Name = "mail")]
    class MailLoggerAutoRegisered : ILogger
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

    [Named]
    class UserServiceAutoRegistered : IUserService
    {
        [Inject]
        private IUserRepository userRepository;
        [Inject]
        private ILogger logger;

        public IUserRepository UserRepository
        {
            get { return userRepository; }
        }

        public ILogger Logger
        {
            get { return logger; }
        }
    }

    [Named(Name = "UserServiceWithName")]
    class UserServiceAutoRegisteredWithName : IUserService
    {
        [Inject("oracle")]
        private IUserRepository userRepository;
        [Inject("mail")]
        private ILogger logger;

        public IUserRepository UserRepository
        {
            get { return userRepository; }
        }

        public ILogger Logger
        {
            get { return logger; }
        }
    }

    [Named(Name = "UserServiceWithLifetime", LifetimeType = typeof(PerLookupLifetime))]
    class UserServiceAutoRegisteredWithLifetime : IUserService
    {
        private IUserRepository userRepository;
        private ILogger logger;

        public IUserRepository UserRepository
        {
            get { return userRepository; }
        }

        public ILogger Logger
        {
            get { return logger; }
        }
    }
}
