using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Di
{
    public class DiContainer : IDiContainer
    {
        private Dictionary<Type, object> _container = new Dictionary<Type, object>();

        public Type[] Container => _container.Keys.ToArray();
        
        public void Bind<T>(T obj)
        {
            if (obj == null) return;
            _container[typeof(T)] = obj;
        }

        public T Resolve<T>()
        {
            return (T)_container[typeof(T)];
        }

        public object Resolve(Type type)
        {
            return _container[type];
        }

        public void InjectTo(object obj)
        {
            MethodInfo method = GetInjectMethod(obj);
            if (method == null) return;
            var types = method.GetParameters().Select(x => x.ParameterType).ToArray();
            var objects = new object[types.Count()];
            for (int i = 0; i < objects.Count(); i++)
                objects[i] = Resolve(types[i]);
            method.Invoke(obj, objects);
        }

        private static MethodInfo GetInjectMethod(object obj) =>
            obj.GetType()
                .GetMethods()
                .SingleOrDefault(x => x.GetCustomAttributes(typeof(InjectAttribute), true).Any());
    }

    public interface IDiContainer
    {
        public void Bind<T>(T obj);
        public T Resolve<T>();
        public void InjectTo(object obj);
    }
}