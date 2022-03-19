using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemy : MonoBehaviour
{

    [SerializeField]
    private GameObject _target;

    public float speed;

    private Vector3 dirNormalized;

    void Start()
    {
        _target = FindClosestEnemy();
        dirNormalized = (_target.transform.position - transform.position).normalized;
    }

    void Update()
    {
        transform.position = transform.position + dirNormalized * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "city")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (other.gameObject.tag == "Player") //we hit the wall
        {
            Destroy(gameObject);
        }
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("city");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
