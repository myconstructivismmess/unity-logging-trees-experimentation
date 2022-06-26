using UnityEngine;
using UnityEngine.SceneManagement;

using System.Linq;

using JetBrains.Annotations;


namespace Cathode.Logger
{
	public static class LoggingFilter
	{
		public static void Log(string logTreeType, object msg)
		{
			Log(logTreeType.Split('/'), msg);
		}
		
		public static void Log(string logTreeType,  object msg, Object context)
		{
			Log(logTreeType.Split('/'), msg, context);
		}
		
		public static void Log(string[] logTreeType, object msg)
		{
			if (ReferenceEquals(LoggingFilterNode.Root, null))
			{
				if (GetRootLoggingFilterNodeInCurrentScene(out LoggingFilterNode rootLoggingFilterNode))
					rootLoggingFilterNode!.Log(logTreeType, msg);
				
				return;
			}
			
			LoggingFilterNode.Root.Log(logTreeType, msg);
		}

		public static void Log(string[] logTreeType, object msg, Object context)
		{
			if (ReferenceEquals(LoggingFilterNode.Root, null))
			{
				if (GetRootLoggingFilterNodeInCurrentScene(out LoggingFilterNode rootLoggingFilterNode))
					rootLoggingFilterNode!.Log(logTreeType, msg, context);
				
				return;
			}
			
			LoggingFilterNode.Root.Log(logTreeType, msg, context);
		}

		private static bool GetRootLoggingFilterNodeInCurrentScene([CanBeNull] out LoggingFilterNode rootLoggingFilterNode)
		{
			Scene currentScene = SceneManager.GetActiveScene();

			LoggingFilterNode loggerNode = currentScene.GetRootGameObjects()
				.Select(o => o.GetComponentsInChildren<LoggingFilterNode>())
				.SelectMany(o => o)
				.FirstOrDefault(o => o.IsRoot);

			if (ReferenceEquals(loggerNode, null))
			{
				rootLoggingFilterNode = null;
				return false;
			}

			rootLoggingFilterNode = loggerNode;
			return true;
		}
	}
}