import {createAction} from "@reduxjs/toolkit";
import {Service} from "../../models/Models";

export const ServicesLoaded = createAction<Service[]>("SERVICES_LOADED");
export const ClearServices = createAction("CLEAR_SERVICES");