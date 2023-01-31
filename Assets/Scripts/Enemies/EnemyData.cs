using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Create Enemy Data")]
public class EnemyData : ScriptableObject
{
    public int Hp;
    public int Speed;
    public int ID;
}
/*
Red - sp 5 // rbe 1
Blue - sp 7 // rbe 2
Green - sp 9 // rbe 3
Yellow - sp 16 // rbe 4
Black - sp 9 // rbe 9 // imune to explosion // split into 2 yellow
White - sp 10 // rbe 9 // imune to ice // split into 2 yellow
*/