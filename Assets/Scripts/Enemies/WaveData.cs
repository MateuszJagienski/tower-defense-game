using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Data", menuName = "Wave Data")]
public class WaveData : ScriptableObject
{
    [SerializeField]
    public List<PartWave> PartWave;
}
