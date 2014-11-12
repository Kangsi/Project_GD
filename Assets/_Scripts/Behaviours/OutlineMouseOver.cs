// John van den Berg

/*=============================================================================
	OutlineMouseOver - Behaviour
=============================================================================*/
using UnityEngine;
using System.Collections;
/**
 * Outline on Mouse Over
 * 
 * Behaviour class to outline an object on mouse over.
 */

public class OutlineMouseOver : MonoBehaviour
{

    private Shader[] shader;
    private Renderer[] render;

    void Awake()
    {
        render = GetComponentsInChildren<Renderer>();
        shader = new Shader[render.Length];
    }

    void Start()
    {
        for (int i = 0; i < render.Length; i++)
        {
            shader[i] = render[i].material.shader;
        }
    }

    public void changeShader()
    {
        for (int i = 0; i < render.Length; i++)
        {
            for (int y = 0; y < render[i].materials.Length; y++)
            {
                render[i].materials[y].shader = Shader.Find("Outline2/Silhouetted Diffuse");
            }            
        }
    }
    public void revertShader()
    {
        for (int i = 0; i < render.Length; i++)
        {
            for (int y = 0; y < render[i].materials.Length; y++)
            {
                render[i].materials[y].shader = shader[i];
            }
        }
    }

    void OnMouseOver()
    {
        changeShader();
    }
    void OnMouseExit()
    {
        revertShader();
    }
}
    
