import {configureStore} from '@reduxjs/toolkit'
import {clientReducer} from './slices/clientSlice'
import {serviceReducer} from "./slices/serviceSlice";
import {snackbarReducer} from "./slices/snackbarSlice";
import {hallReducer} from "./reducers/hallReducer";
import {cartReducer} from "./slices/cartSlice";
import {authModalReducer} from './reducers/authModalReducer'
import {packageReducer} from "./reducers/packageReducer";

export const store = configureStore({
  reducer: {
    client: clientReducer,
    services: serviceReducer,
    halls: hallReducer,
    messages: snackbarReducer,
    cart: cartReducer,
    authModal: authModalReducer,
    packages: packageReducer
  },
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
