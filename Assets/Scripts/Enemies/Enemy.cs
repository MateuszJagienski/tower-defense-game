using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public int Hp => EnemyData.Hp;
        public EnemyModelType EnemyModelType => EnemyData.EnemyModelType;
        public float Speed => EnemyData.Speed;
        public EnemyModelType NextEnemyModelType => EnemyData.EnemyModelType;
        public int NextQuantity => EnemyData.NextQuantity;
        public EnemyType EnemyType = EnemyType.BASIC; // todo()

        public EnemyData EnemyData;
    }
}