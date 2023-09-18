using System.Collections.Generic;
using UnityEngine;

public class GameInstanceFactory
{
    protected readonly List<MonoBehaviour> DefaultPrefabsList;
    private readonly Transform _parent;

    protected GameInstanceFactory(Transform instancesParent)
    {
        _parent = instancesParent;

        DefaultPrefabsList = new();
    }

    protected T CreateInstanceByObject<T>(T prefabToSpawn) where T : MonoBehaviour
    {
        T seekingPrefab = DefaultPrefabsList.Find(b => b == prefabToSpawn) as T;

        return Object.Instantiate(seekingPrefab, _parent);
    } 
    
    protected T CreateInstanceByType<T>() where T : MonoBehaviour
    {
        T seekingPrefab = DefaultPrefabsList.Find(b => b is T) as T;

        return Object.Instantiate(seekingPrefab, _parent);
    } 
}