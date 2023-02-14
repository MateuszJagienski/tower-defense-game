using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Hp { get => enemyData.Hp; }
    public int ID { get => enemyData.ID; }
    public float Speed { get => enemyData.Speed; }

    public EnemyData enemyData;
}