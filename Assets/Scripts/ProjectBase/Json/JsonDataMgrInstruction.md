**使用插件**

JsonDataMgr内部使用的是[LitJson](https://github.com/LitJSON/litjson)

**注意：**

1. 不能直接将字符串转换为json，会报*JsonException: Can't add a value here*的错，虽然网上说是版本太低的问题，但是我之后用了最新0.17的LitJson生成的dll文件，还是会报错。
2. 注意要转换成Json的类需要字段都是Public的，不然转换后的字符串为空串。
3. 注意要转换成Json的类需要一个无参构造函数，不然在读取该类的json文件时会报*System.MissingMethodException: Default constructor not found for type People*的错。
