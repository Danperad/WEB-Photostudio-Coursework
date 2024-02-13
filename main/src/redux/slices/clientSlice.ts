import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {Client} from "../../models/Models";
import {getCookie} from "typescript-cookie";

export interface State {
	isAuth: boolean,
	client: Client | null
}

const initialState: State = {
	isAuth: getCookie("access_token") !== undefined,
	client: null
}

const clientSlice = createSlice({
	name: 'Client',
	initialState: initialState as State,
	reducers: {
		registerSuccess: (state, action: PayloadAction<Client>) => {
			state.isAuth = true;
			state.client = action.payload;
		},
		loginSuccess: (state, action: PayloadAction<Client>) => {
			state.isAuth = true;
			state.client = action.payload;
		},
		logout: (state) => {state.isAuth = false; state.client = null},
		addedAvatar: (state, action: PayloadAction<string>) => {
			state.client!.avatar = action.payload
		},
		clientLoaded: (state, action: PayloadAction<Client>) => {
			state.isAuth = true;
			state.client = action.payload;
		}
	}
});

export const clientReducer = clientSlice.reducer;
export const clientActions = clientSlice.actions;