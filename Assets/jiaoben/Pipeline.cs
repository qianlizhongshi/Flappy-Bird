using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pipeline : MonoBehaviour
{
    public Vector3 speed;
    public Vector2 range;
    public float totalTravelDistance;
    private float traveled = 0;
    void Start()
    {
        Init();
    }
    public void Init()
    {
        float y = Random.Range(range.x, range.y);
        // X=0，使用父物体预设出生位置
        this.transform.localPosition = new Vector3(0, y, 0);
        traveled = 0;

        // 新增：找到子物体ScoreArea，重置加分标记
        ScoreArea scoreArea = GetComponentInChildren<ScoreArea>();
        if (scoreArea != null)
        {
            scoreArea.scored = false;
        }
    }
    void Update()
    {
        float moveDelta = speed.x * Time.deltaTime;
        transform.localPosition += new Vector3(moveDelta, 0, 0);
        traveled += Mathf.Abs(moveDelta);
        // 走够距离：自己重置自己，不用通知管理器换别的管道
        if (traveled >= totalTravelDistance)
        {
            Init();
        }
    }
}
