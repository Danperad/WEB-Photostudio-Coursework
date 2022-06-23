import {createAction} from "@reduxjs/toolkit";
import {ServiceModel} from "../../models/Models";

export const ServicesLoaded = createAction<ServiceModel[]>("SERVICES_LOADED");