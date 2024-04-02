import {createReducer, PayloadAction} from "@reduxjs/toolkit";
import {Hall} from "../../models/Models";
import {HallsLoaded} from "../actions/hallActions";

const state: Hall[] = []

export const hallReducer = createReducer(state, (builder) => {
  builder.addCase(HallsLoaded, (_state, action: PayloadAction<Hall[]>) => {
    return action.payload;
  })
})
