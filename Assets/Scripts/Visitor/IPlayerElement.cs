using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerElement
{
    void Accept(IVisitor visitor);
}
