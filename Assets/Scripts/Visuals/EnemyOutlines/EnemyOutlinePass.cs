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
        if (maskMaterial == null)
            return;

        var enemy = Object.FindFirstObjectByType<BaseEnemy>();
        if (enemy == null)
            return;

        var enemyRenderer = enemy.GetComponentInChildren<MeshRenderer>();
        if (enemyRenderer == null)
            return;

        var resources = frameData.Get<UniversalResourceData>();
        var maskDescription = renderGraph.GetTextureDesc(resources.activeColorTexture);

        maskDescription.name = "Enemy Outline Mask";
        maskDescription.depthBufferBits = DepthBits.None;
        maskDescription.msaaSamples = MSAASamples.None;
        maskDescription.bindTextureMS = false;
        maskDescription.clearBuffer = true;
        maskDescription.clearColor = Color.black;

        var maskTexture = renderGraph.CreateTexture(maskDescription);

        using (var builder = renderGraph.AddRasterRenderPass<PassData>(
            "Enemy Mask Test", out var passData))
        {
            passData.enemyRenderer = enemyRenderer;
            passData.material = maskMaterial;

            builder.SetRenderAttachment(maskTexture, 0, AccessFlags.Write);
            builder.AllowPassCulling(false); //Temporary

            builder.SetRenderFunc(
                static (PassData data, RasterGraphContext context) =>
                {
                    context.cmd.DrawRenderer(
                        data.enemyRenderer, data.material, 0, 0);
                });
        }
    }

    private class PassData
    {
        public Renderer enemyRenderer;
        public Material material;
    }
}