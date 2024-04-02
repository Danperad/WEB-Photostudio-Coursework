import {useEffect, useRef} from "react";
import introJs from 'intro.js';
import {useDispatch} from "react-redux";
import {AppDispatch} from "../redux/store.ts";
import 'intro.js/introjs.css';
import {InitTutorial} from "../redux/actions/tutorialActions.ts";

export default function Tutorial() {
  const key = useRef(false)
  const dispatch = useDispatch<AppDispatch>();

  useEffect(() => {
    if (key.current)
      return
    key.current = true
    const state = localStorage.getItem("tutorialEnabled")
    if (state === null){
      localStorage.setItem("tutorialEnabled", String(true))
    }
    const intro = introJs().setOptions({
      steps: [
        {
          element: "#service-0",
          intro: "Hello",
        },
        {
          element: "#infoServiceModal",
          intro: "Buy",
          position: "right"
        }
      ],
      showBullets: false,
      showProgress: true
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
