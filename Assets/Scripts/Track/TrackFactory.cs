using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrackFactory
{
    private Vector3 _currentSpawnPosition;
    private readonly List<Track> _trackPrefabs;
    private readonly BlockFactory _blockFactory;

    public TrackFactory(Track[] trackPrefabs, Vector3 currentSpawnPosition, BlockFactory blockFactory)
    {
        _trackPrefabs = new(trackPrefabs);
        _currentSpawnPosition = currentSpawnPosition;

        _blockFactory = blockFactory;
    }

    public void SpawnNextTrackSegment(bool playAnimation = false)
    {
        Track randomTrackToSpawn = _trackPrefabs[Random.Range(0, _trackPrefabs.Count)];
        Track newTrack = Object.Instantiate(randomTrackToSpawn, _currentSpawnPosition, quaternion.identity);
        
        float blockLength = newTrack.TrackGround.transform.localScale.z;
        Vector3 offset = new(0, 0, blockLength);
        _currentSpawnPosition += offset;

        newTrack.Init();
        newTrack.OnTrackCompleted += SpawnNextTrackSegment;

        foreach (BlockPickup blockPickup in newTrack.Pickups)
            blockPickup.OnPickupEvent += _blockFactory.SpawnBlock;
        
        if (playAnimation)
            newTrack.StartCoroutine(newTrack.SpawnAnimation());
    }
}