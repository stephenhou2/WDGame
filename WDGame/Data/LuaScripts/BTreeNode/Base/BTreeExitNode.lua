local cls_BTreeExitNode= classV2("BTreeExitNode","BTreeDecNode")
---@class BTreeExitNode:BTreeDecNode

function cls_BTreeExitNode.__initialize(self)
    self._node_name = "BTreeExitNode"
    self._nodeStack = {}
end

function cls_BTreeExitNode.Tick(self,deltaTime)
    if #self._nodeStack == 0 then
        Log.Error("cls_BTreeExitNode Check Error,no child node exist!")
        return false
    end

    local node = self._nodeStack[1]
    local ret = node:Tick(deltaTime)
    BTreeLog.LogNodeTickRet(node,ret)
    if ret == BTreeDef.STATUS_SUCCESS then
        return BTreeDef.STATUS_EXIT
    end

    return ret
end


return cls_BTreeExitNode