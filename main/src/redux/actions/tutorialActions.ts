import {createAction} from "@reduxjs/toolkit";


export const InitTutorial = createAction("INIT_TUTORIAL");
export const StartTutorial = createAction("START_TUTORIAL");
export const ExitTutorial = createAction("EXIT_TUTORIAL");
export const SkipTutorial = createAction("SKIP_TUTORIAL");
export const NextStep = createAction<number>("NEXT_STEP");
