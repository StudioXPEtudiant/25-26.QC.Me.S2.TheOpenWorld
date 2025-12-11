using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float attackDistance = 0.5f;
    public float attackSpeed = 10f;
    public KeyCode attackKey = KeyCode.E;

    Vector3 originalPosition;
    bool isAttacking = false;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(attackKey) && !isAttacking)
            StartCoroutine(Attack());
    }

    System.Collections.IEnumerator Attack()
    {
        isAttacking = true;

        Vector3 targetPos = originalPosition + transform.forward * attackDistance;
        while (Vector3.Distance(transform.localPosition, targetPos) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * attackSpeed);
            yield return null;
        }

        while (Vector3.Distance(transform.localPosition, originalPosition) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * attackSpeed);
            yield return null;
        }

        isAttacking = false;
    }
}
