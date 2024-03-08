using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHuman : HumanMotor
{
    [HideInInspector]
    public bool isThereWasParasite;

    private float _timeToWaitNextWalk;
    private bool _sideToWalk; // false - left; true - right
    private bool _isWalking;
    private bool _isTimerCanStartAgain;

    private Vector3 _enemyPositionAfterWalk;
    private float posXtoWalk;

    protected float _slowWalkSpeed;

    private void Start()
    {
        isThereWasParasite = false;
        _isTimerCanStartAgain = true;

        _slowWalkSpeed = 4f;
    }
    override protected void Movement() // enemy AI movement
    {
        if (_isTimerCanStartAgain)
        {
            _timeToWaitNextWalk = Random.Range(2, 3.5f);
            StartCoroutine(WaitNextWalk(_timeToWaitNextWalk));
            _isWalking = false;
            _isTimerCanStartAgain = false;
        }

        if (_isWalking)
        {
            _thisTransform.position = Vector3.MoveTowards(
                _thisTransform.position,
                new Vector3(posXtoWalk, _thisTransform.position.y, _thisTransform.position.z),
                _slowWalkSpeed * Time.deltaTime);

            if (Vector3.Distance(_thisTransform.position, _enemyPositionAfterWalk) <= 0.001f)
            {
                _isTimerCanStartAgain = true;
            }
        }
    }
    private IEnumerator WaitNextWalk(float timeToWait=0)
    {
        yield return new WaitForSeconds(timeToWait);
        posXtoWalk = _thisTransform.position.x;
        if (_sideToWalk)
            posXtoWalk += 2;
        else
            posXtoWalk -= 2;
        _sideToWalk = !_sideToWalk;

        _enemyPositionAfterWalk = new Vector3(posXtoWalk, _thisTransform.position.y, _thisTransform.position.z);

        _isWalking = true;
    }
    public void ResetComponents()
    {
        _enemyPositionAfterWalk = new Vector3();
        _isTimerCanStartAgain = true;
        StopCoroutine(WaitNextWalk());
    }

    protected override void Attack()
    {
        
    }

    private void OnEnable()
    {
        ResetComponents();
    }
}
