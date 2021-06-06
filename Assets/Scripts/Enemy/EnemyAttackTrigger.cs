using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    private EnemyAI _ai;

    private void Start()
    {
        _ai = GetComponentInParent<EnemyAI>();
        if(_ai == null)
        {
            Debug.LogError("enemy child object cannot get the enemy AI script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _ai.StartAttack();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _ai.StopAttack();
        }
    }

    //_isAttacking = true;
    //_isAttacking = false;
}
