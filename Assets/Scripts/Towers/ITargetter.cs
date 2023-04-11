using UnityEngine;

namespace Assets.Scripts.Towers
{
    public interface ITargetter
    {
        GameObject FindTarget(AttackType attackType);
        bool HasActiveTarget();
        void SetRange(float radius);
    }
}