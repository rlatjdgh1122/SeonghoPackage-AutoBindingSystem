using System.Reflection;
using UnityEngine;

namespace Seongho.BindingSystem
{
    public interface ISetBindingTarget { }

    [DefaultExecutionOrder(-100)]
    public class BindingInstaller : MonoBehaviour
    {
        private void Awake()
        {
            var setBindings = GetComponents<ISetBindingTarget>();

            foreach (var com1 in setBindings)
            {
                RegisterSetBindings(com1);
            }
        }

        private void RegisterSetBindings(ISetBindingTarget component)
        {
            // �ʵ忡 BindingAttribute�� �ִ��� Ȯ��
            var fields = component.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            foreach (var field in fields)
            {
                BindingAttribute bindingAttribute = field.GetCustomAttribute<BindingAttribute>();

                //���� �ش� Atrribute�� ����ߴٸ�
                if (bindingAttribute != null)
                {
                    //�����͸� ������ ��
                    var data = field.GetValue(component);

                    //����Ű�� ������ ��
                    int uniqueKey = (data as IGetUniqeKey).UniqeKey;

                    //ĳ�����ش�.
                    DataBindingManager.Instance.RegisterDataBinding(bindingAttribute.Name, uniqueKey, data);

                } //end if
            } //end foreach
        }
    }
}
