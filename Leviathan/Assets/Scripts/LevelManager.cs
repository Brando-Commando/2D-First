using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckpoint;

    private PlayerController player;

    public GameObject deathParticle;

    public float respawnDelay;

    private float gravityStore;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    // Code to transport the player to any given checkpoint
    public void respawnPlayer()
    {
        StartCoroutine("respawnPlayerCo");
    }

    // Determines what happens when the event that the player dies occurs
    public IEnumerator respawnPlayerCo()
    {
        //Places death particles, disables the movement and visibility of the player, 
        //And gives a respawn player message
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
        gravityStore = player.GetComponent<Rigidbody2D>().gravityScale;
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Debug.Log("Player Respawn");

        //Adds a delay between death and teleportation to previous checkpoint or spawn
        yield return new WaitForSeconds(respawnDelay);
        player.GetComponent<Rigidbody2D>().gravityScale = gravityStore;
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;
        player.transform.position = currentCheckpoint.transform.position;
    }

}

