using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void OnStateEnter();
    void OnStateUpdate();
    void OnStateExit();

}
