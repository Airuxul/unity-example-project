DelayMgr方便使用延时功能，在平时使用延时功能的时候一般都会需要创建协程方法或者其他方式，都会比较麻烦，通过DelayMgr就会比较方便。
<br>
**优化：**
1. 查看网上（2015年）说UnityAction消耗的性能很多，之后进行一些测试后再考虑是否要将此进行优化（√，具体查看我的博客的一篇[文章](https://yuzurihainori.top/unity/C-%E5%92%8CUnity%E7%9A%84%E4%BA%8B%E4%BB%B6%E5%8C%BA%E5%88%AB.html)
2. 修改UnityAction的逻辑，避免过多生成UnityAction