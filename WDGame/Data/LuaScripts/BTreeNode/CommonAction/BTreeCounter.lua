local cls_BTreeCounter= classV2("BTreeCounter","BTreeActionNode")
---@class BTreeCounter:BTreeActionNode

function cls_BTreeCounter.__initialize(self,targetNum)
    self._node_name = "BTreeCounter"
    self.targetNum = targetNum
    self.counter = 0
end

function cls_BTreeCounter.Tick(self)
    if self.counter < self.targetNum then
        self.counter = self.counter+1
        return BTreeDef.STATUS_RUNNING
    end
    
    return BTreeDef.STATUS_SUCCESS
end

function cls_BTreeCounter.__reset(self)
    self.counter = 0
    self._nodeStack = {}
end

return cls_BTreeCounter