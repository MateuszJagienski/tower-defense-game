using System.Collections.Generic;
using Assets.Scripts.Enemies;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Data", menuName = "Wave Data")]
public class WaveData : ScriptableObject
{
    public List<PartWave> PartWave;
}
