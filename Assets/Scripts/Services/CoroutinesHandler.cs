using System.Collections;
using UnityEngine;

namespace MegaChaseGame.Services
{
	// Т.к. некоторые классы, запускающие корутины, не являются МоноБеховскими, 
	// они не могут сделать это самостоятельно. Конечно, есть вариант передавать им
	// "контекст" в виде пустого GameObject'а, но лично мне больше нравится такое решение,
	// потому что здесь всё управление корутинами происходит в одном месте и не нужно париться
	// о передаче контекстов. Кажется, в этом случае даже будет уместно написать всё через синглтон.
	public sealed class CoroutinesHandler : MonoBehaviour
	{
		private static readonly object _lockObject = new object();
		private static CoroutinesHandler _instance;

		// Не то чтобы блокнутый конструктор было нужнен в МоноБеховском синглтоне,
		// но если уж решил писать плохой паттерн - делай грязь до конца.
		private CoroutinesHandler() { }

		public static CoroutinesHandler Instance
		{
			get
			{
				if (_instance is null)
				{
					lock (_lockObject)
					{
						if (_instance is null)
						{
							// Ты говорил, что AddComponent() практически бесполезен?
							// Вот тот случай, когда без него было бы грустно!
							GameObject coroutineHandlerObject = new GameObject("[Coroutines Handler]");
							_instance = coroutineHandlerObject.AddComponent<CoroutinesHandler>();

							DontDestroyOnLoad(coroutineHandlerObject);
						}
					}
				}

				return _instance;
			}
		}

		public Coroutine StartRoutine(IEnumerator coroutine) => StartCoroutine(coroutine);
		public Coroutine StartRoutine(string methodName) => StartCoroutine(methodName);
		public Coroutine StartRoutine(string methodName, object value) => StartCoroutine(methodName, value);

		public void StopRoutine(Coroutine coroutine) => StopCoroutine(coroutine);
		public void StopRoutine(IEnumerator coroutine) => StopCoroutine(coroutine);
		public void StopRoutine(string methodName) => StopCoroutine(methodName);
	}
}
