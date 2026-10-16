# Flappy Bird · Unity 复刻

基于 **Unity + C#** 复刻的 Flappy Bird 横版飞行游戏。

## 玩法

- **鼠标左键**点击 → 小鸟获得一个向上的冲量，松开后受重力下落
- 穿过一组管道 **+1 分**
- 撞到管道或地面 → 游戏结束
- 结算界面显示本局分数，点击「重新开始」再来一局

## 技术要点

| 模块 | 实现 |
| --- | --- |
| 飞行手感 | `Rigidbody2D` 重力 + 点击时**先清零速度再 `AddForce` 向上**，避免连续点击把速度叠加起来 |
| 对象复用 | 开局只 `Instantiate` 3 根管道，之后**每根走完固定距离就自己复位到起点**，运行期间不再创建销毁 |
| 计分去重 | 管道复位时一并重置子物体 `ScoreArea` 的 `scored` 标记，保证同一根管道只加一次分 |
| 状态管理 | `Ready / IsGame / GameOver` 三态状态机，驱动三个 UI 面板的显隐切换 |
| 事件驱动 | `Player` 用 C# `event`（`OnDeath`）通知游戏层，游戏层不需要每帧轮询玩家状态 |
| 碰撞过滤 | 死亡判定改用 `OnTriggerEnter2D` + **Tag 过滤**：只有 `Pipe` 标签（管道、地面）才判定死亡，计分触发器用 `Untagged` 区分 |
| 动画 | `Animator` 触发器驱动 `Idle / Fly` 两种状态，待机时关闭 `Rigidbody2D.simulated` |
| UI | TextMeshPro，游戏内实时分数 + 结算分数 |

## 环境

- **Unity 2021.3.45f2c1**
- 依赖：TextMeshPro

## 运行

1. 用 Unity Hub 以 **2021.3.x** 打开本工程
2. 打开场景 `Assets/Scenes/SampleScene.unity`
3. 点击 **Play**，鼠标左键点击开始

## 目录结构

```
Assets/
├─ jiaoben/                    项目脚本
│  ├─ game.cs                  游戏状态机、计分、UI 面板切换
│  ├─ Player.cs                小鸟物理、点击飞行、死亡事件
│  ├─ Pipeline.cs              单根管道的移动与自复位
│  ├─ PipelineManager.cs       开局生成 3 根管道与启停控制
│  └─ ScoreArea.cs             管道计分触发器
├─ Sprites/                    美术资源
│  ├─ FlappyBird Art Resources
│  ├─ CrazyBird Art Resources
│  └─ flappy bird
└─ Scenes/SampleScene.unity
```

## 可调参数

| 脚本 | 参数 |
| --- | --- |
| `Player` | 点击冲量 `force` (100) |
| `PipelineManager` | 移动速度 `moveSpeed`、管道间距 `pipelineGap` |
| `Pipeline` | 随机高度范围 `range` |
| `game` | 三态状态机、分数文本引用、三个 UI 面板引用 |

> 场景里的 `Player`、`game`、`PipelineManager` 通过 Inspector 拖拽接线；`ScoreArea` 靠
> **Tag = `Player`** 识别小鸟、**Tag = `Pipe`** 判定死亡，改动物体标签时需同步检查。
