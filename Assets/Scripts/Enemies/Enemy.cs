using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public int Hp => EnemyData.Hp;
        public int Id => EnemyData.Id;
        public float Speed => EnemyData.Speed;
        public int NextId => EnemyData.NextId;
        public int NextQuantity => EnemyData.NextQuantity;
        public EnemyType EnemyType = EnemyType.BASIC; // todo()

        public EnemyData EnemyData;
    }
}