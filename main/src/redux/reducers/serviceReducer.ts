import {createReducer, PayloadAction} from "@reduxjs/toolkit";
import {Service} from "../../models/Models";
import {ClearServices, ServicesLoaded} from "../actions/serviceActions";

const state: Service[] = []

export const serviceReducer = createReducer(state, (builder) => {
    builder.addCase(ServicesLoaded, (_, action: PayloadAction<Service[]>) => {
        return action.payload;
    }).addCase(ClearServices, (_, __) => {
        return [];
    })
})