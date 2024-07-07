import {createAction} from "@reduxjs/toolkit";
import {ServicePackage} from "../../models/Models";

export const ServicesPackageLoaded = createAction<ServicePackage[]>("SERVICES_PACKAGE_LOADED");
export const ClearServicesPackage = createAction("CLEAR_SERVICES_PACKAGE");