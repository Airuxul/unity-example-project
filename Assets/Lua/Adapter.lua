local IsSubClassOf = Utility.Utils.IsSubClassOf
local Component = UnityEngine.Component
local GameObject = UnityEngine.GameObject
local LuaBehaviour = GameApp.ToLuaExpand.LuaBehaviour 
local Table2Str = LuaUtils.Table2Str

Adapter={}

local this = Adapter
this.objs = {}

function Adapter.Init(binder, luaPath)
    local tbl = require(luaPath)
    tbl.__binder = binder
    
    if binder ~= nil and IsSubClassOf(binder, typeof(Component)) then
        tbl.gameObject = binder.gameObject
        tbl.transform = tbl.gameObject.transform
    end
    
    this.objs[binder:GetInstanceID()] = tbl 
    this.LoadData(tbl)
end

function Adapter.LoadData(luaCls)
    -- AbstractValue IEnumerator
    local iter = luaCls.__binder:GetValueEnumerator()
    while iter:MoveNext() do
        luaCls[iter.Current.name] = iter.Current.value
    end
end

function Adapter.Call(obj, method, ...)
    local tbl = this.objs[obj:GetInstanceID()]
    if tbl == nil then return end
    local func = tbl[method]

    if func then func(tbl, ...) end

    if method == "OnDestroy" then
        tbl.__binder = nil
        tbl.args = nil
        tbl.gameObject = nil
        tbl.transform = nil
        this.objs[obj:GetInstanceID()] = nil
    elseif method == "OnEnable" then
        this.RegisterUpdate(tbl)
    elseif method == "OnDisable" then
        this.DeregisterUpdate(tbl)
    end
end

function Adapter.RegisterUpdate(tbl)
    if tbl.Update then
        tbl.__updateHandle = UpdateBeat:CreateListener(tbl.Update, tbl)
        UpdateBeat:AddListener(tbl.__updateHandle)
    end
    if tbl.LateUpdate then
        tbl.__lateUpdateHandle = UpdateBeat:CreateListener(tbl.LateUpdate, tbl)
        LateUpdate:AddListener(tbl.__lateUpdateHandle)
    end
end

function Adapter.DeregisterUpdate(tbl)
    if tbl.Update then
        UpdateBeat:RemoveListener(tbl.__updateHandle)
        tbl.__updateHandle = nil
    end
    if tbl.LateUpdate then
        UpdateBeat:RemoveListener(tbl.__lateUpdateHandle)
        tbl.__lateUpdateHandle = nil
    end
end


-- 获取GameObject挂载的LuaBinder
function Adapter.GetLuaBinder(obj)
    if obj ~=nil and obj:GetType() == typeof(GameObject) then
        obj = obj:GetComponent(typeof(LuaBehaviour))
    end
    
    if obj == nil then return end
    
    local id = obj:GetInstanceID()
    if this.objs[id] == nil then
        obj:Init()
    end
    return this.objs[id]
end

return Adapter