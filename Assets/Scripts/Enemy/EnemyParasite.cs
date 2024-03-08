using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParasite : EnemyHuman
{

    private EnemyHuman GetNearestEnemy()
    {
        HumanMotor toReturn;
        try
        {
            toReturn = RuntimeData.enemyHumen[0];
        }
        catch (System.Exception)
        {
            return null;
        }

        foreach (var i in RuntimeData.enemyHumen)
        {
            if (Vector3.Distance(_thisTransform.position, toReturn._thisTransform.position) >
                Vector3.Distance(_thisTransform.position, i._thisTransform.position))
            {
                toReturn = i;
            }
        }

        return toReturn.enemyHuman;
    }
    protected override void Attack()
    {
        if (GetNearestEnemy() == null)
            return;

        if (Vector3.Distance(_thisTransform.position, GetNearestEnemy()._thisTransform.position) < 8)
        {
            if (Vector3.Distance(_thisTransform.position, GetNearestEnemy()._thisTransform.position) > 3)
            {
                _thisTransform.position = Vector3.MoveTowards(_thisTransform.position,
                    GetNearestEnemy()._thisTransform.position, _moveSpeed * Time.deltaTime);
            }
            else if (Vector3.Distance(_thisTransform.position, GetNearestEnemy()._thisTransform.position) < 2)
            {
                _thisTransform.position = Vector3.MoveTowards(_thisTransform.position,
                    GetNearestEnemy()._thisTransform.position, -_moveSpeed * Time.deltaTime);
            }
        }

    }

    private void ResetThisComponents()
    {
        base.ResetComponents();
    }
}
