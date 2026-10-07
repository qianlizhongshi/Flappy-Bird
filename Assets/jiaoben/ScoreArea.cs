using UnityEngine;
public class ScoreArea : MonoBehaviour
{
    public bool scored;
    public game gameCtrl;

    void Awake()
    {
        // 运行时自动在当前场景找到挂载game脚本的物体
        gameCtrl = FindObjectOfType<game>();
        if (gameCtrl == null)
        {
            Debug.LogError("Awake查找失败：场景没有挂载game脚本的物体！");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"【加分检测】进入触发器，碰撞物体：{other.gameObject.name}，Tag：{other.tag}");

        if (!scored && other.CompareTag("Player"))
        {
            Debug.Log($"【加分】满足条件！准备加分，当前scored={scored}");
            scored = true;
            if (gameCtrl != null)
            {
                Debug.Log($"【加分成功】调用AddScore(1)");
                gameCtrl.AddScore(1);
            }
            else
            {
                Debug.LogError($"【加分失败】gameCtrl 是空！");
            }
        }
        else
        {
            Debug.Log($"【跳过加分】scored={scored} 或者 Tag不是Player");
        }
    }
}
