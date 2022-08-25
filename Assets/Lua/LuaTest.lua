LuaTest = {}

local this = LuaTest
this.awakeStr = "LuaTest Awake str"
this.startStr = "LuaTest Start str"
this.person={
    name="xiaoming",
    age=15,
    selfFunc= function() end,
    parent={
        mother = "mom",
        father = "dad"
    }
}

function LuaTest:Awake()
    print(this.awakeStr)
end

function LuaTest:Start()
    print(this.startStr)
end

function LuaTest:Update()
    print("Updating")
end

return this