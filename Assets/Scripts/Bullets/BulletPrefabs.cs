using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Bullets
{
    public class BulletPrefabs
    {
        private static readonly Dictionary<BulletType, Bullet> BulletModels;

        static BulletPrefabs() =>
            BulletModels = Resources.LoadAll("Models/Bullets", typeof(Bullet))
                .Cast<Bullet>()
                .ToDictionary(b => b.BulletType, b => b);
        
        public static Bullet GetBulletByType(BulletType bulletType) => BulletModels[bulletType];
    }
}