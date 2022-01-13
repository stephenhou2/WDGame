local cls_BTreeSelectNode = classV2("BTreeSelectNode","BTreeNode")
---@class BTreeSelectNode:BTreeNode

cls_BTreeSelectNode._type = BTreeDef.BTREE_NODE_SELECT

function cls_BTreeSelectNode.__initialize(self,tickType)
    self._tick_type = tickType or BTreeDef.BTree_TICK_SEQUENCE
    self._node_name = "BTreeSelectNode"
    self._nodeStack = {}
end

function cls_BTreeSelectNode.Tick_Select(self,deltaTime)
    for k,v in ipairs(self._nodeStack) do
        local ret = v:Tick(deltaTime)
        BTreeLog.LogNodeTickRet(v,ret)
        if ret == BTreeDef.STATUS_EXIT then
            return BTreeDef.STATUS_EXIT
        elseif ret == BTreeDef.STATUS_SUCCESS then
            return BTreeDef.STATUS_SUCCESS
        elseif ret == BTreeDef.STATUS_RUNNING then
            return BTreeDef.STATUS_RUNNING
        end
    end

    return BTreeDef.STATUS_FAILURE
end


function cls_BTreeSelectNode.Tick(self,deltaTime)
    if self._tick_type == BTreeDef.BTree_TICK_SEQUENCE then
        return self.Tick_Select(self,deltaTime)
    elseif self._tick_type == BTreeDef.BTree_TICK_RANDOM then
        self.Shuffle(self)
        return self.Tick_Select(self,deltaTime)
    end

    return BTreeDef.STATUS_SUCCESS
end

return cls_BTreeSelectNode