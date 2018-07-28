using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PostRenderer : MonoBehaviour
{
    public Material _material;

    [SerializeField] float offsetPosY;

    [SerializeField] float offsetNoise;
    [SerializeField] float offsetColorScale;
    void Awake()
    {

       // _material = new Material(Shader.Find("Custom/VHSeffect"));
      //  _material.SetTexture("_SecondaryTex", Resources.Load("Effects/tvnoise2") as Texture);
       /* _material.SetFloat("_OffsetPosY", 0f);
        _material.SetFloat("_OffsetColor", 0.01f);
        _material.SetFloat("_OffsetDistortion", 480f);
        _material.SetFloat("_Intensity", 0.64f);

        offsetNoise = _material.GetFloat("_OffsetNoiseY");
        offsetPosY = _material.GetFloat("_OffsetPosY");
        offsetColor = _material.GetFloat("_OffsetColor");*/
    }

    float offset = 0;
    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // TV noise
        _material.SetFloat("_OffsetNoiseX", offsetNoise*Random.Range(0f, 0.6f));
        
        _material.SetFloat("_OffsetNoiseY", offsetNoise* Random.Range(-0.03f, 0.03f));
        /*
        // Vertical shift
       
        if (offsetPosY > 0.0f)
        {
            _material.SetFloat("_OffsetPosY", offsetPosY - Random.Range(0f, offsetPosY));
        }
        else if (offsetPosY < 0.0f)
        {
            _material.SetFloat("_OffsetPosY", offsetPosY + Random.Range(0f, -offsetPosY));
        }
        else if (Random.Range(0, 150) == 1)
        {
            _material.SetFloat("_OffsetPosY", Random.Range(-0.5f, 0.5f));
        }
        */
        // Channel color shift
        offset = Mathf.MoveTowards(offset, Random.Range(0.0005f, 0.002f)* offsetColorScale, 0.0001f* offsetColorScale);
        _material.SetFloat("_OffsetColor", offset);
        /*
        if (offsetColor > 0.003f)
        {
            _material.SetFloat("_OffsetColor", offsetColor - 0.001f);
        }
        else if (Random.Range(0, 400) == 1)
        {
            _material.SetFloat("_OffsetColor", Random.Range(0.003f, 0.1f));
        }
        */
        
        /*
        // Distortion
        if (Random.Range(0, 15) == 1)
        {
            _material.SetFloat("_OffsetDistortion", Random.Range(480f, 2000));
        }
        else
        {
            _material.SetFloat("_OffsetDistortion", 2000);
        }
        */
        Graphics.Blit(source, destination, _material);
    }
}