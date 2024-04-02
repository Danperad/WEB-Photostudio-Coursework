import {createAction} from "@reduxjs/toolkit";
import {IntroJs} from "intro.js/src/intro";


export const InitTutorial = createAction<IntroJs>("INIT_TUTORIAL");
export const StartTutorial = createAction("START_TUTORIAL");
export const ExitTutorial = createAction("EXIT_TUTORIAL");
export const SkipTutorial = createAction("SKIP_TUTORIAL");
export const NextStep = createAction<number>("NEXT_STEP");
