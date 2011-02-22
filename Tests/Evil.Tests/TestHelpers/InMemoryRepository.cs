using System;
using System.Collections.Generic;
using System.Linq;
using Evil.Common;

namespace Evil.Tests.TestHelpers
{
    public class InMemoryRepository<T> : IRepository<T> where T : Entity
    {

        private readonly List<T> _items;
        private List<T> _savedEntities;

        public InMemoryRepository(IEnumerable<T> items)
            : this()
        {
            Add(items);
        }

        public InMemoryRepository(params T[] items)
            : this()
        {
            Add(items);
        }

        public InMemoryRepository()
        {
            _items = new List<T>();
            ClearSavedEntities();
        }

        public T GetById(int id)
        {
            return Get.FirstOrDefault(m => m.Id == id);
        }

        public IQueryable<T> Get
        {
            get { return _items.AsQueryable(); }
        }

        public void Save(T entity)
        {
            var savedEntity = GetById(entity.Id);
            if (savedEntity != null)
            {
                _items[_items.IndexOf(savedEntity)] = entity;
            }
            else
            {
                _items.Add(entity);
            }
            _savedEntities.Add(entity);
        }

        public void Delete(T entity)
        {
            if(_items.Contains(entity))
                _items.Remove(entity);
        }

        public void Add(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public void Add(params T[] items)
        {
            _items.AddRange(items);
        }

        public IEnumerable<T> GetSavedEntities()
        {
            return _savedEntities;
        }

        public void ClearSavedEntities()
        {
            _savedEntities = new List<T>();
        }
    }
}
