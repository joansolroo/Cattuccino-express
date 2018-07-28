using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidGenarate : MonoBehaviour {
	public int _numLiquid = 10;
    public int _numIterations = 10;
    public float timeBetweenDrops = 0.1f;
    public float _width = 1;
	public float _height = 1;
	public GameObject _prefab;

    [SerializeField] bool generate = false;
    [SerializeField] Color color;
    [SerializeField] float density = 1;
    [SerializeField] float scale = 1;

   // [SerializeField] KeyCode key;
    [SerializeField] LiquidParticle.LiquidType liquidType;
    public static  Dictionary<LiquidParticle.LiquidType, Color> liquidColor = new Dictionary<LiquidParticle.LiquidType, Color>();
    private void Start()
    {
        liquidColor[liquidType] = color;
    }
    // Use this for initialization
    void Update () {

        /*
        if (Input.GetKeyDown(key))
        {
            generate = true;
        }*/
        if (generate)
        {
            generate = false;
            Generate();
        }
	}

    public void Generate()
    {
        StartCoroutine(DoGenerate());
    }

    IEnumerator DoGenerate()
    {
        float h, s, v;
        Color.RGBToHSV(color, out h, out s, out v);
        //Color color = colors[currentColor%colors.Length];
        for (int it = 0; it < _numIterations; it++)
        {
            for (int i = 0; i < _numLiquid; i++)
            {
                GameObject temp = (GameObject)Instantiate(_prefab);
                temp.transform.parent = transform;
                float x = Random.Range(-1.0f, 1.0f) * _width / 2;
                float y = Random.Range(-1.0f, 1.0f) * _height / 2;
                temp.transform.localPosition = new Vector3(x, y, 0);
                temp.transform.localScale = Vector3.one * scale;

                LiquidParticle p = temp.GetComponent<LiquidParticle>();
                if (p != null)
                {
                    p.liquidType = liquidType;
                    temp.GetComponent<Renderer>().material.color = Random.ColorHSV(h - 0.01f, h + 0.01f, s - 0.1f, s + 0.1f, v - 0.1f, v + 0.1f);
                }
                Rigidbody2D rb2D = temp.GetComponent<Rigidbody2D>();
                rb2D.mass = density;
            }
            yield return new WaitForSeconds(timeBetweenDrops);
        }

    }
}
