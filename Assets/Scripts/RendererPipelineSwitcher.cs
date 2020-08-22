using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RendererPipelineSwitcher : MonoBehaviour
{
    public RenderPipelineAsset URPAsset;
    public bool useURP;

    private void Awake()
    {
        if (this.useURP)
        {
            GraphicsSettings.renderPipelineAsset = URPAsset;
        }
        else
        {
            GraphicsSettings.renderPipelineAsset = null;
        }
    }
}
