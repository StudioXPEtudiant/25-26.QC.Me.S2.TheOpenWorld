using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Transform body;
    public float attackDistance = 0.5f;
    public float attackSpeed = 10f;
    public KeyCode attackKey = KeyCode.E;

    Vector3 originalLocalPos;
    bool isAttacking = false;

    void Start()
    {
        originalLocalPos = transform.localPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(attackKey) && !isAttacking)
            StartCoroutine(Attack());
    }

    System.Collections.IEnumerator Attack()
    {
        isAttacking = true;

        Vector3 dir = body.forward;
        Vector3 targetPos = originalLocalPos + dir.normalized * attackDistance;

        while (Vector3.Distance(transform.localPosition, targetPos) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * attackSpeed);
            yield return null;
        }

        while (Vector3.Distance(transform.localPosition, originalLocalPos) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalLocalPos, Time.deltaTime * attackSpeed);
            yield return null;
        }

        isAttacking = false;
    }
}
