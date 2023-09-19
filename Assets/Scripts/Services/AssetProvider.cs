using UnityEngine;

public class AssetProvider : IAssetProvider
{
    public T GetObjectOfType<T>(string prefabPath) where T : MonoBehaviour
    {
        T seekingPrefab = Resources.Load<T>(prefabPath);

        if (seekingPrefab is null)
            throw new($"Requested type does not found {typeof(T)}");

        return seekingPrefab;
    }
}