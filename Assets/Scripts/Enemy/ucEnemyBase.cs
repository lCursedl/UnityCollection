using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ucEnemyBase : MonoBehaviour
{
    private enum DIRECTIONS
    {
        kNORTH = 0,
        kSOUTH,
        kEAST,
        kWEST
    }
    [SerializeField]
    float m_speed;
    int m_scoreValue;
    bool m_canCollide;
    bool m_chasePlayer;
    uc_createWorld m_map;
    Vector2Int m_mapPos;
    Vector3 m_directionVec;
    DIRECTIONS m_direction;

    // Start is called before the first frame update
    void Start()
    {
        GameObject map = GameObject.Find("World Origin");
        m_map = map.GetComponent<uc_createWorld>();
        m_mapPos = m_map.obtainWorldPosition(transform.position);
        SetDirection();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += m_directionVec * m_speed * Time.deltaTime;
    }

    void SetDirection()
    {
        DIRECTIONS dir;
        do
        {
            dir = (DIRECTIONS)Random.Range(0, 4);
        }
        while (m_direction == dir);
        m_direction = dir;
        switch (m_direction)
        {
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