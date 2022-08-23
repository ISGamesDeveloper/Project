using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TanksEngine.Character
{
    public abstract class CharacterEntityController : MonoBehaviour
    {
        public bool isMoving;
        public float moveSpeed;
        public bool JoystickEnable;

        protected Rigidbody rb;
        protected CharacterEntity characterEntity;
        protected FloatingJoystick _joystick;

        protected virtual void SetupLocalPlayer()
        {
            characterEntity.IsLocalPlayer = true;
        }
        private void InitJoystick(TanksEngine.UI.Game_UI gameUI)
        {
            if (JoystickEnable)
            {
                _joystick = gameUI.Joystick;
            }
        }
        private void OnEnable()
        {
            Global_EventManager.eventOnAttributeUpdate += HandlerOnAttributeUpdate;
            Global_EventManager.eventOnGameUiLoaded += InitJoystick;
        }

        private void OnDisable()
        {
            Global_EventManager.eventOnAttributeUpdate -= HandlerOnAttributeUpdate;
            Global_EventManager.eventOnGameUiLoaded -= InitJoystick;
        }

        private void HandlerOnAttributeUpdate(AttributeTypes type)
        {
            switch (type)
            {
                case AttributeTypes.Speed:
                    moveSpeed = characterEntity.Speed;
                    break;
            }
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            characterEntity = GetComponent<CharacterEntity>();
            SetupLocalPlayer();
        }
        protected virtual void Start()
        {
            Debug.Log("Player created.");
        }
        protected virtual void FixedUpdate()
        {
            Movement();
        }
        protected void Movement()
        {
            if (_joystick != null)
            {
                if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
                {
                    isMoving = true;
                    Vector3 move = new Vector3(_joystick.Horizontal, rb.velocity.y, _joystick.Vertical);
                    rb.AddForce(move * moveSpeed, ForceMode.Force);

                    if (rb.velocity != Vector3.zero)
                    {
                        transform.rotation = Quaternion.LookRotation(rb.velocity);
                    }
                }
                else
                {
                    isMoving = false;
                }

            }
        }
    }

}
