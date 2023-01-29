using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBehavior : MonoBehaviour
{
    public GameObject currentTarget;
    public int id;
    public float speed = 2.0f;
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float damageMultiplier = 1.0f;
    public float attackSpeed = 1.0f;
    public float attackRange = 1.0f;
    public float rotationSpeed = 1.0f;
    public float hpRegen = 0.5f;
    public float damageReduction = 1.0f;
    public int team = 0;
    public Weapon weapon;
    public float attackCooldown = 0.0f;

    void RotateToTarget()
    {
        if (currentTarget != null)
        {
            Vector3 targetDir = currentTarget.transform.position - transform.position;
            float step = rotationSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    void MoveToTarget()
    {
        if (currentTarget != null)
            if (Vector3.Distance(transform.position, currentTarget.transform.position) > attackRange - 0.1f)
                transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
    }

    void SelectTarget()
    {
        if (currentTarget == null || Vector3.Distance(transform.position, currentTarget.transform.position) > attackRange)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Unit");
            float minDistance = Mathf.Infinity;
            GameObject nearestTarget = null;
            foreach (GameObject target in targets)
            {
                if (target.GetComponent<BasicBehavior>().team == team)
                    continue;
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTarget = target;
                }
            }
            currentTarget = nearestTarget;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage * damageReduction;
        if (currentHealth <= 0)
            Destroy(gameObject);
    }

    void HpRegen()
    {
        currentHealth += hpRegen * Time.deltaTime;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    void Attack()
    {
        if (currentTarget != null
                && Vector3.Distance(transform.position, currentTarget.transform.position) <= attackRange
                && Vector3.Angle(transform.forward, currentTarget.transform.position - transform.position) < 15.0f
                && attackCooldown <= 0.0f)
        {
            weapon.damageTarget(currentTarget.GetComponent<BasicBehavior>(), weapon.baseDamages * damageMultiplier);
            attackCooldown = 1.0f / attackSpeed;
        }
        else if (attackCooldown > 0.0f)
            attackCooldown -= Time.deltaTime;
    }

    void ChangeColor()
    {
        if (team == 0)
            GetComponent<Renderer>().material.color = Color.red;
        else
            GetComponent<Renderer>().material.color = Color.blue;
    }

    void Start() {
        ChangeColor();
    }

    void Update()
    {
        SelectTarget();
        RotateToTarget();
        MoveToTarget();
        HpRegen();
        Attack();
    }
}
