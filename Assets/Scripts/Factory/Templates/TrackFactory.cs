using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

public class TrackFactory : GameInstanceFactory
{
    private const string FirstTrackPrefabPath = "Prefabs/Tracks/Track01";
    private const string SecondTrackPrefabPath = "Prefabs/Tracks/Track02";
    private const string ThirdTrackPrefabPath = "Prefabs/Tracks/Track03";
    private const string FourthTrackPrefabPath = "Prefabs/Tracks/Track04";
    private const string FifthTrackPrefabPath = "Prefabs/Tracks/Track05";
    
    private Vector3 _currentSpawnPosition;

    public TrackFactory(IObjectResolver objectResolver) : base(objectResolver)
    {
        Track firstTrackPrefab = Resources.Load<Track>(FirstTrackPrefabPath);
        DefaultPrefabsList.Add(firstTrackPrefab);
        
        Track secondTrackPrefab = Resources.Load<Track>(SecondTrackPrefabPath);
        DefaultPrefabsList.Add(secondTrackPrefab);
        
        Track thirdTrackPrefab = Resources.Load<Track>(ThirdTrackPrefabPath);
        DefaultPrefabsList.Add(thirdTrackPrefab);
        
        Track fourthTrackPrefab = Resources.Load<Track>(FourthTrackPrefabPath);
        DefaultPrefabsList.Add(fourthTrackPrefab);
        
        Track fifthTrackPrefab = Resources.Load<Track>(FifthTrackPrefabPath);
        DefaultPrefabsList.Add(fifthTrackPrefab);

        _currentSpawnPosition = Vector3.zero;
    }

    public void CreateInstance(Transform parent)
    {
        Track randomTrackToSpawn = DefaultPrefabsList[Random.Range(0, DefaultPrefabsList.Count)] as Track;
        Track newTrack = CreateInstanceByObject(randomTrackToSpawn, parent);
        newTrack.transform.position = _currentSpawnPosition;
        
        float blockLength = newTrack.Ground.transform.localScale.z;
        Vector3 offset = new(0, 0, blockLength);
        _currentSpawnPosition += offset;

        newTrack.OnTrackCompleted += CreateInstance;
    }
}