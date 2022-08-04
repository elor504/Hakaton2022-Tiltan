using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]ParticleSystem tap;
    Vector2 fingerPos;



    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            TapEffect();
        }
    }





    #region VFX
    void TapEffect()
    {
        fingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tap.transform.position = fingerPos;
        tap.Play();
        StartCoroutine(StopEffect());
    }
    IEnumerator StopEffect()
    {
        yield return new WaitForSeconds(0.1f);
        tap.Stop();
    }
    #endregion
}
