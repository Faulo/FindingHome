using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMechanism 
{
    Color MechanismColor { get; }
    void ActivateMechanism();
    void DeactivateMechanism();
}
