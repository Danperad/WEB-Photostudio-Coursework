import {createReducer, PayloadAction} from "@reduxjs/toolkit";
import {Client} from '../../models/Models'
import {AddedAvatar, AvatarError, UpdateUser} from "../actions/clientActions";

const client = localStorage.getItem('user') === null ? null : JSON.parse(localStorage.getItem('user')!) as Client


export const clientReducer = createReducer(client, (builder) => {
	builder.addCase(AddedAvatar, (state, action: PayloadAction<string>) => {
		return {...state!, avatar: action.payload}
	}).addCase(AvatarError, (state, action: PayloadAction<string>) => {
		return state;
	}).addCase(UpdateUser, (state, action: PayloadAction<string>) => {
		return localStorage.getItem('user') === null ? null : JSON.parse(localStorage.getItem('user')!) as Client
	})
});