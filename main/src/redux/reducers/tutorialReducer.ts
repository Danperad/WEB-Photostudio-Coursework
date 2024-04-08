import {createReducer, PayloadAction} from "@reduxjs/toolkit";
import {InitTutorial, StartTutorial, ExitTutorial, SkipTutorial, NextStep} from "../actions/tutorialActions.ts"


interface State {
  currentStep: number,
  started: boolean,
  isInit: boolean,
  isExit: boolean
}

const state: State = {
  currentStep: -1,
  started: false,
  isInit: false,
  isExit: false
}

export const tutorialReducer = createReducer(state, (builder) => {
  builder.addCase(InitTutorial, (_, _action: PayloadAction) => {
    return {currentStep: -1, started: false, isInit: true, isExit: false}
  }).addCase(NextStep, (state, action: PayloadAction<number>) => {
    return {currentStep: action.payload, started: state.started, isInit: state.isInit, isExit: state.isExit}
  }).addCase(StartTutorial, (state, _: PayloadAction) => {
    return {currentStep: 0, started: true, isInit: state.isInit, isExit: state.isExit}
  }).addCase(ExitTutorial, (state, _: PayloadAction) => {
    return {currentStep: state.currentStep, started: false, isInit: state.isInit, isExit: true}
  }).addCase(SkipTutorial, (state, _: PayloadAction) => {
    return {currentStep: state.currentStep, started: false, isInit: state.isInit, isExit: true}
  })
})
