using System;
using System.Collections.Generic;
using UnityEngine;

namespace Seongho.BindingSystem
{
    public class DataContainer  
    {
        //�̸��� �����͸� �����ϴ� ����
        private readonly Dictionary<int, object> _nameToDataDic = new();

        //�������� ����Ű�� �̸��� �����ϴ� ����
        private readonly Dictionary<int, string> _uniqueKeyToNameDic = new();

        //�������� ����Ű�� �����͸� �����ϴ� ����
        private readonly Dictionary<int, object> _uniqueKeyToDataDic = new();

        /// <summary>
        /// �����͸� �߰��ϴ� �Լ�
        /// </summary>
        public void AddData(string name, int uniqeKey, object value)
        {
            _nameToDataDic.TryAdd(name.GetHashCode(), value);
            _uniqueKeyToNameDic.TryAdd(uniqeKey, name);
            _uniqueKeyToDataDic.TryAdd(uniqeKey, value);
        }

        /// <summary>
        /// �̸��� ���� ĳ�̵Ǿ� �ִ� �����͸� �����ϴ� �Լ�
        /// </summary>
        public DataBinding<T> GetData<T>(string name) where T : IEquatable<T>
        {
            if (_nameToDataDic.TryGetValue(name.GetHashCode(), out var data))
            {
                return (DataBinding<T>)data;
            }

            Debug.LogError($"Ű : {name} �� ���� �����ʹ� �����ϴ�.");
            return default;
        }

        /// <summary>
        /// ����Ű�� ���� �ش� �������� �̸��� �����ϴ� �Լ�
        /// </summary>
        public string GetName(int uniqueKey)
        {
            return _uniqueKeyToNameDic[uniqueKey];
        }

        /// <summary>
        /// �������� ������ ������Ʈ�ϴ� �Լ�
        /// </summary>
        public void UpdateData(int uniqueKey, object newData)
        {
            if (_uniqueKeyToDataDic.ContainsKey(uniqueKey))
            {
                _uniqueKeyToDataDic[uniqueKey] = newData;

            } //end if
        }
    }
}
