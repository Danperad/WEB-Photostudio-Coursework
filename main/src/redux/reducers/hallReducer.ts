import {createReducer, PayloadAction} from "@reduxjs/toolkit";
import {HallModel} from "../../models/Models";
import {HallsLoaded} from "../actions/hallActions";

const state : HallModel[] = []

export const hallReducer = createReducer(state, (builder) => {
    builder.addCase(HallsLoaded, (state, action: PayloadAction<HallModel[]>) => {
        return action.payload;
    })
})