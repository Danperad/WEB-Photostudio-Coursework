import {useEffect, useRef} from "react";
import introJs from 'intro.js';
import {useDispatch} from "react-redux";
import {AppDispatch} from "../redux/store.ts";
import 'intro.js/introjs.css';
import {InitTutorial} from "../redux/actions/tutorialActions.ts";
import {current} from "@reduxjs/toolkit";

export default function Tutorial() {
  const key = useRef(false)
  const dispatch = useDispatch<AppDispatch>();

  useEffect(() => {
    if (key.current)
      return
    key.current = true
    const state = localStorage.getItem("tutorialEnabled")
    if (state === null) {
      localStorage.setItem("tutorialEnabled", String(true))
    }
    const intro = introJs().setOptions({
      steps: [
        {
          element: "#service-0",
          title: "Обучение",
          intro: "На странице с услугами необходимо нажать на кнопку “подробнее” напротив выбранной услуги",
          position: "bottom"
        },
        {
          element: "#info-service-modal",
          title: "Обучение",
          intro: "После ознакомления с услугой необходимо нажать на кнопку “добавить в корзину”",
          position: "top",
        },
        {
          element: "#add-service-modal",
          title: "Обучение",
          intro: "В появившемся окне необходимо выбрать дату и время, период, а также сотрудника. Затем нажать на “добавить в корзину”, после чего заявка попадет в корзину",
          position: "top"
        }
      ],
      showBullets: false,
      showProgress: true,
      hidePrev: true,
      hideNext: true,
      exitOnOverlayClick: false,
      exitOnEsc: false,
      disableInteraction: false,
    })
    dispatch(InitTutorial(intro))
  }, [dispatch]);

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
