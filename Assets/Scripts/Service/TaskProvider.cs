using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class TaskProvider
    {
        private readonly Dictionary<Type, object> _tasks = new();

        public void AddTask<T>(params Task<T>[] tasks)
        {
            foreach (var task in tasks)
            {
                if(_tasks.ContainsKey(typeof(T))) return;
                _tasks.Add(typeof(T), task);
            }
        }

        public Task<T> GetTask<T>() => 
            _tasks.TryGetValue(typeof(T), out object result) ? (Task<T>) result : null;
    }
}