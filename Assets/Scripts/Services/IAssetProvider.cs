using UnityEngine;

public interface IAssetProvider
{
    public T GetObjectOfType<T>(string prefabPath) where T : MonoBehaviour;
}