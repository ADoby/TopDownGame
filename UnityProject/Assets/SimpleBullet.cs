﻿using UnityEngine;
using System.Collections;

public class SimpleBullet : MonoBehaviour 
{

    public static bool IsInLayerMask(GameObject obj, LayerMask mask){
        return ((mask.value & (1 << obj.layer)) > 0);
    }

    public float Speed = 5f;

    public float LifeTime = 4f;
    private float LifeTimer = 0f;

    public LayerMask mask;

    private string poolName = "";

    public void Reset()
    {
        LifeTimer = 0f;
    }

    public void SetPoolName(string newPoolName)
    {
        poolName = newPoolName;
    }

    void Update()
    {
        LifeTimer += Time.deltaTime;
        if (LifeTimer >= LifeTime)
        {
            Explode();
        }
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (IsInLayerMask(coll.gameObject, mask))
        {
            SimpleAI ai = coll.gameObject.GetComponent<SimpleAI>();
            if (ai == null)
                return;

            ai.Hit();
        }
        Explode();
    }

    void Explode()
    {

        GameObjectPool.Instance.Despawn(poolName, gameObject);
    }
}
