import {createReducer, PayloadAction} from "@reduxjs/toolkit";
import {InitTutorial, StartTutorial, ExitTutorial, SkipTutorial, NextStep} from "../actions/tutorialActions.ts"
import {IntroJs} from "intro.js/src/intro";

interface State {
  instant: IntroJs | undefined,
  started: boolean
}

const state : State = {
  instant: undefined,
  started: false
}

export const tutorialReducer = createReducer(state, (builder) => {
  builder.addCase(InitTutorial, (_, action: PayloadAction<IntroJs>) => {
    return {instant: action.payload, started: false}
  }).addCase(NextStep, (state, action: PayloadAction<number>) => {
    state.instant?.goToStepNumber(action.payload)
    return state
  }).addCase(StartTutorial, (state, _: PayloadAction) => {
    state.instant?.start()
    return {instant: state.instant, started: true}
  }).addCase(ExitTutorial, (state, _: PayloadAction) => {
    return {instant: state.instant, started: false}
  }).addCase(SkipTutorial, (state, _: PayloadAction) => {
    return {instant: state.instant, started: false}
  })
})
