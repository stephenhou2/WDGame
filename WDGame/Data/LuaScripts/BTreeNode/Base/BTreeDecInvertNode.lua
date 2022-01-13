local cls_BTreeDecInvertNode= classV2("BTreeDecInvertNode","BTreeDecNode")
---@class BTreeDecInvertNode:BTreeDecNode

function cls_BTreeDecInvertNode.__initialize(self)
    self._node_name = "BTreeDecInvertNode"
    self._nodeStack = {}
end

function cls_BTreeDecInvertNode.Tick(self,deltaTime)
    if #self._nodeStack == 0 then
        Log.Error("BTreeDecInvertNode Check Error,no leaf node exist!")
        return false
    end

    local node = self._nodeStack[1]
    local ret = node:Tick(deltaTime)
    BTreeLog.LogNodeTickRet(node,ret)
    if ret == BTreeDef.STATUS_EXIT then
        return BTreeDef.STATUS_EXIT
    elseif ret == BTreeDef.STATUS_SUCCESS then
        return BTreeDef.STATUS_FAILURE
    elseif ret == BTreeDef.STATUS_RUNNING then
        return BTreeDef.STATUS_RUNNING
    elseif ret == BTreeDef.STATUS_FAILURE then
        return BTreeDef.STATUS_SUCCESS
    end

    Log.Error("BTreeDecInvertNode Tick Error,undefined tick result!")
    return BTreeDef.STATUS_FAILURE
end

return cls_BTreeDecInvertNode