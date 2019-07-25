using System.Text;
using System.Collections.Generic;

public static class StringBuilderUtil
{

    private static StringBuilder _debugStringBuilder;
    private static StringBuilder _errorStringBuilder;

    private static List<StringBuilder> _stringBuilderPool;
    private static int _maxPoolSize = 2;
    private static int _activePoolItemIndex = 0;

    public static StringBuilder GetEmptyStringBuilder()
    {
        if (_stringBuilderPool == null)
        {
            _stringBuilderPool = new List<StringBuilder>();
        }

        if (_stringBuilderPool.Count < _maxPoolSize)
        {
            StringBuilder sb = new StringBuilder();
            _stringBuilderPool.Add(sb);
            _activePoolItemIndex = _stringBuilderPool.Count - 1;
            return _stringBuilderPool[_activePoolItemIndex];
        }
        else
        {
            _activePoolItemIndex = +1;
            if (_activePoolItemIndex >= _maxPoolSize)
            {
                _activePoolItemIndex = 0;
            }

            _stringBuilderPool[_activePoolItemIndex].Length = 0;
            return _stringBuilderPool[_activePoolItemIndex];
        }
    }

    public static StringBuilder GetDebugStringBuilder()
    {
        if (_debugStringBuilder == null)
        {
            _debugStringBuilder = new StringBuilder();
            return _debugStringBuilder;
        }
        else
        {
            _debugStringBuilder.Length = 0;
            return _debugStringBuilder;
        }
    }

    public static StringBuilder GetErrorStringBuilder()
    {
        if (_errorStringBuilder == null)
        {
            _errorStringBuilder = new StringBuilder();
            return _errorStringBuilder;
        }
        else
        {
            _errorStringBuilder.Length = 0;
            return _errorStringBuilder;
        }
    }

}
