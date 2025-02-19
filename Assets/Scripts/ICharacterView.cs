using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public interface ICharacterView
{
    void SetPath(List<Vector3> path, CancellationToken token);
}