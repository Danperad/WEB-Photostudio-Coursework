import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {NewService, NewServicePackage} from "../../models/Models";

interface State {
    serviceModels: NewService[],
    servicePackage: NewServicePackage | null
}

const cart = localStorage.getItem('cart');

const initState : State = cart === null ? {
    serviceModels: [],
    servicePackage: null
} : JSON.parse(cart);

const cartSlice = createSlice({
    name: "cart",
    initialState: initState,
    reducers:{
        ServiceAdded: (state, action: PayloadAction<NewService>) => {
            state.serviceModels.push(action.payload);
            localStorage.setItem('cart', JSON.stringify(state))
        },
        ServiceRemoved: (state, action: PayloadAction<NewService>) => {
            state.serviceModels = state.serviceModels.filter(s => s.id!==action.payload.id);
            localStorage.setItem('cart', JSON.stringify(state))
        },
        PackageAdded: (state, action: PayloadAction<NewServicePackage>) => {
            state.servicePackage = action.payload;
            localStorage.setItem('cart', JSON.stringify(state))
        },
        PackageRemoved: (state, action: PayloadAction<NewServicePackage>) => {
            state.servicePackage = null;
            localStorage.setItem('cart', JSON.stringify(state))
        },
        ClearCart: (state, action:PayloadAction<number>) => {
            state.servicePackage = null;
            state.serviceModels = [] as NewService[];
            localStorage.removeItem('cart');
        }
    }
})

export const cartReducer = cartSlice.reducer;
export const cartActions = cartSlice.actions;