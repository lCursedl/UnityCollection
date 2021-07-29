using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uc_createWorld : MonoBehaviour
{
    public uint m_rows = 2;
    public uint m_columns = 2;

    private float m_tileSizeX = 0;
    private float m_tileSizeY = 0;

    public uc_infoTile m_infoTileObj;
    public uc_floorTiles m_floorObject;

    //TODO: Change
    public GameObject m_limitMapSprite;
    
    //Start is called before the first frame update
    void Start()
    {
        //Obtaining bounds of the model to use
        obtainBounds();
        generateWorld();
    }

    private void generateWorld()
    {
        m_floorObject.m_savedTiles = new GameObject[m_rows * m_columns];
        m_floorObject.m_floorPosition = new Vector3[m_rows * m_columns];

        bool oneTime = false;

        uint tempRowsCols = 0;

        for (uint i = 0; i < m_rows; ++i)
        {
            oneTime = false;

            for (uint j = 0; j < m_columns; ++j)
            {
                //Area to draw the complete map
                GameObject tile = (GameObject)Instantiate(m_floorObject.m_floorSprite, 
                                                          transform);
                float posX = j * m_tileSizeX;
                float posZ = i * m_tileSizeY;

                tile.transform.position = new Vector3(posX, 0.0f, posZ);

                //We save the tiles in an arrangement
                m_floorObject.m_savedTiles[tempRowsCols] = tile;
                m_floorObject.m_floorPosition[tempRowsCols] = tile.transform.position;

                ++tempRowsCols;

                //Area to draw obstacles on the right
                if (!oneTime)
                {
                    generateObstacle(posZ);
                    oneTime = true;
                }
            }
        }

        //Overhead obstacle area
        generateObstacle2();

        //Left obstacle area
        generateObstacle3();

        //Area for obstacles below
        generateObstacle4();
    }

    private void generateObstacle(float posZ)
    {
        //Positive side
        GameObject limitMap = (GameObject)Instantiate(m_limitMapSprite, transform);
        float posX_LM = m_rows * m_tileSizeX;

        limitMap.transform.position = new Vector3(posX_LM, 0.0f, posZ);
    }

    private void generateObstacle2()
    {
        for (uint i = 0; i <= m_columns; ++i)
        {
            GameObject limitMap = (GameObject)Instantiate(m_limitMapSprite, transform);

            float posX = i * m_tileSizeX;
            float posZ = m_rows * m_tileSizeY;

            limitMap.transform.position = new Vector3(posX, 0.0f, posZ);
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
        }

        GameObject oneLimit = (GameObject)Instantiate(m_limitMapSprite, transform);

        float posX = -1 * m_tileSizeX;
        float posZ_LM = -1 * m_tileSizeY;

        oneLimit.transform.position = new Vector3(posX, 0.0f, posZ_LM);
    }

    private void generateObstacle4()
    {
        for (uint i = 0; i <= m_columns; ++i)
        {
            GameObject limitMap = (GameObject)Instantiate(m_limitMapSprite, transform);

            float posX = i * m_tileSizeX;
            float posZ = -1 * m_tileSizeY;

            limitMap.transform.position = new Vector3(posX, 0.0f, posZ);
        }
    }

    public Vector2Int obtainWorldPosition(Vector3 worldPosition)
    {
        //Go from the world to the tile
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

        return m_floorObject.m_savedTiles[(tilePos.y * m_columns) 
                                          + tilePos.x].transform.position;
      }

      return new Vector3(-1.0f, -1.0f, -1.0f);
    }

    private void obtainBounds()
    {
        BoxCollider tempSprite = m_floorObject.m_floorSprite.GetComponent<BoxCollider>();

        //We know how long a tile is
        m_tileSizeX = tempSprite.size.x;
        m_tileSizeY = tempSprite.size.y;
    }

    private void OnCollisionEnter(Collision collision) {
        
    }
}
