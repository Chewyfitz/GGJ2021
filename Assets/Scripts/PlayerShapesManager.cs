using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerShapesManager : MonoBehaviour
{
  public GameObject[] shapes = {};
  GameObject currentShape;
  Dictionary<string, GameObject> shapeInstances;
  int index = 0;

  public AudioSource shapeChange;

  void Start() {
    if(shapes.Length < 1){
      Debug.LogError("ERROR NOT ENOUGH SHAPES.");
      Application.Quit();
    }

    shapeInstances = new Dictionary<string, GameObject>();

    changeShape();
  }

  void Update() {
    if(Input.anyKeyDown) {
      if(Input.GetKeyDown(KeyCode.Escape)){
        SceneManager.LoadScene("MainMenu");
      } else {
        changeShape();
        shapeChange.Play();
      }
    }
  }

  void changeShape(){
    if (currentShape)
      currentShape.SetActive(false);

    if(shapeInstances.ContainsKey(shapes[index].name)){
      // Debug.Log("Getting shape from dictionary: "+shapes[index].name);
      currentShape = shapeInstances[shapes[index].name];
      currentShape.SetActive(true);
    } else {
      // Debug.Log("Adding shape to dictionary: "+shapes[index].name);
      currentShape = Instantiate(shapes[index]);
      currentShape.transform.parent = this.gameObject.transform;
      currentShape.transform.position = this.gameObject.transform.position;
      shapeInstances.Add(shapes[index].name, currentShape);
    }

    if(++index >= shapes.Length) index = 0;
  }
}
