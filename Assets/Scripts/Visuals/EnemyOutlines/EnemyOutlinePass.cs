using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

public class EnemyOutlinePass : ScriptableRenderPass
{
    private readonly Material outlineMaterial;
    private readonly Material maskMaterial;
    private static readonly int maskTextureId = Shader.PropertyToID("_EnemyOutlineMask");

    public EnemyOutlinePass(Material outlineMaterial, Material maskMaterial)
    {
        this.outlineMaterial = outlineMaterial;
        this.maskMaterial = maskMaterial;
    }

    public override void RecordRenderGraph(
        RenderGraph renderGraph, ContextContainer frameData)
    {
        if (maskMaterial == null || outlineMaterial == null)
            return;

        var enemies = Object.FindObjectsByType<BaseEnemy>(FindObjectsSortMode.None);
        var resources = frameData.Get<UniversalResourceData>();

        foreach (var enemy in enemies)
        {
            var enemyRenderer = enemy.GetComponentInChildren<MeshRenderer>();
            if (enemyRenderer == null)
                continue;

            var maskDescription = renderGraph.GetTextureDesc(resources.activeColorTexture);
            maskDescription.name = $"Enemy Outline Mask - {enemy.name}";
            maskDescription.depthBufferBits = DepthBits.None;
            maskDescription.msaaSamples = MSAASamples.None;
            maskDescription.bindTextureMS = false;
            maskDescription.clearBuffer = true;
            maskDescription.clearColor = Color.black;

            var maskTexture = renderGraph.CreateTexture(maskDescription);

            using (var builder = renderGraph.AddRasterRenderPass<PassData>(
                $"Enemy Mask - {enemy.name}", out var maskPassData))
            {
                maskPassData.enemyRenderer = enemyRenderer;
                maskPassData.material = maskMaterial;

                builder.SetRenderAttachment(maskTexture, 0, AccessFlags.Write);
                builder.SetGlobalTextureAfterPass(maskTexture, maskTextureId);
                builder.AllowPassCulling(false); // Temporary while developing.

                builder.SetRenderFunc(
                    static (PassData data, RasterGraphContext context) =>
                    {
                        context.cmd.DrawRenderer(
                            data.enemyRenderer, data.material, 0, 0);
                    });
            }

            using (var builder = renderGraph.AddRasterRenderPass<PassData>(
                $"Enemy Outline - {enemy.name}", out var outlinePassData))
            {
                outlinePassData.enemyRenderer = enemyRenderer;
                outlinePassData.material = outlineMaterial;

                builder.UseGlobalTexture(maskTextureId, AccessFlags.Read);
                builder.SetRenderAttachment(
                    resources.activeColorTexture, 0, AccessFlags.Write);

                builder.SetRenderFunc(
                    static (PassData data, RasterGraphContext context) =>
                    {
                        context.cmd.DrawRenderer(
                            data.enemyRenderer, data.material, 0, 0);
                    });
            }
        }
    }

    private class PassData
    {
        public Renderer enemyRenderer;
        public Material material;
    }
}
