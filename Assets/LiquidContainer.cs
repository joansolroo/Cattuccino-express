using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class LiquidContainer : MonoBehaviour
{
    Dictionary<LiquidParticle.LiquidType, List<LiquidParticle>> contents = new Dictionary<LiquidParticle.LiquidType, List<LiquidParticle>>();

    [SerializeField] Rigidbody2D rigidBody;
    int particleLayer = 8;

    [SerializeField] bool compute = false;
    [SerializeField] public LiquidParticle.LiquidType[] ingredients;
    [SerializeField] public int[] amounts;

    //AudioSource audioSource;
    private void Start()
    {
       // audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(compute)
        {
            compute = false;
            RecomputeIngredients();
        }
    }

    public void RecomputeIngredients()
    {
        ingredients = new LiquidParticle.LiquidType[contents.Count];
        amounts = new int[contents.Count];
        
        int idx = 0;
        foreach (var k in contents.Keys)
        {
            ingredients[idx] = k;
            amounts[idx] = contents[k].Count;
            ++idx;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Rigidbody2D otherRb = col.GetComponent<Rigidbody2D>();
        LiquidParticle particle =  col.GetComponent<LiquidParticle>();
        if (particle != null)
        {
            particle.currentContainer = transform.parent;
            particle.rb2DParent = rigidBody;
            Add(particle);
           /* if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.time = 0.1f;
            }*/
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Rigidbody2D otherRb = col.GetComponent<Rigidbody2D>();
        LiquidParticle particle = col.GetComponent<LiquidParticle>();
        if (particle != null)
        {
            particle.currentContainer = null;
            particle.rb2DParent = null;
            Remove(particle);
        }
    }

    void Add(LiquidParticle particle)
    {
        if (!contents.ContainsKey(particle.liquidType))
        {
            contents[particle.liquidType] = new List<LiquidParticle>();
        }
        contents[particle.liquidType].Add(particle);
    }
    void Remove(LiquidParticle particle)
    {
        if (contents.ContainsKey(particle.liquidType))
        {
            contents[particle.liquidType].Remove(particle);
        }
    }
}
