之前看到一个制作场景动画过渡的[视频](https://www.bilibili.com/video/BV1Nu411d7Uk)，使用了屏幕后处理的方式，这里也实现了，但是后处理是无法影响到UI的，所以在实际实现的时候我是采用一个UIPanel覆盖所有UI面板，偷懒的话直接采用一个Animation实现，不偷懒的话也可以通过Shader实现。
<br>
不过视频中还是有帮助的，比如一个[程序生成的纹理网站](http://mebiusbox.github.io/contents/EffectTextureMaker/)。