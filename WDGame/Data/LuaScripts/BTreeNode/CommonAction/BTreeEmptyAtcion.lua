local cls_BTreeEmptyAtcion= classV2("BTreeEmptyAtcion","BTreeActionNode")
---@class BTreeEmptyAtcion:BTreeActionNode

function cls_BTreeEmptyAtcion.__initialize(self,targetNum)
    self._node_name = "BTreeEmptyAtcion"
end

function cls_BTreeEmptyAtcion.Tick(self)
    return BTreeDef.STATUS_SUCCESS
end

return cls_BTreeEmptyAtcion