using UnityEngine;

namespace Project.Scripts.Views.UserInput
{
    public class UIMessage
    {
        private int _eventFrame;

        public void Set()
        {
            _eventFrame = Time.frameCount;
        }

        public bool TryGet()
        {
            return Time.frameCount == _eventFrame;
        }
    }

    public class UIMessage<T1>
    {
        private int _eventFrame;
        private T1 _value1;

        public void Set(T1 arg1)
        {
            _eventFrame = Time.frameCount;
            _value1 = arg1;
        }

        public bool TryGet(out T1 val1)
        {
            val1 = _value1;
            return Time.frameCount == _eventFrame;
        }
    }

    public class UIMessage<T1, T2>
    {
        private int _eventFrame;
        private T1 _value1;
        private T2 _value2;

        public void Set(T1 arg1, T2 arg2)
        {
            _eventFrame = Time.frameCount;
            _value1 = arg1;
            _value2 = arg2;
        }

        public bool TryGet(out T1 val1, out T2 val2)
        {
            val1 = _value1;
            val2 = _value2;
            return Time.frameCount == _eventFrame;
        }
    }

    public class UIMessage<T1, T2, T3>
    {
        private int _eventFrame;
        private T1 _value1;
        private T2 _value2;
        private T3 _value3;

        public void Set(T1 arg1, T2 arg2, T3 arg3)
        {
            _eventFrame = Time.frameCount;
            _value1 = arg1;
            _value2 = arg2;
            _value3 = arg3;
        }

        public bool TryGet(out T1 val1, out T2 val2, out T3 val3)
        {
            val1 = _value1;
            val2 = _value2;
            val3 = _value3;
            return Time.frameCount == _eventFrame;
        }
    }

    public class UIMessage<T1, T2, T3, T4>
    {
        private int _eventFrame;
        private T1 _value1;
        private T2 _value2;
        private T3 _value3;
        private T4 _value4;

        public void Set(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            _eventFrame = Time.frameCount;
            _value1 = arg1;
            _value2 = arg2;
            _value3 = arg3;
            _value4 = arg4;
        }

        public bool TryGet(out T1 val1, out T2 val2, out T3 val3, out T4 val4)
        {
            val1 = _value1;
            val2 = _value2;
            val3 = _value3;
            val4 = _value4;
            return Time.frameCount == _eventFrame;
        }
    }
}