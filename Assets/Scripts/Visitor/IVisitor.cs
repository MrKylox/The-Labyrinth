using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisitor
{
    void Visit(PlayerShield playerShield);
    void Visit(PlayerBoost playerBoost);
    void Visit(PlayerCoin playerCoin);
    void Visit(PlayerHealth playerHealth);
}
