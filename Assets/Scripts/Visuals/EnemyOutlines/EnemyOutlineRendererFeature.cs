using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyOutlineRendererFeature : ScriptableRendererFeature
{
    private EnemyOutlinePass enemyOutlinePass;
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (renderingData.cameraData.cameraType != CameraType.Game)
        return;

        renderer.EnqueuePass(enemyOutlinePass);
    }

    public override void Create()
    {
        enemyOutlinePass = new EnemyOutlinePass();
        enemyOutlinePass.renderPassEvent = RenderPassEvent.AfterRenderingSkybox;
    }
}
