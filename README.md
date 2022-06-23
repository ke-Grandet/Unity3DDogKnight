<img width='720' src='https://raw.githubusercontent.com/ke-Grandet/Unity2DPixelAdventurer/master/Screenshot/Png/MainMenu.png' />

## 说明
* 是一个2D横版动作游戏，玩家有二段跳和三段攻击
* 设计了行为不同的四种敌人：跳跃的青蛙，绕行浮岛的负鼠，俯冲的鹰和巡逻的骷髅；骷髅通过状态模式实现，其它三个通过动画状态机实现；巡逻点由空物体设置
* 提取敌人受击和伤害玩家的行为作为公共组件，并创建敌人父类，以此与玩家交互
* 有分数奖励和生命值奖励两种物品，调用单例管理器更新面板数值
* UGUI显示玩家血量、奖励分数和游戏菜单
* 协程异步加载场景

## 0.2更新
### 0.2.0
* √ 加入主菜单和暂停菜单

## 0.1更新
### 0.1.0
* √ 初始版本

## 截图
<img width='720' src='https://raw.githubusercontent.com/ke-Grandet/Unity2DPixelAdventurer/master/Screenshot/Png/PauseMenu.png' />
<img width='720' src='https://raw.githubusercontent.com/ke-Grandet/Unity2DPixelAdventurer/master/Screenshot/Png/TipSign.png' />
<img width='720' src='https://raw.githubusercontent.com/ke-Grandet/Unity2DPixelAdventurer/master/Screenshot/Png/Attack2.png' />
<img width='720' src='https://raw.githubusercontent.com/ke-Grandet/Unity2DPixelAdventurer/master/Screenshot/Png/Attack1.png' />
<img width='720' src='https://raw.githubusercontent.com/ke-Grandet/Unity2DPixelAdventurer/master/Screenshot/Png/Hurt.png' />
<img width='720' src='https://raw.githubusercontent.com/ke-Grandet/Unity2DPixelAdventurer/master/Screenshot/Png/Door.png' />