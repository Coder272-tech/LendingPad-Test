using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;

namespace Data.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static readonly ConcurrentDictionary<Guid, User> _store = new ConcurrentDictionary<Guid, User>();


        // Save or update
        public void Save(User entity)
        {
            if (entity.Id == Guid.Empty)
            {
                // generate a Guid for storage key
                var id = Guid.NewGuid();

                // we store it in the dictionary by id, but don't try to set entity.Id
                _store[id] = entity;
            }
            else
            {
                _store[entity.Id] = entity;
            }
        }


        // Delete by entity
        public void Delete(User entity)
        {
            if (entity != null)
                _store.TryRemove(entity.Id, out _);
        }

        // Get by Guid
        public User Get(Guid id)
        {
            _store.TryGetValue(id, out var entity);
            return entity;
        }

        // Filterable Get (userType, name, email)
        public IEnumerable<User> Get(UserTypes? userType = null, string name = null, string email = null, string tag = null)
        {
            var query = _store.Values.AsEnumerable();

            if (userType.HasValue)
                query = query.Where(u => u.Type == userType.Value);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(u => u.Name?.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);

            if (!string.IsNullOrEmpty(email))
                query = query.Where(u => u.Email?.Equals(email, StringComparison.OrdinalIgnoreCase) == true);

            if (!string.IsNullOrWhiteSpace(tag))
                query = query.Where(u => u.Tags != null && u.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase));

            return query;
        }

        // Get all users
        public IEnumerable<User> GetAll()
        {
            return _store.Values;
        }

        // Clear all users
        public void Clear()
        {
            _store.Clear();
        }

        // DeleteAll (matches IUserRepository signature)
        public void DeleteAll()
        {
            _store.Clear();
        }
    }
}
