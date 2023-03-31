using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Infrastructure.Pool
{
    public class ObjectPool<T> : IDisposable, IObjectPool<T> where T : class
    {
        private const uint DefaultMaxSize = 1000;
        private const uint DefaultCapacity = 10;

        private uint _maxSize;
        private List<T> _list;

        public ObjectPool(Action<T> actionOnDestroyed = null,
           Action<T> actionOnGot = null,
           Action<T> actionOnReleased = null,
           uint defaultCapacity = DefaultCapacity,
           uint maxSize = DefaultMaxSize)
        {
            SetList(defaultCapacity);

            Destroyed = actionOnDestroyed;
            Got = actionOnGot;
            Released = actionOnReleased;
            MaxSize = maxSize;
        }

        private event Action<T> Destroyed;
        private event Action<T> Got;
        private event Action<T> Released;

        public int CountInactive => _list.Count;

        public uint MaxSize
        {
            get => _maxSize;
            set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException(nameof(_maxSize));

                _maxSize = value;
            }
        }

        public void Dispose()
        {
            if (Destroyed != null)
                foreach (T entity in _list)
                    Destroyed(entity);

            _list.Clear();
        }

        public virtual T Get()
        {
            if (_list.Count == 0)
                return null;

            T entity = _list[_list.Count - 1];

            _list.Remove(entity);

            Got?.Invoke(entity);

            return entity;
        }

        protected void SetList(uint capacity)
        {
            Assert.IsNull(_list);
            _list = new List<T>((int)capacity);
        }

        public void Release(T entity)
        {
            Released?.Invoke(entity);

            if (_list.Count < _maxSize)
                _list.Add(entity);
            else
                Destroyed?.Invoke(entity);
        }
    }
}