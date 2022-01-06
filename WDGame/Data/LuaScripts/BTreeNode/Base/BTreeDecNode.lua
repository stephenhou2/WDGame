local cls_BTreeDecNode= classV2("BTreeDecNode","BTreeNode")
---@class BTreeDecNode:BTreeNode

cls_BTreeDecNode._type = BTreeDef.BTREE_NODE_DECORATE

function cls_BTreeDecNode.__initialize(self,...)
    self._node_name = "BTreeDecNode"
    self._nodeStack = {}
end

function cls_BTreeDecNode.PushToStack(self,node)
    if nil == node then
        Log.Error("PushToStack Failed,push nil node")
        return
    end

    if not node._isBTreeNode then
        Log.Error("PushToStack Failed,push invalid type node, current node name:" ..tostring(self._node_name) ..",push node name:" ..tostring(node._node_name))
        return
    end

    if #self._nodeStack > 0 then
        Log.Error("PushToStack Failed,BTreeDecNode can only add one child node, current node name:" ..tostring(self._node_name) ..",push node name:" ..tostring(node._node_name))
        return
    end

    table.insert(self._nodeStack,node)
    BTreeLog.Log("PushToStack Succeed, current node name:" ..tostring(self._node_name) ..",push node name:" ..tostring(node._node_name))
end


return cls_BTreeDecNode