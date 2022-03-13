using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{
    public LayerMask player;
    public LayerMask range;
    public LayerMask clickable;
    public GameObject selectedUI;
    private NavMeshAgent navMeshAgent;
    public bool selected = false;
    public float speed = 15.0f;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        selectedUI.transform.localScale = new Vector3(speed, .01f, speed);
    }

    void Update()
    {
        SelectAndMovePlayer();
    }

    void SelectAndMovePlayer() {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hitInfo;
            RaycastHit rangeInfo;

            Debug.Log(Physics.Raycast(ray, out rangeInfo, 100, range));
            Debug.Log(Physics.Raycast(ray, out hitInfo, 100, clickable));

            if (selected && Physics.Raycast(ray, out rangeInfo, 100, range) && Physics.Raycast(ray, out hitInfo, 100, clickable))
            {
                navMeshAgent.SetDestination(hitInfo.point);
                selected = false;
                selectedUI.SetActive(false);
            }

            if (Physics.Raycast(ray, out hitInfo, 100, player))
            {
                selected = true;
                selectedUI.SetActive(true);
            }
        }
    }
}
