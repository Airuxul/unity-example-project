-- 加载常用模块
require("LuaUtils")
require("Adapter")

AppFacade = FrameWork.AppFacade.Instance

if AppFacade == nil then
    print("AppFacade is nil")
else
    print(LuaUtils.Table2Str(AppFacade))
end