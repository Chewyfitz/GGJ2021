using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallManager : MonoBehaviour
{
  public float startingSpeed = 1f;

  [Range(0f, 0.01f)]
  public float rampMultiplier = 0.0001f;

  public AnimationCurve curveAcc;

  public int startLocation = 50;
  public int Distance = 20;
  public float nextWallTriggerDistance = 48;
  public GameObject nextWallTriggerObject;

  public GameObject[] walls = {};

  public GameObject scoreManager;
  ScoreManager sm;
  public int correctPoints = 1;
  public int incorrectPoints = -1;

  public AudioSource correctSound;
  public AudioSource incorrectSound;

  Dictionary<string, Queue<GameObject>> wallInstances;

  GameObject[] drawnWalls = {};

  int index = 0;
  float time = 0f;

  // Start is called before the first frame update
  void Start() {
    wallInstances = new Dictionary<string, Queue<GameObject>>();
    for( int i=0; i< walls.Length; i++){
      wallInstances[walls[i].name] = new Queue<GameObject>();
      wallInstances[walls[i].name].Enqueue(newWall(i));
    }
    nextWall();

    nextWallTriggerObject.transform.position = new Vector3(0, 0, nextWallTriggerDistance);
    sm = scoreManager.GetComponent<ScoreManager>();
  }

  // Update is called once per frame
  void Update() {
    time += Time.deltaTime;
    startingSpeed = curveAcc.Evaluate(time);
    // startingSpeed += rampMultiplier*curveAcc.Evaluate(time);
    // speedText.GetComponent<Text>().text = startingSpeed.ToString("0.0");
  }

  public void nextWall(){
    index = (int)(Random.value * walls.Length);
    if(wallInstances[walls[index].name].Count == 0){
      wallInstances[walls[index].name].Enqueue(newWall(index));
    }

    GameObject nextWall = wallInstances[walls[index].name].Dequeue();
    nextWall.transform.position = this.gameObject.transform.position + Vector3.forward*startLocation;
    nextWall.SetActive(true);
  }

  GameObject newWall(int index){
    GameObject temp = Instantiate(walls[index]);
    temp.SetActive(false);
    temp.transform.parent = this.gameObject.transform;
    temp.transform.position = this.gameObject.transform.position + Vector3.forward*startLocation;
    return temp;
  }

  public void shapeScore(bool correct){
    if(correct){
      sm.addScore(correctPoints);
      correctSound.Play();
    } else {
      sm.addScore(incorrectPoints);
      incorrectSound.Play();
    }
  }

  public bool removeWall(GameObject wall){
    Debug.Log("Removing Wall: " + wall.gameObject.name);
    wall.SetActive(false);
    wall.transform.position = this.gameObject.transform.position + Vector3.forward*startLocation;

    wallInstances[
      wall.gameObject.name.Substring(0, wall.gameObject.name.Length - 7)
      ].Enqueue(wall);
    
    return true;
  }
}
