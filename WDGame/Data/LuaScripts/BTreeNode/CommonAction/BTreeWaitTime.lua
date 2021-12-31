local cls_BTreeWaitTime= classV2("BTreeWaitTime","BTreeActionNode")
---@class BTreeWaitTime:BTreeActionNode

function cls_BTreeWaitTime.Tick(self,deltaTime)
    self.timeEclipse = self.timeEclipse + deltaTime

    if self.timeEclipse < self.waitTime then
        return BTreeDef.STATUS_RUNNING
    end

    return BTreeDef.STATUS_SUCCESS
end

function cls_BTreeWaitTime.__initialize(self,waitTime)
    self._node_name = "BTreeWaitTime"
    self.waitTime = waitTime
    self.timeEclipse = 0
    self._nodeStack = {}
end

function cls_BTreeWaitTime.__reset(self)
    self.timeEclipse = 0
end

return cls_BTreeWaitTime