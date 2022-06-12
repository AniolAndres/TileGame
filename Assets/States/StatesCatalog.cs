using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "States Catalog", menuName = "ScriptableObjects/Create States Catalog", order = 1)]
public class StatesCatalog : ScriptableObject
{
    [SerializeField]
    private List<StateCatalogEntry> stateEntries;

    public StateCatalogEntry GetCatalogEntry(string id) {
        foreach(var entry in stateEntries) {
            if(entry.Id == id) {
                return entry;
            }
        }

        throw new ArgumentOutOfRangeException($"Could not find any states with id {id}");
    }
}
