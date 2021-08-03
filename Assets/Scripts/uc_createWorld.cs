using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uc_createWorld : MonoBehaviour
{
    public uint m_rows = 2;
    public uint m_columns = 2;

    public GameObject m_floorSprite;
    public GameObject m_limitMapSprite;

    public GameObject[] m_savedTiles;
    private float m_tileSizeX = 0;
    private float m_tileSizeY = 0;


    // Start is called before the first frame update
    void Start()
    {
        //Obtención de bounds del modelo a usar
        obtainBounds();
        generateWorld();
    }

    private void generateWorld()
    {
        m_savedTiles = new GameObject[m_rows * m_columns];

        bool oneTime = false;

        uint tempRowsCols = 0;

        for (uint i = 0; i < m_rows; ++i)
        {
            oneTime = false;

            for (uint j = 0; j < m_columns; ++j)
            {
                //Zona para dibujar el mapa completo
                GameObject tile = (GameObject)Instantiate(m_floorSprite, transform);

                float posX = j * m_tileSizeX;
                float posZ = i * m_tileSizeY;

                tile.transform.position = new Vector3(posX, 0.0f, posZ);

                m_savedTiles[tempRowsCols] = tile;
                ++tempRowsCols;

                //Zona para dibujar obstáculos a la derecha
                if (!oneTime)
                {
                    generateObstacle(posZ);
                    oneTime = true;
                }
            }
        }

        //Zona para los obstáculos de arriba
        generateObstacle2();

        //Zona para los obstáculos de la izquierda
        generateObstacle3();

        //Zona para los obstáculos de abajo
        generateObstacle4();
    }

    private void generateObstacle(float posZ)
    {
        //Lado positivo
        GameObject limitMap = (GameObject)Instantiate(m_limitMapSprite, transform);
        float posX_LM = m_rows * m_tileSizeX;

        limitMap.transform.position = new Vector3(posX_LM, 0.0f, posZ);
        limitMap.tag = "Limit";
    }

    private void generateObstacle2()
    {
        for (uint i = 0; i <= m_columns; ++i)
        {
            GameObject limitMap = (GameObject)Instantiate(m_limitMapSprite, transform);

            float posX = i * m_tileSizeX;
            float posZ = m_rows * m_tileSizeY;

            limitMap.transform.position = new Vector3(posX, 0.0f, posZ);
            limitMap.tag = "Limit";
        }
    }

    private void generateObstacle3()
    {
        for (uint i = 0; i <= m_rows; ++i)
        {
            GameObject limitMap = (GameObject)Instantiate(m_limitMapSprite, transform);
            float posX_LM = -1 * m_tileSizeX;
            float posZ = i * m_tileSizeY;

            limitMap.transform.position = new Vector3(posX_LM, 0.0f, posZ);
            limitMap.tag = "Limit";
        }

        GameObject oneLimit = (GameObject)Instantiate(m_limitMapSprite, transform);

        float posX = -1 * m_tileSizeX;
        float posZ_LM = -1 * m_tileSizeY;

        oneLimit.transform.position = new Vector3(posX, 0.0f, posZ_LM);
        oneLimit.tag = "Limit";
    }

    private void generateObstacle4()
    {
        for (uint i = 0; i <= m_columns; ++i)
        {
            GameObject limitMap = (GameObject)Instantiate(m_limitMapSprite, transform);

            float posX = i * m_tileSizeX;
            float posZ = -1 * m_tileSizeY;

            limitMap.transform.position = new Vector3(posX, 0.0f, posZ);
            limitMap.tag = "Limit";
        }
    }

    public Vector2Int obtainWorldPosition(Vector3 worldPosition)
    {
        //Pasar del mundo al tile
        Vector2 mapCoord;

        mapCoord.x = (worldPosition.x / m_tileSizeX);
        mapCoord.y = (worldPosition.z / m_tileSizeY);

        mapCoord.x = Mathf.Clamp(mapCoord.x, 0, m_rows - 1);
        mapCoord.y = Mathf.Clamp(mapCoord.y, 0, m_columns - 1);

        return Vector2Int.RoundToInt(mapCoord);
    }

    public Vector3 obtainTileToWorld(Vector2Int tilePos) {

      if((tilePos.x > 0 && tilePos.x < m_rows)
         && (tilePos.y > 0 && tilePos.y < m_columns)) {

        return m_savedTiles[(tilePos.y * m_columns) 
                            + tilePos.x].transform.position;
      }

      return new Vector3(-1.0f, -1.0f, -1.0f);
    }

    private void obtainBounds()
    {
        BoxCollider tempSprite = m_floorSprite.GetComponent<BoxCollider>();

        //Sabemos cuando mide un tile
        m_tileSizeX = tempSprite.size.x;
        m_tileSizeY = tempSprite.size.y;
    }

    private void OnCollisionEnter(Collision collision) {
        
    }
}
