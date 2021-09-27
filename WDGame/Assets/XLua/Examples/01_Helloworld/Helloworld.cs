/*
 * Tencent is pleased to support the open source community by making xLua available.
 * Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
 * Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
*/

using UnityEngine;
using XLua;
using System.Diagnostics;

namespace XLuaTest
{
    public class Helloworld : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            LuaEnv luaenv = new LuaEnv();
            var watch = Stopwatch.StartNew();
            //luaenv.DoString("CS.UnityEngine.Debug.Log('hello world')");
            //            luaenv.DoString(@"
            //local a = 1
            //for i=1,200000 do
            //    a = a + 1
            //end");
            int a = 0;
            for(int i =0;i< 200000;i++)
            {
                a++;
            }
            watch.Stop();
            UnityEngine.Debug.Log("time eclipse:" + watch.Elapsed.TotalMilliseconds);
            luaenv.Dispose();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
