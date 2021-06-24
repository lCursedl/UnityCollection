using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public 
class ucBombs : MonoBehaviour {

  float m_timer = 3.5f; //Time in seconds
  float m_actualTime; 
  int m_range = 1; //Range in tiles
  int m_tileSize;
  GameObject m_thisBomb;
  bool m_isActive = false;


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

  void
  Spawn() {
    m_thisBomb.SetActive(true);
  }


  void
  Explode() {
    m_actualTime = m_timer;
    //Generate the explotion and detect the player
  }
}
