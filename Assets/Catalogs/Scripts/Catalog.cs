using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEditor;

namespace Assets.Catalogs {
    public class Catalog<T> : ScriptableObject  where T : CatalogEntry {

        [SerializeField]
        private List<T> entries;

        [NonSerialized]
        private readonly Dictionary<string, T> cacheEntries = new  Dictionary<string, T>();

        public T GetEntry(string id) {

            if (cacheEntries.TryGetValue(id, out T cachedEntry))
            {
                return cachedEntry;
            }
            
            foreach(T entry in entries) {
                if(entry.Id == id)
                {
                    cacheEntries[id] = entry;
                    return entry;
                }
            }

            throw new NotSupportedException($"Couldn't find any entry with id: {id}");
        }

        public List<T> GetAllEntries() {
            return entries.ToList();
        }

#if UNITY_EDITOR

        public void AddEntry(T entry) {
            var exists = entries.FirstOrDefault(x => x.Id == entry.Id) != null;
            if (exists) {
                throw new NotSupportedException("Trying to add a new state with id {entry.Id} but it already exists in catalog!");
            }

            entries.Add(entry);
        }

#endif
    }
}