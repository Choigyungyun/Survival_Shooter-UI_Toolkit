using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace GameSettingProperty
{
    public class GameProperty
    {

        /// <summary>
        /// ������ XR/VR ��� ��ȯ
        /// </summary>
        /// <param name="inputDeviceCondition">�޾ƿ� ����̽� ����</param>
        /// <param name="deviceNumber">������ ����̽� ����Ʈ ��ȣ</param>
        /// <returns></returns>
        public static InputDevice GetXrInputDeviceToSelect(InputDeviceCharacteristics inputDeviceCondition, int deviceNumber)
        {
            // XR/VR ��� ���ų� �Է��� ������ Null ��ȯ
            if (!XRSettings.isDeviceActive) return default;

            // VR�� ��� ��� ����Ʈ �޾ƿ���
            List<InputDevice> inputDevices = new List<InputDevice>();

            // inputDevices�� Clear�ϰ� input
            InputDevices.GetDevicesWithCharacteristics(inputDeviceCondition, inputDevices);

            if (inputDevices.Count <= 0) return default;
            return inputDevices[deviceNumber];
        }
    }
}
