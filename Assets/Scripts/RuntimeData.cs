using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeData : MonoBehaviour
{
    public static List<EnemyHuman> enemyHumen = new List<EnemyHuman>();
    public static List<EnemyParasite> enemyParasites = new List<EnemyParasite>();

    private void Start()
    {
        foreach (var i in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemyHumen.Add(i.GetComponent<EnemyHuman>());
        }
    }
}
