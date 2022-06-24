import {createReducer, PayloadAction} from "@reduxjs/toolkit";
import {Service} from "../../models/Models";
import {ServicesLoaded, ClearServices} from "../actions/serviceActions";

const state : Service[] = []

export const serviceReducer = createReducer(state, (builder) => {
    builder.addCase(ServicesLoaded, (state, action: PayloadAction<Service[]>) => {
        return action.payload;
    }).addCase(ClearServices, (state, action) => {
        return [];
    })
})