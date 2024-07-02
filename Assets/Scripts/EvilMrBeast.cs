using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EvilMrBeast : MonoBehaviour {
    public GameObject MrBeast;
    public GameObject player;
    public NavMeshAgent agent;
    private const string ISWALKING = "isWalking";
    [SerializeField] private Animator Animator;
    private GameManager gameManager;
    private void Start ( ) {
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameManager.instance;

    }

    private void Update ( ) {
        agent.destination = player.transform.position;
        Animator.SetBool(ISWALKING, agent.velocity.magnitude > 0.01f);
      
    }

    private void OnTriggerEnter ( Collider other ) {
        if(other.gameObject.tag == "Player") {
            // Call the GameOver function from GameManager
            gameManager.GameOver();
        }
    }
}
