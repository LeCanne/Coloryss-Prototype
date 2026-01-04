using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(HorizontalLayoutGroup))]
public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyObject;
    private void Awake()
    {
        BattleHandler.Instance.battleStartInfo += SpawnEnemies;
    }

    void SpawnEnemies(BattleData battleData)
    {
        foreach(EntityData nData in battleData.enemyList)
        {
            GameObject newEnemyObject = Instantiate(enemyObject.gameObject, transform);
            Enemy newEnemy = newEnemyObject.GetComponent<Enemy>();

            newEnemy.InitializeEnemy(nData);
            
            BattleHandler.Instance.AddEnemy(newEnemy);
        }
    }
}
