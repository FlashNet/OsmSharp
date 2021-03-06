﻿// OsmSharp - OpenStreetMap (OSM) SDK
// Copyright (C) 2013 Abelshausen Ben
// 
// This file is part of OsmSharp.
// 
// OsmSharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// OsmSharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with OsmSharp. If not, see <http://www.gnu.org/licenses/>.

#if !WINDOWS_PHONE
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OsmSharp.Collections.Cache
{
    /// <summary>
    /// Generic LRU cache implementation.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class LRUCache<TKey, TValue>
    {
        private ConcurrentDictionary<TKey, CacheEntry> _data;

        private long _id;

        private long _last_id;

        /// <summary>
        /// Initializes this cache.
        /// </summary>
        /// <param name="capacity"></param>
        public LRUCache(int capacity)
        {
            _id = long.MinValue;
            _last_id = _id;
            _data = new ConcurrentDictionary<TKey, CacheEntry>();

            this.Capacity = capacity;
        }

        /// <summary>
        /// Capacity.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Adds a new value for the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            CacheEntry entry = new CacheEntry
            {
                Id = _id,
                Value = value
            };
            _id++;
            _data[key] = entry;

            this.ResizeCache();
        }

        /// <summary>
        /// Returns the value for this given key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGet(TKey key, out TValue value)
        {
            CacheEntry entry;
            _id++;
            if (_data.TryGetValue(key, out entry))
            {
                entry.Id = _id;
                value = entry.Value;
                return true;
            }
            value = default(TValue);
            return false;
        }

        /// <summary>
        /// Returns the value for this given key but does not effect the cache.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryPeek(TKey key, out TValue value)
        {
            CacheEntry entry;
            if (_data.TryGetValue(key, out entry))
            {
                value = entry.Value;
                return true;
            }
            value = default(TValue);
            return false;
        }

        /// <summary>
        /// Clears this cache.
        /// </summary>
        public void Clear()
        {
            _data.Clear();
            _last_id = _id;
            _id = long.MinValue;
        }

        /// <summary>
        /// Removes the value for the given key.
        /// </summary>
        /// <param name="id"></param>
        public void Remove(TKey id)
        {
            CacheEntry entry;
            _data.TryRemove(id, out entry);
        }

        /// <summary>
        /// Resizes the cache.
        /// </summary>
        private void ResizeCache()
        {
            while (_data.Count > this.Capacity)
            { // oeps: too much data.
                // remove the 'oldest' item.
                TKey minKey = default(TKey);
                long minId = long.MaxValue;
                foreach (KeyValuePair<TKey, CacheEntry> pair in _data)
                {
                    if (pair.Value.Id < minId)
                    {
                        minId = pair.Value.Id;
                        minKey = pair.Key;
                    }
                }
                CacheEntry entry;
                _data.TryRemove(minKey, out entry);

                // update the 'last_id'
                _last_id++;

                //foreach (KeyValuePair<TKey, CacheEntry> pair in _data)
                //{
                //    if (pair.Value.Id < id)
                //    {
                //        keys.Add(pair.Key);
                //    }
                //}

                //int new_capacity = (this.Capacity * 75) / 100; // keep 75%

                //// loop over and remove all with smallest id's.
                //long id = _id - new_capacity;
                //List<TKey> keys = new List<TKey>(this.Capacity - new_capacity);
                //foreach (KeyValuePair<TKey, CacheEntry> pair in _data)
                //{
                //    if (pair.Value.Id < id)
                //    {
                //        keys.Add(pair.Key);
                //    }
                //}
                //CacheEntry entry;
                //foreach (TKey key in keys)
                //{
                //    _data.TryRemove(key, out entry);
                //}

                //_last_id = id;
            }
        }

        /// <summary>
        /// An entry in this cache.
        /// </summary>
        private class CacheEntry
        {
            /// <summary>
            /// The id of the object.
            /// </summary>
            public long Id { get; set; }

            /// <summary>
            /// The object being cached.
            /// </summary>
            public TValue Value { get; set; }
        }
    }
}
#endif