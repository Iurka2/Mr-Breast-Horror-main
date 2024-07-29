using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject {
    public int levelNumber;
    public float goldTime;
    public float silverTime;
    public float bronzeTime;
    public int goldScore;
    public int silverScore;
    public int bronzeScore;
}
