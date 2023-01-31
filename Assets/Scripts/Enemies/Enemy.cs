using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    private int hp;
    private Dictionary<int, Enemy> enemies;
    public int Hp { get => enemyData.Hp; }
    public int ID { get => enemyData.ID; }
    public float Speed { get => enemyData.Speed; }

    [SerializeField]
    public EnemyData enemyData;
    public int CurrentWaypointIndex { get; set; }
    void Start()
    {
        enemies = FindObjectOfType<EnemyPrefabs>().GetEnemyPrefabs();
        hp = Hp;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (ID == 1) Kill();
        else ChangePrefab();
    }

    public void ChangePrefab()
    {
        var newPrefab = enemies[ID - 1];
        if (newPrefab.Hp >= hp)
        {
            newPrefab = Instantiate(newPrefab, transform.position, Quaternion.identity);
            newPrefab.CurrentWaypointIndex = CurrentWaypointIndex;
            DeactivateEnemy();
        }

    }

    public void DeactivateEnemy()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 5);
    }

    public void Kill()
    {
        EconomySystem.Instance.IncreaseGold(1);
        DeactivateEnemy();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BulletController>() != null)
        {
            Debug.Log("trigger enter");
            BulletController bulletController = other.gameObject.GetComponent<BulletController>();
            bulletController.EnemyHit();
            Bullet bullet = other.gameObject.GetComponent<Bullet>();

            var bulletTakenDamge = Math.Min(bullet.Damage, hp);
            TakeDamage(bullet.Damage);
            bullet.TakeDamage(bulletTakenDamge);
        }
    }
}