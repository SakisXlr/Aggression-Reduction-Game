using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[RequireComponent(typeof(LineRenderer))]
public class Gun : MonoBehaviour
{
    public GameObject laserStart;
    public GameObject laserEnd;
    public float fireRate = 0.2f;
    public float laserDuration = 0.05f;
 
    LineRenderer laserLine;
    float fireTimer;
 
    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
    }
 
    void Update()
    {
        fireTimer += Time.deltaTime;
        if(fireTimer > fireRate)
        {
            fireTimer = 0;
            StartCoroutine(ShootLaser());
        }
    }
 
    IEnumerator ShootLaser()
    {
        laserLine.SetPosition(0, laserStart.transform.position);
        laserLine.SetPosition(1, laserEnd.transform.position);
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}