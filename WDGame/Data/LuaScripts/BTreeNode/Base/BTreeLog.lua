BTreeLog = {}

function BTreeLog.Log(str,color)
    if color == nil then
        color = "white"
    end

    -- local newStr = debug.traceback(tostring(str), 2)
    -- if BTreeDef.LogEnable then
    --     Util.EditorLogDebug(string.format("<color=%s>%s</color>",color,tostring(newStr)))
    -- end

    if BTreeDef.LogEnable then
        Util.EditorLogDebug(string.format("<color=%s>%s</color>",color,tostring(str)))
    end
end

local logicNodeTypes = 
{
    [BTreeDef.BTREE_NODE_COMPOSITE] = true,
    [BTreeDef.BTREE_NODE_DECORATE] = true,
    [BTreeDef.BTREE_NODE_SELECT] = true,
    [BTreeDef.BTREE_NODE_PARALLEL] = true,
}

function BTreeLog.LogNodeTickRet(node,ret)
    if not BTreeDef.LogEnable then
        return
    end

    if node == nil then
        Log.Error("LogNodeTickRet Error,node is nil")
    end

    local nodeType = node:GetNodeType()
    if BTreeDef.LogicNodeLog and logicNodeTypes[nodeType] == true then
        BTreeLog.Log("BTreeLog Tick Logic Node:" ..tostring(node:GetNodeName()) ..",ret:" ..tostring(ret),"blue")
    end

    if BTreeDef.ActionNodeLog and nodeType == BTreeDef.BTREE_NODE_ACTION then
        BTreeLog.Log("BTreeLog Tick Action Node:" ..tostring(node:GetNodeName()) ..",ret:" ..tostring(ret))
    end
end

return BTreeLog