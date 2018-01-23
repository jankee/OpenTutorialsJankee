using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CComponet
{
    public string name;

    public CComponet(string name)
    {
        this.name = name;
    }
}

public class CTransform : CComponet
{
    public float x, y, z;

    public CTransform(string name, float x, float y, float z) : base(name)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

public class CSpriteRenderer : CComponet
{
    public string sprite;
    public int orderInLayer;

    public CSpriteRenderer(string name, string sprite, int orderInLayer) : base(name)
    {
        this.sprite = sprite;
        this.orderInLayer = orderInLayer;
    }
}

public class CCollider2D : CComponet
{
    public float size;
    public bool isTrigger;

    public CCollider2D(string name, float size, bool isTrigger) : base(name)
    {
        this.size = size;
        this.isTrigger = isTrigger;
    }
}

public class CRigidbody : CComponet
{
    public int gravityScale;

    public CRigidbody(string name, int gravityScale) : base(name)
    {
        this.gravityScale = gravityScale;
    }
}

public class CInspector
{
    private int componentIndex = 0;
    private int maxIndex = 0;
    public CComponet[] components;

    public CInspector(int maxSize)
    {
        this.maxIndex = maxSize;

        components = new CComponet[maxIndex];
    }

    //부모클레스에서 자식클레스에 접근 방법은 버추얼과 오버라이드이다
    public void AddComponent(CComponet component)
    {
        if (componentIndex > maxIndex)
        {
            Debug.Log("더이상 컴포넌트를 추가할 수없습니다.");
            return;
        }

        components[componentIndex++] = component;
    }

    public T GetComponent<T>()
    {
        for (int i = 0; i < components.Length; i++)
        {
            if (typeof(T) == components[i].GetType())
            {
                return (T)(object)components[i];
            }
        }
        return (T)(object)null;
    }
}

public class CGenericMethodBasic : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        CInspector inspector = new CInspector(3);

        string name = "Plane";

        inspector.AddComponent(new CTransform(name, 1, 2, 3));
        inspector.AddComponent(new CSpriteRenderer(name, "plane.png", 0));
        inspector.AddComponent(new CCollider2D(name, 5f, false));
        //inspector.AddComponent(new CRigdbody(name, 10));

        CTransform tr = inspector.GetComponent<CTransform>();
        print("Transform (" + tr.x + ", " + tr.y + ", " + tr.z + ")");

        CSpriteRenderer sr = inspector.GetComponent<CSpriteRenderer>();
        print("SpriteRenderer (" + sr.sprite + ", " + sr.orderInLayer + ")");

        CCollider2D col = inspector.GetComponent<CCollider2D>();
        print("Collider2D (" + col.size + ", " + col.isTrigger + ")");

        CRigidbody rig = inspector.GetComponent<CRigidbody>();
        print("Rigidbody (" + rig.gravityScale + ")");
    }

    // Update is called once per frame
    private void Update()
    {
    }
}