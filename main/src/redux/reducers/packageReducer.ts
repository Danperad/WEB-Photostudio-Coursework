import {createReducer, PayloadAction} from "@reduxjs/toolkit";
import {ServicePackage} from "../../models/Models";
import {ClearServicesPackage, ServicesPackageLoaded} from "../actions/packagesActions";

const state: ServicePackage[] = []

export const packageReducer = createReducer(state, (builder) => {
  builder.addCase(ServicesPackageLoaded, (_, action: PayloadAction<ServicePackage[]>) => {
    return action.payload;
  }).addCase(ClearServicesPackage, () => {
    return [];
  })
})
