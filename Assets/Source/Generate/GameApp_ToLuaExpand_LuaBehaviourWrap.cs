﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using GameApp.ToLuaExpand;
using LuaInterface;

public class GameApp_ToLuaExpand_LuaBehaviourWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(GameApp.ToLuaExpand.LuaBehaviour), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("CallLuaFunc", CallLuaFunc);
		L.RegFunction("Init", Init);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("LuaPath", get_LuaPath, null);
		L.RegVar("Val", get_Val, null);
		L.RegVar("HaveInit", get_HaveInit, set_HaveInit);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CallLuaFunc(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			GameApp.ToLuaExpand.LuaBehaviour obj = (GameApp.ToLuaExpand.LuaBehaviour)ToLua.CheckObject<GameApp.ToLuaExpand.LuaBehaviour>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			obj.CallLuaFunc(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			GameApp.ToLuaExpand.LuaBehaviour obj = (GameApp.ToLuaExpand.LuaBehaviour)ToLua.CheckObject<GameApp.ToLuaExpand.LuaBehaviour>(L, 1);
			obj.Init();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LuaPath(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameApp.ToLuaExpand.LuaBehaviour obj = (GameApp.ToLuaExpand.LuaBehaviour)o;
			string ret = obj.LuaPath;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index LuaPath on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Val(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameApp.ToLuaExpand.LuaBehaviour obj = (GameApp.ToLuaExpand.LuaBehaviour)o;
			GameApp.ToLuaExpand.BindableValue ret = obj.Val;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index Val on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_HaveInit(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameApp.ToLuaExpand.LuaBehaviour obj = (GameApp.ToLuaExpand.LuaBehaviour)o;
			bool ret = obj.HaveInit;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index HaveInit on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_HaveInit(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameApp.ToLuaExpand.LuaBehaviour obj = (GameApp.ToLuaExpand.LuaBehaviour)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.HaveInit = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index HaveInit on a nil value");
		}
	}
}

