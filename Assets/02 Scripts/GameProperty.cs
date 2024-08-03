using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace GameSettingProperty
{
    public class GameProperty
    {

        /// <summary>
        /// 가져올 XR/VR 장비를 반환
        /// </summary>
        /// <param name="inputDeviceCondition">받아올 디바이스 조건</param>
        /// <param name="deviceNumber">선택할 디바이스 리스트 번호</param>
        /// <returns></returns>
        public static InputDevice GetXrInputDeviceToSelect(InputDeviceCharacteristics inputDeviceCondition, int deviceNumber)
        {
            // XR/VR 장비가 없거나 입력이 없으면 Null 반환
            if (!XRSettings.isDeviceActive) return default;

            // VR의 모든 장비 리스트 받아오기
            List<InputDevice> inputDevices = new List<InputDevice>();

            // inputDevices를 Clear하고 input
            InputDevices.GetDevicesWithCharacteristics(inputDeviceCondition, inputDevices);

            if (inputDevices.Count <= 0) return default;
            return inputDevices[deviceNumber];
        }
    }
}
