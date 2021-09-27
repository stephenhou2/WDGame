---@param className string @类名
---@param superName string @父类
function class(className,superName)
    if type(className) ~= "string" or className == "" then
        Log.Error("Create class Fail,class name invalid")
        return nil
    end

    if _G[className] ~= nil then
        Log.Error(string.format("Redefine class \'%s\'",className))
        return nil
    end

    local super_cls = nil
    if type(superName) == "string" then
        local _super = _G[superName]
        if _super == nil then
            Log.Error(string.format("Create class Exception,super class \'%s\' not find",superName))
        else
            super_cls = _super.__cls
        end
    end
    
    local cls = setmetatable({},{__index=super_cls})
    cls.__name=className
    cls.__super = super_cls

    local cls_static = {}
    cls_static.__name=className
    cls_static.__cls=cls
    _G[className] = cls_static

    cls_static.New = function(...)
        local instance = setmetatable({},{__index=cls})
        return instance
    end

    return cls
end