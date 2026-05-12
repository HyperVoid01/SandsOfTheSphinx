using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "ScriptableObjects/Player Data")]
public class PlayerData : ScriptableObject
{
    public int highScore = 0;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;
}
