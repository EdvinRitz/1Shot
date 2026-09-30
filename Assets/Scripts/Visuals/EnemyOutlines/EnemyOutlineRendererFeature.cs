using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyOutlineRendererFeature : ScriptableRendererFeature
{
    private EnemyOutlinePass enemyOutlinePass;
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private Material maskMaterial;
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (!AimMode.XRayActive)
        return;
        
        if (renderingData.cameraData.cameraType != CameraType.Game)
        return;

        renderer.EnqueuePass(enemyOutlinePass);
    }

    public override void Create()
    {
        enemyOutlinePass = new EnemyOutlinePass(outlineMaterial, maskMaterial);
        enemyOutlinePass.renderPassEvent = RenderPassEvent.AfterRenderingSkybox;
    }
}
