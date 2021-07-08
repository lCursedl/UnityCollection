using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public 
class ucBombs : MonoBehaviour {

  float m_timer = 3.5f; //Time in seconds
  float m_timerFire = 3.5f; //Time in seconds
  float m_actualTime; 
  float m_actualTimeFire; 
  int m_range = 2; //Range in tiles
  int m_tileSize;
  public GameObject m_thisBomb;
  public GameObject m_firePrefab;
  public bool m_isActive = false;
  List<GameObject> fire = new List<GameObject>();
  bool m_isExploded = false;


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
      m_actualTimeFire -= Time.deltaTime;
    }

    if (0 >= m_actualTime && !m_isExploded) {
      Explode();
    }
    
    if (0 >= m_actualTimeFire && m_isExploded) {
      FireDespawn();
    }
    
  }

  public void
  Spawn() {
    m_thisBomb.SetActive(true);
  }


  void
  Explode() {
    m_actualTime = m_timer;
    //Generate the explotion and detect the player
    //Directions
    for (int i = 0; i <= 3; ++i) {
      //Range
      for (int j = 1; j <= m_range; ++j) {
        Vector3 tmpPos = m_thisBomb.gameObject.transform.position;
        switch (i) {
          case 0: //derecha
            tmpPos.x += (m_thisBomb.GetComponent<BoxCollider>().size.x * j);

            break;
          case 1: //abajo
            tmpPos.z -= (m_thisBomb.GetComponent<BoxCollider>().size.z * j);

            break;
          case 2: //izquierda
            tmpPos.x -= (m_thisBomb.GetComponent<BoxCollider>().size.x * j);

            break;
          case 3: //ariba
            tmpPos.z += (m_thisBomb.GetComponent<BoxCollider>().size.z * j);
            break;
        }

        GameObject tmpFire = Instantiate(m_firePrefab, tmpPos, Quaternion.identity);
        fire.Add(tmpFire);
      }
    }
    m_isExploded = true;
    m_actualTimeFire = m_timerFire;
  }


  void FireDespawn() {


    for (int i = 0; i < fire.Count; ++i ) {
      DestroyObject(fire[i]);
    }


    m_thisBomb.SetActive(false);
    fire = new List<GameObject>();
    DestroyObject(m_thisBomb);
  }

}
