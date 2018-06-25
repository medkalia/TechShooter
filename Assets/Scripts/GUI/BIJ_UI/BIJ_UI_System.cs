using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace BIJ.UI
{
    public class BIJ_UI_System : MonoBehaviour
    {

        #region Variables
        [Header("Main Properties")]
        public BIJ_UI_Screen m_StartScreen;

        [Header("System Events")]
        public UnityEvent onSwitchedScreen = new UnityEvent();

        [Header("Fader Properties")]
        public Image m_Fader;
        public float m_FadeInDuration = 1f;
        public float m_FadeOutDuration = 1f;

        private Component[] screens = new Component[0];

        private Stack<BIJ_UI_Screen> screenStack = new Stack<BIJ_UI_Screen>();
        public Stack<BIJ_UI_Screen> ScreenStack { get { return ScreenStack; } }

        //private BIJ_UI_Screen previousScreen;
        //public BIJ_UI_Screen PreviousScreen { get { return previousScreen; } }

        private BIJ_UI_Screen currentScreen;
        public BIJ_UI_Screen CurrentScreen { get { return currentScreen; } }
        #endregion

        #region Main Methods

        void Start()
        {
            screens = GetComponentsInChildren<BIJ_UI_Screen>(true);
            InitializeScreen();

            if (m_StartScreen)
            {
                HandleScreenSwitch(m_StartScreen, false);
            }

            if (m_Fader)
            {
                m_Fader.gameObject.SetActive(true);
            }

            FadeIn();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && currentScreen != m_StartScreen)
            {
                GoToPreviousScreen();
            }
        }

        #endregion

        #region Helper Methods

        public void SwitchScreen(BIJ_UI_Screen aScreen)
        {
            HandleScreenSwitch(aScreen, false);
        }

        public void GoToPreviousScreen()
        {
            
            if (screenStack.Count>0/*previousScreen*/)
            {
                HandleScreenSwitch(screenStack.Pop(),true);
                //SwitchScreen(previousScreen);
            }
        }

        public void HandleScreenSwitch (BIJ_UI_Screen aScreen, bool isBackCall)
        {
            if (aScreen)
            {
                if (currentScreen)
                {
                    currentScreen.CloseScreen();
                    if (!isBackCall)
                        screenStack.Push(currentScreen);
                    //previousScreen = currentScreen;
                }

                currentScreen = aScreen;
                currentScreen.gameObject.SetActive(true);
                currentScreen.StartScreen();
                if (onSwitchedScreen != null)
                {
                    onSwitchedScreen.Invoke();
                }
            }
        }

        public void FadeIn()
        {
            if (m_Fader)
            {
                m_Fader.CrossFadeAlpha(0f, m_FadeInDuration, false);
            }
        }

        public void FadeOut()
        {
            if (m_Fader)
            {
                m_Fader.CrossFadeAlpha(1f, m_FadeOutDuration, false);
            }
        }

        public void LoadScene(int sceneIndex)
        {
            StartCoroutine(WaitToLoadScene(sceneIndex));
        }

        IEnumerator WaitToLoadScene(int sceneIndex)
        {
            yield return null;
        }

        void InitializeScreen()
        {
            foreach (var screen in screens)
            {
                screen.gameObject.SetActive(true);
            }
        }
        #endregion
    }
}

