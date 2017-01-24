using System;
using System.Collections;
using UnityEngine;

public class Minimap : MonoBehaviour {

    private CollectableObject[] collectables;
    private Snowball m_Snowball;
    private int m_IconIndex = 0;

    void OnEnable()
    {
        Snowball.CollectEvent += UpdateMinimap;
    }

    void Start()
    {
        collectables = GameObject.FindObjectsOfType<CollectableObject>();
        SortCollectables();
        UpdateMinimap();
    }

    public void SetSnowballTarget(Snowball _snowball)
    {
        m_Snowball = _snowball;
    }

    void SortCollectables()
    {
        Array.Sort<CollectableObject>(collectables, delegate(CollectableObject a, CollectableObject b) 
        {
            return a.Size.CompareTo(b.Size); 
        });
    }

    void UpdateMinimap()
    {
        for (int i = m_IconIndex; i < collectables.Length; i++)
        {
            if (collectables[i].Size <= m_Snowball.SnowballSize)
            {
                // Add map icon
                GameObject icon = Instantiate((GameObject)Resources.Load(ResourcePath.IconCollectable));
                icon.transform.parent = collectables[i].gameObject.transform;
                icon.transform.localPosition = new Vector3(0, 20, 0);
                collectables[i].Icon = icon;
                m_IconIndex = i+1;
            }
            else
            {
                break;
            }
        }
    }

    void OnDisable()
    {
        Snowball.CollectEvent -= UpdateMinimap;
    }
}
