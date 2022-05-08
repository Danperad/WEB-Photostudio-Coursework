import {RegisterFail, RegisterSuccess, LoginFail, LoginSuccess, Logout} from '../actions/authActions'
import {createReducer, PayloadAction} from "@reduxjs/toolkit";

const client = localStorage.getItem('user') !== null

export const authReducer = createReducer(client, (builder) => {
	builder.addCase(RegisterSuccess, (state, action: PayloadAction<any>) => {
		return true;
	})
		.addCase(RegisterFail, (state, action: PayloadAction<string>) => {
			return state;
		})
		.addCase(LoginSuccess, (state, action: PayloadAction<any>) => {
			return true;
		})
		.addCase(LoginFail, (state, action: PayloadAction<string>) => {
			return state;
		})
		.addCase(Logout, (state, action: PayloadAction<any>) => {
			return false;
		})
});