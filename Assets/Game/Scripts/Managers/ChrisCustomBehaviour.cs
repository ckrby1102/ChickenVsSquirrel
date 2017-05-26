using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrisCustomBehaviour : MonoBehaviour {

    protected Transform t;
    // Manually initialize to control execution order
    public virtual void Init()
    {
        t = gameObject.transform;
    }

    // Manually control update as well
    public virtual void MSUpdate()
    {

    }

    // Shortcut for toggling GameObject
    public virtual void Activate(bool val)
    {
        gameObject.SetActive(val);
    }

    public virtual void DestroyMe(float t = -1)
    {
        StopAllCoroutines();

        if (t > 0) Destroy(gameObject, t);
        else Destroy(gameObject);
    }

    // Convenience functions for transformations
    public void AddChild(Transform childT)
    {
        childT.SetParent(t);
    }

    public void AddChild(GameObject child)
    {
        child.transform.SetParent(t);
    }

    public void AddChild(ChrisCustomBehaviour child)
    {
        child.SetParent(t);
    }

    public void SetParent(Transform parentT)
    {
        t.SetParent(parentT);
    }

    public void SetParent(GameObject g)
    {
        t.SetParent(g.transform);
    }

    public void SetParent(ChrisCustomBehaviour m)
    {
        t.SetParent(m.t);
    }

    public void SetPosLocal(float x, float y, float z)
    {
        Vector3 v = t.localPosition;
        v.x = x;
        v.y = y;
        v.z = z;
        t.localPosition = v;
    }

    public Vector3 localPos
    {
        get { return t.localPosition; }
        set
        {
            t.localPosition = value;
        }
    }

    public float localX
    {
        get { return t.localPosition.x; }
        set
        {
            Vector3 v = t.localPosition;
            v.x = value;
            t.localPosition = v;
        }
    }

    public float localY
    {
        get { return t.localPosition.y; }
        set
        {
            Vector3 v = t.localPosition;
            v.y = value;
            t.localPosition = v;
        }
    }

    public float localZ
    {
        get { return t.localPosition.z; }
        set
        {
            Vector3 v = t.localPosition;
            v.z = value;
            t.localPosition = v;
        }
    }

    public void SetPosGlobal(float x, float y, float z)
    {
        Vector3 v = t.position;
        v.x = x;
        v.y = y;
        v.z = z;
        t.position = v;
    }

    public Vector3 globalPos
    {
        get { return t.position; }
        set
        {
            t.position = value;
        }
    }

    public float globalX
    {
        get { return t.position.x; }
        set
        {
            Vector3 v = t.position;
            v.x = value;
            t.position = v;
        }
    }

    public float globalY
    {
        get { return t.position.y; }
        set
        {
            Vector3 v = t.position;
            v.y = value;
            t.position = v;
        }
    }

    public float globalZ
    {
        get { return t.position.z; }
        set
        {
            Vector3 v = t.position;
            v.z = value;
            t.position = v;
        }
    }

    public void SetLocalScale(float x, float y, float z)
    {
        Vector3 v = t.localScale;
        v.x = x;
        v.y = y;
        v.z = z;
        t.localScale = v;
    }

    public Vector3 localScaleV
    {
        get { return t.localScale; }
        set
        {
            t.localScale = value;
        }
    }

    public float localScaleF
    {
        set
        {
            t.localScale = new Vector3(value, value, value);
        }
    }

    public float localScaleX
    {
        get { return t.localScale.x; }
        set
        {
            Vector3 v = t.localScale;
            v.x = value;
            t.localScale = v;
        }
    }

    public float localScaleY
    {
        get { return t.localScale.y; }
        set
        {
            Vector3 v = t.localScale;
            v.y = value;
            t.localScale = v;
        }
    }

    public float localScaleZ
    {
        get { return t.localScale.z; }
        set
        {
            Vector3 v = t.localScale;
            v.z = value;
            t.localScale = v;
        }
    }
}
