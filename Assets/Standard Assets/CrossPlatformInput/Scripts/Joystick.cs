using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
	{
		public enum AxisOption
		{
			// Options for which axes to use
			Both, // Use both
			OnlyHorizontal, // Only horizontal
			OnlyVertical // Only vertical
		}

		public int MovementRange = 100;
        public String fireString = "Shoot";
        public String aimName = "Aim";
        public float shootRange = 99;
		public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
		public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input

		Vector3 m_StartPos;
		bool m_UseX; // Toggle for using the x axis
		bool m_UseY; // Toggle for using the Y axis
		CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input



        void OnEnable()
        {
            CreateVirtualAxes();
            m_StartPos = transform.position;



        }
      

		void UpdateVirtualAxes(Vector3 value)
		{

			var delta = m_StartPos - value;
			delta.y = -delta.y;
			delta /= MovementRange;
			if (m_UseX)
			{
				m_HorizontalVirtualAxis.Update(-delta.x);
			}

			if (m_UseY)
			{
				m_VerticalVirtualAxis.Update(delta.y);
			}
		}

		void CreateVirtualAxes()
		{
			// set axes to use
			m_UseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
			m_UseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

			// create new axes based on axes to use
			if (m_UseX)
			{
                if (CrossPlatformInputManager.VirtualAxisReference(horizontalAxisName) == null) {
                    m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
                    CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
                }
			}
			if (m_UseY)
			{
                if (CrossPlatformInputManager.VirtualAxisReference(verticalAxisName) == null) {
                    m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
                    CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
                }
			}
		}


		public void OnDrag(PointerEventData data)
		{
			Vector3 newPos = Vector3.zero;
            
			if (m_UseX)
			{
				int delta = (int)(data.position.x - m_StartPos.x);
			//	delta = Mathf.Clamp(delta, - MovementRange, MovementRange);
				newPos.x = delta;
			}

			if (m_UseY)
			{
				int delta = (int)(data.position.y - m_StartPos.y);
			//	delta = Mathf.Clamp(delta, -MovementRange, MovementRange);
				newPos.y = delta;
			}
            CrossPlatformInputManager.SetButtonDown(aimName);

            transform.position = Vector3.ClampMagnitude(new Vector3(newPos.x, newPos.y, newPos.z), MovementRange) + m_StartPos;
            if(newPos.sqrMagnitude > 0f && newPos.sqrMagnitude < shootRange * shootRange) {
                CrossPlatformInputManager.SetButtonDown(aimName);
            //    print("aiming");
            }
            else {
                CrossPlatformInputManager.SetButtonUp(aimName);
              //  print("not aiming");
            }
            
            if(newPos.sqrMagnitude >= shootRange*shootRange) {
                CrossPlatformInputManager.SetButtonDown(fireString);


            }
            else {
                CrossPlatformInputManager.SetButtonUp(fireString);
              
            }
			UpdateVirtualAxes(transform.position);
            
		}
        public void OnEndDrag (PointerEventData data) {
            CrossPlatformInputManager.SetButtonUp(aimName);
            CrossPlatformInputManager.SetButtonUp(fireString);
        }


		public void OnPointerUp(PointerEventData data)
		{
			transform.position = m_StartPos;
			UpdateVirtualAxes(m_StartPos);

            
        }


		public void OnPointerDown(PointerEventData data) { }

		void OnDestroy()
		{
            
            // remove the joysticks from the cross platform input
            if (m_UseX)
			{
				m_HorizontalVirtualAxis.Remove();
			}
			if (m_UseY)
			{
				m_VerticalVirtualAxis.Remove();
			}
            CrossPlatformInputManager.UnRegisterVirtualAxis(horizontalAxisName);
            CrossPlatformInputManager.UnRegisterVirtualAxis(verticalAxisName);
            CrossPlatformInputManager.SetButtonUp(aimName);
            CrossPlatformInputManager.SetButtonUp(fireString);
		}
	}
}