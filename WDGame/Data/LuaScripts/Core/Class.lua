---@param className string @类名
---@param superName string @父类
---@return 类对象模板
--- 使用类名直接定义，则是类的静态方法和静态成员变量
--- 使用返回的类对象定义的，是类的成员方法和成员变量
--- new出来的是类的实例
--- 注意new出来的实例，需要自行初始化成员变量，否则所有实例共享类模板里的成员变量
--- 可以考虑new的时候对成员变量进行深copy（classV3）
function classV2(className,superName)
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
        instance.__initialize(instance,...)
        return instance
    end

    return cls
end