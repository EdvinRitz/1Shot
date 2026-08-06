using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int playerScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void addScoreForCompletedWave(int waveNumber)
    {
        playerScore += waveNumber*100;
    }
    public void addScoreForEnemiesShot(int enemiesKilled)
    {
        int killScore = enemiesKilled * 100;
        int comboBonus = (enemiesKilled - 1) * enemiesKilled / 2 * 100;

        playerScore += killScore + comboBonus;
    }
}
