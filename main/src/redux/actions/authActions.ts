import {createAction} from "@reduxjs/toolkit";

export const RegisterSuccess = createAction("REGISTER_SUCCESS");
export const RegisterFail = createAction<string>("REGISTER_FAIL");
export const LoginSuccess = createAction("LOGIN_SUCCESS");
export const LoginFail = createAction<string>("LOGIN_FAIL");
export const Logout = createAction("LOGOUT");
