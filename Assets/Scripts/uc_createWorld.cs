using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uc_createWorld : MonoBehaviour
{
    public uint m_rows = 2;
    public uint m_columns = 2;
    public float m_tileSize = 3.85f;
    public GameObject m_referenceSprite;

    // Start is called before the first frame update
    void Start()
    {
        generateWorld();
    }

    private void generateWorld()
    {
        for (uint i = 0; i < m_rows; ++i)
        {
            for (uint j = 0; j < m_columns; ++j)
            {
                GameObject tile = (GameObject)Instantiate(m_referenceSprite, transform);

                float posX = j * m_tileSize;
                float posZ = i * -m_tileSize;

                tile.transform.position = new Vector3(posX, 0.0f, posZ);
            }
        }
    }
}
