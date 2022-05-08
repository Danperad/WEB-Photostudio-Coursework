import {configureStore, createListenerMiddleware} from '@reduxjs/toolkit'
import {authReducer} from "./reducers/authReducer";
import {clientReducer} from "./reducers/clientReducer";
import {RegisterSuccess, LoginSuccess} from './actions/authActions'
import {UpdateUser} from './actions/clientActions'

const regMiddleware = createListenerMiddleware()
regMiddleware.startListening({
	actionCreator: RegisterSuccess,
	effect: (action => {
		store.dispatch(UpdateUser);
	})
})
const loginMiddleware = createListenerMiddleware()
loginMiddleware.startListening({
	actionCreator: LoginSuccess,
	effect: (action => {
		store.dispatch(UpdateUser);
	})
})

export const store = configureStore({
	reducer: {
		auth: authReducer,
		client: clientReducer
	},
	middleware: (getDefaultMiddleware) => getDefaultMiddleware().prepend(regMiddleware.middleware).prepend(loginMiddleware.middleware)
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch