using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uc_createWorld : MonoBehaviour
{
    public uint m_rows = 2;
    public uint m_columns = 2;

    public GameObject m_referenceSprite;

    private GameObject[] m_savedTiles;
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
        uint tempRowsCols = 0;

        for (uint i = 0; i < m_rows; ++i)
        {
            for (uint j = 0; j < m_columns; ++j)
            {
                GameObject tile = (GameObject)Instantiate(m_referenceSprite, transform);

                float posX = j * m_tileSizeX;
                float posZ = i * m_tileSizeY;

                tile.transform.position = new Vector3(posX, 0.0f, posZ);

                m_savedTiles[tempRowsCols] = tile;
                ++tempRowsCols;
            }
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

    private void obtainBounds()
    {
        BoxCollider tempSprite = m_referenceSprite.GetComponent<BoxCollider>();

        //Sabemos cuando mide un tile
        m_tileSizeX = tempSprite.size.x;
        m_tileSizeY = tempSprite.size.y;
    }

    private void OnCollisionEnter(Collision collision) {
        
    }
}
