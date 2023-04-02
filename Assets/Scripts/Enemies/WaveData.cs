using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    [CreateAssetMenu(fileName = "New Wave Data", menuName = "Wave Data")]
    public class WaveData : ScriptableObject
    {
        public List<PartWave> PartWave;
    }
}
