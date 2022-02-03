MonoMgr是解决Unity的MonoBehaviour消耗问题，有些类偶尔需要使用MonoBehaviour的方法，但是直接继承又会浪费很多性能，通过MonoMgr使用就会节约这部分的性能。
<br>
MonoMgr和很多框架中的类绑定
1. DelayMgr，需要使用到协程方法。
2. ResMgr，需要使用到异步加载资源
3. ScenesMgr，需要使用到异步加载场景