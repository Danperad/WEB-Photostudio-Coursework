import {useEffect, useRef} from "react";
import introJs from 'intro.js';
import {useDispatch, useSelector} from "react-redux";
import {AppDispatch, RootState} from "../redux/store.ts";
import 'intro.js/introjs.css';
import {ExitTutorial, InitTutorial, SkipTutorial} from "../redux/actions/tutorialActions.ts";
import {IntroJs} from "intro.js/src/intro";

export default function Tutorial() {
  const key = useRef(false)
  const intro = useRef<IntroJs | undefined>(undefined)
  const isStarted = useRef(false)
  const isExit = useRef(false)
  const tutorialState = useSelector((state: RootState) => state.tutorial);

  const dispatch = useDispatch<AppDispatch>();

  useEffect(() => {
    if (key.current)
      return
    key.current = true
    const state = localStorage.getItem("tutorialEnabled")
    if (state === null) {
      localStorage.setItem("tutorialEnabled", String(true))
    }
    intro.current = introJs().setOptions({
      steps: [
        {
          element: "#service-0",
          title: "Обучение",
          intro: "На странице с услугами необходимо нажать на кнопку “Подробнее” напротив выбранной услуги",
          position: "bottom"
        },
        {
          element: "#info-service-modal",
          title: "Обучение",
          intro: "После ознакомления с услугой необходимо нажать на кнопку “Настроить”",
          position: "top",
        },
        {
          element: "#add-service-modal",
          title: "Обучение",
          intro: "В появившемся окне необходимо выбрать дату и время, период, а также зал. Затем нажать на “Добавить в корзину”, после чего заявка попадет в корзину",
          position: "top"
        },
        {
          element: "#open-cart-btn",
          title: "Обучение",
          intro: "Для дальнейшего оформления заявки необходимо перейти в корзину, для этого нажмите на кнопку с продуктовой тележкой",
          position: "left"
        },
        {
          element: "#cart-buy-area",
          title: "Обучение",
          intro: "Для завершения оформления заявки необходимо нажать на кнопку “Оплатить”",
          position: "right"
        }
      ],
      showBullets: false,
      showProgress: true,
      hidePrev: true,
      hideNext: true,
      exitOnOverlayClick: false,
      exitOnEsc: false,
      disableInteraction: false,
      buttonClass: "tutorial-btn",
      skipLabel: "<h6 style='margin: 0 0 0 -70px; padding: 0; color: #0D47A1'>Пропустить</h6>"
    }).onexit(() => {
      dispatch(ExitTutorial())
    }).onskip(() => {
      dispatch(SkipTutorial())
    })
    dispatch(InitTutorial())
  }, [dispatch]);

  useEffect(() => {
    if (tutorialState.isExit && !isExit.current) {
      isExit.current = true
      intro.current?.exit(false)
      return
    }
    if (tutorialState.started && !isStarted.current) {
      isStarted.current = true
      intro.current?.start()
    }
    if (tutorialState.currentStep !== intro.current?.currentStep()) {
      // intro.current?.refresh()
      intro.current?.goToStep(tutorialState.currentStep).then(res => {
        // res.refresh()
        res.start()
      }).catch(err => {
        console.log(err);
      })
      return;
    }
  }, [tutorialState]);

  // const onExit = () => {
  //   // setEnabled(false)
  //   // localStorage.setItem("tutorialEnabled", String(false))
  //   if (globalState.client.isAuth) {
  //     console.log(false)
  //   }
  // }
  return (
    <></>
  )
}
