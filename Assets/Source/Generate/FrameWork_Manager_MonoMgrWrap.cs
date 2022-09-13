﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class FrameWork_Manager_MonoMgrWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(FrameWork.Manager.MonoMgr), typeof(FrameWork.Manager.BaseManager));
		L.RegFunction("AddUpdateListener", AddUpdateListener);
		L.RegFunction("RemoveUpdateListener", RemoveUpdateListener);
		L.RegFunction("StartCoroutine", StartCoroutine);
		L.RegFunction("StopCoroutine", StopCoroutine);
		L.RegFunction("StopAllCoroutines", StopAllCoroutines);
		L.RegFunction("Destroy", Destroy);
		L.RegFunction("New", _CreateFrameWork_Manager_MonoMgr);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateFrameWork_Manager_MonoMgr(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				FrameWork.Manager.MonoMgr obj = new FrameWork.Manager.MonoMgr();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: FrameWork.Manager.MonoMgr.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddUpdateListener(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			FrameWork.Manager.MonoMgr obj = (FrameWork.Manager.MonoMgr)ToLua.CheckObject<FrameWork.Manager.MonoMgr>(L, 1);
			UnityEngine.Events.UnityAction arg0 = (UnityEngine.Events.UnityAction)ToLua.CheckDelegate<UnityEngine.Events.UnityAction>(L, 2);
			obj.AddUpdateListener(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveUpdateListener(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			FrameWork.Manager.MonoMgr obj = (FrameWork.Manager.MonoMgr)ToLua.CheckObject<FrameWork.Manager.MonoMgr>(L, 1);
			UnityEngine.Events.UnityAction arg0 = (UnityEngine.Events.UnityAction)ToLua.CheckDelegate<UnityEngine.Events.UnityAction>(L, 2);
			obj.RemoveUpdateListener(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StartCoroutine(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes<System.Collections.IEnumerator>(L, 2))
			{
				FrameWork.Manager.MonoMgr obj = (FrameWork.Manager.MonoMgr)ToLua.CheckObject<FrameWork.Manager.MonoMgr>(L, 1);
				System.Collections.IEnumerator arg0 = (System.Collections.IEnumerator)ToLua.ToObject(L, 2);
				UnityEngine.Coroutine o = obj.StartCoroutine(arg0);
				ToLua.PushSealed(L, o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes<string>(L, 2))
			{
				FrameWork.Manager.MonoMgr obj = (FrameWork.Manager.MonoMgr)ToLua.CheckObject<FrameWork.Manager.MonoMgr>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				UnityEngine.Coroutine o = obj.StartCoroutine(arg0);
				ToLua.PushSealed(L, o);
				return 1;
			}
			else if (count == 3)
			{
				FrameWork.Manager.MonoMgr obj = (FrameWork.Manager.MonoMgr)ToLua.CheckObject<FrameWork.Manager.MonoMgr>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				object arg1 = ToLua.ToVarObject(L, 3);
				UnityEngine.Coroutine o = obj.StartCoroutine(arg0, arg1);
				ToLua.PushSealed(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: FrameWork.Manager.MonoMgr.StartCoroutine");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopCoroutine(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			FrameWork.Manager.MonoMgr obj = (FrameWork.Manager.MonoMgr)ToLua.CheckObject<FrameWork.Manager.MonoMgr>(L, 1);
			UnityEngine.Coroutine arg0 = (UnityEngine.Coroutine)ToLua.CheckObject(L, 2, typeof(UnityEngine.Coroutine));
			obj.StopCoroutine(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopAllCoroutines(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			FrameWork.Manager.MonoMgr obj = (FrameWork.Manager.MonoMgr)ToLua.CheckObject<FrameWork.Manager.MonoMgr>(L, 1);
			obj.StopAllCoroutines();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Destroy(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			FrameWork.Manager.MonoMgr obj = (FrameWork.Manager.MonoMgr)ToLua.CheckObject<FrameWork.Manager.MonoMgr>(L, 1);
			obj.Destroy();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}
