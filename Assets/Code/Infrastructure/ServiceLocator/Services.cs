using System;
using System.Collections.Generic;

namespace Code.Infrastructure.ServiceLocator
{
    public static class Services
    {   
        private static Dictionary<Type, IService> _services = new();
        
        public static void Register<T>(T service) where T : IService =>
            _services[typeof(T)] = service;

        public static T GetService<T>() where T : IService =>
            (T)_services[typeof(T)];
    }
}