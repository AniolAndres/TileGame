using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Assets.Catalogs.Scripts {
    public class Catalog<T> : ScriptableObject  where T : CatalogEntry {

        [SerializeField]
        private List<T> entries;

        public T GetEntry(string id) {
            foreach(var entry in entries) {
                if(entry.Id == id) {
                    return entry;
                }
            }

            throw new NotSupportedException($"Couldn't find any entry with id: {id}");
        }

        public List<T> GetAllEntries() {
            return entries.ToList();
        }
    }
}