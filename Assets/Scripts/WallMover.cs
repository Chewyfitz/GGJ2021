using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMover : MonoBehaviour
{
  public float startingSpeed = 1f;

  public WallManager wallManager;

  public GameObject nextWallTriggerObject;

  bool wallAdded = false;

  // Start is called before the first frame update
  void Start() {
    wallManager = this.transform.parent.GetComponent<WallManager>();
    startingSpeed = wallManager.startingSpeed;
    nextWallTriggerObject = wallManager.nextWallTriggerObject;
  }

  void OnTriggerEnter(Collider other){
    if (other.gameObject != nextWallTriggerObject){
      wallManager.shapeScore(other.gameObject.name.Substring(0,4) == this.gameObject.name.Substring(0,4));
    }
  }

  // Update is called once per frame
  void Update() {
    transform.Translate(Vector3.down * (Time.deltaTime * startingSpeed));
    startingSpeed = wallManager.startingSpeed;
    if(transform.position.z < 0 - wallManager.Distance){
      wallManager.removeWall(this.gameObject);
      wallAdded = false;
    }
    if(!wallAdded && transform.position.z < wallManager.nextWallTriggerObject.transform.position.z){
      wallManager.nextWall();
      wallAdded = true;
    }
  }
}
