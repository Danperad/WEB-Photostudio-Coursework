import {configureStore} from '@reduxjs/toolkit'
import {clientReducer} from './slices/clientSlice'
import {serviceReducer} from "./reducers/serviceReducer";
import {snackbarReducer} from "./slices/snackbarSlice";
import {hallReducer} from "./reducers/hallReducer";
import {cartReducer} from "./slices/cartSlice";

export const store = configureStore({
	reducer: {
		client: clientReducer,
		services: serviceReducer,
		halls: hallReducer,
		messages: snackbarReducer,
		cart: cartReducer
	},
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch