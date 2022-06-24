import {createAction} from "@reduxjs/toolkit";
import {Hall} from "../../models/Models";


export const HallsLoaded = createAction<Hall[]>("HALLS_LOADED");