using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelList", menuName = "Game/LevelList")]
public class LevelList : ScriptableObject
{
    public List<LevelData> levels;
}