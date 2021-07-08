using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ucEnemyBase : MonoBehaviour
{
    protected enum DIRECTIONS {
        kNORTH = 0,
        kSOUTH,
        kEAST,
        kWEST
    }
    [SerializeField]
    protected float m_speed;
    [SerializeField]
    protected int m_scoreValue;

    protected bool m_canCollide;
    protected bool m_chasePlayer;
    protected uc_createWorld m_map;
    protected Vector2Int m_mapPos;
    protected Vector3 m_directionVec;
    protected DIRECTIONS m_direction;
    protected bool m_alive;
    protected float m_directionTime;
    protected float m_actualDirTime;

    // Start is called before the first frame update
    void Start() {
        GameObject map = GameObject.Find("World Origin");
        m_map = map.GetComponent<uc_createWorld>();
        SetDirection();
    }

    // Update is called once per frame
    void Update() {
        transform.position += m_directionVec * m_speed * Time.deltaTime;
        m_mapPos = m_map.obtainWorldPosition(transform.position);
    }

    void SetDirection() {
        DIRECTIONS dir;
        do {
            dir = (DIRECTIONS)Random.Range(0, 4);
        }
        while (m_direction == dir);
        m_direction = dir;
        switch (m_direction) {
            case DIRECTIONS.kNORTH:
                m_directionVec = new Vector3(0.0f, 0.0f, 1.0f);
                break;
            case DIRECTIONS.kSOUTH:
                m_directionVec = new Vector3(0.0f, 0.0f, -1.0f);
                break;
            case DIRECTIONS.kEAST:
                m_directionVec = new Vector3(1.0f, 0.0f, 0.0f);
                break;
            case DIRECTIONS.kWEST:
                m_directionVec = new Vector3(-1.0f, 0.0f, 0.0f);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        SetDirection();
    }
}