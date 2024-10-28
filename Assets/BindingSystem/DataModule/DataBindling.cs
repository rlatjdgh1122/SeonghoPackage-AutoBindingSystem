using System;
namespace Seongho.BindingSystem
{
    public interface IGetUniqeKey
    {
        public int UniqeKey { get; }
    }

    public struct DataBinding<T> : IGetUniqeKey where T : IEquatable<T>
    {
        private T _value;
        private readonly int _uniqeKey;

        int IGetUniqeKey.UniqeKey { get => _uniqeKey; }

        public T Value
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;

                    //�����Ͱ� ����� ������ �����͸� ������Ʈ���ش�.
                    DataBindingManager.Instance.UpdateDataBinding(_uniqeKey, this);
                } //end if
            }
        }

        public DataBinding(T value)
        {
            _value = value;
            _uniqeKey = default; //���� �Ҵ������ GetHashCode���� ��ü�� ��� ��� ����
            _uniqeKey = GetHashCode(); //���� �� ����Ű�� ����

        }
    }
}
