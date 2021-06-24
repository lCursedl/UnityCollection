using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
  public float m_speed;
  uint m_lifes;
  uint m_numBombs;
  uint m_usingBombs = 0;
  Collider m_collider;
  private CharacterController m_controller;

  public List<GameObject> bombs = new List<GameObject>();
  public GameObject bombsPrefab;
    
  public enum ANIMATIONAXIS {
    kRows,
    kColum
  }

  [SerializeField]
  private MeshRenderer m_meshRender;
  
  [SerializeField]
  private string m_rowProperty = "_ActualRow";

  [SerializeField]
  private string m_colProperty = "_ActualCol";

  [SerializeField]
  private ANIMATIONAXIS m_axis;

  [SerializeField]
  private float m_animationSpeed;

  [SerializeField]
  private int m_animationIndex;

    // Start is called before the first frame update
    void 
  Start() {

    m_controller = gameObject.AddComponent<CharacterController>();
    m_lifes = 3;
    m_numBombs = 1;
  }

  // Update is called once per frame
  void 
  Update() {
    //movement of the character
    Vector3 move = new Vector3(Input.GetAxis("Horizontal"),  
                               0,
                               Input.GetAxis("Vertical"));

    m_controller.Move(move * Time.deltaTime * m_speed);

    string clipKey, frameKey;
    if(m_axis == ANIMATIONAXIS.kRows) {
      clipKey = m_rowProperty;
      frameKey = m_colProperty;
    }
    else {
      clipKey = m_colProperty;
      frameKey = m_rowProperty;
    }

    int frame = (int)(Time.time * m_animationSpeed);

    m_meshRender.material.SetFloat(clipKey,m_animationIndex);
    m_meshRender.material.SetFloat(frameKey, frame);

    //inputs to character mecanics
    inputEvent();
    
  }

  void
  inputEvent() {

    if (Input.GetKeyDown(KeyCode.Space)) {
      if (m_usingBombs < m_numBombs) {
        print("bomba puesta");
        Vector3 tmpPos = m_controller.gameObject.transform.position;
        GameObject tmpBomb = Instantiate(bombsPrefab, tmpPos, Quaternion.identity);
        bombs.Add(tmpBomb);
        ucBombs tmpBombInfo = tmpBomb.GetComponent(typeof(ucBombs)) as ucBombs;
        tmpBombInfo.Spawn();
      }
    }
  }
}
