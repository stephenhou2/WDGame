local cls_BTreeParallelNode = classV2("BTreeParallelNode","BTreeNode")
---@class BTreeParallelNode:BTreeNode

cls_BTreeParallelNode._type = BTreeDef.BTREE_NODE_PARALLEL

function cls_BTreeParallelNode.Tick(self,deltaTime)
    local allSucc = true
    for k,v in ipairs(self._nodeStack) do
        local ret = v:Tick(deltaTime)
        BTreeLog.LogNodeTickRet(v,ret)
        if ret == BTreeDef.STATUS_EXIT then
            return BTreeDef.STATUS_EXIT
        end

        if ret ~= BTreeDef.STATUS_SUCCESS then
            allSucc = false
        end
    end

    if allSucc then
        return BTreeDef.STATUS_SUCCESS
    else
        return BTreeDef.STATUS_FAILURE
    end
end

function cls_BTreeParallelNode.__initialize(self,tickType)
    self._node_name = "BTreeParallelNode"
    self._nodeStack = {}
end

return cls_BTreeParallelNode