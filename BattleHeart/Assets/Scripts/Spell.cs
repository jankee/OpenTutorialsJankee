using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private Rigidbody Myrigidbody;

    [SerializeField]
    private float speed;

    private Transform target;

    // Use this for initialization
    private void Start()
    {
        Myrigidbody = this.GetComponent<Rigidbody>();

        target = InputManager.Instance.MyMovePlayer.MyTarget.transform;

        StartCoroutine(CastingSpell());
    }

    // Update is called once per frame
    private IEnumerator CastingSpell()
    {
        Vector3 direction = target.position - this.transform.position;

        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(90f, 0f, angle);

        float distance = Vector3.Distance(transform.position, target.position);

        print("TatgetName : " + target.name + " : " + distance);

        while (distance > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            distance = Vector3.Distance(transform.position, target.position);

            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;

        Destroy(gameObject);
    }
}