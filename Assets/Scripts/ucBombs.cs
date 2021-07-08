using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public 
class ucBombs : MonoBehaviour {

  float m_timer = 3.5f; //Time in seconds
  float m_actualTime; 
  int m_range = 1; //Range in tiles
  int m_tileSize;
  public GameObject m_thisBomb;
  public GameObject m_firePrefab;
  public bool m_isActive = false;


  // Start is called before the first frame update
  void 
  Start() {
    m_actualTime = m_timer;
  }
  
  // Update is called once per frame
  void 
  Update() {
    m_isActive = m_thisBomb.activeSelf;

    if (m_isActive) {
      m_actualTime -= Time.deltaTime;
    }

    if (0 >= m_actualTime) {
      Explode();
    }
  }

  public void
  Spawn() {
    m_thisBomb.SetActive(true);
  }


  void
  Explode() {
    m_actualTime = m_timer;
    m_thisBomb.SetActive(false);
    //Generate the explotion and detect the player
    //Directions
    for (int i = 0; i < 3; ++i) {
      //Range
      Vector3 tmpPos = m_thisBomb.gameObject.transform.position;
      switch (i) {
        case 0: //derecha
          tmpPos.x += m_thisBomb.GetComponent<Collider>().bounds.size.x;
          break;
        case 1: //abajo
          tmpPos.z -= m_thisBomb.GetComponent<Collider>().bounds.size.z;
          break;
        case 2: //izquierda
          tmpPos.x -= m_thisBomb.GetComponent<Collider>().bounds.size.x;
          break;
        case 3: //ariba
          tmpPos.z += m_thisBomb.GetComponent<Collider>().bounds.size.z;
          break;
      }
      for (int j = 0; j < m_range; ++j) { 
        GameObject tmpBomb = Instantiate(m_firePrefab, tmpPos, Quaternion.identity);
      }
    }
  }
}
