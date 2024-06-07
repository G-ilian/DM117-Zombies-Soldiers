using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeSpan;

    private void Start()
    {
        Invoke(nameof(selfDestroy), lifeSpan);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void selfDestroy()
    {
        Destroy(this.gameObject);
    }
}
