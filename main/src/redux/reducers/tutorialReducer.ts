import {createReducer, PayloadAction} from "@reduxjs/toolkit";
import {InitTutorial, StartTutorial, ExitTutorial, SkipTutorial, NextStep} from "../actions/tutorialActions.ts"
import {IntroJs} from "intro.js/src/intro";
import introJs from "intro.js";


interface State {
  instant: IntroJs | undefined,
  started: boolean
}

const state: State = {
  instant: undefined,
  started: false
}

export const tutorialReducer = createReducer(state, (builder) => {
  builder.addCase(InitTutorial, (_, action: PayloadAction<IntroJs>) => {
    return {instant: action.payload, started: false}
  }).addCase(NextStep, (state, action: PayloadAction<number>) => {
    state.instant?.refresh()
    state.instant?.goToStep(action.payload).then(res => {
      res.refresh()
      res.start().then(res => {
        res.refresh()
      })
    })
  }).addCase(StartTutorial, (state, _: PayloadAction) => {
    state.instant?.start().then(res => {
      res._introItems[1].element = document.getElementById("info-service-modal")
    })
    return {instant: state.instant, started: true}
  }).addCase(ExitTutorial, (state, _: PayloadAction) => {
    return {instant: state.instant, started: false}
  }).addCase(SkipTutorial, (state, _: PayloadAction) => {
    return {instant: state.instant, started: false}
  })
})
