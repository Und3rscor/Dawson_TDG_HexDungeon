using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private GridScript gridScript;

    private void Start()
    {
        GridScript grid = new GridScript(4, 2, 1f, Vector3.zero);
    }
}
