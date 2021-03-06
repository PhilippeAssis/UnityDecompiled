using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityEditor
{
	internal class MenuUtils
	{
		private class MenuCallbackObject
		{
			public string menuItemPath;

			public UnityEngine.Object[] temporaryContext;

			public Action<string, UnityEngine.Object[], int> onBeforeExecuteCallback;

			public Action<string, UnityEngine.Object[], int> onAfterExecuteCallback;

			public int userData;
		}

		[CompilerGenerated]
		private static GenericMenu.MenuFunction2 <>f__mg$cache0;

		public static void MenuCallback(object callbackObject)
		{
			MenuUtils.MenuCallbackObject menuCallbackObject = callbackObject as MenuUtils.MenuCallbackObject;
			if (menuCallbackObject.onBeforeExecuteCallback != null)
			{
				menuCallbackObject.onBeforeExecuteCallback(menuCallbackObject.menuItemPath, menuCallbackObject.temporaryContext, menuCallbackObject.userData);
			}
			if (menuCallbackObject.temporaryContext != null)
			{
				EditorApplication.ExecuteMenuItemWithTemporaryContext(menuCallbackObject.menuItemPath, menuCallbackObject.temporaryContext);
			}
			else
			{
				EditorApplication.ExecuteMenuItem(menuCallbackObject.menuItemPath);
			}
			if (menuCallbackObject.onAfterExecuteCallback != null)
			{
				menuCallbackObject.onAfterExecuteCallback(menuCallbackObject.menuItemPath, menuCallbackObject.temporaryContext, menuCallbackObject.userData);
			}
		}

		public static void ExtractSubMenuWithPath(string path, GenericMenu menu, string replacementPath, UnityEngine.Object[] temporaryContext)
		{
			HashSet<string> hashSet = new HashSet<string>(Unsupported.GetSubmenus(path));
			string[] submenusIncludingSeparators = Unsupported.GetSubmenusIncludingSeparators(path);
			for (int i = 0; i < submenusIncludingSeparators.Length; i++)
			{
				string text = submenusIncludingSeparators[i];
				string replacementMenuString = replacementPath + text.Substring(path.Length);
				if (hashSet.Contains(text))
				{
					MenuUtils.ExtractMenuItemWithPath(text, menu, replacementMenuString, temporaryContext, -1, null, null);
				}
			}
		}

		public static void ExtractMenuItemWithPath(string menuString, GenericMenu menu, string replacementMenuString, UnityEngine.Object[] temporaryContext, int userData, Action<string, UnityEngine.Object[], int> onBeforeExecuteCallback, Action<string, UnityEngine.Object[], int> onAfterExecuteCallback)
		{
			MenuUtils.MenuCallbackObject menuCallbackObject = new MenuUtils.MenuCallbackObject();
			menuCallbackObject.menuItemPath = menuString;
			menuCallbackObject.temporaryContext = temporaryContext;
			menuCallbackObject.onBeforeExecuteCallback = onBeforeExecuteCallback;
			menuCallbackObject.onAfterExecuteCallback = onAfterExecuteCallback;
			menuCallbackObject.userData = userData;
			GUIContent arg_53_1 = new GUIContent(replacementMenuString);
			bool arg_53_2 = false;
			if (MenuUtils.<>f__mg$cache0 == null)
			{
				MenuUtils.<>f__mg$cache0 = new GenericMenu.MenuFunction2(MenuUtils.MenuCallback);
			}
			menu.AddItem(arg_53_1, arg_53_2, MenuUtils.<>f__mg$cache0, menuCallbackObject);
		}
	}
}
