local cls_BTreeActionNode= classV2("BTreeActionNode","BTreeNode")
---@class BTreeActionNode:BTreeNode

cls_BTreeActionNode._type = BTreeDef.BTREE_NODE_ACTION

function cls_BTreeActionNode.__initialize(self,...)
    self._node_name = "BTreeActionNode"
end

function cls_BTreeActionNode.Tick(self)
end

return cls_BTreeActionNode

