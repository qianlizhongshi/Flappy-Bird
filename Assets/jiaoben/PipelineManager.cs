using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PipelineManager : MonoBehaviour
{
    public GameObject pipelineTemplate;
    public float moveSpeed;
    public float pipelineGap;
    List<Pipeline> pipelines = new List<Pipeline>();
    Coroutine runner = null;

    public void Init()
    {
        for (int i = 0; i < pipelines.Count; i++)
        {
            Destroy(pipelines[i].gameObject);
        }
        pipelines.Clear();
    }

    public void StartRun()
    {
        runner = StartCoroutine(SpawnAllPipeline());
    }

    public void Stop()
    {
        if (runner != null)
        {
            StopCoroutine(runner);
        }
        foreach (var p in pipelines)
        {
            p.enabled = false;
        }
    }

    // 只负责开局依次生成3根，之后不再管理管道
    IEnumerator SpawnAllPipeline()
    {
        for (int i = 0; i < 3; i++)
        {
            CreatePipeline();
            yield return new WaitForSeconds(pipelineGap / moveSpeed);
        }
    }

    void CreatePipeline()
    {
        if (pipelines.Count >= 3) return;
        GameObject obj = Instantiate(pipelineTemplate, transform);
        Pipeline p = obj.GetComponent<Pipeline>();
        p.speed = new Vector3(-moveSpeed, 0, 0);
        // totalTravelDistance：每根管道完整走完这段距离，自己回到起点
        p.totalTravelDistance = pipelineGap * 3;
        pipelines.Add(p);
    }
}
