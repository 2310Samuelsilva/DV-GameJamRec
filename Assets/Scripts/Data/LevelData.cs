using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName;      // Name or title
    public int levelNumber;      // Name or title
    public string sceneName;      // Scene file name
    public Sprite previewImage; 
    public bool isUnlocked = false;
}