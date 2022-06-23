import {createAction} from "@reduxjs/toolkit";
import {HallModel} from "../../models/Models";


export const HallsLoaded = createAction<HallModel[]>("HALLS_LOADED");