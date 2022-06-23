import {createReducer, PayloadAction} from "@reduxjs/toolkit";
import {ServiceModel} from "../../models/Models";
import {ServicesLoaded} from "../actions/serviceActions";

const state : ServiceModel[] = []

export const serviceReducer = createReducer(state, (builder) => {
    builder.addCase(ServicesLoaded, (state, action: PayloadAction<ServiceModel[]>) => {
        return action.payload;
    })
})