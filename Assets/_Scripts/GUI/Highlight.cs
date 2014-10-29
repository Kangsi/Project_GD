// John van den Berg

/*=============================================================================
	Highlight
=============================================================================*/
using UnityEngine;
using System.Collections;
/**
 * The Soulframe class 
 * 
 * User Interface class which handles a shader switch when the mouse
 * is over the gameobject to which this script is attached. 
 */

/// <summary>
/// TODO: 
/// Rewrite the Self-Illumin/Diffuse shader to a Fresnel Glow Shader
/// </summary>
public class Highlight : MonoBehaviour {

    private Shader shader;

    void Awake()
    {
        shader = renderer.material.shader;
    }

	public void changeShader()
    {
        for (int i = 0; i < renderer.materials.Length; i++)
        {
            renderer.materials[i].shader = Shader.Find("Self-Illumin/Diffuse");
        }
    }
    public void revertShader()
    {
        for (int i = 0; i < renderer.materials.Length; i++)
        {
            renderer.materials[i].shader = shader;
        }        
    }
}
