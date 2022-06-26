using UnityEngine;

using System.Collections.Generic;
using System.Linq;

namespace Cathode.Logger
{
	[DefaultExecutionOrder(-14)]
	public class LoggingFilterNode : MonoBehaviour
	{
		public static LoggingFilterNode Root { private set; get; }

		public bool IsRoot;
		private Dictionary<string, LoggingFilterNode> _childs;

		private void Awake()
		{
			if (IsRoot)
				Root = this;

			_childs = GetLoggingFilterNodeChildrens();
		}

		public void Log(string[] logTreeType, object msg, Object context)
		{
			if (!gameObject.activeSelf)
				return;
			
			Dictionary<string, LoggingFilterNode> childs = _childs ?? GetLoggingFilterNodeChildrens();
			
			if (logTreeType.Length > 0 && childs.ContainsKey(logTreeType.First()))
				childs[logTreeType.First()].Log(logTreeType.Skip(1).ToArray(), msg, context);
			else
				Debug.Log(msg, context);
		}
		
		public void Log(string[] logTreeType, object msg)
		{
			if (!gameObject.activeSelf)
				return;
			
			Dictionary<string, LoggingFilterNode> childs = _childs ?? GetLoggingFilterNodeChildrens();

			if (logTreeType.Length > 0 && childs.ContainsKey(logTreeType.First()))
				childs[logTreeType.First()].Log(logTreeType.Skip(1).ToArray(), msg);
			else
				Debug.Log(msg);
		}

		private Dictionary<string, LoggingFilterNode> GetLoggingFilterNodeChildrens()
		{
			Dictionary<string, LoggingFilterNode> childs = new Dictionary<string, LoggingFilterNode>();

			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				LoggingFilterNode childLoggingFilterNode = child.GetComponent<LoggingFilterNode>();

				if (!ReferenceEquals(childLoggingFilterNode, null))
				{
					childs.Add(child.name, childLoggingFilterNode);
				}
			}

			return childs;
		}
	}
}