using System.Reflection;
using UnityEngine;

namespace Seongho.BindingSystem
{
    public interface IGetBindingTarget { }

    [DefaultExecutionOrder(-100)]
    public class GetBindingInstaller : MonoBehaviour
    {
        void Awake()
        {
            //1. IGetBindingTarget�� ��ӹ��� ������Ʈ�� ã�´�.
            var GetBindings = GetComponents<IGetBindingTarget>();

            foreach (var compo in GetBindings)
            {
                RegisterGetBindings(compo);

            } //end foreach
        }

        private void RegisterGetBindings(IGetBindingTarget compo)
        {
            var methods = compo.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            //1. ��� �޼��带 Ȯ���غ���.
            foreach (var method in methods)
            {
                var bindingAttribute = method.GetCustomAttribute<GetBindingAttribute>();

                //2. GetBinding�� ����� �޼��尡 �ִ��� üũ�Ѵ�.
                if (bindingAttribute != null)
                {
                    //3. �޼��带 ĳ���Ѵ�.
                    DataBindingManager.Instance.RegisterMethodBinding
                        (bindingAttribute.Name,
                        bindingAttribute.DataType,
                        value => method.Invoke(compo, new[] { value }));

                } //end if
            } //end foreach

        }
    }

}