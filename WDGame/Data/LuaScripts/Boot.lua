require("Log")
local loader = require("Loader")

local ManifestFileName = "Manifest"

_G.RegisterLuaFile = function(parentPath,fileTb)
    if type(fileTb) ~= "table" then
        return
    end

    for k,v in pairs(fileTb) do
        if type(k) == "string" and type(v) == "table" then
            RegisterLuaFile(string.format("%s\\%s",parentPath,k),v)
        elseif type(k) == "number" and type(v) == "string" then
            if v ~= ManifestFileName then
                loader.LoadLuaFile(string.format("%s\\%s",parentPath,v))
            end
        else
            Log.Error(ErrorLevel.Fatal,string.format("RegisterLuaFile Error on load \'%s\', table key type: \'%s\' ",type(k),tostring(v)))
        end
    end
end


require("Core/Manifest")
require("Define/Manifest")

