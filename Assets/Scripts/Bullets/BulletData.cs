using UnityEngine;

namespace Assets.Scripts.Bullets
{
    [CreateAssetMenu(fileName = "New Bullet Data", menuName = "Create Bullet Data")]
    public class BulletData : ScriptableObject
    {
        public int[] InitialDamage;
        public int[] Speed;
        public int[] Range;
        public int[] SplashRange;
        public int[] SplashDamage;

        public int Id;

    }
}
