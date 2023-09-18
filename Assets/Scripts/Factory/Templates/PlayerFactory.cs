using UnityEngine;

public class PlayerFactory : GameInstanceFactory
{
    private const string PlayerPrefabPath = "Prefabs/Player";

    public PlayerFactory(Transform parent) : base(parent)
    {
        PlayerBlock blockPrefab = Resources.Load<PlayerBlock>(PlayerPrefabPath);
        DefaultPrefabsList.Add(blockPrefab);
    }

    public PlayerBehaviour CreateInstance()
    {
        PlayerBehaviour newPlayer = CreateInstanceByType<PlayerBehaviour>();
        newPlayer.Init();

        return newPlayer;
    }
}