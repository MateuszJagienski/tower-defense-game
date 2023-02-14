using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int hp;
    private Dictionary<int, Enemy> enemies;
    [SerializeField]
    private List<Enemy> enemiesModels;
    private int currentActiveEnemyID;
    public Enemy CurrentActiveEnemy { get => enemiesModels[currentActiveEnemyID]; }
    private Enemy currentActiveModel;
    public int CurrentWaypointIndex { get; set; }
    void Start()
    {
        enemies = FindObjectOfType<EnemyPrefabs>().GetEnemyPrefabs();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (currentActiveEnemyID == 0) Kill();
        else ActivateEnemyById(--currentActiveEnemyID);
    }
    
    public void ChangePrefab()
    {
        var newPrefab = enemies[currentActiveEnemyID - 1];
        if (newPrefab.Hp >= hp)
        {
            newPrefab = Instantiate(newPrefab, transform.position, Quaternion.identity);
            //  newPrefab.transform.parent = transform.parent;
         //   newPrefab.CurrentWaypointIndex = CurrentWaypointIndex;
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

    public EnemyController ActivateEnemyById(int id)
    {
        currentActiveEnemyID = id;
        if (currentActiveModel != null)
        {
            Destroy(currentActiveModel.gameObject);
        }
        currentActiveModel = Instantiate(enemiesModels[currentActiveEnemyID], transform.position, Quaternion.identity);
        currentActiveModel.gameObject.SetActive(true);
        currentActiveModel.transform.parent = transform;

/*        foreach (Enemy enemy in enemiesModels)
        {
            if (enemy.ID == currentActiveEnemyID)
            {
                enemy.gameObject.SetActive(true);
            }
            else
            {
                enemy.gameObject.SetActive(false);
            }
        }*/
        //hp = enemies[currentActiveEnemyID].Hp;
        return this;
    }

    [ContextMenu("Autofill models")]
    void AutofillModels()
    {
        // 
        enemiesModels = Resources.LoadAll("Models/Enemies", typeof(Enemy))
            .Cast<Enemy>()
            .OrderBy(x => x.ID)
            .ToList();
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