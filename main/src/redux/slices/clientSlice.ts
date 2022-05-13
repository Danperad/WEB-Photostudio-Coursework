import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {Client} from "../../models/ClientModel";

export interface State {
	isAuth: boolean,
	client: Client | null
}

const initialState: State = {
	isAuth: localStorage.getItem('user') !== null,
	client: localStorage.getItem('user') === null ? null : JSON.parse(localStorage.getItem('user')!) as Client
}

const clientSlice = createSlice({
	name: 'Client',
	initialState: initialState as State,
	reducers: {
		registerSuccess: (state, action: PayloadAction<State>) => state = action.payload,
		loginSuccess: (state, action: PayloadAction<State>) => state = action.payload,
		logout: (state) => state = {isAuth: false, client: null},
		addedAvatar: (state, action: PayloadAction<Client>) => {
			state.client = action.payload
		}
	}
});

export const clientReducer = clientSlice.reducer;
export const clientActions = clientSlice.actions;