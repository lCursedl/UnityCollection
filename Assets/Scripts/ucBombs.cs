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
  public uc_createWorld m_map;
  public GameObject m_firePrefab;
  public bool m_isActive = false;
  List<GameObject> fire = new List<GameObject>();
  bool m_isExploded = false;
  
  bool m_left  = true;
  bool m_up    = true;
  bool m_down  = true;
  bool m_right = true;


  // Start is called before the first frame update
  void 
  Start() {
    m_actualTime = m_timer;
    GameObject map = GameObject.FindGameObjectWithTag("World");
    m_map = map.GetComponent<uc_createWorld>();
  }
  
  // Update is called once per frame
  void 
  Update() {
    m_isActive = gameObject.activeSelf;

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
    gameObject.SetActive(true);
  }


  void
  Explode() {
    m_actualTime = m_timer;
    //Generate the explotion and detect the player
    //Directions
    for (int i = 0; i <= 3; ++i) {
      //Range
      for (int j = 1; j <= m_range; ++j) {
        Vector3 tmpPos = gameObject.transform.position;
        switch (i) {
          case 0: //derecha
            if (m_right) {
              tmpPos.x += (GetComponent<BoxCollider>().size.x * j);
            }
            break;
          case 1: //abajo
            if (m_down) {
              tmpPos.z -= (GetComponent<BoxCollider>().size.z * j);
            }
            break;
          case 2: //izquierda
            if (m_left) {
              tmpPos.x -= (GetComponent<BoxCollider>().size.x * j);
            }
            break;
          case 3: //ariba
            if (m_up) {
              tmpPos.z += (GetComponent<BoxCollider>().size.z * j);
            }
            break;
        }

        Vector3 realPos = m_map.obtainTileToWorld
                                (m_map.obtainWorldPosition(tmpPos));

        if (realPos.x != -1 && realPos.y != -1 && realPos.z != -1) {

          Vector3 originalPos = gameObject.transform.position;

          foreach (GameObject element in m_map.m_savedIndestructibles) {
            if (element.transform.position == tmpPos) {
              if (originalPos.x > realPos.x) {
                m_right = false;
              }
              else if (originalPos.x < realPos.x) {
                m_left = false;
              }
              else if (originalPos.z > realPos.z) {
                m_up = false;
              }
              else if (originalPos.z < realPos.z) {
                m_down = false;
              }
            }
          }

          foreach (GameObject element in m_map.m_savedDestructibles) {
            if (element.transform.position == tmpPos) {
              Destroy(element);
            }
          }

          if (originalPos.x > realPos.x) {
            if (m_right) {
              GameObject tmpFire = Instantiate(m_firePrefab, tmpPos, Quaternion.identity);
              fire.Add(tmpFire);
            }
          }
          else if (originalPos.x < realPos.x) {
            if (m_left) {
              GameObject tmpFire = Instantiate(m_firePrefab, tmpPos, Quaternion.identity);
              fire.Add(tmpFire);
            }
          }
          else if (originalPos.z > realPos.z) {
            if (m_up) {
              GameObject tmpFire = Instantiate(m_firePrefab, tmpPos, Quaternion.identity);
              fire.Add(tmpFire);
            }
          }
          else if (originalPos.z < realPos.z) {
            if (m_down) {
              GameObject tmpFire = Instantiate(m_firePrefab, tmpPos, Quaternion.identity);
              fire.Add(tmpFire);
            }
          }

          

          //if (originalPos.x > realPos.x) {
          //  m_right = true;
          //}
          //else if (originalPos.x < realPos.x) {
          //  m_left = true;
          //}
          //else if (originalPos.z > realPos.z) {
          //  m_up = true;
          //}
          //else if (originalPos.z < realPos.z) {
          //  m_down = true;
          //}

        }

      }
    }
    m_isExploded = true;
    m_actualTimeFire = m_timerFire;
  }


  void FireDespawn() {


    for (int i = 0; i < fire.Count; ++i ) {
        Object.DestroyObject(fire[i]);
    }


    gameObject.SetActive(false);
    fire = new List<GameObject>();
    Object.DestroyObject(gameObject);
  }

}
