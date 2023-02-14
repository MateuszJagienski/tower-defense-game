using UnityEngine;

public class Bullet : MonoBehaviour, IDamageable
{
    public int InitialDamage { get => bulletData.InitialDamage[BulletLvl]; }
    public int SplashDamage { get => bulletData.SplashDamage[BulletLvl]; }
    public int SplashRange { get => bulletData.SplashRange[BulletLvl]; }
    public int Speed { get => bulletData.Speed[BulletLvl]; }
    public int Range { get => bulletData.Range[BulletLvl]; }
    public int ID { get => bulletData.ID; }
    private int damage;
    public int Damage { get => damage; }
    [SerializeField]
    private BulletData bulletData;
    public static int BulletLvl = 0;
    void Start()
    {
        damage = InitialDamage;
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("bullet dmg " + this.damage);
        Debug.Log(" dmg " + damage);
        if (this.damage > damage)
        {
            this.damage -= damage;
        }
        else
        {
            DeactivateBullet();
        }
    }
    public void DeactivateBullet()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}