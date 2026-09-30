using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

public class EnemyOutlinePass : ScriptableRenderPass
{
    private readonly Material outlineMaterial;
    private readonly Material maskMaterial;

    public EnemyOutlinePass(Material outlineMaterial, Material maskMaterial)
    {
        this.outlineMaterial = outlineMaterial;
        this.maskMaterial = maskMaterial;
    }

    public override void RecordRenderGraph(
        RenderGraph renderGraph, ContextContainer frameData)
    {
        // We'll describe our drawing operations here.
    }
}