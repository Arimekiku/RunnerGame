using UnityEngine;

[CreateAssetMenu(menuName = "SO/Level Info", fileName = "LevelInfo")]
public class LevelInfo : ScriptableObject
{
    [SerializeField] private int LevelTrackSegmentCount;

    public int TrackSegmentCount => LevelTrackSegmentCount;
}