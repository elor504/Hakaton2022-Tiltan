using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] List<Transform> _path = new List<Transform>();
    

    public Transform GetRandomPath()
	{
        int randomIndex = Random.Range(0, _path.Count);
        return _path[randomIndex];

    }

}
