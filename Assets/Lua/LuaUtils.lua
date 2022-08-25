LuaUtils = {}

local function table2str_recur(tab, prevBlankSpace, blankSpace)
    if nil == tab then
        return "nil"
    end

    if type(tab) ~= 'table' then
        return tostring(tab)
    end

    local result = "{\n"
    for k, v in pairs(tab) do
        local kStr = blankSpace .. table2str_recur(k, blankSpace, blankSpace .. "\t")
        local vStr = table2str_recur(v, blankSpace, blankSpace .. "\t") or "nil"

        if type(v) == 'string' then
            result = result .. kStr .. " = \"" .. vStr .. "\",\n"
        else
            result = result .. kStr .. " = " .. vStr .. ",\n"
        end
    end

    return result .. prevBlankSpace .. "}"
end

--table to string
function LuaUtils.Table2Str(mTable)
    return table2str_recur(mTable, "", "")
end

return LuaUtils