using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SceneIlluminationController : MonoBehaviour {
    public Transform submarine; //NRC: todo: Get from static class element
    public float surface = 0;
    public float bottom = -30;
    
        
    [Space(10)]
    [Header("Caustic reflection Color:")]
    public Material causticShader;
    public Color caustic_surface_color = new Color32(136, 201, 255, 1);
    public Color caustic_bottom_color = new Color32(255, 244, 214, 1);

    
    [Space(10)]
    [Header("Global Light intensity:")]
    public Light global_light;
    public float start_gl_intensity = 1.5f;
    public float end_gl_intensity = 0.02f;
    
    [Space(10)]
    [Header("Global Light Color:")]
    public Color gl_surface_color = new Color32(136, 201, 255, 1);
    public Color gl_bottom_color = new Color32(255, 244, 214, 1);
    
    [Space(10)]
    [Header("Ambient Light Color:")]
    public Color ambient_surface_color = new Color32(13, 63, 207, 1);
    public Color ambient_bottom_color = new Color32(20, 59, 106, 1);
    
    [Space(10)]
    [Header("Fog Color:")]
    public Color fog_surface_color = new Color32(71, 116, 168, 1);
    public Color fog_bottom_color = new Color32(3, 19, 38, 1);
    
    [Space(10)]
    [Header("Background Color:")]
    public Color bg_surface_color = new Color32(97, 138, 200,1);
    public Color bg_bottom_color = new Color32(3, 19, 38, 1);

    private Camera cam;
    
    void Start() {
        cam = Camera.main;
    }

    private float new_depth = 65445;
    void Update() {
        float depth = Mathf.InverseLerp(surface, bottom, submarine.position.y);
        
        if (depth == new_depth) return;

        new_depth = depth;
        global_light.intensity = Mathf.Lerp(start_gl_intensity, end_gl_intensity, depth);
        global_light.color = Color.Lerp(gl_surface_color, gl_bottom_color, depth);
        RenderSettings.ambientLight = Color.Lerp(ambient_surface_color, ambient_bottom_color, depth);
        RenderSettings.fogColor = Color.Lerp(fog_surface_color, fog_bottom_color, depth);
        cam.backgroundColor = Color.Lerp(bg_surface_color, bg_bottom_color, depth);
        causticShader.SetColor("_Color", Color.Lerp(caustic_surface_color, caustic_bottom_color, depth)); 
    }
}
