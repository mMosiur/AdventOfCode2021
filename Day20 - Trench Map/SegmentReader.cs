using System;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode.Year2021.Day20;

public class SegmentReader : IEnumerable<IEnumerable<string>>
{
	private IEnumerable<string> _enumerable;

	public SegmentReader(IEnumerable<string> enumerable)
	{
		_enumerable = enumerable;
	}

	public IEnumerator<IEnumerable<string>> GetEnumerator()
	{
		return new SegmentReaderEnumerator(_enumerable.GetEnumerator());
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	private class SegmentReaderEnumerator : IEnumerator<IEnumerable<string>>
	{
		private bool _started = false;
		private readonly IEnumerator<string> _enumerator;
		private bool _currentFetched = false;

		public IEnumerable<string> Current
		{
			get
			{
				if (!_started) throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
				if (_currentFetched) throw new InvalidOperationException("Cannot fetch again current value after it has been fetched.");
				_currentFetched = true;
				yield return _enumerator.Current;
				while (_enumerator.MoveNext() && !string.IsNullOrEmpty(_enumerator.Current))
				{
					yield return _enumerator.Current;
				}
			}
		}

		object IEnumerator.Current => Current;

		public SegmentReaderEnumerator(IEnumerator<string> enumerator)
		{
			_enumerator = enumerator;
		}

		public bool MoveNext()
		{
			_started = true;
			_currentFetched = false;
			if (!_enumerator.MoveNext()) return false;
			while (string.IsNullOrEmpty(_enumerator.Current))
			{
				if (!_enumerator.MoveNext()) return false;
			}
			return true;
		}

		public void Reset()
		{
			_enumerator.Reset();
			_started = false;
		}

		public void Dispose()
		{
			_enumerator.Dispose();
		}
	}
}
