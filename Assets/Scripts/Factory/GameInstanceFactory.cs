using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameInstanceFactory
{
    protected readonly List<MonoBehaviour> DefaultPrefabsList;
    private readonly IObjectResolver _objectResolver;

    protected GameInstanceFactory(IObjectResolver objectResolver)
    {
        _objectResolver = objectResolver;

        DefaultPrefabsList = new();
    }

    protected T CreateInstanceByObject<T>(T prefabToSpawn, Transform parent = null) where T : MonoBehaviour
    {
        T seekingPrefab = DefaultPrefabsList.Find(b => b == prefabToSpawn) as T;

        return _objectResolver.Instantiate(seekingPrefab, parent);
    } 
    
    protected T CreateInstanceByType<T>(Transform parent = null) where T : MonoBehaviour
    {
        T seekingPrefab = DefaultPrefabsList.Find(b => b is T) as T;

        return _objectResolver.Instantiate(seekingPrefab, parent);
    } 
}